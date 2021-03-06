﻿namespace DogeGo.Core.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    using DogeGo.Core.Services;
    using DogeGo.Models;
    using DogeGo.Utils;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    /// <inheritdoc cref="IApp"/>
    public class App: IApp
    {
        /// <summary>
        /// Интервал раунда.
        /// </summary>
        private readonly TimeSpan _time;

        // Логгер.
        private ILogger _logger;

        /// <summary>
        /// Сервис для работы со ставками.
        /// </summary>
        private IBetService _betService;

        /// <summary>
        /// Сервис для рабоыт с раундами.
        /// </summary>
        private IRoundService _roundService;

        /// <summary>
        /// Список с задачами на ставки каждого рануда.
        /// </summary>
        private List<Task> _betTasks = new List<Task>();

        /// <summary>
        /// Проходит ли принятие ставок в данный момент.
        /// </summary>
        private bool _isBetting;

        /// <summary>
        /// Текущий раунд.
        /// </summary>
        private Round _round;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger"> Логгер. </param>
        /// <param name="betService"> Сервис для работы со ставками. </param>
        /// <param name="roundService"> Сервис для работы с раундами. </param>
        public App(ILogger<App> logger, 
            IBetService betService, 
            IRoundService roundService)
        {
            _logger = logger;
            _betService = betService;
            _roundService = roundService;

            var roundTime = ConfigurationManager.Configuration.GetValue<int>("RoundTime");
            _time = new TimeSpan(0,0, roundTime);
        }

        /// <inheritdoc cref="IApp.Run"/>
        public async Task Run()
        {
            while (true)
            {
                // Создание раунда.
                await MakeRound();
                var roundHash = HashToString(_round.Hash);
                _betTasks.Clear();

                _logger.LogInformation($"Раунд {_round.Id}:{Environment.NewLine}" +
                                       $"Число раунда: {_round.Number}{Environment.NewLine}" +
                                       $"Хеш раунда: {roundHash}");

                // Выжидаем время.
                _logger.LogInformation("Выжидание раунда.");
                _isBetting = true;
                await Task.Delay(_time);
                _isBetting = false;

                // Принятие ставок.
                _logger.LogInformation("Принятие последних ставок.");
                await Task.WhenAll(_betTasks);

                // Расчет ставок.
                _logger.LogInformation("Расчет ставок.");
                await _betService.CalculateBetsByRoundId(_round.Id);
                _logger.LogInformation($"Раунд {_round.Id} окончен.");
            }
        }

        /// <inheritdoc cref="IApp.MakeBet"/>
        public void MakeBet(Bet bet)
        {
            if (_isBetting)
            {
                bet.RoundId = _round.Id;
                var betTask = _betService.MakeBet(bet);
                _betTasks.Add(betTask);
            }
            else
            {
                _logger.Log(LogLevel.Warning, "Ставка не допущена.");
            }
        }

        /// <summary>
        /// Создать раунд.
        /// </summary>
        private async Task MakeRound()
        {
            var rand = new Random();
            _round = new Round();
            _round.Number = rand.NextDouble() * 15;
            _round.CreatedDateTime = DateTime.Now;
            using var sha256 = new SHA256Managed();
            var byteNumber = Encoding.UTF8.GetBytes(_round.Number.ToString());
            _round.Hash = sha256.ComputeHash(byteNumber);
            await _roundService.AddRound(_round);
        }

        /// <summary>
        /// Преобразовать хеш в строку.
        /// </summary>
        /// <param name="hash"> Хеш. </param>
        /// <returns> Строка. </returns>
        private string HashToString(byte[] hash)
        {
            var sb = new StringBuilder();
            foreach (var data in hash)
            {
                sb.Append(data.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}

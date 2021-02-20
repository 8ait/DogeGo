namespace DogeGo
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DogeGo.Core.Models;
    using DogeGo.Core.Services;
    using DogeGo.Utils;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Приложение ДогеГо.
    /// </summary>
    public class App
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
        /// Список с задачами на ставки каждого рануда.
        /// </summary>
        private List<Task> _betTasks = new List<Task>();

        /// <summary>
        /// Проходит ли принятие ставок в данный момент.
        /// </summary>
        private bool _isBetting;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger"> Логгер. </param>
        public App(ILogger<App> logger, IBetService betService)
        {
            _logger = logger;
            _betService = betService;

            var roundTime = ConfigurationManager.Configuration.GetValue<int>("RoundTime");
            _time = new TimeSpan(0,0, roundTime);
        }

        /// <summary>
        /// Запуск сервиса.
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            while (true)
            {
                // Создание раунда.
                var round = new Round();
                _logger.Log(LogLevel.Information, $"Создание раунда {round.Result}");
                _betTasks.Clear();

                // Выжидаем время.
                _logger.Log(LogLevel.Information,"Выжидание раунда.");
                _isBetting = true;
                await Task.Delay(_time);
                _isBetting = false;

                // Принятие ставок.
                _logger.Log(LogLevel.Information, "Принятие последних ставок.");
                await Task.WhenAll(_betTasks);

                // Расчет ставок.
                CalculateResults();
                _logger.Log(LogLevel.Information, "Расчет ставок.");
                _logger.Log(LogLevel.Information, "Раунд окончен.");
            }
        }

        /// <summary>
        /// Сделать ставку.
        /// </summary>
        /// <param name="bet"> Ставка. </param>
        /// <returns> Задача для управления. </returns>
        public void MakeBet(Bet bet)
        {
            if (_isBetting)
            {
                var betTask = _betService.MakeBet(bet);
                _betTasks.Add(betTask);
            }
        }

        /// <summary>
        /// Произвести расчет ставок.
        /// </summary>
        private void CalculateResults()
        {
            //TODO: Реализовать сервис для расчета ставок.
        }
    }

    //TODO: Вынести в отдельные классы.
    public enum Color
    {
        Red,
        Black,
        Green
    }

    public class Round
    {
        public Color Result { get; }

        public Round()
        {
            var rand = new Random();
            Result = (Color)rand.Next(0, 3);
        }
    }
}

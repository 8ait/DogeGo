namespace DogeGo.Core.Implementations
{
    using System;
    using System.Threading.Tasks;

    using DogeGo.Core.Services;
    using DogeGo.Models;

    using Microsoft.Extensions.Logging;

    //TODO: Сервис написан с учетом следующей доработки, но уже с необходимой имитацией записи ставки в базу данных.
    /// <inheritdoc cref="IBetService"/>
    public class BetService: IBetService
    {
        /// <summary>
        /// Рандом для задержки.
        /// </summary>
        private readonly Random _rand;

        /// <summary>
        /// Логгер.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public BetService(ILogger<BetService> logger)
        {
            _rand = new Random();
            _logger = logger;
        }

        /// <inheritdoc cref="IBetService.MakeBet"/>
        public async Task MakeBet(Bet bet)
        {
            try
            {
                await Task.Delay(_rand.Next(100, 3000));
                _logger.Log(LogLevel.Information, "Ставка принята.");
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, "Ставка не принята.");
            }
        }
    }
}

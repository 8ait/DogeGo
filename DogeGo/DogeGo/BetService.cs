namespace DogeGo
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Сервис для работы со ставками.
    /// </summary>
    public class BetService
    {
        private ILogger _logger;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger"> Логгер. </param>
        public BetService(ILogger<BetService> logger)
        {
            _logger = logger;
        }

        public bool IsRound { get; set; }

        /// <summary>
        /// Добавить ставку.
        /// </summary>
        public async Task AddBet(int index)
        {
            if (!IsRound)
            {
                _logger.Log(LogLevel.Warning, $"Раунд закончился ставка номер {index} откланяется.");
                return;
            }

            var rand = new Random();
            await Task.Delay(rand.Next(500, 5000));
            _logger.Log(LogLevel.Information, $"Ставка {index} принята.");
        }
    }
}

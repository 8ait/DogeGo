namespace DogeGo
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Logging;

    /// <summary>
    /// ДогеГо.
    /// </summary>
    public class Startup
    {
        private readonly TimeSpan _time = new TimeSpan(0, 0, 0, 30);

        // Логгер.
        private ILogger _logger;

        /// <summary>
        /// Сервис для работы со ставками.
        /// </summary>
        private BetService _betService;

        private List<Task> _tasks = new List<Task>();

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger"> Логгер. </param>
        public Startup(ILogger<Startup> logger, BetService betService)
        {
            _logger = logger;
            _betService = betService;
        }

        /// <summary>
        /// Запуск сервиса.
        /// </summary>
        /// <returns></returns>
        public async Task Run()
        {
            Task.Run(async () => await MockBets());
            while (true)
            {
                // Создание раунда.
                
                var round = new Round();
                _logger.Log(LogLevel.Information, $"Создание раунда {round.Result}");
                _tasks.Clear();
                // Выжидаем время.

                _logger.Log(LogLevel.Information,"Выжидание раунда.");
                _betService.IsRound = true;
                await Task.Delay(_time);
                _betService.IsRound = false;
                // Принятие ставок.
                _logger.Log(LogLevel.Information, "Принятие ставок.");
                await Task.Delay(15000);
                await Task.WhenAll(_tasks);
                // Расчет ставок.
                _logger.Log(LogLevel.Information, $"Раунд окончен.");
            }
        }

        private async Task MockBets()
        {
            int i = 0;
            var rand = new Random();
            while (true)
            {
                await Task.Delay(rand.Next(500, 1000));
                var task = Task.Run(async () =>
                {
                    await _betService.AddBet(++i);
                });
            }
        }
    }

    public enum Color
    {
        Red,
        Black,
        Green
    }

    public class Round
    {
        public Color Result { get; set; }

        public Round()
        {
            var rand = new Random();
            Result = (Color)rand.Next(0, 3);
        }
    }
}

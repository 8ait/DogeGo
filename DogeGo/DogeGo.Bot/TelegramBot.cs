namespace DogeGo.Bot
{
    using System;
    using System.Threading.Tasks;

    using DogeGo.Core.Services;
    using DogeGo.Models;
    using DogeGo.Models.Enums;

    /// <summary>
    /// Бот в телеграмме.
    /// </summary>
    public class TelegramBot
    {
        private readonly Random _rand;

        /// <summary>
        /// DogeGo приложение.
        /// </summary>
        private readonly IApp _app;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public TelegramBot(IApp app)
        {
            _rand = new Random();
            _app = app;
        }

        //TODO: Mock.
        public async Task TryMakeBet()
        {
            while (true)
            {
                var bet = new Bet
                {
                    Color = (Color) _rand.Next(0, 3),
                    UserId = _rand.Next(0, 2) == 0 ? "admin1" : "admin2",
                    Value = _rand.Next(1, 20)
                };
                _app.MakeBet(bet);
                await Task.Delay(_rand.Next(6000, 12000));
            }
        }
    }
}

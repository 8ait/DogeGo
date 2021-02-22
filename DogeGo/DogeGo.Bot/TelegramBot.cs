namespace DogeGo.Bot
{
    using System;
    using System.Threading.Tasks;

    using DogeGo.Core.Services;
    using DogeGo.Models;

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
                _app.MakeBet(new Bet());
                await Task.Delay(_rand.Next(50, 2000));
            }
        }
    }
}

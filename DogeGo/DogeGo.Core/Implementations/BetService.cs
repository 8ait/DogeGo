namespace DogeGo.Core.Implementations
{
    using System;
    using System.Threading.Tasks;

    using DogeGo.Core.Models;
    using DogeGo.Core.Services;

    //TODO: Сервис написан с учетом следующей доработки, но уже с необходимой имитацией записи ставки в базу данных.
    /// <inheritdoc />
    public class BetService: IBetService
    {
        /// <summary>
        /// Рандом для задержки.
        /// </summary>
        private readonly Random _rand;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public BetService()
        {
            _rand = new Random();
        }

        /// <inheritdoc />
        public async Task MakeBet(Bet bet)
        {
            await Task.Delay(_rand.Next(100, 3000));
        }
    }
}

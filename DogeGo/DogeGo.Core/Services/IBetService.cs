namespace DogeGo.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DogeGo.Models;

    /// <summary>
    /// Сервис для работы со ставками.
    /// </summary>
    public interface IBetService
    {
        /// <summary>
        /// Сделать ставку.
        /// </summary>
        /// <param name="bet"> Ставка. </param>
        /// <returns> Задача для управления. </returns>
        public Task MakeBet(Bet bet);

        /// <summary>
        /// Получить все ставки на раунд.
        /// </summary>
        /// <param name="roundId"> Идентификатор раунда. </param>
        /// <returns> Список ставок. </returns>
        public Task<List<Bet>> GetAllBetsByRoundId(long roundId);

        /// <summary>
        /// Расчитать ставки по раунду.
        /// </summary>
        /// <param name="roundId"> Иеднтификатор раунда. </param>
        /// <returns> Задача. </returns>
        public Task CalculateBetsByRoundId(long roundId);
    }
}

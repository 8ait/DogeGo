namespace DogeGo.Core.Services
{
    using System.Threading.Tasks;

    using DogeGo.Models;

    /// <summary>
    /// Сервис для работы с раундами.
    /// </summary>
    public interface IRoundService
    {
        /// <summary>
        /// Добавить раунд в БД.
        /// </summary>
        /// <param name="round"> Раунд. </param>
        /// <returns> Задача. </returns>
        public Task AddRound(Round round);
    }
}

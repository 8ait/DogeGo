namespace DogeGo.Core.Services
{
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
    }
}

namespace DogeGo.Core.Services
{
    using System.Threading.Tasks;

    using DogeGo.Models;

    /// <summary>
    /// Приложение ДогеГо.
    /// </summary>
    public interface IApp
    {
        /// <summary>
        /// Запуск сервиса.
        /// </summary>
        /// <returns></returns>
        public Task Run();

        /// <summary>
        /// Сделать ставку.
        /// </summary>
        /// <param name="bet"> Ставка. </param>
        /// <returns> Задача для управления. </returns>
        public void MakeBet(Bet bet);
    }
}

namespace DogeGo.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DogeGo.Models;

    /// <summary>
    /// Сервис для работы с пользователями.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        /// <returns> Список пользователей. </returns>
        Task<List<User>> GetAllUsers();
    }
}

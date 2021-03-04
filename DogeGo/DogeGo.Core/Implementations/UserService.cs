namespace DogeGo.Core.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DogeGo.Core.Services;
    using DogeGo.Models;
    using DogeGo.Models.DataBase;

    using Microsoft.EntityFrameworkCore;

    /// <inheritdoc />
    public class UserService: IUserService
    {
        /// <summary>
        /// Контекст.
        /// </summary>
        private readonly DogeGoContext _context;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context"> Контекст. </param>
        public UserService(DogeGoContext context)
        {
            _context = context;
        }

        public Task<List<User>> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }
    }
}

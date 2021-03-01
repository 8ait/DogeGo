namespace DogeGo.Core.Implementations
{
    using System.Threading.Tasks;

    using DogeGo.Core.Services;
    using DogeGo.Models;
    using DogeGo.Models.DataBase;

    /// <inhertidoc />
    public class RoundService: IRoundService
    {
        /// <summary>
        /// Контекст.
        /// </summary>
        private readonly DogeGoContext _context;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context"> Контекст. </param>
        public RoundService(DogeGoContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task AddRound(Round round)
        {
            //TODO: Обработать перехват ошибок.
            await _context.Rounds.AddAsync(round);
            await _context.SaveChangesAsync();
        }
    }
}

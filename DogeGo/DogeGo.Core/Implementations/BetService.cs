namespace DogeGo.Core.Implementations
{
    using System;
    using System.Threading.Tasks;

    using DogeGo.Core.Services;
    using DogeGo.Models;
    using DogeGo.Models.DataBase;

    using Microsoft.Extensions.Logging;

    /// <inheritdoc cref="IBetService"/>
    public class BetService: IBetService
    {
        /// <summary>
        /// Логгер.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Контекст.
        /// </summary>
        private readonly DogeGoContext _context;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public BetService(ILogger<BetService> logger, DogeGoContext context)
        {
            _logger = logger;
            _context = context;
        }

        /// <inheritdoc cref="IBetService.MakeBet"/>
        public async Task MakeBet(Bet bet)
        {
            if (bet.User == null)
                return;

            await using var t = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Bets.AddAsync(bet);
                var user = await _context.Users.FindAsync(bet.User.TelegramId);
                var resultBalance = user.Balance - bet.Value;

                if (resultBalance >= 0)
                {
                    user.Balance = resultBalance;
                    await _context.AddAsync(bet);
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                    await t.CommitAsync();
                    _logger.Log(LogLevel.Information, "Ставка принята.");
                }
                else
                {
                    _logger.LogInformation($"У пользователя {user.TelegramId} не хватает средств для ставки.");
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, "Ставка не принята.");
            }
            finally
            {
                await t.DisposeAsync();
            }
        }
    }
}

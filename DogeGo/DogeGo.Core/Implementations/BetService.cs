namespace DogeGo.Core.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DogeGo.Core.Services;
    using DogeGo.Models;
    using DogeGo.Models.DataBase;
    using DogeGo.Models.Enums;

    using Microsoft.EntityFrameworkCore;
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
            var user = await _context.Users.FindAsync(bet.UserId);

            if (user == null)
                return;

            await using var t = await _context.Database.BeginTransactionAsync();
            try
            {
                var resultBalance = user.Balance - bet.Value;

                if (resultBalance >= 0)
                {
                    user.Balance = resultBalance;
                    _context.Users.Update(user);
                    await _context.Bets.AddAsync(bet);
                    await _context.AddAsync(bet);
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

        /// <inheritdoc />
        public async Task<List<Bet>> GetAllBetsByRoundId(long roundId)
        {
            var bets = _context.Bets
                .Where(x => x.Round.Id == roundId)
                .Include(x => x.Round)
                .Include(x => x.User);
            return await bets.ToListAsync();
        }

        /// <inheritdoc />
        public async Task CalculateBetsByRoundId(long roundId)
        {
            var bets = await _context.Bets
                .Include(x => x.User)
                .ToListAsync();

            var round = await _context.Rounds.FindAsync(roundId);
            var roundColor = RoundNumberToColor(round.Number);

            await using var t = await _context.Database.BeginTransactionAsync();
            try
            {
                var calculateTasks = bets
                    .Select(x => Task.Run(() =>
                    {
                        if (x.Color == roundColor)
                        {
                            x.User.Balance += x.Color == Color.Green ? x.Value * 13 : x.Value * 2;
                        }
                        x.IsCalculated = true;
                    }));
                Task.WaitAll(calculateTasks.ToArray());
                await _context.SaveChangesAsync();
                await t.CommitAsync();
            }
            catch (Exception e)
            {
                _logger.LogError("Не удалось расчитать ставки.");
            }
            finally
            {
                await t.DisposeAsync();
            }
        }

        /// <summary>
        /// Число раунда в цвет.
        /// </summary>
        /// <param name="number"> Число раунда. </param>
        /// <returns> Цвет раунда. </returns>
        private Color RoundNumberToColor(double number)
        {
            var num = (int)number;
            switch (num)
            {
                case 0:
                    return Color.Green;
                case 1:
                case 3:
                case 5:
                case 7:
                case 9:
                case 11:
                case 13:
                    return Color.Black;
                default:
                    return Color.Red;
            }
        }
    }
}

namespace DogeGo.Models.DataBase
{
    using System;

    using DogeGo.Models.DataBase.Mapping;

    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Контекст базы данных.
    /// </summary>
    public class DogeGoContext: DbContext
    {
        /// <summary>
        /// Пользователи.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Ставки.
        /// </summary>
        public DbSet<Bet> Bets { get; set; }

        /// <summary>
        /// Раунды.
        /// </summary>
        public DbSet<Round> Rounds { get; set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public DogeGoContext(DbContextOptions<DogeGoContext> options): base(options)
        {
            Database.EnsureCreated();
            //Database.Migrate();
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoundMap());
            modelBuilder.ApplyConfiguration(new BetMap());
        }
    }
}

namespace DogeGo.Models.DataBase.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Маппинг для пользователя.
    /// </summary>
    public class UserMap : IEntityTypeConfiguration<User>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.TelegramId);
            builder.Property(x => x.CryptoAddress).IsRequired();
            builder.Property(x => x.Balance).IsRequired();

            builder.HasMany(x => x.Bets)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
        }
    }
}

namespace DogeGo.Models.DataBase.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Маппинг для раунда.
    /// </summary>
    public class RoundMap: BaseEntityMap<Round>
    {
        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<Round> builder)
        {
            base.Configure(builder);
            builder.ToTable("rounds");
            builder.Property(x => x.Number).IsRequired();
            builder.Property(x => x.Hash).IsRequired();
            builder.Property(x => x.CreatedDateTime).IsRequired();

            builder.HasMany(x => x.Bets)
                .WithOne(x => x.Round)
                .HasForeignKey(x => x.RoundId)
                .IsRequired();
        }
    }
}

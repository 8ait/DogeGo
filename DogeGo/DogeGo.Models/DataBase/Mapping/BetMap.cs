namespace DogeGo.Models.DataBase.Mapping
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Маппинг ставки.
    /// </summary>
    public class BetMap: BaseEntityMap<Bet>
    {
        /// <inehrtidoc />
        public override void Configure(EntityTypeBuilder<Bet> builder)
        {
            base.Configure(builder);
            builder.ToTable("bets");
            builder.Property(x => x.Value).IsRequired();
            builder.Property(x => x.Color).IsRequired();
        }
    }
}

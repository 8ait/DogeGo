namespace DogeGo.Models.DataBase.Mapping
{
    using DogeGo.Models.Base;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    /// <summary>
    /// Маппинг для базовой сущности.
    /// </summary>
    /// <typeparam name="T"> Базовая сущность в БД. </typeparam>
    public abstract class BaseEntityMap<T>: IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}

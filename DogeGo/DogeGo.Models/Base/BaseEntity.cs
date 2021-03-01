namespace DogeGo.Models.Base
{
    /// <summary>
    /// Базовая сущность для использования моделей в БД.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public long Id { get; set; }
    }
}

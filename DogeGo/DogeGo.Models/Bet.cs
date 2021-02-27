namespace DogeGo.Models
{
    using DogeGo.Models.Base;
    using DogeGo.Models.Enums;

    /// <summary>
    /// Ставка.
    /// </summary>
    public class Bet: BaseEntity
    {
        /// <summary>
        /// Цвет раунда.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Сумма ставки.
        /// </summary>
        public double Value { get; set; }

    }
}

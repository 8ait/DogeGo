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

        /// <summary>
        /// Ставка расчитана?
        /// </summary>
        public bool IsCalculated { get; set; }

        /// <summary>
        /// Иеднтификатор раунда.
        /// </summary>
        public long RoundId { get; set; }

        /// <summary>
        /// Ставка на раунд.
        /// </summary>
        public Round Round { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Пользователь сделавший ставку.
        /// </summary>
        public User User { get; set; }
    }
}

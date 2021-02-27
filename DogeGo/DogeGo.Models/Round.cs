namespace DogeGo.Models
{
    using System;

    using DogeGo.Models.Base;

    /// <summary>
    /// Раунд.
    /// </summary>
    public class Round: BaseEntity
    {
        /// <summary>
        /// Хеш раунда.
        /// </summary>
        public byte[] Hash { get; set; }

        /// <summary>
        /// Число раунда.
        /// </summary>
        public double Number { get; set; }

        /// <summary>
        /// Время создания раунда.
        /// </summary>
        public DateTime CreatedDateTime { get; set; }
    }
}

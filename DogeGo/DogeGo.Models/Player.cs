namespace DogeGo.Models
{
    using DogeGo.Models.Base;

    /// <summary>
    /// Игрок.
    /// </summary>
    public class Player: BaseEntity
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Адрес криптовалютного кошелька.
        /// </summary>
        public byte[] CryptoAddress { get; set; }

        /// <summary>
        /// Приватный ключ доступа к кошельку.
        /// </summary>
        public byte[] PrivateKey { get; set; }

        /// <summary>
        /// Получить баланс кошелька.
        /// </summary>
        /// <returns> Баланс кошелька. </returns>
        public double GetBalance()
        {
            //TODO: Получать баланс кошелька с помощью Block.io
            return 0;
        }
    }
}

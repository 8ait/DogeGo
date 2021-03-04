namespace DogeGo.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Игрок.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор.
        /// </summary>
        public string TelegramId { get; set; }

        /// <summary>
        /// Адрес криптовалютного кошелька.
        /// </summary>
        public byte[] CryptoAddress { get; set; }

        /// <summary>
        /// Баланс пользователя.
        /// </summary>
        public double Balance { get; set; }

        /// <summary>
        /// Ставки пользователя.
        /// </summary>
        public List<Bet> Bets { get; set; }

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

namespace DogeGo.Utils
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Менеджер конфигруаций.
    /// </summary>
    public static class ConfigurationManager
    {
        /// <summary>
        /// Конфигурация приложения.
        /// </summary>
        public static IConfiguration Configuration { get; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        static ConfigurationManager()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}

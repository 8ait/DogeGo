namespace DogeGo
{
    using System.Threading.Tasks;

    using DogeGo.Bot;
    using DogeGo.Core.Implementations;
    using DogeGo.Core.Services;
    using DogeGo.Models.DataBase;
    using DogeGo.Utils;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    class Program
    {
        private static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            using var serviceProvider = serviceCollection.BuildServiceProvider();
            var app = serviceProvider.GetService<IApp>();
            var bot = serviceProvider.GetService<TelegramBot>();
            try
            {
                app.Run();
                await bot.TryMakeBet();
            }
            finally
            {
                serviceProvider.Dispose();
            }
        }

        /// <summary>
        ///     Конфигурация сервисов.
        /// </summary>
        /// <param name="services"> Сервисы. </param>
        private static void ConfigureServices(IServiceCollection services)
        {
            var connectionString = ConfigurationManager.Configuration.GetValue<string>("ConnectionString");
            services.AddLogging(conf => conf.AddConsole());

            services.AddDogeGoDatabase(connectionString);

            services.AddScoped<IBetService, BetService>();
            services.AddScoped<IRoundService, RoundService>();
            services.AddSingleton<IApp, App>();
            services.AddSingleton<TelegramBot>();
        }
    }
}

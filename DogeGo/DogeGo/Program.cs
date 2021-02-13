namespace DogeGo
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    class Program
    {
        private static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            using var serviceProvider = serviceCollection.BuildServiceProvider();
            var app = serviceProvider.GetService<Startup>();
            try
            {
                await app.Run();
            }
            finally
            {
                serviceProvider.Dispose();
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(conf => conf.AddConsole());
            services.AddSingleton<BetService>();
            services.AddTransient<Startup>();
        }
    }
}

namespace DogeGo.Models.DataBase
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Расширения для базы данных.
    /// </summary>
    public static class DataBaseExtensions
    {
        /// <summary>
        /// Регистрация контекста базы данных.
        /// </summary>
        /// <param name="serviceCollection"> Коллекция сервисов. </param>
        /// <param name="connectionString"> Строка подключения. </param>
        /// <returns> Коллекция сервисов. </returns>
        public static void AddDogeGoDatabase(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<DogeGoContext>(builder => builder.UseNpgsql(connectionString));
        }
    }
}

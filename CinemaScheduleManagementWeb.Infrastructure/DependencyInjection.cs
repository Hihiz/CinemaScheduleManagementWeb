using CinemaScheduleManagementWeb.Application.Interfaces.Repositories;
using CinemaScheduleManagementWeb.Infrastructure.Data;
using CinemaScheduleManagementWeb.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaScheduleManagementWeb.Infrastructure
{
    /// <summary>
    /// Класс регистрирует зависимости в контейнере.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Метод добавляет инициализацию сервисов слоя Infrastructure в коллекцию сервисов.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <param name="configuration">Конфигурация.</param>
        /// <returns>Коллекция зарегистрированных сервисов.</returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration
            configuration)
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.RepositoriesInit();

            return services;
        }

        /// <summary>
        /// Метод регистрирует сервисы.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        private static void RepositoriesInit(this IServiceCollection services)
        {
            services.AddScoped<ISessionRepository, SessionRepository>();
            services.AddScoped<IHallRepository, HallRepository>();
            services.AddScoped<IFilmRepository, FilmRepository>();
            services.AddScoped<ICinemaSettingRepository, CinemaSettingRepository>();
        }
    }
}

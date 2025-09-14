using CinemaScheduleManagementWeb.Application.Interfaces.Services;
using CinemaScheduleManagementWeb.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaScheduleManagementWeb.Application
{
    /// <summary>
    /// Класс регистрирует зависимости в контейнере.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Метод добавляет инициализацию сервисов слоя Application в коллекцию сервисов.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <returns>Коллекция зарегистрированных сервисов.</returns>
        public static IServiceCollection AddApplicationsServices(this IServiceCollection services)
        {
            services.ServicesInit();

            return services;
        }

        /// <summary>
        /// Метод регистрирует сервисы.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        private static void ServicesInit(this IServiceCollection services)
        {
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IHallService, HallService>();
            services.AddScoped<IFilmService, FilmService>();
        }
    }
}

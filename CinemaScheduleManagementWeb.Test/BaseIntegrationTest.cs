using CinemaScheduleManagementWeb.Infrastructure.Data;
using CinemaScheduleManagementWeb.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CinemaScheduleManagementWeb.Test
{
    /// <summary>
    /// Базовый класс интеграционных тестов.
    /// </summary>
    public class BaseIntegrationTest
    {
        private readonly IConfiguration appConfiguration;

        protected readonly FilmRepository filmRepository;
        protected readonly HallRepository hallRepository;
        protected readonly SessionRepository sessionRepository;
        protected readonly CinemaSettingRepository cinemaSettingRepository;
        
        public BaseIntegrationTest()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            appConfiguration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(appConfiguration.GetConnectionString("DefaultConnection")!);
            var applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);

            filmRepository = new FilmRepository(applicationDbContext);
            hallRepository = new HallRepository(applicationDbContext);
            sessionRepository = new SessionRepository(applicationDbContext);
            cinemaSettingRepository = new CinemaSettingRepository(applicationDbContext);
        }
    }
}

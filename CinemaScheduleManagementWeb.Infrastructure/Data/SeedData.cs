using CinemaScheduleManagementWeb.Application.Services;
using CinemaScheduleManagementWeb.Domain.Entities;
using CinemaScheduleManagementWeb.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaScheduleManagementWeb.Infrastructure.Data
{
    /// <summary>
    /// Класс заполняет БД начальными данными.
    /// </summary>
    public class SeedData
    {
        /// <summary>
        /// Метод инициализирует БД начальными данными.
        /// </summary>
        /// <param name="serviceProvider">Провайдер сервисов.</param>
        /// <returns>Признак выполнилась ли инициализация БД.</returns>
        public static async Task<bool> Initializer(IServiceProvider serviceProvider)
        {
            using ApplicationDbContext db = new ApplicationDbContext(serviceProvider
                .GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (!await db.Database.CanConnectAsync())
            {
                await db.Database.MigrateAsync();
            }

            using IDbContextTransaction transaction = await db.Database.BeginTransactionAsync(System.Data
                .IsolationLevel.ReadCommitted);

            try
            {
                if (await db.CinemaSettings.AnyAsync()
                    && await db.Films.AnyAsync()
                    && await db.Genres.AnyAsync()
                    && await db.FilmGenres.AnyAsync()
                    && await db.Halls.AnyAsync()
                    && await db.Sessions.AnyAsync())
                {
                    return false;
                }

                CinemaSettingEntity cinemaSettingEntity = new CinemaSettingEntity
                {
                    Id = 1,
                    OpenTime = new TimeSpan(09, 00, 00),
                    CloseTime = new TimeSpan(20, 00, 00)
                };

                await db.CinemaSettings.AddAsync(cinemaSettingEntity);

                List<FilmEntity> filmsEntity = new List<FilmEntity>()
                {
                    new FilmEntity
                    {
                        Id = 1,
                        Title = "Title1",
                        Duration = 10,
                        AgeLimit = 1,
                        Status = FilmStatusEnum.Active.ToString(),
                        PosterUrl = "url1"
                    },

                     new FilmEntity
                    {
                        Id = 2,
                        Title = "Title2",
                        Duration = 60,
                        AgeLimit = 3,
                        Status = FilmStatusEnum.Active.ToString(),
                        PosterUrl = "url2",
                    },
                };

                await db.Films.AddRangeAsync(filmsEntity);

                List<GenreEntity> genresEntity = new List<GenreEntity>
                {
                    new GenreEntity
                    {
                        Id = 1,
                        Title = "Title1"
                    },

                    new GenreEntity
                    {
                        Id = 2,
                        Title = "Title2"
                    },
                };

                await db.Genres.AddRangeAsync(genresEntity);

                List<FilmGenreEntity> filmGenres = new List<FilmGenreEntity>()
                {
                    new FilmGenreEntity
                    {
                        Id = 1,
                        FilmId = 1,
                        GenreId = 1
                    },

                    new FilmGenreEntity
                    {
                        Id = 2,
                        FilmId = 1,
                        GenreId = 2
                    },

                    new FilmGenreEntity
                    {
                        Id = 3,
                        FilmId = 2,
                        GenreId = 2
                    },
                };

                await db.FilmGenres.AddRangeAsync(filmGenres);

                List<HallEntity> hallEntity = new List<HallEntity>
                {
                    new HallEntity
                    {
                        Id = 1,
                        Title = "Hall1",
                        TotalSeat = 10,
                        TechBreak = 10
                    },

                    new HallEntity
                    {
                        Id = 2,
                        Title = "Hall2",
                        TotalSeat = 60,
                        TechBreak = 20
                    },
                };

                await db.Halls.AddRangeAsync(hallEntity);

                List<SessionEntity> sessionEntity = new List<SessionEntity>
                {
                    new SessionEntity
                    {
                        Id = 1,
                        FilmId = 1,
                        HallId = 1,
                        SessionStart = DateTime.UtcNow,
                        SessionEnd = DateTime.UtcNow.AddHours(2),
                        Price = 1000,
                        Status = SessionStatusEnum.Active.ToString()
                    },

                    new SessionEntity
                    {
                        Id = 2,
                        FilmId = 2,
                        HallId = 1,
                        SessionStart = DateTime.UtcNow,
                        SessionEnd = DateTime.UtcNow.AddHours(1),
                        Price = 2000,
                        Status = SessionStatusEnum.Active.ToString()
                    },
                };

                await db.Sessions.AddRangeAsync(sessionEntity);

                await db.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }

            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

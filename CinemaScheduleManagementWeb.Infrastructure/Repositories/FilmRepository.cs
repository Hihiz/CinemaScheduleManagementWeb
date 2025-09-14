using CinemaScheduleManagementWeb.Application.Dto.Intput.Film;
using CinemaScheduleManagementWeb.Application.Dto.Output.Film;
using CinemaScheduleManagementWeb.Application.Dto.Output.Session;
using CinemaScheduleManagementWeb.Application.Interfaces.Repositories;
using CinemaScheduleManagementWeb.Domain.Entities;
using CinemaScheduleManagementWeb.Domain.Enums;
using CinemaScheduleManagementWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CinemaScheduleManagementWeb.Infrastructure.Repositories
{
    /// <summary>
    /// Класс реулизует репозиторий фильмов.
    /// </summary>
    public class FilmRepository : IFilmRepository
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="db">Класс контекста.</param>
        public FilmRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<IEnumerable<FilmOutput>> GetFilmsAsync()
        {
            List<FilmOutput> result = await _db.Films
                 .AsNoTracking()
                 .Select(f => new FilmOutput
                 {
                     Id = f.Id,
                     Title = f.Title,
                     Duration = f.Duration,
                     AgeLimit = f.AgeLimit,
                     FilmGenresOutput = f.FilmGenresEntity.Select(fg => new FilmGenreOutput
                     {
                         GenreId = fg.GenreId,
                         GenreTitle = fg.GenreEntity.Title
                     }).OrderByDescending(g => g.GenreId)
                       .ToList(),
                     PosterUrl = f.PosterUrl,
                     StatusEnum = Enum.Parse<FilmStatusEnum>(f.Status)
                 })
                 .OrderByDescending(f => f.Id)
                 .ToListAsync();

            return result;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<FilmFilterOutput>> GetFilmsFilterAsync(string searchText,
            FilmFilterInput filterFilmInput)
        {
            IQueryable<FilmEntity> query = _db.Films
                .AsNoTracking();

            ApplyFilterFilms(searchText, filterFilmInput, query);

            List<FilmFilterOutput> result = await query
                .Select(f => new FilmFilterOutput
                {
                    Id = f.Id,
                    Title = f.Title,
                    Duration = f.Duration,
                    AgeLimit = f.AgeLimit,
                    FilmGenresOutput = f.FilmGenresEntity.Select(fg => new FilmGenreOutput
                    {
                        GenreId = fg.GenreId,
                        GenreTitle = fg.GenreEntity.Title
                    }).OrderByDescending(g => g.GenreId)
                       .ToList(),
                    PosterUrl = f.PosterUrl,
                    StatusEnum = Enum.Parse<FilmStatusEnum>(f.Status),
                    SessionDetailOutput = f.SessionsEntity
                    .Where(sd => sd.Status == SessionStatusEnum.Active.ToString())
                    .OrderBy(sd => sd.SessionStart)
                    .Select(sd => new SessionDetailOutput
                    {
                        HallId = sd.HallId,
                        HallTitle = sd.HallEntity.Title,
                        SessionStart = sd.SessionStart,
                        SessionEnd = sd.SessionEnd,
                        Price = sd.Price,
                        Status = sd.Status,
                    }).FirstOrDefault()
                })
                .ToListAsync();

            return result;
        }

        /// <inheritdoc />
        public async Task<bool> CheckFilmActiveStatusByFilmIdAsync(int filmId)
        {
            bool result = await _db.Films
                .AnyAsync(f => f.Id == filmId &&
                    f.Status == FilmStatusEnum.Active.ToString());

            return result;
        }

        /// <inheritdoc />
        public async Task ExcludeFilmByFilmIdAsync(int filmId)
        {
            using IDbContextTransaction transaction = await _db.Database.BeginTransactionAsync(System.Data
               .IsolationLevel.ReadCommitted);

            try
            {
                FilmEntity? film = await _db.Films.FirstOrDefaultAsync(f => f.Id == filmId);

                if (film is null)
                {
                    throw new InvalidOperationException("Ошибка получения фильма при снятии фильма с проката. " +
                                                        $"FilmId: {filmId}.");
                }

                film.Status = FilmStatusEnum.InActive.ToString();

                await _db.Sessions
                  .Where(s => s.FilmId == filmId)
                  .ExecuteUpdateAsync(setter => setter
                  .SetProperty(s => s.Status, SessionStatusEnum.Canceled.ToString())
                  );

                await transaction.CommitAsync();
            }

            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        #endregion

        #region Приватные методы.

        /// <summary>
        /// Метод применяет фильтры к фильмам.
        /// </summary>
        /// <param name="searchText">Текст поиска.</param>
        /// <param name="filterFilmInput">Входная модель.</param>
        /// <param name="query">Запрос.</param>
        private void ApplyFilterFilms(string searchText, FilmFilterInput filterFilmInput,
            IQueryable<FilmEntity> query)
        {
            // Поиск.
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                query = query.Where(f => f.Title
                    .Contains(searchText));
            }

            // Фильтр по жанру.
            if (filterFilmInput.GenreId > 0)
            {
                query = query.Where(f => f.FilmGenresEntity
                    .Any(fg => fg.GenreId == filterFilmInput.GenreId));
            }

            // Фильтр по продолжительности.
            if (filterFilmInput.Duration > 0)
            {
                query = query.Where(f => f.Duration >= filterFilmInput.Duration);
            }

            // Фильтр по возрастному ограничению.
            if (filterFilmInput.AgeLimit > 0)
            {
                query = query.Where(f => f.AgeLimit >= filterFilmInput.AgeLimit);
            }

            // Фильтр по минимальной цене.
            if (filterFilmInput.MinPrice > 0)
            {
                query = query.Where(f => f.SessionsEntity
                    .Any(s => s.Price >= filterFilmInput.MinPrice));
            }

            // Фильтр по максимальной цене.
            if (filterFilmInput.MaxPrice > 0)
            {
                query = query.Where(f => f.SessionsEntity
                    .Any(s => s.Price <= filterFilmInput.MaxPrice));
            }
        }

        #endregion
    }
}

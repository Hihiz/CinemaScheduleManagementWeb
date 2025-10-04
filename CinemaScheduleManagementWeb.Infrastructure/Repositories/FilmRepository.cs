namespace CinemaScheduleManagementWeb.Infrastructure.Repositories
{
    /// <summary>
    /// Класс реулизует репозиторий фильмов.
    /// </summary>
    public class FilmRepository : IFilmRepository
    {
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
        public async Task<IEnumerable<FilmFilterOutput>> GetFilmsFilterAsync(string searchText,
            FilmFilterInput filterFilmInput)
        {
            IQueryable<FilmEntity> query = _db.Films
                .AsNoTracking();

            query = ApplyFilterFilms(searchText, filterFilmInput, query);

            List<FilmFilterOutput> result = await query
                .Select(f => new FilmFilterOutput
                {
                    Id = f.Id,
                    Title = f.Title,
                    Duration = f.Duration,
                    AgeLimit = f.AgeLimit,
                    FilmGenresOutput = f.FilmGenresEntity
                    .Where(fg => filterFilmInput.GenreId > 0
                        ? fg.GenreId == filterFilmInput.GenreId
                        : true)
                    .Select(fg => new FilmGenreOutput
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
                    .Where(sd => filterFilmInput.MinPrice > 0
                        ? sd.Price >= filterFilmInput.MinPrice
                        : true)
                    .Where(sd => filterFilmInput.MaxPrice > 0
                        ? sd.Price <= filterFilmInput.MaxPrice
                        : true)
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

        #endregion
    }
}

namespace CinemaScheduleManagementWeb.Infrastructure.Repositories
{
    /// <summary>
    /// Класс реализует методы репозитория сеансов.
    /// </summary>
    public class SessionRepository : ISessionRepository
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="db">Класс кнтекста.</param>
        public SessionRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<IEnumerable<SessionOutput>> GetActiveSessionsAsync(SessionFilterInput sessionFilterInput)
        {
            IQueryable<FilmEntity> query = _db.Films
                .AsNoTracking()
                .Where(s => s.SessionsEntity
                    .Any(s => s.Status == SessionStatusEnum.Active.ToString()));

            query = ApplyFilterFilms(sessionFilterInput, query);

            List<SessionOutput> result = await query
                .Select(f => new SessionOutput
                {
                    FilmId = f.Id,
                    FilmTitle = f.Title,
                    AgeLimit = f.AgeLimit,
                    FilmDuration = f.Duration,
                    SessionDetailsOutput = f.SessionsEntity
                        .Where(s => sessionFilterInput.DateStart.HasValue && sessionFilterInput.DateEnd.HasValue
                            ? s.SessionStart >= sessionFilterInput.DateStart &&
                              s.SessionEnd <= sessionFilterInput.DateEnd
                            : true)
                        .Where(s => sessionFilterInput.DateStart.HasValue
                            ? s.SessionStart >= sessionFilterInput.DateStart
                            : true)
                        .Where(s => sessionFilterInput.MinPrice > 0
                            ? s.Price >= sessionFilterInput.MinPrice
                            : true)
                        .Where(s => sessionFilterInput.MaxPrice > 0
                            ? s.Price <= sessionFilterInput.MaxPrice
                            : true)
                        .Where(s => sessionFilterInput.HollId > 0
                            ? s.HallId == sessionFilterInput.HollId
                            : true)
                    .Select(s => new SessionDetailOutput
                    {
                        HallId = s.HallId,
                        HallTitle = s.HallEntity.Title,
                        SessionStart = s.SessionStart,
                        SessionEnd = s.SessionEnd,
                        Price = s.Price,
                        Status = s.Status
                    }).OrderBy(sd => sd.SessionStart)
                    .ToList()
                }).OrderBy(f => f.SessionDetailsOutput!.Min(sd => sd.SessionStart))
                .Where(s => s.SessionDetailsOutput!.Any())
                .ToListAsync();

            return result;
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}

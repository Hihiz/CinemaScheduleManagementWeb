namespace CinemaScheduleManagementWeb.Infrastructure.Repositories
{
    /// <summary>
    /// Класс реализует методы репозитория зала.
    /// </summary>
    public class HallRepository : IHallRepository
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="db">Класс контекста.</param>
        public HallRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<bool> IsHallExistsAsync(int hallId, DateTime sessionStart, DateTime sessionEnd)
        {
            int techBreak = (await _db.Halls
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.Id == hallId))!.TechBreak;

            bool result = await _db.Sessions
                .Where(s => s.HallId == hallId && s.Status == SessionStatusEnum.Active.ToString())
                .AnyAsync(s =>
                    sessionStart < s.SessionEnd.AddMinutes(techBreak)
                    && sessionEnd > s.SessionStart
                );

            return result;
        }

        #endregion

        #region Приватные методы.

        #endregion       
    }
}

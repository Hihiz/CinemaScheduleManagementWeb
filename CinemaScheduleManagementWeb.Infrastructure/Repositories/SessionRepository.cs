using CinemaScheduleManagementWeb.Application.Dto.Intput.Session;
using CinemaScheduleManagementWeb.Application.Dto.Output.Session;
using CinemaScheduleManagementWeb.Application.Interfaces.Repositories;
using CinemaScheduleManagementWeb.Domain.Entities;
using CinemaScheduleManagementWeb.Domain.Enums;
using CinemaScheduleManagementWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaScheduleManagementWeb.Infrastructure.Repositories
{
    /// <summary>
    /// Класс реализует методы репозитория сеансов.
    /// </summary>
    public class SessionRepository : ISessionRepository
    {
        private readonly ApplicationDbContext _db;

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
        public async Task<IEnumerable<SessionOutput>> GetSessionsAsync()
        {
            List<SessionOutput> result = await _db.Films
                 .AsNoTracking()
                 .Select(s => new SessionOutput
                 {
                     FilmId = s.Id,
                     FilmTitle = s.Title,
                     AgeLimit = s.AgeLimit,
                     FilmDuration = s.Duration,
                     SessionDetailsOutput = s.SessionsEntity.Select(sd => new SessionDetailOutput
                     {
                         HallId = sd.HallId,
                         HallTitle = sd.HallEntity.Title,
                         SessionStart = sd.SessionStart,
                         SessionEnd = sd.SessionEnd,
                         Price = sd.Price,
                         Status = sd.Status
                     })
                     .OrderByDescending(sd => sd.SessionStart)
                     .ToList()
                 })
                 .OrderByDescending(r => r.FilmId)
                 .ToListAsync();

            return result;
        }

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


        /// <inheritdoc />
        public async Task<SessionOutput> GetSessionByFilmIdAsync(int filmId)
        {
            SessionOutput? result = await _db.Films
                .AsNoTracking()
                .Where(f => f.Id == filmId)
                .Select(s => new SessionOutput
                {
                    FilmId = s.Id,
                    FilmTitle = s.Title,
                    AgeLimit = s.AgeLimit,
                    FilmDuration = s.Duration,
                    SessionDetailsOutput = s.SessionsEntity
                        .Where(s => s.Status == SessionStatusEnum.Active.ToString())
                        .Select(sd => new SessionDetailOutput
                        {
                            HallId = sd.HallId,
                            HallTitle = sd.HallEntity.Title,
                            SessionStart = sd.SessionStart,
                            SessionEnd = sd.SessionEnd,
                            Price = sd.Price,
                            Status = sd.Status
                        })
                        .OrderByDescending(sd => sd.SessionStart)
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return result!;
        }

        /// <inheritdoc />
        public async Task CreateSessionAsync(SessionEntity sessionEntity)
        {
            sessionEntity.Status = SessionStatusEnum.Active.ToString();

            await _db.Sessions.AddAsync(sessionEntity);
            await _db.SaveChangesAsync();
        }

        #endregion

        #region Приватные методы.

        /// <summary>
        /// Метод применяет фильтры к фильмам.
        /// </summary>
        /// <param name="sessionFilterInput">Входная модель.</param>
        /// <param name="query">Запрос.</param>
        private IQueryable<FilmEntity> ApplyFilterFilms(SessionFilterInput sessionFilterInput,
            IQueryable<FilmEntity> query)
        {
            // Фильтр по жанру.
            if (sessionFilterInput.GenreId > 0)
            {
                query = query.Where(f => f.FilmGenresEntity
                    .Any(fg => fg.GenreId == sessionFilterInput.GenreId));
            }

            // Фильтр по возрастному ограничению.
            if (sessionFilterInput.AgeLimit > 0)
            {
                query = query.Where(f => f.AgeLimit >= sessionFilterInput.AgeLimit);
            }

            // Фильтр по продолжительность.
            if (sessionFilterInput.Duration > 0)
            {
                query = query.Where(f => f.Duration >= sessionFilterInput.Duration);
            }

            return query;
        }

        #endregion
    }
}

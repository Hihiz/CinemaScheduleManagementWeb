using CinemaScheduleManagementWeb.Application.Dto.Intput.Session;
using CinemaScheduleManagementWeb.Application.Dto.Output.Session;
using CinemaScheduleManagementWeb.Application.Interfaces.Services;
using CinemaScheduleManagementWeb.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CinemaScheduleManagementWeb.Api.Controllers
{
    [Route("api/session")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _sessionService;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="sessionService">Сервис сеансов.</param>
        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        #region Публичные методы.

        /// <summary>
        /// Метод получает список сеансов.
        /// </summary>
        /// <returns>Список сеансов.</returns>
        [HttpGet]
        [Route("sessions")]
        public async Task<IActionResult> GetSessionsAsync()
        {
            IEnumerable<SessionOutput> result = await _sessionService.GetSessionsAsync();

            return Ok(result);
        }

        /// <summary>
        /// Метод получает сеансы по Id фильма.
        /// </summary>
        /// <param name="filmId">Id фильма.</param>
        /// <returns>Сеансы.</returns>
        [HttpGet]
        [Route("session")]
        public async Task<IActionResult> GetSessionByFilmIdAsync([FromQuery] int filmId)
        {
            SessionOutput result = await _sessionService.GetSessionByFilmIdAsync(filmId);

            return Ok(result);
        }

        /// <summary>
        /// Метод получает список активных сеансов.
        /// </summary>
        /// <param name="sessionFilterInput">Входная модель.</param>
        /// <returns>Список активных сеансов.</returns>
        [HttpPost]
        [Route("session-filter")]
        public async Task<IActionResult> GetActiveSessionsAsync([FromBody] SessionFilterInput sessionFilterInput)
        {
            IEnumerable<SessionOutput> result = await _sessionService.GetActiveSessionsAsync(sessionFilterInput);

            return Ok(result);
        }

        /// <summary>
        /// Метод создает сеанс.
        /// </summary>
        /// <param name="sessionEntity">Модель сеанса.</param>
        [HttpPost]
        [Route("session")]
        public async Task CreateSessionAsync([FromBody] CreateSessionInput createSessionInput)
        {         
            SessionEntity entity = new SessionEntity
            {
                FilmId = createSessionInput.FilmId,
                HallId = createSessionInput.HallId,
                SessionStart = createSessionInput.SessionStart,
                SessionEnd = createSessionInput.SessionEnd,
                Price = createSessionInput.Price
            };

            await _sessionService.CreateSessionAsync(entity);
        }

        #endregion

        #region Приватные методы.

        #endregion
    }
}

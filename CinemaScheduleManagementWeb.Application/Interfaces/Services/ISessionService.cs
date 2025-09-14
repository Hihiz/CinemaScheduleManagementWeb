using CinemaScheduleManagementWeb.Application.Dto.Intput.Session;
using CinemaScheduleManagementWeb.Application.Dto.Output.Session;
using CinemaScheduleManagementWeb.Domain.Entities;

namespace CinemaScheduleManagementWeb.Application.Interfaces.Services
{
    /// <summary>
    /// Интерфейс сервиса сеанса.
    /// </summary>
    public interface ISessionService
    {
        /// <summary>
        /// Метод получает список сеансов.
        /// </summary>
        /// <returns>Список сеансов.</returns>
        Task<IEnumerable<SessionOutput>> GetSessionsAsync();

        /// <summary>
        /// Метод получает сеансы по Id фильма.
        /// </summary>
        /// <param name="filmId">Id фильма.</param>
        /// <returns>Сеансы.</returns>
        Task<SessionOutput> GetSessionByFilmIdAsync(int filmId);

        /// <summary>
        /// Метод получает список активных сеансов.
        /// </summary>
        /// <param name="sessionFilterInput">Входная модель.</param>
        /// <returns>Список активных сеансов.</returns>
        Task<IEnumerable<SessionOutput>> GetActiveSessionsAsync(SessionFilterInput sessionFilterInput);

        /// <summary>
        /// Метод создает сеанс.
        /// </summary>
        /// <param name="sessionEntity">Модель сеанса.</param>
        Task CreateSessionAsync(SessionEntity sessionEntity);
    }
}

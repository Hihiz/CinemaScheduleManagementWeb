using CinemaScheduleManagementWeb.Application.Dto.Output.Hall;

namespace CinemaScheduleManagementWeb.Application.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс репозитория залов.
    /// </summary>
    public interface IHallRepository
    {
        /// <summary>
        /// Метод получает список залов.
        /// </summary>
        /// <returns>Спсиок залов.</returns>
        Task<IEnumerable<HallOutput>> GetHallsAsync();

        /// <summary>
        /// Метод проверяет занятость зала по времени.
        /// </summary>
        /// <param name="hallId">Id зала.</param>
        /// <param name="sessionStart">Дата начала сеанса.</param>
        /// <param name="sessionEnd">Дата завершения сеанса.</param>
        /// <returns>Признак проверки.</returns>
        Task<bool> IsHallExistsAsync(int hallId, DateTime sessionStart, DateTime sessionEnd);
    }
}

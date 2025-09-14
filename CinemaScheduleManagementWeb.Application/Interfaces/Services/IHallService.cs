using CinemaScheduleManagementWeb.Application.Dto.Output.Hall;

namespace CinemaScheduleManagementWeb.Application.Interfaces.Services
{
    /// <summary>
    /// Сервис интерфейса зала.
    /// </summary>
    public interface IHallService
    {
        /// <summary>
        /// Метод получает список залов.
        /// </summary>
        /// <returns>Спсиок залов.</returns>
        Task<IEnumerable<HallOutput>> GetHallsAsync();
    }
}

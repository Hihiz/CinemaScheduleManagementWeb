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
    }
}

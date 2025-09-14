using CinemaScheduleManagementWeb.Application.Dto.Output.CinemaSetting;

namespace CinemaScheduleManagementWeb.Application.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс репозитория настроек кинотеатра.
    /// </summary>
    public interface ICinemaSettingRepository
    {
        /// <summary>
        /// Метод получает время работы кинотеатра.
        /// </summary>
        /// <returns>Время работы.</returns>
        Task<CinemaSettingOutput> GetCinemaWorkingHoursAsync();
    }
}

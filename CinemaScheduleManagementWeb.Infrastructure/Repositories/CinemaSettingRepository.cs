using CinemaScheduleManagementWeb.Application.Dto.Output.CinemaSetting;
using CinemaScheduleManagementWeb.Application.Interfaces.Repositories;
using CinemaScheduleManagementWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaScheduleManagementWeb.Infrastructure.Repositories
{
    /// <summary>
    /// Класс реалиует методы репозитория настроек кинотеатра.
    /// </summary>
    public class CinemaSettingRepository : ICinemaSettingRepository
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="db">Класс контекста.</param>
        public CinemaSettingRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<CinemaSettingOutput> GetCinemaWorkingHoursAsync()
        {
            CinemaSettingOutput result = await _db.CinemaSettings
                .AsNoTracking()
                .Select(c => new CinemaSettingOutput
                {
                    OpenTime = c.OpenTime,
                    CloseTime = c.CloseTime
                })
                .SingleAsync();

            return result;
        }

        #endregion

        #region Приватные методы.

        #endregion     
    }
}

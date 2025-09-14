using CinemaScheduleManagementWeb.Application.Dto.Output.Hall;
using CinemaScheduleManagementWeb.Application.Interfaces.Repositories;
using CinemaScheduleManagementWeb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IEnumerable<HallOutput>> GetHallsAsync()
        {
            List<HallOutput> result = await _db.Halls
             .AsNoTracking()
             .Select(h => new HallOutput
             {
                 Id = h.Id,
                 Title = h.Title,
                 TotalSeat = h.TotalSeat,
                 TechBreak = h.TechBreak
             })
             .OrderByDescending(h => h.Id)
             .ToListAsync();

            return result;
        }

        #endregion

        #region Приватные методы.

        #endregion       
    }
}

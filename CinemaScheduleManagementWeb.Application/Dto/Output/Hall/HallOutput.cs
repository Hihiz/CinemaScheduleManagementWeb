
namespace CinemaScheduleManagementWeb.Application.Dto.Output.Hall
{
    /// <summary>
    /// Класс выходной модели зала.
    /// </summary>
    public class HallOutput
    {
        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование зала.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Количество мест зала.
        /// </summary>
        public int TotalSeat { get; set; }

        /// <summary>
        /// Время технического перерыва (в минутах).
        /// </summary>
        public int TechBreak { get; set; }
    }
}

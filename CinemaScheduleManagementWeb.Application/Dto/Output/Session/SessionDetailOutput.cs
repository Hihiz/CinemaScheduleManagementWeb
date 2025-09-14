namespace CinemaScheduleManagementWeb.Application.Dto.Output.Session
{
    /// <summary>
    /// Класс выходной модели деталей сеанса.
    /// </summary>
    public class SessionDetailOutput
    {
        /// <summary>
        /// Id зала.
        /// </summary>
        public int HallId { get; set; }

        /// <summary>
        /// Наименование зала.
        /// </summary>
        public string? HallTitle { get; set; }

        /// <summary>
        /// Время начала сеанса.
        /// </summary>
        public DateTime SessionStart { get; set; }

        /// <summary>
        /// Время окончания сеанса.
        /// </summary>
        public DateTime? SessionEnd { get; set; }

        /// <summary>
        /// Цена сеанса.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Статус сеанса.
        /// </summary>
        public string? Status { get; set; }
    }
}

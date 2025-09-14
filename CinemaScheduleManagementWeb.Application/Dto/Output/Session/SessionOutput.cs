namespace CinemaScheduleManagementWeb.Application.Dto.Output.Session
{
    /// <summary>
    /// Класс выходной модели сеанса.
    /// </summary>
    public class SessionOutput
    {
        /// <summary>
        /// Id фильма.
        /// </summary>
        public int FilmId { get; set; }

        /// <summary>
        /// Наименование фильма.
        /// </summary>
        public string? FilmTitle { get; set; }

        /// <summary>
        /// Возрастное ограничение.
        /// </summary>
        public short AgeLimit { get; set; }

        /// <summary>
        /// Продолжительность фильма.
        /// </summary>
        public int FilmDuration { get; set; }

        /// <summary>
        /// Список деталей сеанса.
        /// </summary>
        public IEnumerable<SessionDetailOutput>? SessionDetailsOutput { get; set; }
    }
}

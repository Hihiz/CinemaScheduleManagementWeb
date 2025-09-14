namespace CinemaScheduleManagementWeb.Application.Dto.Intput.Session
{
    /// <summary>
    /// Класс входной модели создания сеанса.
    /// </summary>
    public class CreateSessionInput
    {       
        /// <summary>
        /// FK на Id фильма.
        /// </summary>
        public int FilmId { get; set; }

      
        /// <summary>
        /// FK на Id зала.
        /// </summary>
        public int HallId { get; set; }

     
        /// <summary>
        /// Время начала сеанса.
        /// </summary>
        public DateTime SessionStart { get; set; }

        /// <summary>
        /// Время окончания сеанса.
        /// </summary>
        public DateTime SessionEnd { get; set; }

        /// <summary>
        /// Цена сеанса.
        /// </summary>
        public decimal Price { get; set; }
    }
}

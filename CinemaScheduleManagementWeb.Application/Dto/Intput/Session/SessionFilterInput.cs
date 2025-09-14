namespace CinemaScheduleManagementWeb.Application.Dto.Intput.Session
{
    /// <summary>
    /// Класс входной модели фильтров сеанса.
    /// </summary>
    public class SessionFilterInput
    {
        /// <summary>
        /// Жанр фильма.
        /// </summary>
        public int GenreId { get; set; }

        /// <summary>
        /// Возрастное ограничение.
        /// </summary>
        public int AgeLimit { get; set; }

        /// <summary>
        /// Продолжительность фильма.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Минимальная цена.
        /// </summary>
        public decimal MinPrice { get; set; }

        /// <summary>
        /// Максимальная цена.
        /// </summary>
        public decimal MaxPrice { get; set; }

        /// <summary>
        /// Зал сеанса.
        /// </summary>
        public int HollId { get; set; }

        /// <summary>
        /// Дата начала.
        /// </summary>
        public DateTime? DateStart { get; set; }

        /// <summary>
        /// Дата конца.
        /// </summary>
        public DateTime? DateEnd { get; set; }
    }
}

namespace CinemaScheduleManagementWeb.Application.Dto.Intput.Film
{
    /// <summary>
    /// Класс входной модели фильтров фильма.
    /// </summary>
    public class FilmFilterInput
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
    }
}

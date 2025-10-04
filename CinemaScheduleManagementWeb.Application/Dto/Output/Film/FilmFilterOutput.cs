namespace CinemaScheduleManagementWeb.Application.Dto.Output.Film
{
    /// <summary>
    /// Класс выходной модели фильмов с примененными фильтрами.
    /// </summary>
    public class FilmFilterOutput
    {
        /// <summary>
        /// Id фильма.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Список жанров фильма.
        /// </summary>
        public IEnumerable<FilmGenreOutput>? FilmGenresOutput { get; set; }

        /// <summary>
        /// Постер фильма.
        /// </summary>
        public string? PosterUrl { get; set; }

        /// <summary>
        /// Статус фильма.
        /// </summary>
        public FilmStatusEnum StatusEnum { get; set; }

        public SessionDetailOutput? SessionDetailOutput { get; set; }
    }
}

using CinemaScheduleManagementWeb.Domain.Enums;

namespace CinemaScheduleManagementWeb.Application.Dto.Output.Film
{
    /// <summary>
    /// Класс выходной модели фильмов.
    /// </summary>
    public class FilmOutput
    {
        /// <summary>
        /// Id фильма.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Наименование фильма.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Продолжительность фильма (в минутах).
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Возрастное ограничение фильма.
        /// </summary>
        public short AgeLimit { get; set; }

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
    }
}

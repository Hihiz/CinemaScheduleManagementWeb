namespace CinemaScheduleManagementWeb.Domain.Entities
{
    /// <summary>
    /// Класс жанров фильма сопоставляется с таблицей public.FilmGenres.
    /// </summary>
    public class FilmGenreEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// FK на Id фильма.
        /// </summary>
        public int FilmId { get; set; }

        /// <summary>
        /// Навигационное свойство фильм.
        /// </summary>
        public FilmEntity FilmEntity { get; set; } = null!;

        /// <summary>
        /// FK на Id жанра.
        /// </summary>
        public int GenreId { get; set; }

        /// <summary>
        /// Навигационное свойство жанр.
        /// </summary>
        public GenreEntity GenreEntity { get; set; } = null!;
    }
}

namespace CinemaScheduleManagementWeb.Application.Dto.Output.Film
{
    /// <summary>
    /// Класс выходной модели жанров фильма.
    /// </summary>
    public class FilmGenreOutput
    {
        public int GenreId { get; set; }

        public string? GenreTitle { get; set; }
    }
}

namespace CinemaScheduleManagementWeb.Domain.Entities;

/// <summary>
/// Класс жанра сопоставляется с таблицей puiblic.Genres.
/// </summary>
public class GenreEntity
{
    /// <summary>
    /// PK.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование жанра.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Список фильмов.
    /// </summary>
    public ICollection<FilmEntity> FilmsEntity { get; set; } = new List<FilmEntity>();

    /// <summary>
    /// Список жанров фильма.
    /// </summary>
    public virtual ICollection<FilmGenreEntity> FilmGenresEntity { get; set; } = new List<FilmGenreEntity>();
}

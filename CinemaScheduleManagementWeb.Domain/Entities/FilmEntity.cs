namespace CinemaScheduleManagementWeb.Domain.Entities;

/// <summary>
/// Класс фильма сопоставляется с таблицей public.Films.
/// </summary>
public class FilmEntity
{
    /// <summary>
    /// PK.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Постер фильма.
    /// </summary>
    public string? PosterUrl { get; set; }

    /// <summary>
    /// Список сеансов.
    /// </summary>
    public virtual ICollection<SessionEntity> SessionsEntity { get; set; } = new List<SessionEntity>();

    /// <summary>
    /// Список жанров фильма.
    /// </summary>
    public virtual ICollection<FilmGenreEntity> FilmGenresEntity { get; set; } = new List<FilmGenreEntity>();
}

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
    /// Наименование фильма.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Продолжительность фильма (в минутах).
    /// </summary>
    public int Duration { get; set; }

    /// <summary>
    /// Возрастное ограничение фильма.
    /// </summary>
    public short AgeLimit { get; set; }

    /// <summary>
    /// Статус фильма.
    /// </summary>
    public string Status { get; set; } = null!;

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

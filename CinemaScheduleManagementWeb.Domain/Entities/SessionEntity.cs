namespace CinemaScheduleManagementWeb.Domain.Entities;

/// <summary>
/// Класс сеанса сопоставляется с таблицей public.Sessions.
/// </summary>
public class SessionEntity
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
    /// Навигационное свойство фильма.
    /// </summary>
    public FilmEntity FilmEntity { get; set; } = null!;

    /// <summary>
    /// FK на Id зала.
    /// </summary>
    public int HallId { get; set; }

    /// <summary>
    /// Навигационное свойство зала.
    /// </summary>
    public HallEntity HallEntity { get; set; } = null!;

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

    /// <summary>
    /// Статус сеанса.
    /// </summary>
    public string? Status { get; set; }
}

namespace CinemaScheduleManagementWeb.Domain.Entities;

/// <summary>
/// Класс зала сопоставляется с таблицей public.Halls. 
/// </summary>
public class HallEntity
{
    /// <summary>
    /// PK.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Наименование зала.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Количество мест зала.
    /// </summary>
    public int TotalSeat { get; set; }

    /// <summary>
    /// Время технического перерыва (в минутах).
    /// </summary>
    public int TechBreak { get; set; }

    /// <summary>
    /// Список сеансов.
    /// </summary>
    public ICollection<SessionEntity> SessionsEntity { get; set; } = new List<SessionEntity>();
}

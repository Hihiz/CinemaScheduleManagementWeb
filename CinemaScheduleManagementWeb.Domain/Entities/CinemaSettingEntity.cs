namespace CinemaScheduleManagementWeb.Domain.Entities
{
    /// <summary>
    /// Класс настроек работы кинотеатра сопоставляется с таблицей public.CinemaSettings.
    /// </summary>
    public class CinemaSettingEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Время открытия.
        /// </summary>
        public TimeSpan OpenTime { get; set; }

        /// <summary>
        /// Время закрытия.
        /// </summary>
        public TimeSpan CloseTime { get; set; }
    }
}

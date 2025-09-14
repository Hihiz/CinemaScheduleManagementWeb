namespace CinemaScheduleManagementWeb.Application.Dto.Output.CinemaSetting
{
    /// <summary>
    /// Класс входной модели настроек работы кинотеатра.
    /// </summary>
    public class CinemaSettingOutput
    {
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

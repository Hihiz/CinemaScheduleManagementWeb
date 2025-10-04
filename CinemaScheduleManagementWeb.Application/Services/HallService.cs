namespace CinemaScheduleManagementWeb.Application.Services
{
    /// <summary>
    /// Класс реализует методы сервиса залов.
    /// </summary>
    public class HallService : IHallService
    {
        private readonly ILogger<HallService> _logger;
        private readonly IHallRepository _hallRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger">Логгер.</param>
        /// <param name="hallRepository">Репозиторий залов.</param>
        public HallService(ILogger<HallService> logger,
            IHallRepository hallRepository)
        {
            _logger = logger;
            _hallRepository = hallRepository;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<IEnumerable<HallOutput>> GetHallsAsync()
        {
            try
            {
                IEnumerable<HallOutput> result = await _hallRepository.GetHallsAsync();

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        #endregion

        #region Приватные методы.

        #endregion       
    }
}

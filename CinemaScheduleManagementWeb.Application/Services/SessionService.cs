namespace CinemaScheduleManagementWeb.Application.Services
{
    /// <summary>
    /// Класс реализует методы сервиса сеансов.
    /// </summary>
    public class SessionService : ISessionService
    {
        private readonly ILogger<SessionService> _logger;
        private readonly ISessionRepository _sessionRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly ICinemaSettingRepository _cinemaSettingRepository;
        private readonly IHallRepository _hallRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger">Логгер.</param>
        /// <param name="sessionRepository">Репозиторий сеансов.</param>
        /// <param name="filmRepository">Репозиторий фильмов.</param>
        /// <param name="cinemaSettingRepository">Репозиторий настройки кинотеатра.</param>
        /// <param name="hallRepository">Репозиторий залов.</param>
        public SessionService(ILogger<SessionService> logger,
            ISessionRepository sessionRepository,
            IFilmRepository filmRepository,
            ICinemaSettingRepository cinemaSettingRepository,
            IHallRepository hallRepository)
        {
            _logger = logger;
            _sessionRepository = sessionRepository;
            _filmRepository = filmRepository;
            _cinemaSettingRepository = cinemaSettingRepository;
            _hallRepository = hallRepository;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<IEnumerable<SessionOutput>> GetActiveSessionsAsync(SessionFilterInput sessionFilterInput)
        {
            try
            {
                if (sessionFilterInput is null)
                {
                    throw new InvalidOperationException("Недопустимое значение фильтров.");
                }

                IEnumerable<SessionOutput> result = await _sessionRepository.GetActiveSessionsAsync(
                    sessionFilterInput);

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

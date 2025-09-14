using CinemaScheduleManagementWeb.Application.Dto.Intput.Session;
using CinemaScheduleManagementWeb.Application.Dto.Output.CinemaSetting;
using CinemaScheduleManagementWeb.Application.Dto.Output.Session;
using CinemaScheduleManagementWeb.Application.Interfaces.Repositories;
using CinemaScheduleManagementWeb.Application.Interfaces.Services;
using CinemaScheduleManagementWeb.Domain.Entities;
using Microsoft.Extensions.Logging;

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

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger">Логгер.</param>
        /// <param name="sessionRepository">Репозиторий сеансов.</param>
        public SessionService(ILogger<SessionService> logger,
            ISessionRepository sessionRepository,
            IFilmRepository filmRepository,
            ICinemaSettingRepository cinemaSettingRepository)
        {
            _logger = logger;
            _sessionRepository = sessionRepository;
            _filmRepository = filmRepository;
            _cinemaSettingRepository = cinemaSettingRepository;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<IEnumerable<SessionOutput>> GetSessionsAsync()
        {
            try
            {
                IEnumerable<SessionOutput> result = await _sessionRepository.GetSessionsAsync();

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<SessionOutput>> GetActiveSessionsAsync(SessionFilterInput sessionFilterInput)
        {
            try
            {
                if (sessionFilterInput is null)
                {
                    throw new InvalidOperationException("Недопустимое значение фидьтров.");
                }

                IEnumerable<SessionOutput> result = await _sessionRepository.GetSessionsAsync();

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<SessionOutput> GetSessionByFilmIdAsync(int filmId)
        {
            try
            {
                if (filmId <= 0)
                {
                    throw new InvalidOperationException("Недопустимый Id фильма. " +
                                                        $"FilmId: {filmId}.");
                }

                SessionOutput result = await _sessionRepository.GetSessionByFilmIdAsync(filmId);

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task CreateSessionAsync(SessionEntity sessionEntity)
        {
            try
            {
                if (sessionEntity.FilmId <= 0)
                {
                    throw new InvalidOperationException("Недопустимый Id фильма. " +
                                                       $"FilmId: {sessionEntity.FilmId}.");
                }

                if (sessionEntity.HallId <= 0)
                {
                    throw new InvalidOperationException("Недопустимый Id зала. " +
                                                        $"HallId: {sessionEntity.HallId}.");
                }

                if (sessionEntity.Price <= 0)
                {
                    throw new InvalidOperationException("Недопустимая цена сеанса. " +
                                                        $"Price: {sessionEntity.Price}.");
                }

                await ValidateCinemaAsync(sessionEntity);

                await _sessionRepository.CreateSessionAsync(sessionEntity);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        #endregion

        #region Приватные методы.

        /// <summary>
        /// Метод проверяет параметры сеанса перед созданием.
        /// </summary>
        /// <param name="sessionEntity">Мдель сеанса.</param>
        private async Task ValidateCinemaAsync(SessionEntity sessionEntity)
        {
            bool isFilmAcitve = await _filmRepository.CheckFilmActiveStatusByFilmIdAsync(sessionEntity.FilmId);

            if (!isFilmAcitve)
            {
                throw new InvalidOperationException("Недопустимый статус фильма. " +
                                                    $"FilmId: {sessionEntity.FilmId}.");
            }

            CinemaSettingOutput cinemaSetting = await _cinemaSettingRepository.GetCinemaWorkingHoursAsync();

            if (sessionEntity.SessionStart.TimeOfDay < cinemaSetting.OpenTime
                || sessionEntity.SessionEnd.TimeOfDay > cinemaSetting.CloseTime)
            {
                throw new InvalidOperationException("Сеанс выходит за время работы кинотеатра.");
            }

            bool isHallExists = await _sessionRepository.IsHallExistsAsync(sessionEntity.HallId,
                sessionEntity.SessionStart, sessionEntity.SessionEnd);

            if (isHallExists)
            {
                throw new InvalidOperationException("Сеанс на выбранное время занят.");
            }
        }

        #endregion
    }
}

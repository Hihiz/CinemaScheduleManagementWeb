using CinemaScheduleManagementWeb.Application.Dto.Intput.Film;
using CinemaScheduleManagementWeb.Application.Dto.Output.Film;
using CinemaScheduleManagementWeb.Application.Interfaces.Repositories;
using CinemaScheduleManagementWeb.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace CinemaScheduleManagementWeb.Application.Services
{
    /// <summary>
    /// Класс реализует методы сервиса фильмов.
    /// </summary>
    public class FilmService : IFilmService
    {
        private readonly ILogger<FilmService> _logger;
        private readonly IFilmRepository _filmRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger">Логгер.</param>
        /// <param name="filmRepository">Репозиторий фильмов.</param>
        public FilmService(ILogger<FilmService> logger,
            IFilmRepository filmRepository)
        {
            _logger = logger;
            _filmRepository = filmRepository;
        }

        #region Публичные методы.

        /// <inheritdoc />
        public async Task<IEnumerable<FilmOutput>> GetFilmsAsync()
        {
            try
            {
                IEnumerable<FilmOutput> result = await _filmRepository.GetFilmsAsync();

                return result;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task ExcludeFilmByFilmIdAsync(int filmId)
        {
            try
            {
                if (filmId <= 0)
                {
                    throw new InvalidOperationException("Недопустимый Id фильма. " +
                                                        $"FilmId: {filmId}.");
                }

                await _filmRepository.ExcludeFilmByFilmIdAsync(filmId);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<FilmFilterOutput>> GetFilmsFilterAsync(string searchText,
            FilmFilterInput filterFilmInput)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    throw new InvalidOperationException("Недопустимое значение строки поиска. " +
                                                        $"SearchText: {searchText}.");
                }

                if (filterFilmInput is null)
                {
                    throw new InvalidOperationException("Недопустимое значение строки поиска. " +
                                                        $"SearchText: {searchText}.");
                }

                IEnumerable<FilmFilterOutput> result = await _filmRepository.GetFilmsFilterAsync(searchText,
                    filterFilmInput);

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

namespace CinemaScheduleManagementWeb.Application.Services
{
    /// <summary>
    /// Класс реализует методы сервиса фильмов.
    /// </summary>
    public class FilmService : IFilmService
    {
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

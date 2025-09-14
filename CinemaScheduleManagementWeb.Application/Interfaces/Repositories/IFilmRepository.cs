using CinemaScheduleManagementWeb.Application.Dto.Intput.Film;
using CinemaScheduleManagementWeb.Application.Dto.Output.Film;

namespace CinemaScheduleManagementWeb.Application.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс репозитория фильма.
    /// </summary>
    public interface IFilmRepository
    {
        /// <summary>
        /// Метод получает список фильмов.
        /// </summary>
        /// <returns>Список фильмов.</returns>
        Task<IEnumerable<FilmOutput>> GetFilmsAsync();

        /// <summary>
        /// Метод получает список фильмов с фильтрами.
        /// </summary>
        /// <param name="searchText">Текс для поиска.</param>
        /// <param name="filterFilmInput">Входная модель.</param>
        /// <returns>Список фильмов.</returns>
        Task<IEnumerable<FilmFilterOutput>> GetFilmsFilterAsync(string searchText, FilmFilterInput filterFilmInput);

        /// <summary>
        /// Метод проверяет на прокате ли фильм по Id фильма.
        /// </summary>
        /// <param name="filmId">Id фильма.</param>
        /// <returns>Признак проверки.</returns>
        Task<bool> CheckFilmActiveStatusByFilmIdAsync(int filmId);

        /// <summary>
        /// Метод снимает фильм с проката.
        /// </summary>
        /// <param name="filmId">Id фильма.</param>
        Task ExcludeFilmByFilmIdAsync(int filmId);
    }
}

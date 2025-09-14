using CinemaScheduleManagementWeb.Application.Dto.Intput.Film;
using CinemaScheduleManagementWeb.Application.Dto.Output.Film;

namespace CinemaScheduleManagementWeb.Application.Interfaces.Services
{
    /// <summary>
    /// Интерфейс сервиса фильмов.
    /// </summary>
    public interface IFilmService
    {
        /// <summary>
        /// Метод получает список фильмов.
        /// </summary>
        /// <returns>Список фильмов.</returns>
        Task<IEnumerable<FilmOutput>> GetFilmsAsync();

        /// Метод получает список фильмов с фильтрами.
        /// </summary>
        /// <param name="searchText">Текс для поиска.</param>
        /// <param name="filterFilmInput">Входная модель.</param>
        /// <returns>Список фильмов.</returns>
        Task<IEnumerable<FilmFilterOutput>> GetFilmsFilterAsync(string searchText, FilmFilterInput filterFilmInput);

        /// <summary>
        /// Метод снимает фильм с проката.
        /// </summary>
        /// <param name="filmId">Id фильма.</param>
        Task ExcludeFilmByFilmIdAsync(int filmId);
    }
}

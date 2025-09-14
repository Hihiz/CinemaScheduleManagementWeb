using CinemaScheduleManagementWeb.Application.Dto.Intput.Film;

namespace CinemaScheduleManagementWeb.Test.Integration.Repository.Session
{
    public class GetFilmsFilterTest : BaseIntegrationTest
    {
        [Fact]
        public async Task GetFilmsFilterAsyncTest()
        {
            // Arrange
            var filter = new FilmFilterInput()
            {
               
            };
            // Act
            var result = await filmRepository.GetFilmsFilterAsync(null, filter);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetFilmsFilterAsyncSearchTextTest()
        {
            // Arrange
            var filter = new FilmFilterInput()
            {
                GenreId = 0,
                AgeLimit = 0,
                Duration = 0,
                MinPrice = 0,
                MaxPrice = 500
            };
            // Act
            var result = await filmRepository.GetFilmsFilterAsync("льм", filter);

            // Assert
            Assert.NotNull(result);
        }
    }
}

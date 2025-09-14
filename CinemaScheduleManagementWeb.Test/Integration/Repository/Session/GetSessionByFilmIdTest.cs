namespace CinemaScheduleManagementWeb.Test.Integration.Repository.Session
{
    public class GetSessionByFilmIdTest : BaseIntegrationTest
    {
        [Fact]
        public async Task GetSessionByFilmIdAsyncTest()
        {
            // Arrange && Act
            var result = await sessionRepository.GetSessionByFilmIdAsync(2);

            // Assert
            Assert.NotNull(result);
        }
    }
}

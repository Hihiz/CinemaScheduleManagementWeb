namespace CinemaScheduleManagementWeb.Test.Integration.Repository.Film
{
    public class GetFilmsTest : BaseIntegrationTest
    {
        [Fact]
        public async Task GetFilmsAsyncTest()
        {
            // Arrange && Act
            var result = await filmRepository.GetFilmsAsync();

            // Assert
            Assert.NotNull(result);
        }
    }
}

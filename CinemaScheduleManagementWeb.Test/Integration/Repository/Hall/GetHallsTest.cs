namespace CinemaScheduleManagementWeb.Test.Integration.Repository.Hall
{
    public class GetHallsTest : BaseIntegrationTest
    {
        [Fact]
        public async Task GetHallsAsyncTest()
        {
            // Arrange && Act
            var result = await hallRepository.GetHallsAsync();

            // Assert
            Assert.NotNull(result);
        }
    }
}

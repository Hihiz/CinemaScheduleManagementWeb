namespace CinemaScheduleManagementWeb.Test.Integration.Repository.Session
{
    public class GetSessionsTest : BaseIntegrationTest
    {
        [Fact]
        public async Task GetSessionsAsyncTest()
        {
            // Arrange && Act
            var result = await sessionRepository.GetSessionsAsync();

            // Assert
            Assert.NotNull(result);
        }
    }
}

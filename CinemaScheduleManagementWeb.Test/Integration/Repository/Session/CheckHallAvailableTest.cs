namespace CinemaScheduleManagementWeb.Test.Integration.Repository.Session
{
    public class CheckHallAvailableTest : BaseIntegrationTest
    {
        [Fact]
        public async Task IsHallExistsAsyncTest()
        {
            // Arrange
            var start = new DateTime(2025, 9, 12, 15, 00, 00, DateTimeKind.Utc);
            var end = new DateTime(2025, 9, 12, 15, 10, 00, DateTimeKind.Utc);

            //  Act
            var result = await hallRepository.IsHallExistsAsync(1, start, end);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task IsHallExistsAsyncExistsTest()
        {
            // Arrange
            var start = new DateTime(2025, 9, 12, 14, 30, 00, DateTimeKind.Utc);
            var end = new DateTime(2025, 9, 12, 15, 30, 00, DateTimeKind.Utc);

            //  Act
            var result = await hallRepository.IsHallExistsAsync(1, start, end);

            // Assert
            Assert.True(result);
        }
    }
}

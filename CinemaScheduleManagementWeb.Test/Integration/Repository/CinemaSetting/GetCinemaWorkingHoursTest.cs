namespace CinemaScheduleManagementWeb.Test.Integration.Repository.CinemaSetting
{
    public class GetCinemaWorkingHoursTest : BaseIntegrationTest
    {
        [Fact]
        public async Task GetCinemaWorkingHoursAsyncTest()
        {
            // Arrange && Act
            var result = await cinemaSettingRepository.GetCinemaWorkingHoursAsync();

            // Assert
            Assert.NotNull(result);
        }
    }
}

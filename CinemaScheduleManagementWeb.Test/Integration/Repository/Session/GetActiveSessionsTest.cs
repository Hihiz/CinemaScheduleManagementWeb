using CinemaScheduleManagementWeb.Application.Dto.Intput.Session;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CinemaScheduleManagementWeb.Test.Integration.Repository.Session
{
    public class GetActiveSessionsTest : BaseIntegrationTest
    {
        [Fact]
        public async Task GetActiveSessionsAsyncTest()
        {
            // Arrange         
            var filter = new SessionFilterInput
            {
              /*  MaxPrice =  1000*/
            
                //HollId = 1,
                AgeLimit = 1,
                DateStart = new DateTime(2025, 09, 15, 9, 59, 00, DateTimeKind.Utc),
                DateEnd = new DateTime(2025, 09, 15, 18, 50, 00, DateTimeKind.Utc),
            };

            // Act
            var result = await sessionRepository.GetActiveSessionsAsync(filter);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetActiveSessionsAsyncApplyTest()
        {
            // Arrange
            var filter = new SessionFilterInput
            {
                HollId = 1,
                DateStart = new DateTime(2025, 09, 12, 10, 00, 00, DateTimeKind.Utc),
                DateEnd = new DateTime(2025, 09, 12, 13, 00, 00, DateTimeKind.Utc),
            };

          
            // Act
            var result = await sessionRepository.GetActiveSessionsAsync(filter);

            // Assert
            Assert.NotNull(result);
        }
    }

}
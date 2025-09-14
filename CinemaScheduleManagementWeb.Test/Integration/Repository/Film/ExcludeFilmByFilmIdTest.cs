namespace CinemaScheduleManagementWeb.Test.Integration.Repository.Film
{
    public class ExcludeFilmByFilmIdTest : BaseIntegrationTest
    {
        [Fact]
        public async Task ExcludeFilmByFilmIdAsyncTest()
        {
            await filmRepository.ExcludeFilmByFilmIdAsync(2);
        }
    }
}

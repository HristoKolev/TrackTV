namespace TrackTv.Models.Tests
{
    using TrackTv.Data.Models;

    using Xunit;

    public class ShowsUsersTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Constructor_Takes_Correct_Parameters()
        {
            int showId = 1;

            int profileId = 2;

            var showsUsers = new Subscription(profileId, showId);

            Assert.Equal(showId, showsUsers.ShowId);
            Assert.Equal(profileId, showsUsers.ProfileId);
        }
    }
}
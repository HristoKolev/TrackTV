namespace TrackTv.Models.Tests
{
    using TrackTv.Models.Joint;

    using Xunit;

    public class ShowsUsersTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Constructor_Takes_Correct_Parameters()
        {
            int showId = 1;

            int userId = 2;

            var showsUsers = new ShowsUsers(userId, showId);

            Assert.Equal(showId, showsUsers.ShowId);
            Assert.Equal(userId, showsUsers.UserId);
        }
    }
}
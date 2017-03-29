namespace TrackTv.Models.Tests
{
    using System.Collections.Generic;

    using TrackTv.Data.Models;

    using Xunit;

    public class ProfileTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public void ShowsUsers_Is_Initialize()
        {
            var user = new Profile();

            Assert.NotNull(user.ShowsUsers);

            Assert.IsType<List<ShowsProfiles>>(user.ShowsUsers);
        }
    }
}
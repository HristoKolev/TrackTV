namespace TrackTv.Models.Tests
{
    using System.Collections.Generic;

    using TrackTv.Models.Joint;

    using Xunit;

    public class UserTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public void ShowsUsers_Is_Initialize()
        {
            var user = new User();

            Assert.NotNull(user.ShowsUsers);

            Assert.IsType<List<ShowsUsers>>(user.ShowsUsers);
        }
    }
}
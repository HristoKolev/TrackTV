namespace TrackTv.Models.Tests
{
    using TrackTv.Data.Models;

    using Xunit;

    public class ShowsActorsTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Constructor_Takes_Correct_Parameters()
        {
            var actor = new Actor();

            string role = "Role";

            var showsActors = new Role(actor, role);

            Assert.Equal(actor, showsActors.Actor);
            Assert.Equal(role, showsActors.RoleName);
        }
    }
}
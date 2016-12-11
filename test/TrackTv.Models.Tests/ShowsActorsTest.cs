namespace TrackTv.Models.Tests
{
    using TrackTv.Models.Joint;

    using Xunit;

    public class ShowsActorsTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Constructor_Takes_Correct_Parameters()
        {
            var actor = new Actor();

            string role = "Role";

            var showsActors = new ShowsActors(actor, role);

            Assert.Equal(actor, showsActors.Actor);
            Assert.Equal(role, showsActors.Role);
        }
    }
}
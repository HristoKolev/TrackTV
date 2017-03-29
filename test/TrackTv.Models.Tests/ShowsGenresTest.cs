namespace TrackTv.Models.Tests
{
    using TrackTv.Data.Models;

    using Xunit;

    public class ShowsGenresTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Constructor_Task_Correct_Parameters()
        {
            var genre = new Genre();

            var showsGenres = new ShowsGenres(genre);

            Assert.Equal(genre, showsGenres.Genre);
        }
    }
}
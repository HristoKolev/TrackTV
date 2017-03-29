namespace TrackTv.Models.Tests
{
    using System.Collections.Generic;

    using Xunit;

    public class GenreTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Constructors_Accepts_The_Right_Parameters()
        {
            string name = "genre name";

            var genre = new Genre(name);

            Assert.Equal(name, genre.Name);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void ShowsGenres_Is_Initialized()
        {
            var genre = new Genre();

            Assert.NotNull(genre.ShowsGenres);

            Assert.IsType<List<ShowsGenres>>(genre.ShowsGenres);
        }
    }
}
namespace TrackTv.Data.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TrackTV.Data;
    using TrackTV.Data.Repositories;

    using TrackTv.Models;

    using Xunit;

    public class GenresRepositoryTest : BaseRepositoryTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetGenresByNamesAsync_should_return_genres_by_names()
        {
            var context = CreateContext(nameof(this.GetGenresByNamesAsync_should_return_genres_by_names));

            await SeedGenresAsync(context);

            var repo = new GenresRepository(context);

            var names = new[]
            {
                "action",
                "crime"
            };

            var genres = await repo.GetGenresByNamesAsync(names);

            Assert.Equal(names.Length, genres.Length);

            for (int i = 0; i < names.Length; i++)
            {
                Assert.Equal(names[i], genres[i].Name);
            }
        }

        private static async Task SeedGenresAsync(TrackTvDbContext context, IEnumerable<Genre> genres = null)
        {
            if (genres == null)
            {
                genres = new[]
                {
                    new Genre("action"),
                    new Genre("drama"),
                    new Genre("comedy"),
                    new Genre("crime"),
                    new Genre("aventure")
                };
            }

            context.Genres.AddRange(genres);
            await context.SaveChangesAsync();
        }
    }
}
namespace TrackTv.DataRetrieval.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using NSubstitute;

    using TrackTv.Data.Models;
    using TrackTv.DataRetrieval.Data;
    using TrackTv.DataRetrieval.Fetchers;

    using Xunit;

    public class GenreFetcherTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateGenresAsync_should_add_new_genres()
        {
            var genresRepository = Substitute.For<IGenresRepository>();

            var names = new[]
            {
                "action",
                "drama",
                "comedy"
            };

            genresRepository.GetGenresByNamesAsync(names).Returns(new Genre[0]);

            var fetcher = new GenreFetcher(genresRepository);

            var show = new Show();

            await fetcher.PopulateGenresAsync(show, names).ConfigureAwait(false);

            await genresRepository.Received().GetGenresByNamesAsync(names).ConfigureAwait(false);

            var relationships = show.ShowsGenres.ToArray();

            for (int i = 0; i < names.Length; i++)
            {
                Assert.Equal(names[i], relationships[i].Genre.GenreName);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateGenresAsync_should_attach_existing_and_add_new_genres()
        {
            var genresRepository = Substitute.For<IGenresRepository>();

            var names = new[]
            {
                "action",
                "comedy",
                "drama",
                "Sci-Fi"
            };

            var genres = new[]
            {
                new Genre("action"),
                new Genre("comedy")
            };

            genresRepository.GetGenresByNamesAsync(names).Returns(genres);

            var fetcher = new GenreFetcher(genresRepository);

            var show = new Show();

            await fetcher.PopulateGenresAsync(show, names).ConfigureAwait(false);

            await genresRepository.Received().GetGenresByNamesAsync(names).ConfigureAwait(false);

            var relationships = show.ShowsGenres.ToArray();

            Assert.Equal(genres[0], relationships[0].Genre);
            Assert.Equal(genres[1], relationships[1].Genre);

            Assert.Equal(names[2], relationships[2].Genre.GenreName);
            Assert.Equal(names[3], relationships[3].Genre.GenreName);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateGenresAsync_should_attach_existing_genres()
        {
            var genresRepository = Substitute.For<IGenresRepository>();

            var names = new[]
            {
                "action",
                "drama",
                "comedy"
            };

            var genres = new[]
            {
                new Genre("action"),
                new Genre("drama"),
                new Genre("comedy")
            };

            genresRepository.GetGenresByNamesAsync(names).Returns(genres);

            var fetcher = new GenreFetcher(genresRepository);

            var show = new Show();

            await fetcher.PopulateGenresAsync(show, names).ConfigureAwait(false);

            await genresRepository.Received().GetGenresByNamesAsync(names).ConfigureAwait(false);

            var relationships = show.ShowsGenres.ToArray();

            for (int i = 0; i < names.Length; i++)
            {
                Assert.Equal(genres[i], relationships[i].Genre);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateGenresAsync_should_query_the_genres_by_name()
        {
            var genresRepository = Substitute.For<IGenresRepository>();

            var names = new[]
            {
                "action"
            };

            var genres = new[]
            {
                new Genre("action")
            };

            genresRepository.GetGenresByNamesAsync(names).Returns(genres);

            var fetcher = new GenreFetcher(genresRepository);

            var show = new Show();

            await fetcher.PopulateGenresAsync(show, names).ConfigureAwait(false);

            await genresRepository.Received().GetGenresByNamesAsync(names).ConfigureAwait(false);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateGenresAsync_should_throw_if_passed_empty_array_genre_names()
        {
            var fetcher = new GenreFetcher(Substitute.For<IGenresRepository>());

            await Assert.ThrowsAsync<ArgumentException>(
                            async () => await fetcher.PopulateGenresAsync(new Show(), Array.Empty<string>()).ConfigureAwait(false))
                        .ConfigureAwait(false);
        }
    }
}
﻿namespace TrackTv.DataRetrieval.Tests.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TrackTv.Data;
    using TrackTv.Data.Models;
    using TrackTv.DataRetrieval.Data;

    using Xunit;

    public class GenresRepositoryTest : BaseRepositoryTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetGenresByNamesAsync_should_return_empty_array_if_no_matching_genres_are_found()
        {
            using (var context = CreateContext())
            {
                await SeedGenresAsync(context).ConfigureAwait(false);

                var repo = new GenresRepository(context);

                var names = new[]
                {
                    "action1",
                    "crime1"
                };

                var genres = await repo.GetGenresByNamesAsync(names).ConfigureAwait(false);

                Assert.Equal(0, genres.Length);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetGenresByNamesAsync_should_return_empty_array_if_the_genre_names_array_is_empty()
        {
            using (var context = CreateContext())
            {
                await SeedGenresAsync(context).ConfigureAwait(false);

                var repo = new GenresRepository(context);

                var genres = await repo.GetGenresByNamesAsync(Array.Empty<string>()).ConfigureAwait(false);

                Assert.Equal(0, genres.Length);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetGenresByNamesAsync_should_return_genres_by_names()
        {
            using (var context = CreateContext())
            {
                await SeedGenresAsync(context).ConfigureAwait(false);

                var repo = new GenresRepository(context);

                var names = new[]
                {
                    "action",
                    "crime"
                };

                var genres = await repo.GetGenresByNamesAsync(names).ConfigureAwait(false);

                Assert.Equal(names.Length, genres.Length);

                for (int i = 0; i < names.Length; i++)
                {
                    Assert.Equal(names[i], genres[i].GenreName);
                }
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
            await context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
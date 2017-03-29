namespace TrackTv.DataRetrieval.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTv.Data;
    using TrackTv.Data.Models;

    using Xunit;

    public class EpisodeRepositoryTest : BaseRepositoryTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetEpisodeById_should_return_episode_by_id()
        {
            using (var context = CreateContext())
            {
                await SeedEpisodesAsync(context);

                var repository = new EpisodeRepository(context);

                int id = 2;

                var episode = await repository.GetEpisodeById(id);

                Assert.Equal(id, episode.Id);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetEpisodeById_should_return_null_if_episode_does_not_exist()
        {
            using (var context = CreateContext())
            {
                await SeedEpisodesAsync(context);

                var repository = new EpisodeRepository(context);

                int id = 42;

                var episode = await repository.GetEpisodeById(id);

                Assert.Null(episode);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetEpisodesByShowIdAsync_should_return_empty_array_with_showId_that_does_not_exist()
        {
            using (var context = CreateContext())
            {
                await SeedEpisodesAsync(context);

                var repository = new EpisodeRepository(context);

                int showId = 0;

                var episodes = await repository.GetEpisodesByShowIdAsync(showId);

                Assert.Equal(0, episodes.Length);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetEpisodesByShowIdAsync_should_return_episodes_with_a_given_showId()
        {
            using (var context = CreateContext())
            {
                await SeedEpisodesAsync(context);

                var repository = new EpisodeRepository(context);

                int showId = 100;

                var episodes = await repository.GetEpisodesByShowIdAsync(showId);

                Assert.Equal(1, episodes.Length);

                Assert.Equal(showId, episodes.First().ShowId);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetEpisodesByTheTvDbIdsAsync_should_return_empty_array_when_called_with_empty_array()
        {
            using (var context = CreateContext())
            {
                await SeedEpisodesAsync(context);

                var repository = new EpisodeRepository(context);

                var episodes = await repository.GetEpisodesByTheTvDbIdsAsync(Array.Empty<int>());

                Assert.Equal(0, episodes.Length);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetEpisodesByTheTvDbIdsAsync_should_return_episodes_with_mathing_ids()
        {
            using (var context = CreateContext())
            {
                await SeedEpisodesAsync(context);

                var repository = new EpisodeRepository(context);

                int[] ids = {
                    10,
                    20,
                    30
                };

                var episodes = await repository.GetEpisodesByTheTvDbIdsAsync(ids);

                Assert.Equal(ids.Length, episodes.Length);

                for (int i = 0; i < episodes.Length; i++)
                {
                    Assert.Equal(ids[i], episodes[i].TheTvDbId);
                }
            }
        }

        private static async Task SeedEpisodesAsync(TrackTvDbContext context, IEnumerable<Episode> episodes = null)
        {
            if (episodes == null)
            {
                var list = new List<Episode>();

                for (int i = 1; i <= 10; i++)
                {
                    list.Add(new Episode
                    {
                        Id = i,
                        TheTvDbId = i * 10,
                        ShowId = i * 100
                    });
                }

                episodes = list;
            }

            context.Episodes.AddRange(episodes);
            await context.SaveChangesAsync();
        }
    }
}
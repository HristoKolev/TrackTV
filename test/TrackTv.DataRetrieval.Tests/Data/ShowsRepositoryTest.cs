namespace TrackTv.DataRetrieval.Tests.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Data.Models;
    using TrackTv.DataRetrieval.Data;

    using Xunit;

    public class ShowsRepositoryTest : BaseRepositoryTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task AddShowAsync_should_save_the_show()
        {
            using (var context = CreateContext())
            {
                var repository = new ShowsRepository(context);

                var network = new Network("bbc");
                var genre = new Genre("action");
                var user = new Profile
                {
                    Username = "bestOfHumanity"
                };
                var actor = new Actor
                {
                    Name = "Cat"
                };

                var show = CreateShow("Show1", 1000, network, genre, actor, user);

                await repository.AddShowAsync(show).ConfigureAwait(false);

                var savedShow = await context.Shows.SingleOrDefaultAsync().ConfigureAwait(false);

                Assert.NotNull(savedShow);

                Assert.Equal(show.TheTvDbId, savedShow.TheTvDbId);
                Assert.Equal(show.Name, savedShow.Name);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetFullShowById_should_return_full_show_by_id()
        {
            using (var context = CreateContext())
            {
                await SeedGenresAsync(context).ConfigureAwait(false);

                var repo = new ShowsRepository(context);

                var show = await repo.GetFullShowByIdAsync(2).ConfigureAwait(false);

                Assert.Equal("Show2", show.Name);

                AssertIsFull(show);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetFullShowById_should_return_null_if_the_id_does_not_exists()
        {
            using (var context = CreateContext())
            {
                await SeedGenresAsync(context).ConfigureAwait(false);

                var repo = new ShowsRepository(context);

                var show = await repo.GetFullShowByIdAsync(10000).ConfigureAwait(false);

                Assert.Null(show);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetFullShowsByTheTvDbIdsAsync_should_return_full_shows_by_ids()
        {
            using (var context = CreateContext())
            {
                await SeedGenresAsync(context).ConfigureAwait(false);

                var repo = new ShowsRepository(context);

                int[] ids = {
                    1002,
                    1003,
                    1004
                };

                var shows = await repo.GetFullShowsByTheTvDbIdsAsync(ids).ConfigureAwait(false);

                Assert.Equal(ids.Length, shows.Length);

                for (int i = 0; i < ids.Length; i++)
                {
                    Assert.Equal(ids[i], shows[i].TheTvDbId);

                    AssertIsFull(shows[i]);
                }
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetFullShowsByTheTvDbIdsAsync_should_return_null_if_ids_array_is_empty()
        {
            using (var context = CreateContext())
            {
                await SeedGenresAsync(context).ConfigureAwait(false);

                var repo = new ShowsRepository(context);

                var shows = await repo.GetFullShowsByTheTvDbIdsAsync(Array.Empty<int>()).ConfigureAwait(false);

                Assert.Equal(0, shows.Length);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetFullShowsByTheTvDbIdsAsync_should_return_null_if_no_id_matches()
        {
            using (var context = CreateContext())
            {
                await SeedGenresAsync(context).ConfigureAwait(false);

                var repo = new ShowsRepository(context);

                int[] ids = {
                    999999999
                };

                var shows = await repo.GetFullShowsByTheTvDbIdsAsync(ids).ConfigureAwait(false);

                Assert.Equal(0, shows.Length);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task UpdateShowAsync_should_update_navigation_properties()
        {
            using (var context = CreateContext())
            {
                var network = new Network("bbc");
                var genre = new Genre("action");
                var user = new Profile
                {
                    Username = "bestOfHumanity"
                };
                var actor = new Actor
                {
                    Name = "Cat"
                };

                var show = CreateShow("Show1", 1000, network, genre, actor, user);

                context.Shows.Add(show);

                await context.SaveChangesAsync().ConfigureAwait(false);

                string newNetworkName = "CatWork";
                var newNetwork = new Network(newNetworkName);

                show.Network = newNetwork;

                var repository = new ShowsRepository(context);

                await repository.UpdateShowAsync(show).ConfigureAwait(false);

                var savedShow = await context.Shows.Include(s => s.Network).SingleOrDefaultAsync().ConfigureAwait(false);

                Assert.NotNull(savedShow);

                Assert.Equal(newNetworkName, savedShow.Network.Name);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task UpdateShowAsync_should_update_the_show()
        {
            using (var context = CreateContext())
            {
                var network = new Network("bbc");
                var genre = new Genre("action");
                var user = new Profile
                {
                    Username = "bestOfHumanity"
                };
                var actor = new Actor
                {
                    Name = "Cat"
                };

                var show = CreateShow("Show1", 1000, network, genre, actor, user);

                context.Shows.Add(show);

                await context.SaveChangesAsync().ConfigureAwait(false);

                string newName = "New Name";
                string newBanner = "New Banner";

                context.Entry(show).State = EntityState.Detached;

                show.Name = newName;
                show.Banner = newBanner;

                var repository = new ShowsRepository(context);

                await repository.UpdateShowAsync(show).ConfigureAwait(false);

                var savedShow = await context.Shows.SingleOrDefaultAsync().ConfigureAwait(false);

                Assert.NotNull(savedShow);

                Assert.Equal(newName, savedShow.Name);
                Assert.Equal(newBanner, savedShow.Banner);
            }
        }

        private static void AssertIsFull(Show show)
        {
            Assert.NotNull(show.Network);
            Assert.True(show.Episodes.Any());
            Assert.True(show.ShowsGenres.Any());
            Assert.True(show.ShowsActors.Any());
            Assert.True(show.ShowsProfiles.Any());
        }

        private static Show CreateShow(string name, int theTvDbId, Network network, Genre genre, Actor actor, Profile profile)
        {
            return new Show
            {
                Name = name,
                TheTvDbId = theTvDbId,
                Network = network,
                ShowsGenres = {
                    new ShowsGenres(genre)
                },
                ShowsActors = {
                    new ShowsActors
                    {
                        Actor = actor
                    }
                },
                ShowsProfiles = {
                    new ShowsProfiles
                    {
                        Profile = profile
                    }
                },
                Episodes = {
                    new Episode
                    {
                        Title = "Title"
                    }
                }
            };
        }

        private static async Task SeedGenresAsync(TrackTvDbContext context, IEnumerable<Show> shows = null)
        {
            if (shows == null)
            {
                var network = new Network("bbc");
                var genre = new Genre("action");
                var user = new Profile
                {
                    Username = "bestOfHumanity"
                };
                var actor = new Actor
                {
                    Name = "Cat"
                };

                shows = new[]
                {
                    CreateShow("Show1", 1000, network, genre, actor, user),
                    CreateShow("Show2", 1001, network, genre, actor, user),
                    CreateShow("Show3", 1002, network, genre, actor, user),
                    CreateShow("Show4", 1003, network, genre, actor, user),
                    CreateShow("Show5", 1004, network, genre, actor, user)
                };
            }

            context.Shows.AddRange(shows);
            await context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
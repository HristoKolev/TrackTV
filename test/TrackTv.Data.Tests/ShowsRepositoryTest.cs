namespace TrackTv.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTV.Data;
    using TrackTV.Data.Repositories;

    using TrackTv.Models;
    using TrackTv.Models.Joint;

    using Xunit;

    public class ShowsRepositoryTest : BaseRepositoryTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetFullShowById_should_return_full_show_by_id()
        {
            using (var context = CreateContext())
            {
                await SeedGenresAsync(context);

                var repo = new ShowsRepository(context);

                var show = await repo.GetFullShowById(2);

                Assert.Equal("Show2", show.Name);

                AssertIsFull(show);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetFullShowsByTheTvDbIdsAsync_should_return_full_shows_by_ids()
        {
            using (var context = CreateContext())
            {
                await SeedGenresAsync(context);

                var repo = new ShowsRepository(context);

                int[] ids = {
                    1002,
                    1003,
                    1004
                };

                var shows = await repo.GetFullShowsByTheTvDbIdsAsync(ids);

                Assert.Equal(ids.Length, shows.Length);

                for (int i = 0; i < ids.Length; i++)
                {
                    Assert.Equal(ids[i], shows[i].TheTvDbId);

                    AssertIsFull(shows[i]);
                }
            }
        }

        private static void AssertIsFull(Show show)
        {
            Assert.NotNull(show.Network);
            Assert.True(show.Episodes.Any());
            Assert.True(show.ShowsGenres.Any());
            Assert.True(show.ShowsActors.Any());
            Assert.True(show.ShowsUsers.Any());
        }

        private static Show CreateShow(string name, int theTvDbId, Network network, Genre genre, Actor actor, User user)
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
                ShowsUsers = {
                    new ShowsUsers
                    {
                        User = user
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
                var user = new User
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
            await context.SaveChangesAsync();
        }
    }
}
namespace TrackTv.Services.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTv.Data.Models;
    using TrackTv.Services.Data;
    using TrackTv.Services.Data.Exceptions;

    using Xunit;

    public class ShowsRepositoryTest : BaseRepositoryTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task CountAllAsync_returns_correct_count()
        {
            using (var context = CreateContext())
            {
                var list = new List<Show>();

                for (int i = 1; i <= 10; i++)
                {
                    var show = new Show
                    {
                        Id = i
                    };

                    list.Add(show);
                }

                context.Shows.AddRange(list);
                await context.SaveChangesAsync().ConfigureAwait(false);

                var repository = new ShowsRepository(context);

                Assert.Equal(10, await repository.CountAllAsync().ConfigureAwait(false));
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task CountAllResultsAsync_returns_correct_count()
        {
            using (var context = CreateContext())
            {
                var list = new List<Show>();

                for (int i = 1; i <= 10; i++)
                {
                    var show = new Show
                    {
                        Id = i,
                        Name = i % 2 == 0 ? "new show " + i : "old show " + i
                    };

                    list.Add(show);
                }

                context.Shows.AddRange(list);
                await context.SaveChangesAsync().ConfigureAwait(false);

                var repository = new ShowsRepository(context);

                Assert.Equal(5, await repository.CountAllResultsAsync("new show").ConfigureAwait(false));
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("\t")]

        // ReSharper disable once InconsistentNaming
        public async Task CountAllResultsAsync_throws_if_query_is_invalid(string query)
        {
            using (var context = CreateContext())
            {
                var list = new List<Show>();

                for (int i = 1; i <= 10; i++)
                {
                    var show = new Show
                    {
                        Id = i,
                        Name = i % 2 == 0 ? "new show " + i : "old show " + i
                    };

                    list.Add(show);
                }

                context.Shows.AddRange(list);
                await context.SaveChangesAsync().ConfigureAwait(false);

                var repository = new ShowsRepository(context);

                await Assert.ThrowsAsync<InvalidQueryException>(
                    async () => await repository.CountAllResultsAsync(query).ConfigureAwait(false)).ConfigureAwait(false);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task CountByGenreAsync_returns_correct_count()
        {
            using (var context = CreateContext())
            {
                var list = new List<Show>();

                var actionGenre = new Genre("action");
                var dramaGenre = new Genre("drama");

                for (int i = 1; i <= 10; i++)
                {
                    var show = new Show
                    {
                        Id = i
                    };

                    show.ShowsGenres.Add(new ShowsGenres
                    {
                        Genre = i % 2 == 0 ? actionGenre : dramaGenre
                    });

                    list.Add(show);
                }

                context.Shows.AddRange(list);
                await context.SaveChangesAsync().ConfigureAwait(false);

                var repository = new ShowsRepository(context);

                Assert.Equal(5, await repository.CountByGenreAsync("action").ConfigureAwait(false));
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task CountSubscribersAsync_returns_correct_count()
        {
            using (var context = CreateContext())
            {
                var list = new List<Show>();

                for (int i = 1; i <= 10; i++)
                {
                    var show = new Show
                    {
                        Id = i
                    };

                    for (int j = 1; j <= 5; j++)
                    {
                        show.Subscriptions.Add(new Subscription
                        {
                            Profile = new Profile
                            {
                                Id = j
                            }
                        });
                    }

                    list.Add(show);
                }

                context.Shows.AddRange(list);
                await context.SaveChangesAsync().ConfigureAwait(false);

                var repository = new ShowsRepository(context);

                int[] showIds =
                {
                    1,
                    2,
                    3
                };

                var summaries = await repository.CountSubscribersAsync(showIds).ConfigureAwait(false);

                foreach (int showId in showIds)
                {
                    var summary = summaries.Single(s => s.ShowId == showId);

                    Assert.Equal(5, summary.SubscriberCount);
                }
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetShowsByProfileIdAsync_returns_correct_count()
        {
            using (var context = CreateContext())
            {
                var list = new List<Show>();

                for (int i = 1; i <= 10; i++)
                {
                    var show = new Show
                    {
                        Id = i
                    };

                    for (int j = 1; j <= 5; j++)
                    {
                        show.Subscriptions.Add(new Subscription
                        {
                            Profile = new Profile
                            {
                                Id = j
                            }
                        });
                    }

                    list.Add(show);
                }

                context.Shows.AddRange(list);
                await context.SaveChangesAsync().ConfigureAwait(false);

                var repository = new ShowsRepository(context);

                var shows = await repository.GetShowsByProfileIdAsync(1).ConfigureAwait(false);

                Assert.Equal(10, shows.Length);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetShowWithNetworkByIdAsync_returns_show_with_the_network()
        {
            using (var context = CreateContext())
            {
                var list = new List<Show>();

                for (int i = 1; i <= 10; i++)
                {
                    var show = new Show
                    {
                        Id = i,
                        Network = new Network
                        {
                            Id = i
                        }
                    };

                    list.Add(show);
                }

                context.Shows.AddRange(list);
                await context.SaveChangesAsync().ConfigureAwait(false);

                var repository = new ShowsRepository(context);

                var firstShow = await repository.GetShowWithNetworkByIdAsync(1).ConfigureAwait(false);

                Assert.Equal(1, firstShow.Id);
                Assert.NotNull(firstShow.Network);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetTopAsync_returns_the_correct_result()
        {
            using (var context = CreateContext())
            {
                var list = new List<Show>();

                int userCount = 10;

                for (int i = 1; i <= 10; i++)
                {
                    var show = new Show
                    {
                        Id = i
                    };

                    for (int j = userCount - 1; j >= 0; j--)
                    {
                        show.Subscriptions.Add(new Subscription
                        {
                            Profile = new Profile()
                        });
                    }

                    userCount -= 1;

                    list.Add(show);
                }

                context.Shows.AddRange(list);
                await context.SaveChangesAsync().ConfigureAwait(false);

                var repository = new ShowsRepository(context);

                var shows = await repository.GetTopAsync(1, 5).ConfigureAwait(false);

                Assert.True(shows.Select(x => x.Id).SequenceEqual(Enumerable.Range(1, 5)));
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetTopByGenreAsync_returns_the_correct_result()
        {
            using (var context = CreateContext())
            {
                var list = new List<Show>();

                int userCount = 10;

                var actionGenre = new Genre("action");
                var dramaGenre = new Genre("drama");

                for (int i = 1; i <= 10; i++)
                {
                    var show = new Show
                    {
                        Id = i
                    };

                    show.ShowsGenres.Add(new ShowsGenres
                    {
                        Genre = i % 2 == 0 ? actionGenre : dramaGenre
                    });

                    for (int j = userCount - 1; j >= 0; j--)
                    {
                        show.Subscriptions.Add(new Subscription
                        {
                            Profile = new Profile()
                        });
                    }

                    userCount -= 1;

                    list.Add(show);
                }

                context.Shows.AddRange(list);
                await context.SaveChangesAsync().ConfigureAwait(false);

                var repository = new ShowsRepository(context);

                var shows = await repository.GetTopByGenreAsync(dramaGenre.Name, 1, 5).ConfigureAwait(false);

                int[] expectedIds =
                {
                    1,
                    3,
                    5,
                    7,
                    9
                };

                Assert.True(shows.Select(x => x.Id).SequenceEqual(expectedIds));
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task SearchTopAsync_returns_correct_shows()
        {
            using (var context = CreateContext())
            {
                var list = new List<Show>();

                int userCount = 10;

                for (int i = 1; i <= 10; i++)
                {
                    var show = new Show
                    {
                        Id = i,
                        Name = i % 2 == 0 ? "new show " + i : "old show " + i
                    };

                    for (int j = userCount - 1; j >= 0; j--)
                    {
                        show.Subscriptions.Add(new Subscription
                        {
                            Profile = new Profile()
                        });
                    }

                    userCount -= 1;

                    list.Add(show);
                }

                context.Shows.AddRange(list);
                await context.SaveChangesAsync().ConfigureAwait(false);

                var repository = new ShowsRepository(context);

                var shows = await repository.SearchTopAsync("old show", 1, 5).ConfigureAwait(false);

                int[] ids =
                {
                    1,
                    3,
                    5,
                    7,
                    9
                };

                Assert.True(shows.Select(x => x.Id).SequenceEqual(ids));
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("\t")]

        // ReSharper disable once InconsistentNaming
        public async Task SearchTopAsync_throws_if_the_query_is_invalid(string query)
        {
            var repository = new ShowsRepository(null);

            await Assert.ThrowsAsync<InvalidQueryException>(async () => await repository.SearchTopAsync(query, 1, 5).ConfigureAwait(false))
                        .ConfigureAwait(false);
        }
    }
}
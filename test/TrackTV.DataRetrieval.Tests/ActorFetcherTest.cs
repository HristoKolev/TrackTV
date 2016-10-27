namespace TrackTV.DataRetrieval.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using NSubstitute;

    using TrackTV.Data.Repositories.Contracts;
    using TrackTV.DataRetrieval.Fetchers;

    using TrackTv.Models;

    using TvDbSharper.BaseSchemas;
    using TvDbSharper.Clients.Series;

    using Xunit;

    using ActorData = TvDbSharper.Clients.Series.Json.Actor;

    public class ActorFetcherTest
    {
        private const string DefaultLastUpdated = "0001-01-01 00:00:00";

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateActorsAsync_should_add_existing_actors()
        {
            var repository = Substitute.For<IActorsRepository>();
            var client = Substitute.For<ISeriesClient>();

            var response = new TvDbResponse<ActorData[]>();

            var data = new List<ActorData>();

            for (int i = 0; i < 5; i++)
            {
                data.Add(new ActorData
                {
                    Id = i,
                    Name = $"Actor {i}",
                    LastUpdated = $"0001-01-0{i + 1}",
                    Image = $"Image {i}"
                });
            }

            response.Data = data.ToArray();

            var show = new Show
            {
                TheTvDbId = 42
            };

            var actors = response.Data.Select(x => new Actor
                                 {
                                     TheTvDbId = x.Id
                                 }).ToArray();

            RigRepository(repository, actors);
            RigClient(client, show.TheTvDbId, response);

            var fetcher = new ActorFetcher(repository, client);

            await fetcher.PopulateActorsAsync(show);

            var relationships = show.ShowsActors.ToArray();

            Assert.Equal(response.Data.Length, relationships.Length);

            for (int i = 0; i < response.Data.Length; i++)
            {
                Assert.Equal(actors[i], relationships[i].Actor);
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateActorsAsync_should_add_new_actors()
        {
            var repository = Substitute.For<IActorsRepository>();
            var client = Substitute.For<ISeriesClient>();

            var response = new TvDbResponse<ActorData[]>();

            var data = new List<ActorData>();

            for (int i = 0; i < 5; i++)
            {
                data.Add(new ActorData
                {
                    Id = i,
                    Name = $"Actor {i}",
                    LastUpdated = $"0001-01-0{i + 1}",
                    Image = $"Image {i}"
                });
            }

            response.Data = data.ToArray();

            var show = new Show
            {
                TheTvDbId = 42
            };

            RigRepository(repository, Array.Empty<Actor>());
            RigClient(client, show.TheTvDbId, response);

            var fetcher = new ActorFetcher(repository, client);

            await fetcher.PopulateActorsAsync(show);

            var relationships = show.ShowsActors.ToArray();

            Assert.Equal(response.Data.Length, relationships.Length);

            for (int i = 0; i < response.Data.Length; i++)
            {
                Assert.Equal(response.Data[i].Id, relationships[i].Actor.TheTvDbId);
                Assert.Equal(response.Data[i].Name, relationships[i].Actor.Name);
                Assert.Equal(response.Data[i].Image, relationships[i].Actor.Image);
                Assert.Equal(response.Data[i].LastUpdated, relationships[i].Actor.LastUpdated.ToString("yyyy-MM-dd"));
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateActorsAsync_should_get_actors_from_the_client()
        {
            var repository = Substitute.For<IActorsRepository>();
            var client = Substitute.For<ISeriesClient>();

            var expectedIds = new[]
            {
                1,
                2,
                3
            };

            var actors = new Actor[0];

            var show = new Show
            {
                TheTvDbId = 42
            };

            var actorsResponse = new TvDbResponse<ActorData[]>
            {
                Data = new[]
                {
                    new ActorData
                    {
                        Id = 1,
                        LastUpdated = DefaultLastUpdated
                    },
                    new ActorData
                    {
                        Id = 2,
                        LastUpdated = DefaultLastUpdated
                    },
                    new ActorData
                    {
                        Id = 3,
                        LastUpdated = DefaultLastUpdated
                    }
                }
            };

            RigRepository(repository, actors, expectedIds);
            RigClient(client, show.TheTvDbId, actorsResponse);

            var fetcher = new ActorFetcher(repository, client);
            await fetcher.PopulateActorsAsync(show);

            await AssertClientCalledAsync(client, show.TheTvDbId);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateActorsAsync_should_query_actors_from_the_repository()
        {
            var repository = Substitute.For<IActorsRepository>();
            var client = Substitute.For<ISeriesClient>();

            var expectedIds = new[]
            {
                1,
                2,
                3
            };

            var actors = new Actor[0];

            var show = new Show
            {
                TheTvDbId = 42
            };

            var actorsResponse = new TvDbResponse<ActorData[]>
            {
                Data = new[]
                {
                    new ActorData
                    {
                        Id = 1,
                        LastUpdated = DefaultLastUpdated
                    },
                    new ActorData
                    {
                        Id = 2,
                        LastUpdated = DefaultLastUpdated
                    },
                    new ActorData
                    {
                        Id = 3,
                        LastUpdated = DefaultLastUpdated
                    }
                }
            };

            RigRepository(repository, actors, expectedIds);
            RigClient(client, show.TheTvDbId, actorsResponse);

            var fetcher = new ActorFetcher(repository, client);
            await fetcher.PopulateActorsAsync(show);

            await AssertRepositoryCalledAsync(repository, expectedIds);
        }

        private static async Task AssertClientCalledAsync(ISeriesClient client, int theTvDbId)
        {
            await client.Received().GetActorsAsync(theTvDbId);
        }

        private static async Task AssertRepositoryCalledAsync(IActorsRepository repository, int[] expectedIds)
        {
            Expression<Predicate<int[]>> allInIds = x => x.All(expectedIds.Contains);

            await repository.Received().GetActorsByTheTvDbIdsAsync(Arg.Is(allInIds));
        }

        private static void RigClient(ISeriesClient client, int theTvDbId, TvDbResponse<ActorData[]> actorResponse)
        {
            client.GetActorsAsync(theTvDbId).Returns(actorResponse);
        }

        private static void RigRepository(IActorsRepository repository, Actor[] actors, int[] expectedIds)
        {
            Expression<Predicate<int[]>> allInIds = x => x.All(expectedIds.Contains);

            repository.GetActorsByTheTvDbIdsAsync(Arg.Is(allInIds)).Returns(actors);
        }

        private static void RigRepository(IActorsRepository repository, Actor[] actors)
        {
            repository.GetActorsByTheTvDbIdsAsync(Arg.Any<int[]>()).Returns(actors);
        }
    }
}
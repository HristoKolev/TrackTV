namespace TrackTv.DataRetrieval.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using NSubstitute;

    using TrackTv.Data.Models;
    using TrackTv.DataRetrieval.Data;
    using TrackTv.DataRetrieval.Fetchers;

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
                    LastUpdated = $"0001-02-0{i + 1}",
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

            await fetcher.PopulateActorsAsync(show).ConfigureAwait(false);

            var relationships = show.ShowsActors.ToArray();

            Assert.Equal(response.Data.Length, relationships.Length);

            for (int i = 0; i < response.Data.Length; i++)
            {
                Assert.Equal(actors[i], relationships[i].Actor);
                Assert.Equal(response.Data[i].Role, relationships[i].Role);

                Assert.Equal(response.Data[i].Id, actors[i].TheTvDbId);
                Assert.Equal(response.Data[i].Name, actors[i].Name);
                Assert.Equal(response.Data[i].Image, actors[i].Image);
                Assert.Equal(response.Data[i].LastUpdated, actors[i].LastUpdated.ToString("yyyy-MM-dd"));
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateActorsAsync_should_add_existing_and_new_actors()
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
                    Image = $"Image {i}",
                    Role = $"Role {i}"
                });
            }

            response.Data = data.ToArray();

            var show = new Show
            {
                TheTvDbId = 42
            };

            var actors = response.Data.Take(2).Select(x => new Actor
            {
                TheTvDbId = x.Id
            }).ToArray();

            RigRepository(repository, actors);
            RigClient(client, show.TheTvDbId, response);

            var fetcher = new ActorFetcher(repository, client);

            await fetcher.PopulateActorsAsync(show).ConfigureAwait(false);

            var relationships = show.ShowsActors.ToArray();

            Assert.Equal(response.Data.Length, relationships.Length);

            Assert.Equal(actors[0], relationships[0].Actor);
            Assert.Equal(actors[1], relationships[1].Actor);
            Assert.Equal(response.Data[2].Id, relationships[2].Actor.TheTvDbId);
            Assert.Equal(response.Data[3].Id, relationships[3].Actor.TheTvDbId);
            Assert.Equal(response.Data[4].Id, relationships[4].Actor.TheTvDbId);

            for (int i = 0; i < response.Data.Length; i++)
            {
                Assert.Equal(response.Data[i].Role, relationships[i].Role);
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

            await fetcher.PopulateActorsAsync(show).ConfigureAwait(false);

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
            await fetcher.PopulateActorsAsync(show).ConfigureAwait(false);

            await client.Received().GetActorsAsync(show.TheTvDbId).ConfigureAwait(false);
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
            await fetcher.PopulateActorsAsync(show).ConfigureAwait(false);

            await repository.Received().GetActorsByTheTvDbIdsAsync(Arg.Is<int[]>(x => x.All(expectedIds.Contains))).ConfigureAwait(false);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateActorsAsync_should_update_role_on_existing_actor()
        {
            var repository = Substitute.For<IActorsRepository>();
            var client = Substitute.For<ISeriesClient>();

            var response = new TvDbResponse<ActorData[]>
            {
                Data = new[]
                {
                    new ActorData
                    {
                        Id = 1,
                        Name = "Actor 1",
                        LastUpdated = "0001-02-01",
                        Image = "Image 1",
                        Role = "Role 1"
                    }
                }
            };

            var actor = new Actor
            {
                TheTvDbId = 1
            };

            var show = new Show
            {
                TheTvDbId = 42,
                ShowsActors = {
                    new ShowsActors
                    {
                        Actor = actor
                    }
                }
            };

            var actors = new List<Actor>
            {
                actor
            };

            RigRepository(repository, actors.ToArray());
            RigClient(client, show.TheTvDbId, response);

            var fetcher = new ActorFetcher(repository, client);

            await fetcher.PopulateActorsAsync(show).ConfigureAwait(false);

            Assert.Equal(response.Data.First().Role, show.ShowsActors.First().Role);
        }

        private static void RigClient(ISeriesClient client, int theTvDbId, TvDbResponse<ActorData[]> actorResponse)
        {
            client.GetActorsAsync(theTvDbId).Returns(actorResponse);
        }

        private static void RigRepository(IActorsRepository repository, Actor[] actors, int[] expectedIds)
        {
            repository.GetActorsByTheTvDbIdsAsync(Arg.Is<int[]>(x => x.All(expectedIds.Contains))).Returns(actors);
        }

        private static void RigRepository(IActorsRepository repository, Actor[] actors)
        {
            repository.GetActorsByTheTvDbIdsAsync(Arg.Any<int[]>()).Returns(actors);
        }
    }
}
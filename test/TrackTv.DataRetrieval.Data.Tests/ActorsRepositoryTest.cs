namespace TrackTv.DataRetrieval.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TrackTv.Data;
    using TrackTv.Models;

    using Xunit;

    public class ActorsRepositoryTest : BaseRepositoryTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetActorsByTheTvDbIdsAsync_should_return_actors_with_specified_ids()
        {
            using (var context = CreateContext())
            {
                await SeedActorsAsync(context);

                var repository = new ActorsRepository(context);

                int[] ids = {
                    1,
                    2,
                    3
                };

                var actors = await repository.GetActorsByTheTvDbIdsAsync(ids);

                Assert.Equal(ids.Length, actors.Length);

                for (int i = 0; i < actors.Length; i++)
                {
                    Assert.Equal(ids[i], actors[i].TheTvDbId);
                }
            }
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetActorsByTheTvDbIdsAsync_should_return_empty_array_if_ids_is_empty()
        {
            using (var context = CreateContext())
            {
                await SeedActorsAsync(context);

                var repository = new ActorsRepository(context);

                var actors = await repository.GetActorsByTheTvDbIdsAsync(Array.Empty<int>());

                Assert.Equal(0, actors.Length);
            }
        }

        private static async Task SeedActorsAsync(TrackTvDbContext context, IEnumerable<Actor> actors = null)
        {
            if (actors == null)
            {
                var list = new List<Actor>();

                for (int i = 1; i <= 10; i++)
                {
                    list.Add(new Actor
                    {
                        TheTvDbId = i
                    });
                }

                actors = list;
            }

            context.Actors.AddRange(actors);
            await context.SaveChangesAsync();
        }
    }
}
namespace TrackTv.Data.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TrackTV.Data;
    using TrackTV.Data.Repositories;

    using TrackTv.Models;

    using Xunit;

    public class NetworkRepositoryTest : BaseRepositoryTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetNetworkByNameAsync_should_return_network_by_name()
        {
            using (var context = CreateContext())
            {
                await SeedGenresAsync(context);

                var repo = new NetworkRepository(context);

                const string Name = "bbc";

                var network = await repo.GetNetworkByNameAsync(Name);

                Assert.Equal(Name, network.Name);
            }
        }

        private static async Task SeedGenresAsync(TrackTvDbContext context, IEnumerable<Network> networks = null)
        {
            if (networks == null)
            {
                networks = new[]
                {
                    new Network("abc"),
                    new Network("cbs"),
                    new Network("bbc")
                };
            }

            context.Networks.AddRange(networks);
            await context.SaveChangesAsync();
        }
    }
}
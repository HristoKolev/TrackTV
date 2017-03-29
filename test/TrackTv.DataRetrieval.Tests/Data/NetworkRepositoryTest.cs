namespace TrackTv.DataRetrieval.Data.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TrackTv.Data;
    using TrackTv.Data.Models;

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

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task GetNetworkByNameAsync_should_return_null_if_no_network_found()
        {
            using (var context = CreateContext())
            {
                await SeedGenresAsync(context);

                var repo = new NetworkRepository(context);

                const string Name = "bbc1";

                var network = await repo.GetNetworkByNameAsync(Name);

                Assert.Null(network);
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
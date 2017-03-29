namespace TrackTv.DataRetrieval.Tests
{
    using System;
    using System.Threading.Tasks;

    using NSubstitute;

    using TrackTv.Data.Models;
    using TrackTv.Data.Models.Enums;
    using TrackTv.DataRetrieval.Data;
    using TrackTv.DataRetrieval.Fetchers;

    using TvDbSharper.Clients.Series.Json;

    using Xunit;

    public class ShowFetcherTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateShowAsync_should_add_existing_network()
        {
            var repository = Substitute.For<INetworkRepository>();

            var data = new Series
            {
                Network = "bbc"
            };

            var network = new Network(data.Network);

            repository.GetNetworkByNameAsync(data.Network).Returns(network);

            var fetcher = new ShowFetcher(repository);

            var show = new Show();

            await fetcher.PopulateShowAsync(show, data);

            await repository.Received().GetNetworkByNameAsync(data.Network);

            Assert.Equal(network, show.Network);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateShowAsync_should_add_new_network()
        {
            var repository = Substitute.For<INetworkRepository>();

            var data = new Series
            {
                Network = "bbc"
            };

            repository.GetNetworkByNameAsync(data.Network).Returns((Network)null);

            var fetcher = new ShowFetcher(repository);

            var show = new Show();

            await fetcher.PopulateShowAsync(show, data);

            await repository.Received().GetNetworkByNameAsync(data.Network);

            Assert.Equal(data.Network, show.Network.Name);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateShowAsync_should_copy_data_to_the_show()
        {
            var data = new Series
            {
                Network = "BBC One",
                SeriesName = "Doctor Who (2005)",
                AirsTime = "8:30 PM",
                Banner = "graphical/78804-g51.jpg",
                FirstAired = "2005-03-26",
                ImdbId = "tt0436992",
                LastUpdated = 1477651508,
                Overview = "The Doctor is an alien Time Lord... ",
                Status = "Continuing",
                AirsDayOfWeek = "Monday"
            };

            var fetcher = CreateFetcher();

            var show = new Show();

            await fetcher.PopulateShowAsync(show, data);

            Assert.Equal(data.Network, show.Network.Name);
            Assert.Equal(data.SeriesName, show.Name);
            Assert.Equal(data.Banner, show.Banner);
            Assert.Equal(data.Overview, show.Description);
            Assert.Equal(data.ImdbId, show.ImdbId);
            Assert.Equal(ShowStatus.Continuing, show.Status);
            Assert.Equal(AirDay.Monday, show.AirDay);
            Assert.Equal(new DateTime(2016, 10, 28, 10, 45, 8), show.LastUpdated);

            // ReSharper disable once PossibleInvalidOperationException
            Assert.Equal("20:30", show.AirTime.Value.ToString("HH:mm"));

            // ReSharper disable once PossibleInvalidOperationException
            Assert.Equal("2005-03-26", show.FirstAired.Value.ToString("yyy-MM-dd"));
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateShowAsync_should_not_replace_the_network_if_it_has_the_same_name()
        {
            var data = new Series
            {
                Network = "bbc"
            };

            var fetcher = CreateFetcher();

            var network = new Network(data.Network);

            var show = new Show
            {
                Network = network
            };

            await fetcher.PopulateShowAsync(show, data);

            Assert.Equal(network, show.Network);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task PopulateShowAsync_should_replace_existing_network()
        {
            var data = new Series
            {
                Network = "bbc 2"
            };

            var fetcher = CreateFetcher();

            var network = new Network("bbc");

            var show = new Show
            {
                Network = network
            };

            await fetcher.PopulateShowAsync(show, data);

            Assert.Equal(data.Network, show.Network.Name);
        }

        private static ShowFetcher CreateFetcher()
        {
            return new ShowFetcher(Substitute.For<INetworkRepository>());
        }
    }
}
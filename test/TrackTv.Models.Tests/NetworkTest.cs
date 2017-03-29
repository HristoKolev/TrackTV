namespace TrackTv.Models.Tests
{
    using System.Collections.Generic;

    using TrackTv.Data.Models;

    using Xunit;

    public class NetworkTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Constructor_Accepts_The_Correct_Parameters()
        {
            string name = "network name";

            var network = new Network(name);

            Assert.Equal(name, network.Name);
        }

        [Fact]

        // ReSharper disable once InconsistentNaming
        public void Shows_Is_Initialized()
        {
            var network = new Network();

            Assert.NotNull(network.Shows);

            Assert.IsType<List<Show>>(network.Shows);
        }
    }
}
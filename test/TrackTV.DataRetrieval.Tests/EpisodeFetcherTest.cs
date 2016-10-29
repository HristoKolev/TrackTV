namespace TrackTV.DataRetrieval.Tests
{
    using System.Threading.Tasks;

    using NSubstitute;

    using TrackTV.DataRetrieval.ClientExtensions;
    using TrackTV.DataRetrieval.Fetchers;

    using TrackTv.Models;

    using TvDbSharper.Clients.Episodes;

    using Xunit;

    public class EpisodeFetcherTest
    {
        [Fact]

        // ReSharper disable once InconsistentNaming
        public async Task AddAllEpisodesAsync_should_call_the_client()
        {
            var episodesClient = Substitute.For<IEpisodesClient>();
            var advancedEpisodesClient = Substitute.For<IAdvancedEpisodeClient>();
            var advancedSeriesClient = Substitute.For<IAdvancedSeriesClient>();

            var fetcher = new EpisodeFetcher(episodesClient, advancedEpisodesClient, advancedSeriesClient);

            await fetcher.AddAllEpisodesAsync(new Show());
        }
    }
}
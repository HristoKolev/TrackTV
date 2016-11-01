namespace TrackTV.DataRetrieval.ClientExtensions
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TvDbSharper.BaseSchemas;
    using TvDbSharper.Clients.Series;
    using TvDbSharper.Clients.Series.Json;

    public class AdvancedSeriesClient : IAdvancedSeriesClient
    {
        public AdvancedSeriesClient(ISeriesClient client)
        {
            this.Client = client;
        }

        private ISeriesClient Client { get; }

        public async Task<IEnumerable<BasicEpisode>> GetBasicEpisodesAsync(int seriesId)
        {
            var tasks = new List<Task<TvDbResponse<BasicEpisode[]>>>();

            var firstResponse = await this.Client.GetEpisodesAsync(seriesId, 1);

            for (int i = 2; i <= firstResponse.Links.Last; i++)
            {
                tasks.Add(this.Client.GetEpisodesAsync(seriesId, i));
            }

            var results = await Task.WhenAll(tasks);

            var episodes = firstResponse.Data.Concat(results.SelectMany(x => x.Data));

            return episodes;
        }
    }
}
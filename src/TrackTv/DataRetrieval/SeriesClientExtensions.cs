namespace TrackTv.DataRetrieval
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TvDbSharper.BaseSchemas;
    using TvDbSharper.Clients.Series;
    using TvDbSharper.Clients.Series.Json;

    public static class SeriesClientExtensions
    {
        public static async Task<IEnumerable<BasicEpisode>> GetBasicEpisodesAsync(this ISeriesClient client, int seriesId)
        {
            var tasks = new List<Task<TvDbResponse<BasicEpisode[]>>>();

            var firstResponse = await client.GetEpisodesAsync(seriesId, 1);

            for (int i = 2; i <= firstResponse.Links.Last; i++)
            {
                tasks.Add(client.GetEpisodesAsync(seriesId, i));
            }

            var results = await Task.WhenAll(tasks);

            var episodes = firstResponse.Data.Concat(results.SelectMany(x => x.Data));

            return episodes;
        }
    }
}
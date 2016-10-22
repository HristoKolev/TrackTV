namespace TrackTv.DataRetrieval
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TvDbSharper.BaseSchemas;
    using TvDbSharper.Clients.Series;
    using TvDbSharper.Clients.Series.Json;

    public static class SeriesClientExtensions
    {
        public static async Task<List<BasicEpisode>> GetBasicEpisodesAsync(this ISeriesClient client, int seriesId)
        {
            var tasks = new List<Task<TvDbResponse<BasicEpisode[]>>>();

            var firstResponse = await client.GetEpisodesAsync(seriesId, 1);

            for (int i = 2; i <= firstResponse.Links.Last; i++)
            {
                tasks.Add(client.GetEpisodesAsync(seriesId, i));
            }

            // ReSharper disable once CoVariantArrayConversion
            Task.WaitAll(tasks.ToArray());

            var basicEpisodes = new List<BasicEpisode>(firstResponse.Data);

            foreach (var task in tasks)
            {
                basicEpisodes.AddRange((await task).Data);
            }

            return basicEpisodes;
        }
    }
}
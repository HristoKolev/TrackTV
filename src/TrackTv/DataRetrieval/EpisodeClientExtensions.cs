namespace TrackTv.DataRetrieval
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TvDbSharper.BaseSchemas;
    using TvDbSharper.Clients.Episodes;
    using TvDbSharper.Clients.Episodes.Json;

    public static class EpisodeClientExtensions
    {
        public static async Task<List<EpisodeRecord>> GetFullEpisodesAsync(this IEpisodesClient client, int[] ids)
        {
            var tasks = new List<Task<TvDbResponse<EpisodeRecord>>>();

            foreach (int id in ids)
            {
                tasks.Add(client.GetAsync(id));
            }

            // ReSharper disable once CoVariantArrayConversion
            Task.WaitAll(tasks.ToArray());

            var episodes = new List<EpisodeRecord>();

            foreach (var task in tasks)
            {
                episodes.Add((await task).Data);
            }

            return episodes;
        }
    }
}
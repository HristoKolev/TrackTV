namespace TrackTv.Updater
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TvDbSharper;
    using TvDbSharper.Dto;

    public static class UpdatesClientExtensions
    {
        private static readonly TimeSpan MaxRangeLength = TimeSpan.FromDays(7);

        public static async Task<TvDbResponse<Update[]>> GetAccumulatedAsync(this IUpdatesClient client, DateTime fromTime, DateTime toTime)
        {
            if (fromTime > toTime)
            {
                throw new NotSupportedException($"The {nameof(fromTime)} is past the {nameof(toTime)}");
            }

            if (toTime - fromTime <= MaxRangeLength)
            {
                return await client.GetAsync(fromTime, toTime).ConfigureAwait(false);
            }

            var ranges = BreakDownRanges(fromTime, toTime);

            var responses = await GetResponsesAsync(client, ranges).ConfigureAwait(false);

            var updates = FilterResults(responses);

            return new TvDbResponse<Update[]>
            {
                Data = updates
            };
        }

        private static IDictionary<DateTime, DateTime> BreakDownRanges(DateTime fromTime, DateTime toTime)
        {
            var ranges = new Dictionary<DateTime, DateTime>();

            while (toTime - fromTime > MaxRangeLength)
            {
                ranges.Add(fromTime, fromTime.Add(MaxRangeLength));

                fromTime = fromTime.Add(MaxRangeLength);
            }

            if (fromTime != toTime)
            {
                ranges.Add(fromTime, toTime);
            }

            return ranges;
        }

        private static Update[] FilterResults(IEnumerable<TvDbResponse<Update[]>> responses)
        {
            var results = new Dictionary<int, Update>();

            foreach (var update in responses.Where(r => r.Data != null).SelectMany(x => x.Data))
            {
                if (!results.ContainsKey(update.Id) || update.LastUpdated > results[update.Id].LastUpdated)
                {
                    results[update.Id] = update;
                }
            }

            return results.Select(x => x.Value).OrderBy(x => x.LastUpdated).ToArray();
        }

        private static Task<TvDbResponse<Update[]>[]> GetResponsesAsync(IUpdatesClient client, IDictionary<DateTime, DateTime> ranges)
        {
            return Task.WhenAll(ranges.Select(range => client.GetAsync(range.Key, range.Value)));
        }
    }

    public static class EpisodeClientExtensions
    {
        public static async Task<List<EpisodeRecord>> GetFullEpisodesAsync(this IEpisodesClient episodesClient, IEnumerable<int> ids)
        {
            var episodes = await Task.WhenAll(ids.Select(episodesClient.GetAsync)).ConfigureAwait(false);

            return episodes.Select(x => x.Data).ToList();
        }
    }

    public static class SeriesClientExtensions
    {
        public static async Task<List<BasicEpisode>> GetBasicEpisodesAsync(this ISeriesClient client, int seriesId)
        {
            try
            {
                var tasks = new List<Task<TvDbResponse<BasicEpisode[]>>>();

                var firstResponse = await client.GetEpisodesAsync(seriesId, 1).ConfigureAwait(false);

                for (int i = 2; i <= firstResponse.Links.Last; i++)
                {
                    tasks.Add(client.GetEpisodesAsync(seriesId, i));
                }

                var results = await Task.WhenAll(tasks).ConfigureAwait(false);

                var episodes = firstResponse.Data.Concat(results.SelectMany(x => x.Data)).ToList();

                return episodes;
            }
            catch (TvDbServerException ex) when (ex.StatusCode == 404)
            {
                return new List<BasicEpisode>();
            }
        }
        
        public static async Task<EpisodeRecord> GetExternalEpisodeAsync(this IEpisodesClient client, int updateId)
        {
            if (updateId == 0)
            {
                return null;
            }

            try
            {
                var response = await client.GetAsync(updateId).ConfigureAwait(false);

                return response?.Data;
            }
            catch (TvDbServerException ex)
            {
                if (ex.StatusCode == 404)
                {
                    return null;
                }

                throw;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public static async Task<Series> GetExternalShowAsync(this ISeriesClient client, int updateId)
        {
            if (updateId == 0)
            {
                return null;
            }

            try
            {
                var response = await client.GetAsync(updateId).ConfigureAwait(false);

                return response?.Data;
            }
            catch (TvDbServerException ex)
            {
                if (ex.StatusCode == 404)
                {
                    return null;
                }

                throw;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }
    }
}
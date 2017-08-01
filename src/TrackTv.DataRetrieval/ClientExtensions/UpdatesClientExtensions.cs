namespace TrackTv.DataRetrieval.ClientExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TvDbSharper;
    using TvDbSharper.Dto;

    public static class UpdatesClientExtensions
    {
        private static readonly TimeSpan MaxRangeLength = new TimeSpan(7, 0, 0, 0);

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

            foreach (var update in responses.SelectMany(x => x.Data))
            {
                if (!results.ContainsKey(update.Id) || update.LastUpdated > results[update.Id].LastUpdated)
                {
                    results[update.Id] = update;
                }
            }

            return results.Select(x => x.Value).ToArray();
        }

        private static Task<TvDbResponse<Update[]>[]> GetResponsesAsync(IUpdatesClient client, IDictionary<DateTime, DateTime> ranges)
        {
            return Task.WhenAll(ranges.Select(range => client.GetAsync(range.Key, range.Value)));
        }
    }
}
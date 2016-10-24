namespace TrackTV.DataRetrieval.ClientExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using TvDbSharper.BaseSchemas;
    using TvDbSharper.Clients.Updates;
    using TvDbSharper.Clients.Updates.Json;

    public static class UpdatesClientExtensions
    {
        public static async Task<TvDbResponse<Update[]>> GetAccumulatedAsync(this IUpdatesClient client, DateTime fromTime, DateTime toTime)
        {
            if (fromTime > toTime)
            {
                throw new NotSupportedException($"The {nameof(fromTime)} is past the {nameof(toTime)}");
            }

            var maxRangeLength = new TimeSpan(7, 0, 0, 0);

            if (toTime - fromTime <= maxRangeLength)
            {
                return await client.GetAsync(fromTime, toTime);
            }

            var ranges = BreakDownRanges(fromTime, toTime, maxRangeLength);

            var responses = await GetResponsesAsync(client, ranges);

            var updates = FilterResults(responses);

            return new TvDbResponse<Update[]>
            {
                Data = updates
            };
        }

        private static Update[] FilterResults(IEnumerable<TvDbResponse<Update[]>> responses)
        {
            var results = new Dictionary<int, Update>();

            foreach (var update in responses.SelectMany(x => x.Data))
            {
                if (!results.ContainsKey(update.Id) || (update.LastUpdated > results[update.Id].LastUpdated))
                {
                    results[update.Id] = update;
                }
            }

            return results.Select(x => x.Value).ToArray();
        }

        private static Dictionary<DateTime, DateTime> BreakDownRanges(DateTime fromTime, DateTime toTime, TimeSpan maxRangeLength)
        {
            var ranges = new Dictionary<DateTime, DateTime>();

            while (toTime - fromTime > maxRangeLength)
            {
                ranges.Add(fromTime, fromTime.Add(maxRangeLength));

                fromTime = fromTime.Add(maxRangeLength);
            }

            if (fromTime != toTime)
            {
                ranges.Add(fromTime, toTime);
            }

            return ranges;
        }

        private static async Task<TvDbResponse<Update[]>[]> GetResponsesAsync(IUpdatesClient client, Dictionary<DateTime, DateTime> ranges)
        {
            var tasks = new List<Task<TvDbResponse<Update[]>>>();

            foreach (var range in ranges)
            {
                tasks.Add(client.GetAsync(range.Key, range.Value));
            }

            return await Task.WhenAll(tasks);
        }
    }
}
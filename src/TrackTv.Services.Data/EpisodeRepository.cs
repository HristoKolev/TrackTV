namespace TrackTv.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Models;
    using TrackTv.Services.Data.Models;

    public class EpisodeRepository : IEpisodeRepository
    {
        public EpisodeRepository(ICoreDataStore dataStore)
        {
            this.DataStore = dataStore;
        }

        private ICoreDataStore DataStore { get; }

        public Task<EpisodesSummary[]> GetEpisodesSummariesAsync(int[] ids, DateTime time)
        {
            return this.DataStore.Shows.Where(x => ids.Contains(x.Id)).Select(s => new EpisodesSummary
                       {
                           ShowId = s.Id,
                           LastEpisode =
                               s.Episodes.Where(e => (e.FirstAired != null) && (e.FirstAired <= time))
                                .OrderByDescending(e => e.FirstAired)
                                .FirstOrDefault(),
                           NextEpisode =
                               s.Episodes.Where(e => (e.FirstAired != null) && (e.FirstAired > time))
                                .OrderBy(e => e.FirstAired)
                                .FirstOrDefault()
                       }).ToArrayAsync();
        }

        public Task<Episode[]> GetMonthlyEpisodesAsync(int userId, DateTime startDay, DateTime endDay)
        {
            return
                this.DataStore.Episodes.Include(e => e.Show)
                    .Where(e => e.Show.ShowsUsers.Any(x => x.UserId == userId))
                    .Where(episode => (episode.FirstAired > startDay) && (episode.FirstAired < endDay))
                    .ToArrayAsync();
        }
    }
}
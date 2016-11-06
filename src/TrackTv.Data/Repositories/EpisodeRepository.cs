namespace TrackTV.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTV.Data.Repositories.Contracts;

    using TrackTv.Data.Repositories.Models;
    using TrackTv.Models;

    public class EpisodeRepository : IEpisodeRepository
    {
        public EpisodeRepository(ICoreDataStore dataStore)
        {
            this.DataStore = dataStore;
        }

        private ICoreDataStore DataStore { get; }

        public Task<Episode> GetEpisodeById(int id)
        {
            return this.DataStore.Episodes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Episode>> GetEpisodesByShowIdAsync(int id)
        {
            return this.DataStore.Episodes.Where(x => x.ShowId == id).ToListAsync();
        }

        public Task<List<Episode>> GetEpisodesByTheTvDbIdsAsync(int[] ids)
        {
            return this.DataStore.Episodes.Where(x => ids.Contains(x.TheTvDbId)).ToListAsync();
        }

        public Task<EpisodesSummary[]> GetEpisodesSummariesAsync(int[] ids, DateTime time)
        {
            var summariesQuery = this.DataStore.Shows.Where(x => ids.Contains(x.Id)).Select(s => new EpisodesSummary
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
                                     });

            return summariesQuery.ToArrayAsync();
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
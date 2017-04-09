namespace TrackTv.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Data.Models;
    using TrackTv.Services.Data.Models;

    public class EpisodeRepository : IEpisodeRepository
    {
        public EpisodeRepository(TrackTvDbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        private TrackTvDbContext DbContext { get; }

        public Task<EpisodesSummary[]> GetEpisodesSummariesAsync(int[] showIds, DateTime time)
        {
            return this.DbContext.Shows.Where(x => showIds.Contains(x.Id)).Select(s => new EpisodesSummary
            {
                ShowId = s.Id,
                LastEpisode =
                    s.Episodes.Where(e => e.FirstAired != null && e.FirstAired <= time)
                     .OrderByDescending(e => e.FirstAired)
                     .FirstOrDefault(),
                NextEpisode = s.Episodes.Where(e => e.FirstAired != null && e.FirstAired > time).OrderBy(e => e.FirstAired).FirstOrDefault()
            }).ToArrayAsync();
        }

        public Task<Episode[]> GetMonthlyEpisodesAsync(int profileId, DateTime startDay, DateTime endDay)
        {
            return
                this.DbContext.Episodes.AsNoTracking()
                    .Include(e => e.Show)
                    .Where(e => e.Show.Subscriptions.Any(x => x.ProfileId == profileId))
                    .Where(episode => episode.FirstAired > startDay && episode.FirstAired < endDay)
                    .ToArrayAsync();
        }
    }
}
namespace TrackTv.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Data;
    using TrackTv.Data.Models;
    using TrackTv.Services.MyShows.Models;

    public class EpisodeRepository : IEpisodeRepository
    {
        public EpisodeRepository(TrackTvDbContext context)
        {
            this.Context = context;
        }

        private TrackTvDbContext Context { get; }

        public async Task<MyShow[]> GetEpisodesSummariesAsync(int[] showIds, DateTime time)
        {
            const string Query = @"
                SELECT s1.*, lastEpisode.* , nextEpisode.* from Shows s1 JOIN
                  (SELECT s.ShowId as ShowId,
                      (SELECT e1.EpisodeId from Episodes e1
                        WHERE e1.ShowId = s.ShowId and e1.SeasonNumber != 0 and e1.FirstAired is not NULL
                        AND e1.FirstAired <= @time
                        ORDER BY e1.FirstAired DESC LIMIT 1) as LastEpisodeId,

                      (SELECT e2.EpisodeId from Episodes e2
                        WHERE e2.ShowId = s.ShowId and e2.SeasonNumber != 0 and  e2.FirstAired is not NULL
                        AND e2.FirstAired > @time
                        ORDER BY e2.FirstAired LIMIT 1) as NextEpisodeId
                   from Shows s) sub on s1.ShowId = sub.ShowId
                LEFT JOIN Episodes nextEpisode on nextEpisode.EpisodeId = sub.NextEpisodeId
                LEFT JOIN Episodes lastEpisode on lastEpisode.EpisodeId = sub.LastEpisodeId

                WHERE s1.ShowId in @showIds";

            var parameters = new
            {
                showIds,
                time
            };

            var shows = await this.Context.Database.GetDbConnection()
                                  .QueryAsync<MyShow, MyEpisode, MyEpisode, MyShow>(Query, Map, parameters,
                                      splitOn: "ShowId,EpisodeId,EpisodeId");

            return shows.ToArray();
        }

        public Task<Episode[]> GetMonthlyEpisodesAsync(int profileId, DateTime startDay, DateTime endDay)
        {
            return this.Context.Episodes.AsNoTracking().Include(e => e.Show)
                       .Where(e => e.Show.Subscriptions.Any(x => x.ProfileId == profileId))
                       .Where(episode => episode.FirstAired > startDay && episode.FirstAired < endDay).ToArrayAsync();
        }

        private static MyShow Map(MyShow show, MyEpisode lastEpisode, MyEpisode nextEpisode)
        {
            show.LastEpisode = lastEpisode;
            show.NextEpisode = nextEpisode;

            return show;
        }
    }
}
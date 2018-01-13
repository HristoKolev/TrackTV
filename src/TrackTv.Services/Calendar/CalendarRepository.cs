namespace TrackTv.Services.Calendar
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;

    public class CalendarRepository
    {
        public CalendarRepository(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public async Task<CalendarEpisode[]> GetMonthlyEpisodesAsync(int profileId, DateTime startDay, DateTime endDay)
        {
            var episodes = await (from profile in this.DbService.Profiles
                                  join subscription in this.DbService.Subscriptions on profile.ProfileId equals subscription.ProfileId
                                  join show in this.DbService.Shows on subscription.ShowId equals show.ShowId
                                  join episode in this.DbService.Episodes on show.ShowId equals episode.ShowId
                                  where profile.ProfileId == profileId && episode.FirstAired > startDay && episode.FirstAired < endDay
                                  select new CalendarEpisode
                                  {
                                      FirstAired = episode.FirstAired,
                                      EpisodeTitle = episode.EpisodeTitle,
                                      EpisodeNumber = episode.EpisodeNumber,
                                      SeasonNumber = episode.SeasonNumber,
                                      ShowId = episode.ShowId,
                                      ShowName = show.ShowName
                                  }).ToArrayAsync()
                                    .ConfigureAwait(false);

            return episodes;
        }
    }
}
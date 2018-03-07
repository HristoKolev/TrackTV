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
                                  join subscription in this.DbService.Subscriptions on profile.ProfileID equals subscription.ProfileID
                                  join show in this.DbService.Shows on subscription.ShowID equals show.ShowID
                                  join episode in this.DbService.Episodes on show.ShowID equals episode.ShowID
                                  where profile.ProfileID == profileId && episode.FirstAired > startDay && episode.FirstAired < endDay
                                  select new CalendarEpisode
                                  {
                                      FirstAired = episode.FirstAired,
                                      EpisodeTitle = episode.EpisodeTitle,
                                      EpisodeNumber = episode.EpisodeNumber,
                                      SeasonNumber = episode.SeasonNumber,
                                      ShowId = episode.ShowID,
                                      ShowName = show.ShowName
                                  }).ToArrayAsync()
                                    .ConfigureAwait(false);

            return episodes;
        }
    }
}
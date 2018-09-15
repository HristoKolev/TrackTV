namespace TrackTv.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using LinqToDB;

    using TrackTv.Data;
    using TrackTv.Services.Data;
    using TrackTv.Services.Exceptions;

    public class CalendarService
    {
        private const int CalendarLength = 7 * NumberOfWeeks;

        private const int NumberOfWeeks = 6;

        public CalendarService(IDbService dbService, ProfilesRepository profilesRepository)
        {
            this.DbService = dbService;
            this.ProfilesRepository = profilesRepository;
        }

        private IDbService DbService { get; }

        private ProfilesRepository ProfilesRepository { get; }

        public async Task<CalendarDay[][]> GetCalendarAsync(int profileId, DateTime time, DateTime today)
        {
            if (!await this.ProfilesRepository.ProfileExistsAsync(profileId))
            {
                throw new ProfileNotFoundException(profileId);
            }

            return await this.CreateAsync(profileId, time, today);
        }

        private static DayOfWeek GetDayOfWeek(DateTime day) => new GregorianCalendar().GetDayOfWeek(day);

        private static DateTime GetStartDate(DateTime currentDate)
        {
            var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

            // days to subtract to get to the start of the week
            int weekDays = (int)GetDayOfWeek(startOfMonth) - (int)DayOfWeek.Monday;

            var startOfWeek = startOfMonth.Subtract(new TimeSpan(weekDays, 0, 0, 0));

            return startOfWeek;
        }

        private async Task<CalendarDay[][]> CreateAsync(int profileId, DateTime currentDate, DateTime today)
        {
            var startDate = GetStartDate(currentDate.Date);

            var endDate = startDate.AddDays(CalendarLength);

            var monthlyEpisodes = await this.GetMonthlyEpisodesAsync(profileId, startDate, endDate);

            var month = new ICollection<CalendarDay>[NumberOfWeeks];

            int weekIndex = 0;

            for (var day = startDate; day < endDate; day = day.AddDays(1))
            {
                if (month[weekIndex] == null)
                {
                    month[weekIndex] = new List<CalendarDay>();
                }

                month[weekIndex]
                    .Add(new CalendarDay
                    {
                        Date = day,
                        Episodes = monthlyEpisodes.Where(e => e.FirstAired?.Date == day).ToArray(),
                        IsToday = day.Date == today.Date
                    });

                if (GetDayOfWeek(day) == DayOfWeek.Sunday)
                {
                    weekIndex++;
                }
            }

            return month.Select(x => x.ToArray()).ToArray();
        }

        private async Task<CalendarEpisode[]> GetMonthlyEpisodesAsync(int profileId, DateTime startDay, DateTime endDay)
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
                                    ;

            return episodes;
        }
    }

    public class CalendarDay
    {
        public DateTime Date { get; set; }

        public CalendarEpisode[] Episodes { get; set; }

        public bool IsToday { get; set; }
    }

    public class CalendarEpisode
    {
        public int EpisodeNumber { get; set; }

        public string EpisodeTitle { get; set; }

        public DateTime? FirstAired { get; set; }

        public int SeasonNumber { get; set; }

        public int ShowId { get; set; }

        public string ShowName { get; set; }
    }
}
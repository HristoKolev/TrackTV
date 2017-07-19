namespace TrackTv.Services.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    using TrackTv.Data.Models;
    using TrackTv.Services.Calendar.Models;
    using TrackTv.Services.Data;

    public class EpisodeCalendar : IEpisodeCalendar
    {
        private const int CalendarLength = 7 * NumberOfWeeks;

        private const int NumberOfWeeks = 6;

        public EpisodeCalendar(IEpisodeRepository episodeRepository, Calendar calendar)
        {
            this.EpisodeRepository = episodeRepository;

            this.Calendar = calendar;
        }

        private Calendar Calendar { get; }

        private IEpisodeRepository EpisodeRepository { get; }

        public async Task<CalendarDay[][]> CreateAsync(int profileId, DateTime currentDate)
        {
            var startDate = this.GetStartDate(currentDate);

            var endDate = startDate.AddDays(CalendarLength);

            var monthlyEpisodes = await this.GetMonthlyEpisodesAsync(profileId, startDate, endDate)
                                            .ConfigureAwait(false);

            var month = ConstructMonth();

            int weekIndex = 0;

            for (var day = startDate; day < endDate; day = day.AddDays(1))
            {
                AddEpisodes(month[weekIndex], monthlyEpisodes, day);

                if (this.Calendar.GetDayOfWeek(day) == DayOfWeek.Sunday)
                {
                    weekIndex++;
                }
            }

            return month.Select(x => x.ToArray()).ToArray();
        }

        private static void AddEpisodes(ICollection<CalendarDay> week, IEnumerable<CalendarEpisode> allEpisodes, DateTime currentDay)
        {
            var dailyEpisodes = allEpisodes
                .Where(e => e.FirstAired != null && e.FirstAired.Value >= currentDay && e.FirstAired < currentDay.AddDays(1));

            week.Add(new CalendarDay
            {
                Date = currentDay,
                Episodes = dailyEpisodes.ToArray()
            });
        }

        private static ICollection<CalendarDay>[] ConstructMonth()
        {
            var model = new ICollection<CalendarDay>[NumberOfWeeks];

            for (int i = 0; i < NumberOfWeeks; i++)
            {
                model[i] = new List<CalendarDay>();
            }

            return model;
        }

        private static CalendarEpisode MapToModel(Episode episode)
        {
            return new CalendarEpisode
            {
                FirstAired = episode.FirstAired,
                Title = episode.Title,
                Number = episode.Number,
                SeasonNumber = episode.SeasonNumber,
                ShowId = episode.ShowId,
                ShowName = episode.Show.Name
            };
        }

        private async Task<CalendarEpisode[]> GetMonthlyEpisodesAsync(int profileId, DateTime startDay, DateTime endDay)
        {
            var episodes = await this.EpisodeRepository
                                     .GetMonthlyEpisodesAsync(profileId, startDay, endDay)
                                     .ConfigureAwait(false);

            return episodes.Select(MapToModel).ToArray();
        }

        private DateTime GetStartDate(DateTime currentDate)
        {
            var startDate = new DateTime(currentDate.Year, currentDate.Month, 1);

            int offsetDays = (int)this.Calendar.GetDayOfWeek(startDate) - (int)DayOfWeek.Monday;

            startDate = startDate.Subtract(new TimeSpan(offsetDays, 0, 0, 0));

            return startDate;
        }
    }
}
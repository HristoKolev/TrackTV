namespace TrackTv.Services.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    public class EpisodeCalendarCalculator
    {
        private const int CalendarLength = 7 * NumberOfWeeks;

        private const int NumberOfWeeks = 6;

        public EpisodeCalendarCalculator(CalendarRepository calendarRepository)
        {
            this.CalendarRepository = calendarRepository;
        }

        private CalendarRepository CalendarRepository { get; }

        public async Task<CalendarDay[][]> CreateAsync(int profileId, DateTime currentDate, DateTime today)
        {
            var startDate = GetStartDate(currentDate.Date);

            var endDate = startDate.AddDays(CalendarLength);

            var monthlyEpisodes =
                await this.CalendarRepository.GetMonthlyEpisodesAsync(profileId, startDate, endDate).ConfigureAwait(false);

            var month = new ICollection<CalendarDay>[NumberOfWeeks];

            int weekIndex = 0;

            for (var day = startDate; day < endDate; day = day.AddDays(1))
            {
                if (month[weekIndex] == null)
                {
                    month[weekIndex] = new List<CalendarDay>();
                }

                month[weekIndex].Add(new CalendarDay
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

        private static DayOfWeek GetDayOfWeek(DateTime day) => new GregorianCalendar().GetDayOfWeek(day);

        private static DateTime GetStartDate(DateTime currentDate)
        {
            var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);

            // days to subtract to get to the start of the week
            int weekDays = (int)GetDayOfWeek(startOfMonth) - (int)DayOfWeek.Monday;

            var startOfWeek = startOfMonth.Subtract(new TimeSpan(weekDays, 0, 0, 0));

            return startOfWeek;
        }
    }
}
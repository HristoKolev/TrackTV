namespace TrackTv.Services.Calendar
{
    using System;
    using System.Threading.Tasks;

    using TrackTv.Services.Calendar.Models;

    public class CalendarService
    {
        public CalendarService(IEpisodeCalendar episodeCalendar)
        {
            this.EpisodeCalendar = episodeCalendar;
        }

        private IEpisodeCalendar EpisodeCalendar { get; }

        public Task<CalendarDay[][]> GetCalendarAsync(int userId)
        {
            var now = DateTime.UtcNow;

            return this.EpisodeCalendar.CreateAsync(userId, now);
        }
    }
}
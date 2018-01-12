namespace TrackTv.Services.Calendar
{
    using System;

    public class CalendarDay
    {
        public DateTime Date { get; set; }

        public CalendarEpisode[] Episodes { get; set; }

        public bool IsToday { get; set; }
    }
}
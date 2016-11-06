namespace TrackTv.Services.Calendar.Models
{
    using System;

    public class CalendarDay
    {
        public DateTime Date { get; set; }

        public CalendarEpisode[] Episodes { get; set; }
    }
}
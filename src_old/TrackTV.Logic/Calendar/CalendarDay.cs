namespace TrackTV.Logic.Calendar
{
    using System;
    using System.Collections.Generic;

    public class CalendarDay
    {
        public CalendarDay(DateTime date)
        {
            this.Date = date;
        }

        public CalendarDay()
        {
        }

        public DateTime Date { get; set; }

        public ICollection<CalendarEpisode> Episodes { get; set; }
    }
}
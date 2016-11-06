namespace TrackTv.Services.Calendar.Models
{
    using System;

    public class CalendarEpisode
    {
        public DateTime? FirstAired { get; set; }

        public int Number { get; set; }

        public int SeasonNumber { get; set; }

        public int ShowId { get; set; }

        public string ShowName { get; set; }

        public string Title { get; set; }
    }
}
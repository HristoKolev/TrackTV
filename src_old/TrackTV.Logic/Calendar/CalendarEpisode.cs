namespace TrackTV.Logic.Calendar
{
    using System;

    public class CalendarEpisode
    {
        public DateTime? FirstAired { get; set; }

        public int Number { get; set; }

        public int SeasonNumber { get; set; }

        public string ShowName { get; set; }

        public string Title { get; set; }

        public string UserFriendlyId { get; set; }
    }
}
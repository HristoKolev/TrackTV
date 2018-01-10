namespace TrackTv.Services.Calendar
{
    using System;

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
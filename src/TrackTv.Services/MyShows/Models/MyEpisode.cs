namespace TrackTv.Services.MyShows.Models
{
    using System;

    public class MyEpisode
    {
        public DateTime? FirstAired { get; set; }

        public int EpisodeNumber { get; set; }

        public int SeasonNumber { get; set; }

        public string EpisodeTitle { get; set; }
    }
}
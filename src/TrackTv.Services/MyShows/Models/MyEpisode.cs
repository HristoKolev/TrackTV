namespace TrackTv.Services.MyShows.Models
{
    using System;

    public class MyEpisode
    {
        public int EpisodeId { get; set; }

        public int EpisodeNumber { get; set; }

        public string EpisodeTitle { get; set; }

        public DateTime? FirstAired { get; set; }

        public int SeasonNumber { get; set; }
    }
}
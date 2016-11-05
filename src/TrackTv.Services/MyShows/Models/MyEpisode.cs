namespace TrackTv.Services.MyShows.Models
{
    using System;

    public class MyEpisode
    {
        public DateTime? FirstAired { get; set; }

        public int Number { get; set; }

        public int SeasonNumber { get; set; }

        public string Title { get; set; }
    }
}
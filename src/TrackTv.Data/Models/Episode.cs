namespace TrackTv.Data.Models
{
    using System;

    using TrackTv.Data.Models.Contracts;

    public class Episode : ITvDbRecord
    {
        public string EpisodeDescription { get; set; }

        public DateTime? FirstAired { get; set; }

        public int EpisodeId { get; set; }

        public string ImdbId { get; set; }

        public DateTime LastUpdated { get; set; }

        public int EpisodeNumber { get; set; }

        public int SeasonNumber { get; set; }

        public virtual Show Show { get; set; }

        public int ShowId { get; set; }

        public int TheTvDbId { get; set; }

        public string EpisodeTitle { get; set; }
    }
}
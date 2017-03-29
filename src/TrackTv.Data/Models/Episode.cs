﻿namespace TrackTv.Data.Models
{
    using System;

    using TrackTv.Data.Models.Contracts;

    public class Episode : ITvDbRecord
    {
        public string Description { get; set; }

        public DateTime? FirstAired { get; set; }

        public int Id { get; set; }

        public string ImdbId { get; set; }

        public DateTime LastUpdated { get; set; }

        public int Number { get; set; }

        public int SeasonNumber { get; set; }

        public virtual Show Show { get; set; }

        public int ShowId { get; set; }

        public int TheTvDbId { get; set; }

        public string Title { get; set; }
    }
}
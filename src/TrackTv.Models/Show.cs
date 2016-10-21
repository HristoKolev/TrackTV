namespace TrackTv.Models
{
    using System;
    using System.Collections.Generic;

    using TrackTv.Models.Contracts;
    using TrackTv.Models.Enums;
    using TrackTv.Models.Joint;

    public class Show : ITvDbRecord
    {
        public AirDay? AirDay { get; set; }

        public TimeSpan? AirTime { get; set; }

        public string Banner { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Episode> Episodes { get; } = new List<Episode>();

        public DateTime? FirstAired { get; set; }

        public int Id { get; set; }

        public string ImdbId { get; set; }

        public string Language { get; set; }

        public long LastUpdated { get; set; }

        public string Name { get; set; }

        public virtual Network Network { get; set; }

        public virtual int NetworkId { get; set; }

        public virtual ICollection<ShowsActors> ShowsActors { get; } = new List<ShowsActors>();

        public virtual ICollection<ShowsGenres> ShowsGenres { get; } = new List<ShowsGenres>();

        public virtual ICollection<ShowsUsers> ShowsUsers { get; } = new List<ShowsUsers>();

        public int TvDbId { get; set; }
    }
}
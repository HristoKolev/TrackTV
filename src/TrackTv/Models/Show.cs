namespace TrackTv.Models
{
    using System;
    using System.Collections.Generic;

    using TrackTv.Models.Contracts;
    using TrackTv.Models.Enums;

    public class Show : ITvDbRecord
    {
        public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();

        public AirDay? AirDay { get; set; }

        public TimeSpan? AirTime { get; set; }

        public string Banner { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Episode> Episodes { get; set; } = new List<Episode>();

        public DateTime? FirstAired { get; set; }

        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

        public string ImdbId { get; set; }

        public string Language { get; set; }

        public long LastUpdated { get; set; }

        public string Name { get; set; }

        public virtual Network Network { get; set; }

        public virtual int NetworkId { get; set; }

        public string Poster { get; set; }

        public virtual ICollection<User> Subscribers { get; set; } = new List<User>();

        public int TvDbId { get; set; }
    }
}
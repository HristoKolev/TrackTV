﻿namespace TrackTv.Models
{
    using System.Collections.Generic;

    using TrackTv.Models.Extensions;
    using TrackTv.Models.Joint;

    public class Genre : IPersistedModel
    {
        public Genre(string name)
        {
            this.Name = name;
        }

        public Genre()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ShowsGenres> ShowsGenres { get; } = new List<ShowsGenres>();
    }
}
﻿namespace TrackTv.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TrackTv.Models.Contracts;
    using TrackTv.Models.Enums;
    using TrackTv.Models.Joint;

    public class Show : ITvDbRecord
    {
        public AirDay? AirDay { get; set; }

        public DateTime? AirTime { get; set; }

        public string Banner { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Episode> Episodes { get; } = new List<Episode>();

        public DateTime? FirstAired { get; set; }

        public int Id { get; set; }

        public string ImdbId { get; set; }

        public DateTime LastUpdated { get; set; }

        public string Name { get; set; }

        public virtual Network Network { get; set; }

        public virtual int NetworkId { get; set; }

        public virtual ICollection<ShowsActors> ShowsActors { get; } = new List<ShowsActors>();

        public virtual ICollection<ShowsGenres> ShowsGenres { get; } = new List<ShowsGenres>();

        public virtual ICollection<ShowsUsers> ShowsUsers { get; } = new List<ShowsUsers>();

        public ShowStatus Status { get; set; }

        public int TvDbId { get; set; }

        public bool HasActor(Actor actor)
        {
            return this.ShowsActors.Any(x => ((x.ShowId == this.Id) && (x.ActorId == actor.Id)) || ((x.Show == this) && (x.Actor == actor)));
        }

        public bool HasGenre(Genre genre)
        {
            return this.ShowsGenres.Any(x => ((x.ShowId == this.Id) && (x.GenreId == genre.Id)) || ((x.Show == this) && (x.Genre == genre)));
        }

        public bool HasNetwork()
        {
            return (this.NetworkId != default(int)) || (this.Network != null);
        }
    }
}
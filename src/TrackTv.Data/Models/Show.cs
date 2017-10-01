﻿namespace TrackTv.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using TrackTv.Data.Models.Contracts;
    using TrackTv.Data.Models.Enums;

    public class Show : ITvDbRecord
    {
        public AirDay? AirDay { get; set; }

        public DateTime? AirTime { get; set; }

        public string ShowBanner { get; set; }

        public string ShowDescription { get; set; }

        public virtual ICollection<Episode> Episodes { get; } = new List<Episode>();

        public DateTime? FirstAired { get; set; }

        public int ShowId { get; set; }

        public string ImdbId { get; set; }

        public DateTime LastUpdated { get; set; }

        public string ShowName { get; set; }

        public Network Network { get; set; }

        public int NetworkId { get; set; }

        public virtual ICollection<Role> Roles { get; } = new List<Role>();

        public virtual ICollection<ShowsGenres> ShowsGenres { get; } = new List<ShowsGenres>();

        public virtual ICollection<Subscription> Subscriptions { get; } = new List<Subscription>();

        public ShowStatus ShowStatus { get; set; }

        public int TheTvDbId { get; set; }

        public bool HasActor(Actor actor)
        {
            return this.Roles.Any(x => x.Actor == actor || x.ActorId != default(int) && x.ActorId == actor.ActorId);
        }

        public bool HasGenre(Genre genre)
        {
            return this.ShowsGenres.Any(x => x.Genre == genre || x.GenreId != default(int) && x.GenreId == genre.GenreId);
        }

        public bool HasNetwork()
        {
            return this.NetworkId != default(int) || this.Network != null;
        }
    }
}
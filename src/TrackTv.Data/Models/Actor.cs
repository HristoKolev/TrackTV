namespace TrackTv.Data.Models
{
    using System;
    using System.Collections.Generic;

    using TrackTv.Data.Models.Contracts;

    public class Actor : ITvDbRecord
    {
        public Actor(int theTvDbId, string actorName, DateTime lastUpdated, string actorImage)
        {
            this.TheTvDbId = theTvDbId;
            this.ActorName = actorName;
            this.LastUpdated = lastUpdated;
            this.ActorImage = actorImage;
        }

        public Actor()
        {
        }

        public int ActorId { get; set; }

        public string ActorImage { get; set; }

        public string ActorName { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual ICollection<Role> Roles { get; } = new List<Role>();

        public int TheTvDbId { get; set; }
    }
}
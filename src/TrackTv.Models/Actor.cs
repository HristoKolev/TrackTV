namespace TrackTv.Models
{
    using System;
    using System.Collections.Generic;

    using TrackTv.Models.Contracts;
    using TrackTv.Models.Extensions;
    using TrackTv.Models.Joint;

    public class Actor : ITvDbRecord, IPersistedModel
    {
        // ReSharper disable once StyleCop.SA1305
        public Actor(int tvDbId, string name, DateTime lastUpdated, string image)
        {
            this.TvDbId = tvDbId;
            this.Name = name;
            this.LastUpdated = lastUpdated;
            this.Image = image;
        }

        public Actor()
        {
        }

        public int Id { get; set; }

        public string Image { get; set; }

        public DateTime LastUpdated { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ShowsActors> ShowsActors { get; } = new List<ShowsActors>();

        public int TvDbId { get; set; }
    }
}
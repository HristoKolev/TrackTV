namespace TrackTv.Models
{
    using System;
    using System.Collections.Generic;

    using TrackTv.Models.Contracts;
    using TrackTv.Models.Joint;

    public class Actor : ITvDbRecord
    {
        // ReSharper disable once StyleCop.SA1305
        public Actor(int theTvDbId, string name, DateTime lastUpdated, string image)
        {
            this.TheTvDbId = theTvDbId;
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

        public int TheTvDbId { get; set; }
    }
}
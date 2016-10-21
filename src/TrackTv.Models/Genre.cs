namespace TrackTv.Models
{
    using System.Collections.Generic;

    using TrackTv.Models.Joint;

    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ShowsGenres> ShowsGenres { get; } = new List<ShowsGenres>();
    }
}
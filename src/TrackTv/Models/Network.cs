namespace TrackTv.Models
{
    using System.Collections.Generic;

    public class Network
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Show> Shows { get; set; } = new List<Show>();
    }
}
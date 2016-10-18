namespace TrackTv.Models
{
    using System.Collections.Generic;

    public class User
    {
        public int Id { get; set; }

        public virtual ICollection<Show> Shows { get; set; } = new List<Show>();

        public string Username { get; set; }
    }
}
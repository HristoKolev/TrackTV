namespace TrackTv.Data.Models
{
    using System.Collections.Generic;

    public class Profile
    {
        public int Id { get; set; }

        public virtual ICollection<ShowsProfiles> ShowsProfiles { get; } = new List<ShowsProfiles>();

        public string Username { get; set; }
    }
}
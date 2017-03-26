namespace TrackTv.Models
{
    using System.Collections.Generic;

    using TrackTv.Models.Joint;

    public class Profile
    {
        public int Id { get; set; }

        public virtual ICollection<ShowsProfiles> ShowsUsers { get; } = new List<ShowsProfiles>();

        public string Username { get; set; }
    }
}
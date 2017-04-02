namespace TrackTv.Data.Models
{
    using System.Collections.Generic;

    public class Profile
    {
        public int Id { get; set; }

        public virtual ICollection<ShowsProfiles> ShowsUsers { get; } = new List<ShowsProfiles>();

        public string UserId { get; set; }

        public string Username { get; set; }
    }
}
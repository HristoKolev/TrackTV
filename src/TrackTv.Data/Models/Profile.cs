namespace TrackTv.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Profile
    {
        public int Id { get; set; }

        public virtual ICollection<ShowsProfiles> ShowsUsers { get; } = new List<ShowsProfiles>();

        public User User { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }

        public string Username { get; set; }
    }
}
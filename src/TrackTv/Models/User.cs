namespace TrackTv.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TrackTv.Models.Joint;

    public class User
    {
        public int Id { get; set; }

        public virtual ICollection<ShowsUsers> ShowsUsers { get; } = new List<ShowsUsers>();

        [Required]
        public string Username { get; set; }
    }
}
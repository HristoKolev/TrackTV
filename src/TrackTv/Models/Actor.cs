namespace TrackTv.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TrackTv.Models.Joint;

    public class Actor
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<ShowsActors> ShowsActors { get; } = new List<ShowsActors>();
    }
}
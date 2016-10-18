namespace TrackTv.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Network
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Show> Shows { get; } = new List<Show>();
    }
}
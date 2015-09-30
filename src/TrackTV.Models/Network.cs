namespace TrackTV.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TrackTV.Models.Contracts;

    public class Network : IAuditInfo
    {
        public Network()
        {
            this.CreatedOn = DateTime.Now;
        }

        public DateTime CreatedOn { get; set; }

        public int Id { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Show> Shows { get; set; } = new HashSet<Show>();

        public string StringId { get; set; }
    }
}
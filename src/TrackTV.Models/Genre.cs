namespace TrackTV.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TrackTV.Models.Contracts;

    public class Genre : IAuditInfo
    {
        public Genre()
        {
            this.CreatedOn = DateTime.Now;
        }

        public DateTime CreatedOn { get; set; }

        public int Id { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Show> Shows { get; set; } = new HashSet<Show>();

        [Required]
        public string UserFriendlyId { get; set; }
    }
}
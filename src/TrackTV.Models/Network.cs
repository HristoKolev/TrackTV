namespace TrackTV.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TrackTV.Data.Common.Models;

    public class Network : AuditInfo
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Show> Shows { get; set; } = new HashSet<Show>();

        public string StringId { get; set; }
    }
}
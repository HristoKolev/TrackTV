namespace TrackTV.Models
{
    using System;
    using System.Collections.Generic;

    using TrackTV.Models.Contracts;

    public class Season : IAuditInfo
    {
        public Season()
        {
            this.CreatedOn = DateTime.Now;
        }

        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Episode> Episodes { get; set; } = new HashSet<Episode>();

        public int Id { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int Number { get; set; }

        public virtual Show Show { get; set; }

        public int ShowId { get; set; }
    }
}
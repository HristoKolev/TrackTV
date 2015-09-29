namespace TrackTV.Models
{
    using System.Collections.Generic;

    using TrackTV.Data.Common.Models;

    public class Season : AuditInfo
    {
        public virtual ICollection<Episode> Episodes { get; set; } = new HashSet<Episode>();

        public int Id { get; set; }

        public int Number { get; set; }

        public virtual Show Show { get; set; }

        public int ShowId { get; set; }
    }
}
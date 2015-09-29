namespace TrackTV.Models
{
    using System;
    using System.Collections.Generic;

    using TrackTV.Data.Common.Models;

    public class Episode : AuditInfo
    {
        public Episode()
        {
            this.CreatedOn = DateTime.Now;
        }

        public string Description { get; set; }

        public DateTime? FirstAired { get; set; }

        public int Id { get; set; }

        public string ImdbId { get; set; }

        public long? LastUpdated { get; set; }

        public int Number { get; set; }

        public virtual Season Season { get; set; }

        public int SeasonId { get; set; }

        public string Title { get; set; }

        public int TvDbId { get; set; }

        public virtual ICollection<ApplicationUser> Viewers { get; set; } = new HashSet<ApplicationUser>();
    }
}
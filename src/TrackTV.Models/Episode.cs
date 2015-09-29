namespace TrackTV.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TrackTV.Data.Common.Models;

    public class Episode : AuditInfo
    {
        private ICollection<ApplicationUser> viewers;

        public Episode()
        {
            this.viewers = new HashSet<ApplicationUser>();
            this.CreatedOn = DateTime.Now;
        }

        public string Description { get; set; }

        public DateTime? FirstAired { get; set; }

        [Key]
        public int Id { get; set; }

        public string ImdbId { get; set; }

        public long? LastUpdated { get; set; }

        public int Number { get; set; }

        public virtual Season Season { get; set; }

        [ForeignKey("Season")]
        public int SeasonId { get; set; }

        public string Title { get; set; }

        public int TvDbId { get; set; }

        public virtual ICollection<ApplicationUser> Viewers
        {
            get
            {
                return this.viewers;
            }

            set
            {
                this.viewers = value;
            }
        }
    }
}
namespace TrackTV.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TrackTV.Data.Common.Models;

    public class Season : AuditInfo
    {
        private ICollection<Episode> episodes;

        public Season()
        {
            this.episodes = new HashSet<Episode>();
        }

        public virtual ICollection<Episode> Episodes
        {
            get
            {
                return this.episodes;
            }

            set
            {
                this.episodes = value;
            }
        }

        [Key]
        public int Id { get; set; }

        public int Number { get; set; }

        public virtual Show Show { get; set; }

        [ForeignKey("Show")]
        public int ShowId { get; set; }
    }
}
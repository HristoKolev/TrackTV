namespace TrackTV.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TrackTV.Data.Common.Models;

    public class Genre : AuditInfo
    {
        private ICollection<Show> shows;

        public Genre()
        {
            this.shows = new HashSet<Show>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Show> Shows
        {
            get
            {
                return this.shows;
            }

            set
            {
                this.shows = value;
            }
        }

        [Required]
        public string StringId { get; set; }
    }
}
namespace TrackTV.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using TrackTV.Data.Common.Models;

    public class Show : AuditInfo
    {
        private ICollection<Genre> genres;

        private ICollection<Season> seasons;

        private ICollection<ApplicationUser> subscribers;

        public Show()
        {
            this.genres = new HashSet<Genre>();
            this.seasons = new HashSet<Season>();
            this.subscribers = new HashSet<ApplicationUser>();
        }

        public AirDay? AirDay { get; set; }

        public TimeSpan? AirTime { get; set; }

        public string BannerBig { get; set; }

        public string PosterBig { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime? FirstAired { get; set; }

        public virtual ICollection<Genre> Genres
        {
            get
            {
                return this.genres;
            }

            set
            {
                this.genres = value;
            }
        }

        [Key]
        public int Id { get; set; }

        public string ImdbId { get; set; }

        public string Language { get; set; }

        public int? LastEpisodeId { get; set; }

        public long? LastUpdated { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Network Network { get; set; }

        [ForeignKey("Network")]
        public int? NetworkId { get; set; }

        public int? NextEpisodeId { get; set; }

        public int? Runtime { get; set; }

        public virtual ICollection<Season> Seasons
        {
            get
            {
                return this.seasons;
            }

            set
            {
                this.seasons = value;
            }
        }

        public ShowStatus Status { get; set; }

        public string StringId { get; set; }

        public virtual ICollection<ApplicationUser> Subscribers
        {
            get
            {
                return this.subscribers;
            }

            set
            {
                this.subscribers = value;
            }
        }

        public int TvDbId { get; set; }
    }
}
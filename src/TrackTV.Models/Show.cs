namespace TrackTV.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using TrackTV.Data.Common.Models;

    public class Show : AuditInfo
    {
        public AirDay? AirDay { get; set; }

        public TimeSpan? AirTime { get; set; }

        public string BannerBig { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime? FirstAired { get; set; }

        public virtual ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();

        public int Id { get; set; }

        public string ImdbId { get; set; }

        public string Language { get; set; }

        public int? LastEpisodeId { get; set; }

        public long? LastUpdated { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual Network Network { get; set; }

        public int? NetworkId { get; set; }

        public int? NextEpisodeId { get; set; }

        public string PosterBig { get; set; }

        public int? Runtime { get; set; }

        public virtual ICollection<Season> Seasons { get; set; } = new HashSet<Season>();

        public ShowStatus Status { get; set; }

        public string StringId { get; set; }

        public virtual ICollection<ApplicationUser> Subscribers { get; set; } = new HashSet<ApplicationUser>();

        public int TvDbId { get; set; }
    }
}
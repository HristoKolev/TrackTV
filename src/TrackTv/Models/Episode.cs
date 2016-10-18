namespace TrackTv.Models
{
    using System.ComponentModel.DataAnnotations;

    using TrackTv.Models.Contracts;

    public class Episode : ITvDbRecord
    {
        public string Description { get; set; }

        public int Id { get; set; }

        public string ImdbId { get; set; }

        public long LastUpdated { get; set; }

        public int Number { get; set; }

        [Required]
        public virtual Show Show { get; set; }

        public int ShowId { get; set; }

        public string Title { get; set; }

        public int TvDbId { get; set; }
    }
}
namespace TrackTv.Models
{
    using TrackTv.Models.Contracts;

    public class Episode : ITvDbRecord
    {
        public string Description { get; set; }

        public int Id { get; set; }

        public string ImdbId { get; set; }

        public long LastUpdated { get; set; }

        public int Number { get; set; }

        public string Title { get; set; }

        public int TvDbId { get; set; }
    }
}
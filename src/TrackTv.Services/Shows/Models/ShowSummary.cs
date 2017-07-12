namespace TrackTv.Services.Shows.Models
{
    using TrackTv.Data.Models.Enums;

    public class ShowSummary
    {
        public string Banner { get; set; }

        public string ImdbId { get; set; }

        public string Name { get; set; }

        public ShowStatus Status { get; set; }

        public int SubscriberCount { get; set; }
        public int Id { get; internal set; }
    }
}
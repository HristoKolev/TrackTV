namespace TrackTv.Services.Shows.Models
{
    using TrackTv.Data.Models.Enums;

    public class ShowSummary
    {
        public string ShowBanner { get; set; }

        public string ImdbId { get; set; }

        public string ShowName { get; set; }

        public ShowStatus ShowStatus { get; set; }

        public int SubscriberCount { get; set; }
        public int ShowId { get; internal set; }
    }
}
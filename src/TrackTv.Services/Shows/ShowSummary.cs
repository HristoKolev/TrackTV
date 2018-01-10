namespace TrackTv.Services.Shows
{
    using TrackTv.Data.Enums;

    public class ShowSummary
    {
        public string ImdbId { get; set; }

        public string ShowBanner { get; set; }

        public int ShowId { get; internal set; }

        public string ShowName { get; set; }

        public ShowStatus ShowStatus { get; set; }

        public int SubscriberCount { get; set; }
    }
}
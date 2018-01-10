namespace TrackTv.Services.Show
{
    using System;

    public class FullShow
    {
        public int? AirDay { get; set; }

        public AirTime AirTime { get; set; }

        public DateTime? AirTimeDate { get; set; }

        public DateTime? FirstAired { get; set; }

        public string ImdbId { get; set; }

        public bool IsUserSubscribed { get; set; }

        public string NetworkName { get; set; }

        public string ShowBanner { get; set; }

        public string ShowDescription { get; set; }

        public int ShowId { get; set; }

        public string ShowName { get; set; }

        public int ShowStatus { get; set; }

        public int TheTvDbId { get; set; }
    }
}
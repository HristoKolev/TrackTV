namespace TrackTv.Services.Show.Models
{
    using System;

    using TrackTv.Data.Models.Enums;

    public class FullShow
    {
        public AirDay? AirDay { get; set; }

        public AirTime AirTime { get; set; }

        public string ShowBanner { get; set; }

        public string ShowDescription { get; set; }

        public DateTime? FirstAired { get; set; }

        public int ShowId { get; set; }

        public string ImdbId { get; set; }

        public bool IsUserSubscribed { get; set; }

        public string ShowName { get; set; }

        public string NetworkName { get; set; }

        public ShowStatus ShowStatus { get; set; }

        public int TheTvDbId { get; set; }
    }
}
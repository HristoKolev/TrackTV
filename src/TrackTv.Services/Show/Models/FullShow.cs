namespace TrackTv.Services.Show.Models
{
    using System;

    using TrackTv.Models.Enums;

    public class FullShow
    {
        public AirDay? AirDay { get; set; }

        public AirTime AirTime { get; set; }

        public string Banner { get; set; }

        public string Description { get; set; }

        public DateTime? FirstAired { get; set; }

        public int Id { get; set; }

        public string ImdbId { get; set; }

        public bool IsUserSubscribed { get; set; }

        public string Name { get; set; }

        public string NetworkName { get; set; }

        public ShowStatus Status { get; set; }

        public int TheTvDbId { get; set; }
    }
}
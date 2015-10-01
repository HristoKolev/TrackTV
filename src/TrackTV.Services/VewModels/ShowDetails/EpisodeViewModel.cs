namespace TrackTV.Services.VewModels.ShowDetails
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NetInfrastructure.AutoMapper;

    using TrackTV.Models;

    [MapFrom(typeof(Episode))]
    public class EpisodeViewModel 
    {
        [UIHint("EpisodeDescription")]
        public string Description { get; set; }

        [UIHint("LongDate")]
        public DateTime? FirstAired { get; set; }

        [UIHint("TwoDigitNumber")]
        public int Number { get; set; }

        [UIHint("EpisodeTitle")]
        public string Title { get; set; }
    }
}
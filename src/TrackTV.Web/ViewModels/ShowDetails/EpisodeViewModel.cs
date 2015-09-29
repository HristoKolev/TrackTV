namespace TrackTV.Web.ViewModels.ShowDetails
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using TrackTV.Models;
    using TrackTV.Web.Infrastructure.Mapping.Contracts;

    public class EpisodeViewModel : IMapFrom<Episode>
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
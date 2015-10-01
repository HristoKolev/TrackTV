namespace TrackTV.Services.VewModels.MyShows
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using NetInfrastructure.AutoMapper;

    using TrackTV.Models;

    [MapFrom(typeof(Episode))]
    public class SimpleEpisodeViewModel : ICustomMap<Episode, SimpleEpisodeViewModel>
    {
        [UIHint("LongDate")]
        public DateTime? FirstAired { get; set; }

        [UIHint("TwoDigitNumber")]
        public int Number { get; set; }

        [UIHint("TwoDigitNumber")]
        public int SeasonNumber { get; set; }

        public string Title { get; set; }

        public void CreateMappings(ICustomMapper<Episode, SimpleEpisodeViewModel> mapper)
        {
            mapper.QuickMap(model => model.SeasonNumber, episode => episode.Season.Number);
        }
    }
}
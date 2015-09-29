namespace TrackTV.Web.ViewModels.MyShows
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using TrackTV.Models;
    using TrackTV.Web.Infrastructure.Mapping.Contracts;

    public class SimpleEpisodeViewModel : IMapFrom<Episode>, IHaveCustomMappings
    {
        [UIHint("LongDate")]
        public DateTime? FirstAired { get; set; }

        [UIHint("TwoDigitNumber")]
        public int Number { get; set; }

        [UIHint("TwoDigitNumber")]
        public int SeasonNumber { get; set; }

        public string Title { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Episode, SimpleEpisodeViewModel>().ForMember(model => model.SeasonNumber, expression => expression.MapFrom(episode => episode.Season.Number));
        }
    }
}
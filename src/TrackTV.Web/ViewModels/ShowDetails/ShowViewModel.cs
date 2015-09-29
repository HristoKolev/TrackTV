namespace TrackTV.Web.ViewModels.ShowDetails
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;

    using TrackTV.Logic;
    using TrackTV.Models;
    using TrackTV.Web.Infrastructure.Mapping.Contracts;

    public class ShowViewModel : IMapFrom<Show>, IHaveCustomMappings
    {
        public AirTimeViewModel AirTimeAndDate { get; set; }

        public string Banner { get; set; }

        public string Description { get; set; }

        public int EpisodeCount { get; set; }

        [UIHint("PremieredDate")]
        public DateTime? FirstAired { get; set; }

        public int Id { get; set; }

        public string ImdbId { get; set; }

        public bool IsUserSubscribed { get; set; }

        public string Name { get; set; }

        public string Network { get; set; }

        public string NetworkStringId { get; set; }

        public int NumberOfSeasones { get; set; }

        public int? Runtime { get; set; }

        public ShowStatus Status { get; set; }

        public string StringId { get; set; }

        public int SubscriberCount { get; set; }

        public int TvDbId { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Show, ShowViewModel>()
                .ForMember(model => model.Banner, expression => expression.MapFrom(show => ApplicationSettings.BannerPath + show.BannerBig))
                .ForMember(model => model.EpisodeCount, expression => expression.MapFrom(show => show.Seasons.SelectMany(season => season.Episodes).Count()))
                .ForMember(model => model.SubscriberCount, expression => expression.MapFrom(show => show.Subscribers.Count))
                .ForMember(model => model.Network, expression => expression.MapFrom(show => show.Network.Name))
                .ForMember(model => model.NetworkStringId, expression => expression.MapFrom(show => show.Network.StringId))
                .ForMember(
                    model => model.NumberOfSeasones,
                    expression => expression.MapFrom(show => show.Seasons.Count(season => season.Episodes.Count != 0 && season.Number != 0)))
                .ForMember(model => model.AirTimeAndDate, expression => expression.MapFrom(show => new AirTimeViewModel(show.AirDay, show.AirTime)));
        }
    }
}
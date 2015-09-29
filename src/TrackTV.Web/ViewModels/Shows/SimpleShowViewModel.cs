namespace TrackTV.Web.ViewModels.Shows
{
    using System.Linq;

    using AutoMapper;

    using TrackTV.Logic;
    using TrackTV.Models;
    using TrackTV.Web.Infrastructure.Mapping.Contracts;

    public class SimpleShowViewModel : IMapFrom<Show>, IHaveCustomMappings
    {
        public string Banner { get; set; }

        public int EpisodeCount { get; set; }

        public string Name { get; set; }

        public string Poster { get; set; }

        public string StringId { get; set; }

        public int SubscriberCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Show, SimpleShowViewModel>()
                .ForMember(model => model.Banner, expression => expression.MapFrom(show => ApplicationSettings.BannerPath + show.BannerBig))
                .ForMember(model => model.Poster, expression => expression.MapFrom(show => ApplicationSettings.PosterPath + (show.PosterBig ?? "/DefaultPoster.jpg")))
                .ForMember(model => model.EpisodeCount, expression => expression.MapFrom(show => show.Seasons.SelectMany(season => season.Episodes).Count()))
                .ForMember(model => model.SubscriberCount, expression => expression.MapFrom(show => show.Subscribers.Count));
        }
    }
}
namespace TrackTV.Web.ViewModels.Shows
{
    using System.Linq;

    using NetInfrastructure.AutoMapper;

    using TrackTV.Logic;
    using TrackTV.Models;

    [MapFrom(typeof(Show))]
    public class SimpleShowViewModel : ICustomMap<Show, SimpleShowViewModel>
    {
        public string Banner { get; set; }

        public int EpisodeCount { get; set; }

        public string Name { get; set; }

        public string Poster { get; set; }

        public string StringId { get; set; }

        public int SubscriberCount { get; set; }

        public void CreateMappings(ICustomMapper<Show, SimpleShowViewModel> mapper)
        {
            mapper.QuickMap(model => model.Banner, show => ApplicationSettings.BannerPath + show.BannerBig);
            mapper.QuickMap(model => model.Poster, show => ApplicationSettings.PosterPath + (show.PosterBig ?? "/DefaultPoster.jpg"));
            mapper.QuickMap(model => model.EpisodeCount, show => show.Seasons.SelectMany(season => season.Episodes).Count());
            mapper.QuickMap(model => model.SubscriberCount, show => show.Subscribers.Count);
        }
    }
}
namespace TrackTV.Services.VewModels.ShowDetails
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using NetInfrastructure.AutoMapper;

    using TrackTV.Logic;
    using TrackTV.Models;

    [MapFrom(typeof(Show))]
    public class ShowViewModel : ICustomMap<Show, ShowViewModel>
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

        public string NetworkUserFriendlyId { get; set; }

        public int NumberOfSeasones { get; set; }

        public int? Runtime { get; set; }

        public ShowStatus Status { get; set; }

        public string UserFriendlyId { get; set; }

        public int SubscriberCount { get; set; }

        public int TvDbId { get; set; }

        public void CreateMappings(ICustomMapper<Show, ShowViewModel> mapper)
        {
            mapper.QuickMap(model => model.Banner, show => ApplicationSettings.BannerPath + show.BannerBig);
            mapper.QuickMap(model => model.EpisodeCount, show => show.Seasons.SelectMany(season => season.Episodes).Count());
            mapper.QuickMap(model => model.SubscriberCount, show => show.Subscribers.Count);
            mapper.QuickMap(model => model.Network, show => show.Network.Name);
            mapper.QuickMap(model => model.NetworkUserFriendlyId, show => show.Network.UserFriendlyId);
            mapper.QuickMap(model => model.NumberOfSeasones,
                show => show.Seasons.Count(season => season.Episodes.Count != 0 && season.Number != 0));
            mapper.QuickMap(model => model.AirTimeAndDate, show => new AirTimeViewModel(show.AirDay, show.AirTime));
        }
    }
}
namespace TrackTV.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using TrackTV.Logic;
    using TrackTV.Logic.Fetchers;
    using TrackTV.Models;
    using TrackTV.Services.VewModels.ShowDetails;

    public class ShowDetailsService
    {
        public ShowDetailsService(
            IMappingEngine mappingEngine, 
            ShowManager showManager, 
            EpisodeManager episodeManager, 
            SubscriptionManager subscriptionManager, 
            IFetcher fetcher)
        {
            this.MappingEngine = mappingEngine;
            this.ShowManager = showManager;
            this.EpisodeManager = episodeManager;
            this.SubscriptionManager = subscriptionManager;
            this.Fetcher = fetcher;
        }

        private EpisodeManager EpisodeManager { get; }

        private IFetcher Fetcher { get; }

        private IMappingEngine MappingEngine { get; }

        private ShowManager ShowManager { get; }

        private SubscriptionManager SubscriptionManager { get; }

        public ShowViewModel GetByUserFriendlyId(string userFriendlyId, string currentUserId)
        {
            Show show = this.ShowManager.GetShowByUserFriendlyId(userFriendlyId);

            if (show == null)
            {
                return null;
            }

            ShowViewModel model = this.MappingEngine.Map<ShowViewModel>(show);

            if (currentUserId == null)
            {
                model.IsUserSubscribed = false;
            }
            else
            {
                model.IsUserSubscribed = show.Subscribers.Any(user => user.Id == currentUserId);
            }

            return model;
        }

        public IList<EpisodeViewModel> GetSeason(int showId, int seasonNumber)
        {
            IList<EpisodeViewModel> models =
                this.EpisodeManager.GetSeasonEpisodes(showId, seasonNumber).Project().To<EpisodeViewModel>().ToList();

            return models;
        }

        public void Remove(int id)
        {
            this.ShowManager.RemoveShow(id);
        }

        public string Subscribe(ApplicationUser user, int showId)
        {
            Show show = this.ShowManager.GetShowById(showId);

            this.SubscriptionManager.Subscribe(user, show);

            return show.UserFriendlyId;
        }

        public string Unsubscribe(ApplicationUser user, int showId)
        {
            Show show = this.ShowManager.GetShowById(showId);

            this.SubscriptionManager.Unsubscribe(user, show);

            return show.UserFriendlyId;
        }

        public string Update(int showId)
        {
            Show show = this.ShowManager.GetShowById(showId);

            this.Fetcher.UpdateShow(show);

            return show.UserFriendlyId;
        }
    }
}
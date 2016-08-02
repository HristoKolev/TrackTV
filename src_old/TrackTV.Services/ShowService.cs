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

    public class ShowService
    {
        public ShowService(
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

        public ShowViewModel Show(string currentUserId, string userFriendlyId)
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

        private IList<EpisodeViewModel> GetSeason(int showId, int seasonNumber)
        {
            IList<EpisodeViewModel> models =
                this.EpisodeManager.GetSeasonEpisodes(showId, seasonNumber).Project().To<EpisodeViewModel>().ToList();

            return models;
        }

        private void Remove(int id)
        {
            this.ShowManager.RemoveShow(id);
        }

        private string Update(int showId)
        {
            Show show = this.ShowManager.GetShowById(showId);

            this.Fetcher.UpdateShow(show);

            return show.UserFriendlyId;
        }
    }
}
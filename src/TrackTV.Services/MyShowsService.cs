namespace TrackTV.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using TrackTV.Logic;
    using TrackTV.Models;
    using TrackTV.Services.VewModels.MyShows;

    public class MyShowsService
    {
        public MyShowsService(
            ShowManager showManager, 
            EpisodeManager episodeManager, 
            SubscriptionManager subscriptionManager, 
            IMappingEngine mappingEngine)
        {
            this.ShowManager = showManager;
            this.EpisodeManager = episodeManager;
            this.SubscriptionManager = subscriptionManager;
            this.MappingEngine = mappingEngine;
        }

        private EpisodeManager EpisodeManager { get; }

        private IMappingEngine MappingEngine { get; }

        private ShowManager ShowManager { get; }

        private SubscriptionManager SubscriptionManager { get; }

        public MyShowsViewModel GetMyShows(string userId)
        {
            IList<Show> shows = this.ShowManager.GetUserShows(userId).ToList();

            MyShowsViewModel model = new MyShowsViewModel
            {
                Running = new List<MyShowViewModel>(), 
                Ended = new List<MyShowViewModel>()
            };

            if (shows.Count == 0)
            {
                return model;
            }

            IEnumerable<Show> running = shows.Where(show => show.Status == ShowStatus.Continuing);
            IEnumerable<Show> ended = shows.Where(show => show.Status == ShowStatus.Ended);

            foreach (Show show in running)
            {
                MyShowViewModel showModel = this.MappingEngine.Map<MyShowViewModel>(show);

                showModel.LastEpisode = this.MappingEngine.Map<SimpleEpisodeViewModel>(this.EpisodeManager.GetLastEpisode(show));
                showModel.NextEpisode = this.MappingEngine.Map<SimpleEpisodeViewModel>(this.EpisodeManager.GetNextEpisode(show));

                model.Running.Add(showModel);
            }

            foreach (Show show in ended)
            {
                MyShowViewModel showModel = this.MappingEngine.Map<MyShowViewModel>(show);

                showModel.LastEpisode = this.MappingEngine.Map<SimpleEpisodeViewModel>(this.EpisodeManager.GetLastEpisode(show));

                model.Ended.Add(showModel);
            }

            return model;
        }

        public void Unsubscribe(ApplicationUser user, int showId)
        {
            Show show = this.ShowManager.GetShowById(showId);

            this.SubscriptionManager.Unsubscribe(user, show);
        }
    }
}
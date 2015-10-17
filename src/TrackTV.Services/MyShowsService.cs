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
        private const int PageSize = 10;

        public MyShowsService(
            ShowManager showManager, 
            EpisodeManager episodeManager, 
            SubscriptionManager subscriptionManager, 
            IMappingEngine mappingEngine, 
            UserManager userManager)
        {
            this.ShowManager = showManager;
            this.EpisodeManager = episodeManager;
            this.SubscriptionManager = subscriptionManager;
            this.MappingEngine = mappingEngine;
            this.UserManager = userManager;
        }

        private EpisodeManager EpisodeManager { get; }

        private IMappingEngine MappingEngine { get; }

        private ShowManager ShowManager { get; }

        private SubscriptionManager SubscriptionManager { get; }

        private UserManager UserManager { get; }

        public MyShowsViewModel Continuing(string userId, int? page)
        {
            return this.GetShows(userId, ShowStatus.Continuing, page);
        }

        public MyShowsViewModel Ended(string userId, int? page)
        {
            return this.GetShows(userId, ShowStatus.Ended, page);
        }

        private MyShowsViewModel GetShows(string userId, ShowStatus status, int? page)
        {
            IQueryable<Show> shows = this.ShowManager.GetUserShows(userId).Where(show => show.Status == status);

            MyShowsViewModel model = new MyShowsViewModel
            {
                Shows = new List<MyShowViewModel>(), 
                Count = shows.Count()
            };

            shows = shows.OrderBy(show => show.Name).Page(page, PageSize);

            foreach (Show show in shows)
            {
                MyShowViewModel showModel = this.MappingEngine.Map<MyShowViewModel>(show);

                showModel.LastEpisode = this.MappingEngine.Map<SimpleEpisodeViewModel>(this.EpisodeManager.GetLastEpisode(show));

                if (status == ShowStatus.Continuing)
                {
                    showModel.NextEpisode = this.MappingEngine.Map<SimpleEpisodeViewModel>(this.EpisodeManager.GetNextEpisode(show));
                }

                model.Shows.Add(showModel);
            }

            return model;
        }
    }
}
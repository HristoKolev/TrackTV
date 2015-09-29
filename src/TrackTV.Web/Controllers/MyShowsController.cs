namespace TrackTV.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using AutoMapper;

    using TrackTV.Data.Contracts;
    using TrackTV.Logic;
    using TrackTV.Models;
    using TrackTV.Web.ViewModels.MyShows;

    [Authorize]
    public class MyShowsController : BaseController
    {
        public MyShowsController(ITrackTVData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            ShowManager showManager = new ShowManager(this.Data);

            IList<Show> shows = showManager.GetUserShows(this.CurrentUserId).ToList();

            if (shows.Count == 0)
            {
                return this.View("NoShows");
            }

            IEnumerable<Show> running = shows.Where(show => show.Status == ShowStatus.Continuing);
            IEnumerable<Show> ended = shows.Where(show => show.Status == ShowStatus.Ended);

            MyShowsViewModel model = new MyShowsViewModel
            {
                Running = new List<MyShowViewModel>(),
                Ended = new List<MyShowViewModel>()
            };

            EpisodeManager episodeManager = new EpisodeManager(this.Data);

            foreach (Show show in running)
            {
                MyShowViewModel showModel = Mapper.Map<MyShowViewModel>(show);

                showModel.LastEpisode = Mapper.Map<SimpleEpisodeViewModel>(episodeManager.GetLastEpisode(show));
                showModel.NextEpisode = Mapper.Map<SimpleEpisodeViewModel>(episodeManager.GetNextEpisode(show));

                model.Running.Add(showModel);
            }

            foreach (Show show in ended)
            {
                MyShowViewModel showModel = Mapper.Map<MyShowViewModel>(show);

                showModel.LastEpisode = Mapper.Map<SimpleEpisodeViewModel>(episodeManager.GetLastEpisode(show));

                model.Ended.Add(showModel);
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Unsubscribe(int id)
        {
            ApplicationUser user = this.GetCurrentUser();

            ShowManager showManager = new ShowManager(this.Data);

            Show show = showManager.GetShowById(id);

            SubscriptionManager subscriptionManager = new SubscriptionManager(this.Data);

            try
            {
                subscriptionManager.Unsubscribe(user, show);
            }
            catch (InvalidOperationException exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }

            return this.RedirectToAction("Index");
        }
    }
}
namespace TrackTV.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using TrackTV.Data.Contracts;
    using TrackTV.Logic;
    using TrackTV.Logic.Fetchers;
    using TrackTV.Models;
    using TrackTV.Web.ViewModels.ShowDetails;

    public class ShowDetailsController : BaseController
    {
        public ShowDetailsController(ITrackTVData data)
            : base(data)
        {
        }

        public ActionResult ById(string stringId)
        {
            ShowManager showManager = new ShowManager(this.Data);

            Show show = showManager.GetShowByStringId(stringId);

            if (show == null)
            {
                return this.NotFound();
            }

            ShowViewModel model = Mapper.Map<Show, ShowViewModel>(show);

            bool isSubscribed;

            if (!this.IsLoggedIn)
            {
                isSubscribed = false;
            }
            else
            {
                isSubscribed = show.Subscribers.Any(user => user.Id == this.CurrentUserId);
            }

            model.IsUserSubscribed = isSubscribed;

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Remove(int id)
        {
            ShowManager showManager = new ShowManager(this.Data);

            showManager.RemoveShow(id);

            return this.RedirectToAction("Index", "Shows");
        }

        public ActionResult Season(int id, int seasonNumber)
        {
            EpisodeManager episodeManager = new EpisodeManager(this.Data);

            IList<EpisodeViewModel> models = episodeManager.GetSeasonEpisodes(id, seasonNumber).Project().To<EpisodeViewModel>().ToList();

            return this.PartialView(models);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Subscribe(int id)
        {
            ApplicationUser user = this.GetCurrentUser();

            ShowManager showManager = new ShowManager(this.Data);

            Show show = showManager.GetShowById(id);

            SubscriptionManager subscriptionManager = new SubscriptionManager(this.Data);

            subscriptionManager.Subscribe(user, show);

            return this.RedirectToAction(
                "ById",
                new
                {
                    stringId = show.StringId
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Unsubscribe(int id)
        {
            ApplicationUser user = this.GetCurrentUser();

            ShowManager showManager = new ShowManager(this.Data);

            Show show = showManager.GetShowById(id);

            SubscriptionManager manager = new SubscriptionManager(this.Data);

            manager.Unsubscribe(user, show);

            return this.RedirectToAction(
                "ById",
                new
                {
                    stringId = show.StringId
                });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Update(int id)
        {
            ShowManager showManager = new ShowManager(this.Data);

            Show show = showManager.GetShowById(id);

            IFetcher fetcher = new Fetcher(this.Data);

            fetcher.UpdateShow(show);

            return this.RedirectToAction(
                "ById",
                new
                {
                    stringid = show.StringId
                });
        }
    }
}
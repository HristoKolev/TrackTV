namespace TrackTV.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Data;
    using TrackTV.Models;
    using TrackTV.Services;
    using TrackTV.Services.VewModels.ShowDetails;

    public class ShowDetailsController : BaseController
    {
        public ShowDetailsController(IRepository<ApplicationUser, string> users, ShowDetailsService showDetailsService)
            : base(users)
        {
            this.ShowDetailsService = showDetailsService;
        }

        private ShowDetailsService ShowDetailsService { get; }

        public ActionResult ById(string stringId)
        {
            ShowViewModel model = this.ShowDetailsService.GetByStringId(stringId, this.CurrentUserId);

            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Remove(int id)
        {
            this.ShowDetailsService.Remove(id);

            return this.RedirectToAction("Index", "Shows");
        }

        public ActionResult Season(int id, int seasonNumber)
        {
            IList<EpisodeViewModel> models = this.ShowDetailsService.GetSeason(id, seasonNumber);

            return this.PartialView(models);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Subscribe(int id)
        {
            string stringId = this.ShowDetailsService.Subscribe(this.GetCurrentUser(), id);

            return this.RedirectToAction("ById", new
            {
                stringId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Unsubscribe(int id)
        {
            string stringId = this.ShowDetailsService.Unsubscribe(this.GetCurrentUser(), id);

            return this.RedirectToAction("ById", new
            {
                stringId
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Update(int id)
        {
            string stringid = this.ShowDetailsService.Update(id);

            return this.RedirectToAction("ById", new
            {
                stringid
            });
        }
    }
}
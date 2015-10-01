﻿namespace TrackTV.Web.Controllers
{
    using System.Web.Mvc;
    using System.Web.Mvc.Expressions;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Models;
    using TrackTV.Services;

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
            var model = this.ShowDetailsService.GetByStringId(stringId, this.CurrentUserId);

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

            return this.RedirectToAction<ShowsController>(controller => controller.Index());
        }

        public ActionResult Season(int id, int seasonNumber)
        {
            var model = this.ShowDetailsService.GetSeason(id, seasonNumber);

            return this.PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Subscribe(int id)
        {
            string stringId = this.ShowDetailsService.Subscribe(this.GetCurrentUser(), id);

            return this.RedirectToAction(controller => controller.ById(stringId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Unsubscribe(int id)
        {
            string stringId = this.ShowDetailsService.Unsubscribe(this.GetCurrentUser(), id);

            return this.RedirectToAction(controller => controller.ById(stringId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Update(int id)
        {
            string stringId = this.ShowDetailsService.Update(id);

            return this.RedirectToAction(controller => controller.ById(stringId));
        }
    }
}
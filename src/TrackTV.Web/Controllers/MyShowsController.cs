namespace TrackTV.Web.Controllers
{
    using System;
    using System.Net;
    using System.Web.Mvc;

    using TrackTV.Data;
    using TrackTV.Services;
    using TrackTV.Services.VewModels.MyShows;

    [Authorize]
    public class MyShowsController : BaseController
    {
        public MyShowsController(ITrackTVData data, MyShowsService myShowsService)
            : base(data)
        {
            this.MyShowsService = myShowsService;
        }

        private MyShowsService MyShowsService { get; }

        public ActionResult Index()
        {
            MyShowsViewModel model = this.MyShowsService.GetMyShows(this.CurrentUserId);

            if (model.IsEmpty)
            {
                return this.View("NoShows");
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Unsubscribe(int id)
        {
            try
            {
                this.MyShowsService.Unsubscribe(this.GetCurrentUser(), id);
            }
            catch (InvalidOperationException exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, exception.Message);
            }

            return this.RedirectToAction("Index");
        }
    }
}
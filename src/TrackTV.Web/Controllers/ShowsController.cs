namespace TrackTV.Web.Controllers
{
    using System.Web.Mvc;
    using System.Web.Mvc.Expressions;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Data;
    using TrackTV.Models;
    using TrackTV.Services;
    using TrackTV.Services.VewModels.Shows;

    public class ShowsController : BaseController
    {
        public ShowsController(IRepository<ApplicationUser, string> users, ShowsService showsService)
            : base(users)
        {
            this.ShowsService = showsService;
        }

        private ShowsService ShowsService { get; }

        public ActionResult ByGenre(string stringId)
        {
            var model = this.ShowsService.GetByGenre(stringId);

            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        public ActionResult ByNetwork(string stringId, int? page)
        {
            var model = this.ShowsService.GetByNetwork(stringId, page);

            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        public ActionResult Index()
        {
            var model = this.ShowsService.GetTopShows();

            return this.View(model);
        }

        public ActionResult Search(string query, int? page)
        {
            if (query.Trim() == string.Empty)
            {
                return this.RedirectToAction(controller => controller.Index());
            }

            var model = this.ShowsService.Search(query, page);

            model.ActionName = nameof(this.Search);
            model.ControllerName = nameof(ShowsController);

            if (model.Shows == null)
            {
                return this.View("NoSearchResults", model);
            }

            return this.View(model);
        }
    }
}
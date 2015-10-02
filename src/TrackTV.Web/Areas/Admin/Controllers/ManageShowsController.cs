namespace TrackTV.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using System.Web.Mvc.Expressions;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Logic;
    using TrackTV.Logic.Fetchers;
    using TrackTV.Models;
    using TrackTV.Services;
    using TrackTV.Web.Controllers;

    public class ManageShowsController : AdminController
    {
        public ManageShowsController(
            IRepository<ApplicationUser, string> users, 
            IAppSettings appSettings, 
            IFetcher fetcher, 
            ManageShowsService manageShowsService)
            : base(users, appSettings)
        {
            this.Fetcher = fetcher;
            this.ManageShowsService = manageShowsService;
        }

        private IFetcher Fetcher { get; set; }

        private ManageShowsService ManageShowsService { get; }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddShow(int id)
        {
            string userFriendlyId = this.ManageShowsService.AddShow(id);

            var routeValues = new
            {
                Area = string.Empty
            };

            return this.RedirectToAction<ShowDetailsController>(controller => controller.ById(userFriendlyId), routeValues);
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return this.RedirectToAction(controller => controller.Index());
            }

            var model = this.ManageShowsService.Search(query);

            if (model == null)
            {
                return this.HttpNotFound();
            }

            return this.View(model);
        }
    }
}
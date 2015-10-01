namespace TrackTV.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Logic.Fetchers;
    using TrackTV.Models;
    using TrackTV.Services;

    public class ManageShowsController : AdminController
    {
        public ManageShowsController(IRepository<ApplicationUser, string> users, IFetcher fetcher, ManageShowsService manageShowsService)
            : base(users)
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
            string stringId = this.ManageShowsService.AddShow(id);

            return this.RedirectToAction(actionName: "ById", controllerName: "ShowDetails", routeValues: new
            {
                Area = string.Empty, 
                stringId
            });
        }

        public ActionResult Index()
        {
            return this.View();
        }

        public ActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return this.Redirect("Index");
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
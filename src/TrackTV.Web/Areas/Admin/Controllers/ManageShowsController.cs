namespace TrackTV.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;

    using TrackTV.Data;
    using TrackTV.Logic.Fetchers;
    using TrackTV.Services;

    public class ManageShowsController : AdminController
    {
        public ManageShowsController(ITrackTVData data, IFetcher fetcher, ManageShowsService manageShowsService)
            : base(data)
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
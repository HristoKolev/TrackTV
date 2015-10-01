namespace TrackTV.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using TrackTV.Data;
    using TrackTV.Logic.Fetchers;
    using TrackTV.Logic.Models;
    using TrackTV.Models;
    using TrackTV.Web.Areas.Admin.ViewModels.ManageShows;

    public class ManageShowsController : AdminController
    {
        public ManageShowsController(ITrackTVData data, IFetcher fetcher)
            : base(data)
        {
            this.Fetcher = fetcher;
        }

        private IFetcher Fetcher { get; set; }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddShow(int id)
        {
            Show show = this.Fetcher.AddShow(id);

            return this.RedirectToAction(actionName: "ById", controllerName: "ShowDetails", routeValues: new
            {
                Area = string.Empty, 
                stringId = show.StringId
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

            IList<ShowSample> samples = this.Fetcher.GetSamples(query);

            if (!samples.Any())
            {
                return this.HttpNotFound();
            }

            foreach (ShowSample sample in samples)
            {
                int id = sample.Id;
                sample.IsAdded = this.Data.Shows.All().Any(show => show.TvDbId == id);
            }

            SampleShowsViewModel model = new SampleShowsViewModel
            {
                Samples = samples, 
                Query = query
            };

            return this.View(model);
        }
    }
}
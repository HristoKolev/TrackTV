namespace TrackTV.Web.Areas.Admin.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using TrackTV.Data.Contracts;
    using TrackTV.Logic.Fetchers;
    using TrackTV.Logic.Models;
    using TrackTV.Models;
    using TrackTV.Web.Areas.Admin.ViewModels.ManageShows;

    public class ManageShowsController : AdminController
    {
        public ManageShowsController(ITrackTVData data)
            : base(data)
        {
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddShow(int id)
        {
            IFetcher fetcher = new Fetcher(this.Data);
            Show show = fetcher.AddShow(id);

            return this.RedirectToAction(
                actionName: "ById",
                controllerName: "ShowDetails",
                routeValues: new
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

            IFetcher fetcher = new Fetcher(this.Data);

            IList<ShowSample> samples = fetcher.GetSamples(query);

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
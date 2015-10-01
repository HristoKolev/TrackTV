namespace TrackTV.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using NetInfrastructure.Data.Repositories;

    using TrackTV.Logic.Fetchers;
    using TrackTV.Logic.Models;
    using TrackTV.Models;
    using TrackTV.Services.VewModels.ManageShows;

    public class ManageShowsService
    {
        public ManageShowsService(IFetcher fetcher, IRepository<Show> shows)
        {
            this.Fetcher = fetcher;
            this.Shows = shows;
        }

        private IFetcher Fetcher { get; }

        private IRepository<Show> Shows { get; }

        public string AddShow(int id)
        {
            Show show = this.Fetcher.AddShow(id);

            return show.StringId;
        }

        public SampleShowsViewModel Search(string query)
        {
            IList<ShowSample> samples = this.Fetcher.GetSamples(query);

            if (!samples.Any())
            {
                return null;
            }

            foreach (ShowSample sample in samples)
            {
                int id = sample.Id;
                sample.IsAdded = this.Shows.All().Any(show => show.TvDbId == id);
            }

            SampleShowsViewModel model = new SampleShowsViewModel
            {
                Samples = samples, 
                Query = query
            };

            return model;
        }
    }
}
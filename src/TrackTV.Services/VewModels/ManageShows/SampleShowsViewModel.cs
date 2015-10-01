namespace TrackTV.Services.VewModels.ManageShows
{
    using System.Collections.Generic;

    using TrackTV.Logic.Models;

    public class SampleShowsViewModel
    {
        public string Query { get; set; }

        public IList<ShowSample> Samples { get; set; }
    }
}
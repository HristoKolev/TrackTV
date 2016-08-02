namespace TrackTV.Services.VewModels.MyShows
{
    using System.Collections.Generic;

    public class MyShowsViewModel
    {
        public int Count { get; set; }

        public IList<MyShowViewModel> Shows { get; set; }
    }
}
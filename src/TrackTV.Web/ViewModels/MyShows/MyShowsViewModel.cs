namespace TrackTV.Web.ViewModels.MyShows
{
    using System.Collections.Generic;

    public class MyShowsViewModel
    {
        public IList<MyShowViewModel> Ended { get; set; }

        public IList<MyShowViewModel> Running { get; set; }
    }
}
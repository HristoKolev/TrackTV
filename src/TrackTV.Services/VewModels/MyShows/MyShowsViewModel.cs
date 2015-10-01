namespace TrackTV.Services.VewModels.MyShows
{
    using System.Collections.Generic;
    using System.Linq;

    public class MyShowsViewModel
    {
        public IList<MyShowViewModel> Ended { get; set; }

        public bool IsEmpty => !this.Running.Any() && !this.Ended.Any();

        public IList<MyShowViewModel> Running { get; set; }
    }
}
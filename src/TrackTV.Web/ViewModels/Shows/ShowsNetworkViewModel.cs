namespace TrackTV.Web.ViewModels.Shows
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ShowsNetworkViewModel : PagerViewModel
    {
        public string NetworkName { get; set; }

        [UIHint("ShowList")]
        public List<SimpleShowViewModel> Shows { get; set; }
    }
}
namespace TrackTV.Web.ViewModels.Shows
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ShowsSearchViewModel : PagerViewModel
    {
        public string Query { get; set; }

        [UIHint("ShowList")]
        public List<SimpleShowViewModel> Shows { get; set; }
    }
}
namespace TrackTV.Services.VewModels.Shows
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ShowsNetworkViewModel
    {
        public string NetworkName { get; set; }

        [UIHint("ShowList")]
        public IEnumerable<SimpleShowViewModel> Shows { get; set; }

        public int Count { get; set; }
    }
}
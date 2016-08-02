namespace TrackTV.Services.VewModels.Shows
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ShowsSearchViewModel
    {
        public string Query { get; set; }

        [UIHint("ShowList")]
        public IEnumerable<SimpleShowViewModel> Shows { get; set; }

        public int Count { get; set; }
    }
}
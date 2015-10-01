namespace TrackTV.Services.VewModels.Shows
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ShowsViewModel
    {
        [UIHint("ShowList")]
        public IList<SimpleShowViewModel> Ended { get; set; }

        public IList<GenreViewModel> Genres { get; set; }

        [UIHint("ShowList")]
        public IList<SimpleShowViewModel> Running { get; set; }
    }
}
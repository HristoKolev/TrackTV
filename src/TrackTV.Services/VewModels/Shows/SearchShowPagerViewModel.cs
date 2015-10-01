namespace TrackTV.Services.VewModels.Shows
{
    public class SearchShowPagerViewModel : PagerViewModel
    {
        public SearchShowPagerViewModel(int currentPage, int totalPages, string query)
        {
            this.TotalPages = totalPages;
            this.Query = query;
            this.CurrentPage = currentPage;
        }

        public string Query { get; set; }
    }
}
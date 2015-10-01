namespace TrackTV.Services.VewModels
{
    public class PagerViewModel
    {
        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
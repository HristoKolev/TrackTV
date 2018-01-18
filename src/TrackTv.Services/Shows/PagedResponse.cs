namespace TrackTv.Services.Shows
{
    public class PagedResponse<T>
        where T : class
    {
     
        public T Data { get; set; }

        public int TotalCount { get; set; }
    }
}
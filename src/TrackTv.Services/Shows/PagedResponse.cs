namespace TrackTv.Services.Shows.Models
{
    public class PagedResponse<T>
        where T : class
    {
        public PagedResponse(T data, int totalCount)
        {
            this.Data = data;
            this.TotalCount = totalCount;
        }

        public T Data { get; set; }

        public int TotalCount { get; set; }
    }
}
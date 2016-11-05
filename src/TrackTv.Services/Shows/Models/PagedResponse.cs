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

        public int TotalCount { get; set; }

        private T Data { get; set; }
    }
}
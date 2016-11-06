namespace TrackTv.Services.Data
{
    using System.Linq;

    public static class QueryableExtensions
    {
        private const int MaxPageSize = 50;

        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int page, int pageSize)
        {
            if (page < 1)
            {
                page = 1;
            }

            if ((pageSize > MaxPageSize) || (pageSize < 1))
            {
                pageSize = MaxPageSize;
            }

            return queryable.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
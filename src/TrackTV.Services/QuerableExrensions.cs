namespace TrackTV.Services
{
    using System.Linq;

    public static class QueryableExtensions
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> collection, int? page, int pageSize)
        {
            if (page.HasValue)
            {
                collection = collection.Skip((page.Value - 1) * pageSize);
            }

            collection = collection.Take(pageSize);

            return collection;
        }
    }
}
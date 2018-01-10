namespace TrackTv.Data
{
    using System;
    using System.Linq;

    public static class IQuerablePaging
    {
        private const int MaxPageSize = 50;

        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int page, int pageSize)
        {
            page = Math.Max(page, 1);

            pageSize = Math.Max(pageSize, 1);

            pageSize = Math.Min(pageSize, MaxPageSize);

            return queryable.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
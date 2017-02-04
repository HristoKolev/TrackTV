using System.Collections.Generic;

namespace TrackTv.Services.Data
{
    using System;
    using System.Linq;

    public static class EnumerableExtensions
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
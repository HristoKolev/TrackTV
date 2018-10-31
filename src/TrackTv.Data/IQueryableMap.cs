namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public static class QueryableMapExtensions
    {
        public static IQueryable<TCm> Map<TPoco, TCm>(this IQueryable<TPoco> collection)
            where TPoco : IPoco<TPoco> where TCm : ICatalogModel<TPoco>
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Select(MapExpressionGenerator.GenerateMapToCm<TPoco, TCm>());
        }
    }

    public static class MapExpressionGenerator
    {
        private static readonly Dictionary<Type, object> MapToCmCache = new Dictionary<Type, object>();

        public static Expression<Func<TPoco, TCm>> GenerateMapToCm<TPoco, TCm>()
        {
            var pocoType = typeof(TPoco);

            // ReSharper disable once InconsistentlySynchronizedField
            if (MapToCmCache.ContainsKey(pocoType))
            {
                // ReSharper disable once InconsistentlySynchronizedField
                return (Expression<Func<TPoco, TCm>>)MapToCmCache[pocoType];
            }

            var cmType = typeof(TCm);

            var parameter = Expression.Parameter(pocoType, "x");
            var newExpression = Expression.New(cmType);
            var bindExpressions = new List<MemberAssignment>();

            foreach (var pocoProperty in pocoType.GetProperties())
            {
                var cmProperty = cmType.GetProperty(pocoProperty.Name);
                var propertyExpression = Expression.Property(parameter, pocoProperty);
                bindExpressions.Add(Expression.Bind(cmProperty, propertyExpression));
            }

            var body = Expression.MemberInit(newExpression, bindExpressions);
            var mapToCm = Expression.Lambda<Func<TPoco, TCm>>(body, parameter);

            lock (MapToCmCache)
            {
                MapToCmCache[pocoType] = mapToCm;
            }

            return mapToCm;
        }
    }
}
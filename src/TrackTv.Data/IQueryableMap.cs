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

            return collection.Select(MapExpressionGenerator.GenerateMap<TPoco, TCm>());
        }
    }

    public static class MapExpressionGenerator
    {
        public static Expression<Func<TPoco, TCm>> GenerateMap<TPoco, TCm>()
        {
            var parameter = Expression.Parameter(typeof(TPoco), "x");
            var newExpression = Expression.New(typeof(TCm));

            var bindExpressions = new List<MemberAssignment>();

            foreach (var pocoProperty in typeof(TPoco).GetProperties())
            {
                var cmProperty = typeof(TCm).GetProperty(pocoProperty.Name);
                var propertyExpression = Expression.Property(parameter, pocoProperty);
                bindExpressions.Add(Expression.Bind(cmProperty, propertyExpression));
            }

            var body = Expression.MemberInit(newExpression, bindExpressions);

            return Expression.Lambda<Func<TPoco, TCm>>(body, parameter);
        }
    }
}
namespace TrackTv.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class QueryableExtensions
    {
        public static IQueryable<TCm> SelectCm<TPoco, TCm>(this IQueryable<TPoco> collection)
            where TCm : ICatalogModel<TPoco>
            where TPoco : IPoco<TPoco>
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.Select(MapExpressionGenerator.MapToCmExpression<TPoco, TCm>());
        }

        public static IOrderedQueryable<TPoco> OrderByPrimaryKey<TPoco>(this IQueryable<TPoco> collection)
            where TPoco : IPoco<TPoco>, new()
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.OrderBy(OrderExpressionGenerator.OrderByPrimaryKeyExpression<TPoco, int>());
        }

        public static IOrderedQueryable<TPoco> OrderByPrimaryKeyDescending<TPoco>(this IQueryable<TPoco> collection)
            where TPoco : IPoco<TPoco>, new()
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            return collection.OrderByDescending(OrderExpressionGenerator.OrderByPrimaryKeyExpression<TPoco, int>());
        }

        public static IQueryable<TPoco> Filter<TFilter, TPoco>(this IQueryable<TPoco> collection, TFilter filter)
            where TFilter : IFilterModel<TPoco>
            where TPoco : IPoco<TPoco>
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var expr = FilterExpressionGenerator.CreateFilterExpression<TPoco>(filter);

            if (expr == null)
            {
                return collection;
            }

            return collection.Where(expr);
        }

        public static IQueryable<TPoco> Page<TPoco>(this IQueryable<TPoco> collection, int page, int pageSize)
            where TPoco : IPoco<TPoco>
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            page = Math.Max(page, 1);
            pageSize = Math.Max(pageSize, 1);

            return collection.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }

    public static class MapExpressionGenerator
    {
        /// <summary>
        /// Cache dictionary for objects generated with the `MapToCmExpression` method.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, object> MapToCmExpressionCache = new ConcurrentDictionary<Type, object>();

        public static Expression<Func<TPoco, TCm>> MapToCmExpression<TPoco, TCm>()
        {
            Expression<Func<TPoco, TCm>> ValueFactory(Type pocoType)
            {
                var cmType = typeof(TCm);

                var parameter = Expression.Parameter(pocoType, "x");
                var newExpression = Expression.New(cmType);
                var bindExpressions = new List<MemberAssignment>();

                foreach (var pocoProperty in pocoType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var cmProperty = cmType.GetProperty(pocoProperty.Name);
                    var propertyExpression = Expression.Property(parameter, pocoProperty);
                    bindExpressions.Add(Expression.Bind(cmProperty, propertyExpression));
                }

                var body = Expression.MemberInit(newExpression, bindExpressions);

                return Expression.Lambda<Func<TPoco, TCm>>(body, parameter);
            }

            return (Expression<Func<TPoco, TCm>>)MapToCmExpressionCache.GetOrAdd(typeof(TPoco), ValueFactory);
        }
    }

    public static class OrderExpressionGenerator
    {
        /// <summary>
        /// Cache dictionary for objects generated with the `OrderByPrimaryKeyExpression` method.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, object> OrderByPrimaryKeyExpressionCache =
            new ConcurrentDictionary<Type, object>();

        public static Expression<Func<TPoco, TKey>> OrderByPrimaryKeyExpression<TPoco, TKey>()
            where TPoco : IPoco<TPoco>, new()
        {
            Expression<Func<TPoco, TKey>> ValueFactory(Type type)
            {
                var metadata = DbCodeGenerator.GetMetadata<TPoco>();

                ParameterExpression parameter = Expression.Parameter(typeof(TPoco), "x");
                var propertyInfo = typeof(TPoco).GetProperty(metadata.PrimaryKeyPropertyName);

                Expression propertyExpression = Expression.Property(parameter, propertyInfo);

                return Expression.Lambda<Func<TPoco, TKey>>(propertyExpression, parameter);
            }

            return (Expression<Func<TPoco, TKey>>)OrderByPrimaryKeyExpressionCache.GetOrAdd(typeof(TPoco), ValueFactory);
        }
    }

    public static class FilterExpressionGenerator
    {
        private static readonly MethodInfo StringStartsWithMethod = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) });

        private static readonly MethodInfo StringEndsWithMethod = typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) });

        private static readonly MethodInfo StringContainsMethod = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });

        private static readonly MethodInfo EnumerableContainsMethod = typeof(Enumerable)
                                                                      .GetMethods()
                                                                      .First(x => x.Name == nameof(Enumerable.Contains) && x.GetParameters().Length == 2);

        public static Expression<Func<TPoco, bool>> CreateFilterExpression<TPoco>(object filter)
        {
            var expressions = new List<Expression<Func<TPoco, bool>>>();

            foreach (var propertyInfo in filter.GetType().GetProperties().Where(x => x.GetCustomAttribute<FilterOperatorAttribute>() != null))
            {
                var filterAttribute = propertyInfo.GetCustomAttribute<FilterOperatorAttribute>();

                QueryOperatorType queryOperatorType = filterAttribute.QueryOperatorType;
                string propertyName = filterAttribute.PropertyName;
                object propertyValue = propertyInfo.GetValue(filter);

                // Properties with null values will not be used when filtering.
                if (propertyValue == null)
                {
                    continue;
                }

                // Create expression for the current property
                var expression = CreatePropertyExpression<TPoco>(propertyName, queryOperatorType, propertyValue);

                expressions.Add(expression);
            }

            if (!expressions.Any())
            {
                return null;
            }

            if (expressions.Count == 1)
            {
                return expressions.First();
            }

            var result = expressions[0];

            // this combines all the expressions into one.
            foreach (var expression in expressions.Skip(1))
            {
                var invoke = Expression.Invoke(expression, result.Parameters);

                var andAlso = Expression.AndAlso(result.Body, invoke);

                result = Expression.Lambda<Func<TPoco, bool>>(andAlso, result.Parameters);
            }

            return result;
        }

        // ReSharper disable once CyclomaticComplexity
        private static Expression<Func<TPoco, bool>> CreatePropertyExpression<TPoco>(string memberName, QueryOperatorType queryOperatorType, object value)
        {
            var parameter = Expression.Parameter(typeof(TPoco), "x");
            Expression member = Expression.PropertyOrField(parameter, memberName);
            Expression memberValue = Expression.Constant(value);

            bool isMemberTypeNullabe = member.Type.IsGenericType && member.Type.GetGenericTypeDefinition() == typeof(Nullable<>);

            if (isMemberTypeNullabe
                && queryOperatorType != QueryOperatorType.IsIn
                && queryOperatorType != QueryOperatorType.IsNotIn
                && queryOperatorType != QueryOperatorType.IsNull
                && queryOperatorType != QueryOperatorType.IsNotNull)
            {
                memberValue = Expression.Convert(memberValue, member.Type);
            }

            Expression expression;

            switch (queryOperatorType)
            {
                case QueryOperatorType.Equal :
                {
                    expression = Expression.Equal(member, memberValue);
                    break;
                }
                case QueryOperatorType.NotEqual :
                {
                    expression = Expression.NotEqual(member, memberValue);
                    break;
                }
                case QueryOperatorType.LessThan :
                {
                    expression = Expression.LessThan(member, memberValue);
                    break;
                }
                case QueryOperatorType.LessThanOrEqual :
                {
                    expression = Expression.LessThanOrEqual(member, memberValue);
                    break;
                }
                case QueryOperatorType.GreaterThan :
                {
                    expression = Expression.GreaterThan(member, memberValue);
                    break;
                }
                case QueryOperatorType.GreaterThanOrEqual :
                {
                    expression = Expression.GreaterThanOrEqual(member, memberValue);
                    break;
                }
                case QueryOperatorType.StartsWith :
                {
                    expression = Expression.Call(member, StringStartsWithMethod, memberValue);
                    break;
                }
                case QueryOperatorType.DoesNotStartWith :
                {
                    expression = Expression.Not(Expression.Call(member, StringStartsWithMethod, memberValue));
                    break;
                }
                case QueryOperatorType.EndsWith :
                {
                    expression = Expression.Call(member, StringEndsWithMethod, memberValue);
                    break;
                }
                case QueryOperatorType.DoesNotEndWith :
                {
                    expression = Expression.Not(Expression.Call(member, StringEndsWithMethod, memberValue));
                    break;
                }
                case QueryOperatorType.IsIn :
                {
                    MethodInfo method;

                    if (isMemberTypeNullabe)
                    {
                        var type = Nullable.GetUnderlyingType(member.Type);
                        member = Expression.Convert(member, type);
                        method = EnumerableContainsMethod.MakeGenericMethod(type);
                    }
                    else
                    {
                        method = EnumerableContainsMethod.MakeGenericMethod(member.Type);
                    }

                    expression = Expression.Call(null, method, memberValue, member);
                    break;
                }
                case QueryOperatorType.IsNotIn :
                {
                    MethodInfo method;

                    if (isMemberTypeNullabe)
                    {
                        var type = Nullable.GetUnderlyingType(member.Type);
                        member = Expression.Convert(member, type);
                        method = EnumerableContainsMethod.MakeGenericMethod(type);
                    }
                    else
                    {
                        method = EnumerableContainsMethod.MakeGenericMethod(member.Type);
                    }

                    expression = Expression.Not(Expression.Call(null, method, memberValue, member));
                    break;
                }
                case QueryOperatorType.Contains :
                {
                    expression = Expression.Call(member, StringContainsMethod, memberValue);
                    break;
                }
                case QueryOperatorType.DoesNotContain :
                {
                    expression = Expression.Not(Expression.Call(member, StringContainsMethod, memberValue));
                    break;
                }
                case QueryOperatorType.IsNull :
                {
                    expression = Expression.Equal(member, Expression.Constant(null, member.Type));
                    break;
                }
                case QueryOperatorType.IsNotNull :
                {
                    expression = Expression.NotEqual(member, Expression.Constant(null, member.Type));
                    break;
                }
                default :
                {
                    throw new ArgumentOutOfRangeException(nameof(queryOperatorType), queryOperatorType, null);
                }
            }

            return Expression.Lambda<Func<TPoco, bool>>(expression, parameter);
        }
    }
}
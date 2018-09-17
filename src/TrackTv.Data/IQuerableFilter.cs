namespace TrackTv.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class QueryableExtensions
    {
        public static IQueryable<TModel> Filter<TFilter, TModel>(this IQueryable<TModel> collection, TFilter filter)
            where TFilter : class 
            where TModel : class
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var expr = ExpressionGenerator.CreateFilterExpression<TModel>(filter);

            if (expr == null)
            {
                return collection;
            }

            return collection.Where(expr);
        }
    }

    public class QueryableFilterException : Exception
    {
        public QueryableFilterException(string message) : base(message)
        {
        }

        public QueryableFilterException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public static class ExpressionGenerator
    {
        private static MethodInfo StringContainsMethod { get; } = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });

        private static MethodInfo StringEndsWithMethod { get; } = typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) });

        private static MethodInfo StringStartsWithMethod { get; } = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) });

        public static Expression<Func<T, bool>> CreateFilterExpression<T>(object filter)
            where T : class
        {
            // Make sure the filter is valid.
            // Throws an exception if there is something wrong with the filter.
            ValidateFilter(filter.GetType());

            var expressions = new List<Expression<Func<T, bool>>>();

            foreach (var propertyInfo in filter.GetType().GetProperties())
            {
                var filterAttribute = propertyInfo.GetCustomAttribute<FilterOperatorAttribute>();

                QueryOperatorType queryOperatorType = filterAttribute?.QueryOperatorType ?? QueryOperatorType.Equal;
                string propertyName = filterAttribute?.PropertyName ?? propertyInfo.Name;
                object propertyValue = propertyInfo.GetValue(filter);

                // Properties with null values will not be used when filtering.
                if (propertyValue == null)
                {
                    continue;
                }

                // Create expression for the current property
                var expression = CreatePropertyExpression<T>(propertyName, queryOperatorType, propertyValue);

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

                result = Expression.Lambda<Func<T, bool>>(andAlso, result.Parameters);
            }

            return result;
        }

        private static Expression<Func<T, bool>> CreatePropertyExpression<T>(string memberName, QueryOperatorType queryOperatorType, object value)
            where T : class
        {
            var parameter = Expression.Parameter(typeof(T), "t");
            var member = Expression.PropertyOrField(parameter, memberName);

            ConstantExpression memberValue;

            // When these query types are used, the member type must be a collection.
            if (queryOperatorType == QueryOperatorType.IsIn || queryOperatorType == QueryOperatorType.IsNotIn)
            {
                memberValue = Expression.Constant(value, typeof(List<>).MakeGenericType(member.Type));
            }
            else
            {
                memberValue = Expression.Constant(value, member.Type);
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
                    Type type = member.Type;
                    expression = Expression.Call(memberValue, typeof(List<>).MakeGenericType(type).GetMethod("Contains", new[] { type }), member);
                    break;
                }
                case QueryOperatorType.IsNotIn :
                {
                    Type type = member.Type;
                    expression = Expression.Not(Expression.Call(memberValue, typeof(List<>).MakeGenericType(type).GetMethod("Contains", new[] { type }), member));
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

            return Expression.Lambda<Func<T, bool>>(expression, parameter);
        }

        // ReSharper disable once CyclomaticComplexity
        private static void ValidateFilter(Type filterType)
        {
            var propertyInfos = filterType.GetProperties();

            foreach (var propertyInfo in propertyInfos)
            {
                // All properties should be nullable. 
                // When a property has a value of null,
                // it means that it should not be used in the filtering logic.
                if (propertyInfo.PropertyType.IsValueType && propertyInfo.PropertyType.GetGenericTypeDefinition() != typeof(Nullable<>))
                {
                    string message = "All filter properties must be reference types (use T?).\r\n";
                    message += $"FilterType: {filterType.Name};\r\n ";
                    message += $"PropertyName: {propertyInfo.Name};\r\n";
                    message += $"PropertyType: {propertyInfo.PropertyType.Name}\r\n";

                    throw new QueryableFilterException(message);
                }

                var queryFilterAttribute = propertyInfo.GetCustomAttribute<FilterOperatorAttribute>();

                // Get the query type from the QueryFilterAttribute. 
                // If such attribute is not provided the query type defaults to QueryType.Equal.
                var queryType = queryFilterAttribute?.QueryOperatorType ?? QueryOperatorType.Equal;

                // Get the property name from the QueryFilterAttribute. 
                // If such attribute is not provided the property name defaults to the declared property name.
                var targetPropertyName = queryFilterAttribute?.PropertyName ?? propertyInfo.Name;

                // The target property name must exist on the filter type.
                if (propertyInfos.All(info => info.Name != targetPropertyName))
                {
                    throw new QueryableFilterException($"Target property {targetPropertyName} does not exists on type {filterType.Name}."
                                                    + $"FilterType: {filterType.Name};\r\n PropertyName: {propertyInfo.Name} \r\n PropertyType: {propertyInfo.PropertyType}");
                }

                switch (queryType)
                {
                    // These can't be used with collection types.
                    case QueryOperatorType.Equal :
                    case QueryOperatorType.NotEqual :
                    {
                        if (propertyInfo.PropertyType != typeof(string) && typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                        {
                            throw new QueryableFilterException(
                                $"Property with QueryType: {queryType} should not have a type that is a collection.\r\n"
                                + $"FilterType: {filterType.Name};\r\n PropertyName: {propertyInfo.Name} \r\n PropertyType: {propertyInfo.PropertyType}");
                        }

                        break;
                    }

                    // These types are only used for numeric or DateTime types.
                    case QueryOperatorType.LessThan :
                    case QueryOperatorType.LessThanOrEqual :
                    case QueryOperatorType.GreaterThan :
                    case QueryOperatorType.GreaterThanOrEqual :
                    {
                        var comparableTypes = new[]
                        {
                            typeof(byte),
                            typeof(sbyte),
                            typeof(short),
                            typeof(ushort),
                            typeof(ushort),
                            typeof(int),
                            typeof(uint),
                            typeof(long),
                            typeof(ulong),
                            typeof(DateTime)
                        };

                        if (!comparableTypes.Contains(propertyInfo.PropertyType))
                        {
                            throw new QueryableFilterException(
                                $"Property with QueryType: {queryType} should not have a type of {propertyInfo.PropertyType} .\r\n"
                                + $"FilterType: {filterType.Name};\r\n PropertyName: {propertyInfo.Name} \r\n PropertyType: {propertyInfo.PropertyType}");
                        }

                        break;
                    }

                    // These types cn only be used with strings.
                    case QueryOperatorType.StartsWith :
                    case QueryOperatorType.DoesNotStartWith :
                    case QueryOperatorType.EndsWith :
                    case QueryOperatorType.DoesNotEndWith :
                    case QueryOperatorType.Contains :
                    case QueryOperatorType.DoesNotContain :
                    {
                        if (propertyInfo.PropertyType != typeof(string))
                        {
                            throw new QueryableFilterException(
                                $"Property with QueryType: {queryType} should not be used with any type other than string.\r\n"
                                + $"FilterType: {filterType.Name};\r\n PropertyName: {propertyInfo.Name} \r\n PropertyType: {propertyInfo.PropertyType}");
                        }

                        break;
                    }

                    // Only boolean values allowed here.
                    case QueryOperatorType.IsNull :
                    case QueryOperatorType.IsNotNull :
                    {
                        if (propertyInfo.PropertyType != typeof(bool))
                        {
                            throw new QueryableFilterException(
                                $"Property with QueryType: {queryType} should not be used with any type other than boolean.\r\n"
                                + $"FilterType: {filterType.Name};\r\n PropertyName: {propertyInfo.Name} \r\n PropertyType: {propertyInfo.PropertyType}");
                        }

                        break;
                    }

                    // These types can only be used with collection types.
                    case QueryOperatorType.IsIn :
                    case QueryOperatorType.IsNotIn :
                    {
                        if (!typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                        {
                            throw new QueryableFilterException(
                                $"Property with QueryType: {queryType} should only be used with collections.\r\n"
                                + $"FilterType: {filterType.Name};\r\n PropertyName: {propertyInfo.Name} \r\n PropertyType: {propertyInfo.PropertyType}");
                        }

                        break;
                    }

                    default :
                        throw new ArgumentOutOfRangeException(nameof(queryType), queryType, null);
                }
            }
        }
    }
}
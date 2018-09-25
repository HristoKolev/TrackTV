namespace TrackTv.Data
{
    using System;
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

    public static class ExpressionGenerator
    {
        public static Expression<Func<T, bool>> CreateFilterExpression<T>(object filter)
            where T : class
        {
            var expressions = new List<Expression<Func<T, bool>>>();

            foreach (var propertyInfo in filter.GetType().GetProperties())
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

        // ReSharper disable once CyclomaticComplexity
        private static Expression<Func<T, bool>> CreatePropertyExpression<T>(string memberName, QueryOperatorType queryOperatorType, object value)
            where T : class
        {
            var parameter = Expression.Parameter(typeof(T), "t");
            Expression member = Expression.PropertyOrField(parameter, memberName);
            Expression memberValue = Expression.Constant(value);

            if (member.Type.IsGenericType 
                && member.Type.GetGenericTypeDefinition() == typeof(Nullable<>)
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
                    var method = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) });
                    expression = Expression.Call(member, method, memberValue);
                    break;
                }
                case QueryOperatorType.DoesNotStartWith :
                {
                    var method = typeof(string).GetMethod(nameof(string.StartsWith), new[] { typeof(string) });
                    expression = Expression.Not(Expression.Call(member, method, memberValue));
                    break;
                }
                case QueryOperatorType.EndsWith :
                {
                    var method = typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) });
                    expression = Expression.Call(member, method, memberValue);
                    break;
                }
                case QueryOperatorType.DoesNotEndWith :
                {
                    var method = typeof(string).GetMethod(nameof(string.EndsWith), new[] { typeof(string) });
                    expression = Expression.Not(Expression.Call(member, method, memberValue));
                    break;
                }
                case QueryOperatorType.IsIn :
                {
                    var genericMethod = typeof(Enumerable)
                                        .GetMethods()
                                        .First(x => x.Name == nameof(Enumerable.Contains) && x.GetParameters().Length == 2);

                    MethodInfo method;

                    if (member.Type.IsGenericType && member.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        var type = Nullable.GetUnderlyingType(member.Type);

                        member = Expression.Convert(member, type);
                        method = genericMethod.MakeGenericMethod(type);
                    }
                    else
                    {
                        method = genericMethod.MakeGenericMethod(member.Type);
                    }

                    expression = Expression.Call(null, method, memberValue, member);
                    break;
                }
                case QueryOperatorType.IsNotIn :
                {
                    var genericMethod = typeof(Enumerable)
                                        .GetMethods()
                                        .First(x => x.Name == nameof(Enumerable.Contains) && x.GetParameters().Length == 2);

                    MethodInfo method;

                    if (member.Type.IsGenericType && member.Type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        var type = Nullable.GetUnderlyingType(member.Type);

                        member = Expression.Convert(member, type);
                        method = genericMethod.MakeGenericMethod(type);
                    }
                    else
                    {
                        method = genericMethod.MakeGenericMethod(member.Type);
                    }

                    expression = Expression.Not(Expression.Call(null, method, memberValue, member));
                    break;
                }
                case QueryOperatorType.Contains :
                {
                    var method = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
                    expression = Expression.Call(member, method, memberValue);
                    break;
                }
                case QueryOperatorType.DoesNotContain :
                {
                    var method = typeof(string).GetMethod(nameof(string.Contains), new[] { typeof(string) });
                    expression = Expression.Not(Expression.Call(member, method, memberValue));
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
    }
}
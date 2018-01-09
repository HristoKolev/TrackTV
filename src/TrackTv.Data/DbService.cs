namespace TrackTv.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Threading.Tasks;

    using LinqToDB;
    using LinqToDB.Data;
    using LinqToDB.DataProvider;
    using LinqToDB.Mapping;

    public partial class DbService
    {
        public DbService(IDbConnection dbConnection, IDataProvider dataProvider)
        {
            this.DataConnection = new DataConnection(dataProvider, dbConnection);
        }

        private DataConnection DataConnection { get; }

        public Task DeleteAsync<TPoco>(TPoco poco)
            where TPoco : IPoco =>
            this.DataConnection.DeleteAsync(poco);

        public Task<int> InsertAsync<TPoco>(TPoco poco)
            where TPoco : IPoco =>
            this.DataConnection.InsertWithInt32IdentityAsync(poco);

        public async Task<int> SaveAsync<TPoco>(TPoco poco)
            where TPoco : IPoco
        {
            int pkValue = ReflectionHelpers.GetPrimaryKey(poco);

            if (pkValue == default)
            {
                return await this.InsertAsync(poco).ConfigureAwait(false);
            }

            await this.UpdateAsync(poco).ConfigureAwait(false);

            return pkValue;
        }

        public Task UpdateAsync<TPoco>(TPoco poco)
            where TPoco : IPoco =>
            this.DataConnection.UpdateAsync(poco);
    }

    public interface IPoco
    {
    }

    public class ReflectionHelpers
    {
        private static readonly ConcurrentDictionary<Type, object> ExpressionCache = new ConcurrentDictionary<Type, object>();

        public static int GetPrimaryKey<TPoco>(TPoco poco)
            where TPoco : IPoco
        {
            var expression = ExpressionCache.GetOrAdd(typeof(TPoco), BuildExpression);

            return ((Func<TPoco, int>)expression)(poco);
        }

        private static object BuildExpression(Type type)
        {
            var property = GetPrimaryKeyProperty(type);

            var parameter = Expression.Parameter(type, "x");

            var lambda = typeof(Expression).GetMethods()
                                           .First(x => x.Name == "Lambda" && x.GetParameters().Length == 2
                                                                          && x.GetGenericArguments().Length == 1)
                                           .MakeGenericMethod(typeof(Func<,>).MakeGenericType(type, property.PropertyType));

            var expression = lambda.Invoke(null, new object[]
            {
                Expression.Property(parameter, property),
                new[]
                {
                    parameter
                }
            });

            var compile = expression.GetType().GetMethods().First(x => x.Name == "Compile" && x.GetParameters().Length == 0);

            return compile.Invoke(expression, Array.Empty<object>());
        }

        private static PropertyInfo GetPrimaryKeyProperty(Type pocoType)
        {
            PropertyInfo primaryKeyProperty = null;

            foreach (var propertyInfo in pocoType.GetProperties())
            {
                var primaryKeyAttribute = propertyInfo.GetCustomAttribute<PrimaryKeyAttribute>();

                if (primaryKeyAttribute != null)
                {
                    primaryKeyProperty = propertyInfo;
                    break;
                }
            }

            if (primaryKeyProperty == null)
            {
                throw new ApplicationException(
                    $"The poco type '{pocoType.Name}' does not have a property with {typeof(PrimaryKeyAttribute).Name}.");
            }

            return primaryKeyProperty;
        }
    }
}
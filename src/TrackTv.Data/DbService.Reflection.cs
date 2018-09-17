namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public partial class DbService
    {
        private static Action<TObject, object> GetSetter<TObject>(string propertyName)
        {
            var objectType = typeof(TObject);

            var propertyInfo = objectType.GetProperty(propertyName);

            if (propertyInfo == null)
            {
                throw new ApplicationException("No property forund.");
            }

            var instanceParameter = Expression.Parameter(objectType);
            var valueParameter = Expression.Parameter(typeof(object), propertyName);
            var castExpression = Expression.Convert(valueParameter, propertyInfo.PropertyType);

            var propertySetterExpression = Expression.Property(instanceParameter, propertyName);

            var result = Expression.Lambda<Action<TObject, object>>(Expression.Assign(propertySetterExpression, castExpression),
                                                                    instanceParameter, valueParameter);

            return result.Compile();
        }

        private static Dictionary<string, Action<T, object>> GetSetterMap<T>(string tableName)
        {
            return TableToPropertyMap[tableName].ToDictionary(pair => pair.Key, pair => GetSetter<T>(pair.Value));
        }

        private static TableMetadataModel<T> GetMetadata<T>()
            where T : IPoco<T>
        {
            return (TableMetadataModel<T>)MetadataByPocoType[typeof(T)];
        }
    }
}
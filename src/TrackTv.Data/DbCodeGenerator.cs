namespace TrackTv.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;
    using System.Text;

    using Npgsql;

    using NpgsqlTypes;

    public static class DbCodeGenerator
    {
        /// <summary>
        /// Cache dictionary for objects generated with the `GenerateSetters` method.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, object> GenerateSettersCache = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// Cache dictionary for objects generated with the `GenerateGetters` method.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, object> GenerateGettersCache = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// Cache dictionary for objects generated with the `GetMetadata` method.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, object> GetMetadataCache = new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// A helper method that takes care of setting the metadata for a DynamicMethod
        /// that allows you to work with the ILGenerator without needing to do any other work
        /// in order to have a working method.
        /// </summary>
        public static T GenerateMethod<T>(Action<ILGenerator> generate)
            where T : Delegate
        {
            var type = typeof(T);

            // all delegates have an `Invoke` method.
            var invoke = type.GetMethod("Invoke");

            var returnType = invoke.ReturnType;
            var parameterTypes = invoke.GetParameters().Select(x => x.ParameterType).ToArray();

            var dynamicMethod = new DynamicMethod("dynamic method", returnType, parameterTypes);

            var il = dynamicMethod.GetILGenerator();

            generate(il);

            var method = dynamicMethod.CreateDelegate(type);

            return (T)method;
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        private static Func<T, object> GetGetter<T>(string propertyName)
        {
            var instanceType = typeof(T);

            var property = instanceType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            if (!property.IsAutoImplemented())
            {
                throw new ApplicationException($"The propety `{property.Name}` of type `{instanceType.Name}` is not autoimplemented.");
            }

            return GenerateMethod<Func<T, object>>(il =>
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldfld, property.GetBackingField());

                if (property.PropertyType.IsValueType)
                {
                    il.Emit(OpCodes.Box, property.PropertyType);
                }
                
                il.Emit(OpCodes.Ret);
            });
        }

        private static Action<T, object> GetSetter<T>(string propertyName)
        {
            var instanceType = typeof(T);

            var property = instanceType.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance);

            if (!property.IsAutoImplemented())
            {
                throw new ApplicationException($"The propety `{property.Name}` of type `{instanceType.Name}` is not autoimplemented.");
            }

            return GenerateMethod<Action<T, object>>(il =>
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldarg_1);

                if (property.PropertyType.IsValueType)
                {
                    il.Emit(OpCodes.Unbox, property.PropertyType);
                    il.Emit(OpCodes.Ldobj, property.PropertyType);
                }

                il.Emit(OpCodes.Stfld, property.GetBackingField());
                il.Emit(OpCodes.Ret);
            });
        }

        public static Func<T, T> GetClone<T>()
        {
            var instanceType = typeof(T);
            
            return GenerateMethod<Func<T, T>>(il =>
            {
                var cloneObject = il.DeclareLocal(instanceType);

                il.Emit(OpCodes.Newobj, instanceType.GetConstructor(Array.Empty<Type>()));
                il.Emit(OpCodes.Stloc, cloneObject);

                foreach (var fieldInfo in instanceType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public))
                {
                    il.Emit(OpCodes.Ldloc, cloneObject);
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Ldfld, fieldInfo);
                    il.Emit(OpCodes.Stfld, fieldInfo);
                }

                il.Emit(OpCodes.Ldloc, cloneObject);
                il.Emit(OpCodes.Ret);
            });
        }

        public static Func<TPoco, NpgsqlParameter[]> GetGenerateParameters<TPoco>(TableMetadataModel<TPoco> metadata)
            where TPoco : IPoco<TPoco>
        {
            var pocoType = typeof(TPoco);
            var parameterType = typeof(NpgsqlParameter);
      
            var nonPrimaryKeyColumns = metadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();

            return GenerateMethod<Func<TPoco, NpgsqlParameter[]>>(il =>
            {
                il.DeclareLocal(typeof(NpgsqlParameter[]));
                
                // create the array and save it in local0
                il.Emit(OpCodes.Ldc_I4, nonPrimaryKeyColumns.Length);
                il.Emit(OpCodes.Newarr, parameterType);
                il.Emit(OpCodes.Stloc_0);

                int i = 0;

                foreach (var column in nonPrimaryKeyColumns)
                {
                    var property = pocoType.GetProperty(column.PropertyName);

                    // load the array and the index
                    il.Emit(OpCodes.Ldloc_0);
                    il.Emit(OpCodes.Ldc_I4, i++);

                    EmitNpgsqlParameter(il, property, column.NpgsDataType, () => il.Emit(OpCodes.Ldarg_0));

                    // set the parameter to the appropriate index
                    il.Emit(OpCodes.Stelem, parameterType);
                }

                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ret);
            });
        }

        public static Func<TPoco, ValueTuple<string[], NpgsqlParameter[]>> GetGetAllColumns<TPoco>(TableMetadataModel<TPoco> metadata)
            where TPoco : IPoco<TPoco>
        {
            var pocoType = typeof(TPoco);
            var parameterType = typeof(NpgsqlParameter);

            var tupleConstructor = typeof(ValueTuple<string[], NpgsqlParameter[]>)
                .GetConstructor(new[] { typeof(string[]), typeof(NpgsqlParameter[]) });

            var nonPrimaryKeyColumns = metadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();

            return GenerateMethod<Func<TPoco, ValueTuple<string[], NpgsqlParameter[]>>>(il =>
            {
                var columnNamesLocal = il.DeclareLocal(typeof(string[]));
                var parametersLocal = il.DeclareLocal(typeof(NpgsqlParameter[]));

                il.Emit(OpCodes.Ldc_I4, nonPrimaryKeyColumns.Length);
                il.Emit(OpCodes.Newarr, typeof(string));
                il.Emit(OpCodes.Stloc, columnNamesLocal);

                il.Emit(OpCodes.Ldc_I4, nonPrimaryKeyColumns.Length);
                il.Emit(OpCodes.Newarr, parameterType);
                il.Emit(OpCodes.Stloc, parametersLocal);

                int i = 0;

                foreach (var column in nonPrimaryKeyColumns)
                {
                    // add the column name to the array
                    il.Emit(OpCodes.Ldloc, columnNamesLocal);
                    il.Emit(OpCodes.Ldc_I4, i);
                    il.Emit(OpCodes.Ldstr, column.ColumnName);
                    il.Emit(OpCodes.Stelem, typeof(string));

                    // add the parameter to the array
                    il.Emit(OpCodes.Ldloc, parametersLocal);
                    il.Emit(OpCodes.Ldc_I4, i);
                    EmitNpgsqlParameter(il, pocoType.GetProperty(column.PropertyName), column.NpgsDataType, () => il.Emit(OpCodes.Ldarg_0));
                    il.Emit(OpCodes.Stelem, parameterType);

                    i++;
                }

                il.Emit(OpCodes.Ldloc, columnNamesLocal);
                il.Emit(OpCodes.Ldloc, parametersLocal);

                il.Emit(OpCodes.Newobj, tupleConstructor);

                il.Emit(OpCodes.Ret);
            });
        }

        public static Func<TPoco, TPoco, ValueTuple<List<string>, List<NpgsqlParameter>>> GetGetColumnChanges<TPoco>(TableMetadataModel<TPoco> metadata)
            where TPoco : IPoco<TPoco>
        {
            var pocoType = typeof(TPoco);

            var tupleConstructor = typeof(ValueTuple<List<string>, List<NpgsqlParameter>>)
                .GetConstructor(new[] { typeof(List<string>), typeof(List<NpgsqlParameter>) });

            var nonPrimaryKeyColumns = metadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();

            var stringListType = typeof(List<string>);
            var parameterListType = typeof(List<NpgsqlParameter>);

            return GenerateMethod<Func<TPoco, TPoco, ValueTuple<List<string>, List<NpgsqlParameter>>>>(il =>
            {
                // create the columnNames list.
                var columnNamesLocal = il.DeclareLocal(stringListType);
                il.Emit(OpCodes.Newobj, stringListType.GetConstructor(Array.Empty<Type>()));
                il.Emit(OpCodes.Stloc, columnNamesLocal);

                // create the parameter list.
                var parameterListLocal = il.DeclareLocal(parameterListType);
                il.Emit(OpCodes.Newobj, parameterListType.GetConstructor(Array.Empty<Type>()));
                il.Emit(OpCodes.Stloc, parameterListLocal);

                foreach (var column in nonPrimaryKeyColumns)
                {
                    var notChangedEndif = il.DefineLabel();

                    var property = pocoType.GetProperty(column.PropertyName);

                    if (IsNullableType(property.PropertyType))
                    {
                        var nullableType = property.PropertyType;
                        var getValueOrDefault = nullableType.GetMethod("GetValueOrDefault", Array.Empty<Type>());
                        var hasValue = nullableType.GetProperty("HasValue");
                        
                        // compare the HasValue properties
                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Ldflda, property.GetBackingField());
                        il.Emit(OpCodes.Call, hasValue.GetMethod);

                        il.Emit(OpCodes.Ldarg_1);
                        il.Emit(OpCodes.Ldflda, property.GetBackingField());
                        il.Emit(OpCodes.Call, hasValue.GetMethod);

                        il.Emit(OpCodes.Ceq);

                        // compare the GetValueOrDefault() result.
                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Ldflda, property.GetBackingField());
                        il.Emit(OpCodes.Call, getValueOrDefault);

                        il.Emit(OpCodes.Ldarg_1);
                        il.Emit(OpCodes.Ldflda, property.GetBackingField());
                        il.Emit(OpCodes.Call, getValueOrDefault);

                        var underlyingType = Nullable.GetUnderlyingType(nullableType);

                        var eqOperator = underlyingType.GetMethod("op_Equality");

                        if (eqOperator != null)
                        {
                            il.Emit(OpCodes.Call, eqOperator);
                        }
                        else
                        {
                            il.Emit(OpCodes.Ceq);
                        }

                        il.Emit(OpCodes.And);
                    }
                    else
                    {
                        // simple equality check

                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Ldfld, property.GetBackingField());

                        il.Emit(OpCodes.Ldarg_1);
                        il.Emit(OpCodes.Ldfld, property.GetBackingField());

                        var eqOperator = property.PropertyType.GetMethod("op_Equality");

                        if (eqOperator!= null)
                        {
                            il.Emit(OpCodes.Call, eqOperator);
                        }
                        else
                        {
                            il.Emit(OpCodes.Ceq);
                        }
                    }

                    il.Emit(OpCodes.Brtrue_S, notChangedEndif);

                    // Add the column name
                    il.Emit(OpCodes.Ldloc, columnNamesLocal);
                    il.Emit(OpCodes.Ldstr, column.ColumnName);
                    il.Emit(OpCodes.Call, stringListType.GetMethod("Add"));

                    il.Emit(OpCodes.Ldloc, parameterListLocal); // the parameter list
                    EmitNpgsqlParameter(il, property, column.NpgsDataType, () => il.Emit(OpCodes.Ldarg_1));
                    il.Emit(OpCodes.Call, parameterListType.GetMethod("Add"));

                    il.MarkLabel(notChangedEndif);
                }

                il.Emit(OpCodes.Ldloc, columnNamesLocal);
                il.Emit(OpCodes.Ldloc, parameterListLocal);

                il.Emit(OpCodes.Newobj, tupleConstructor);

                il.Emit(OpCodes.Ret);
            });
        }

        public static Func<IFilterModel<TPoco>, ValueTuple<List<string>, List<NpgsqlParameter>, List<QueryOperatorType>>> GetParseFm<TPoco>(
            TableMetadataModel<TPoco> metadata,
            Type fmType) 
            where TPoco : IReadOnlyPoco<TPoco>
        {
            var tupleConstructor = typeof(ValueTuple<List<string>, List<NpgsqlParameter>, List<QueryOperatorType>>)
                .GetConstructor(new[] { typeof(List<string>), typeof(List<NpgsqlParameter>), typeof(List<QueryOperatorType>) });

            var columnNamesListType = typeof(List<string>);
            var parameterListType = typeof(List<NpgsqlParameter>);
            var operatorListType = typeof(List<QueryOperatorType>);

            return GenerateMethod<Func<IFilterModel<TPoco>, ValueTuple<List<string>, List<NpgsqlParameter>, List<QueryOperatorType>>>>(il =>
            {
                // create the columnNames list.
                var columnNamesLocal = il.DeclareLocal(columnNamesListType);
                il.Emit(OpCodes.Newobj, columnNamesListType.GetConstructor(Array.Empty<Type>()));
                il.Emit(OpCodes.Stloc, columnNamesLocal);

                // create the parameter list.
                var parameterListLocal = il.DeclareLocal(parameterListType);
                il.Emit(OpCodes.Newobj, parameterListType.GetConstructor(Array.Empty<Type>()));
                il.Emit(OpCodes.Stloc, parameterListLocal);

                // create the operator list.
                var operatorListLocal = il.DeclareLocal(operatorListType);
                il.Emit(OpCodes.Newobj, operatorListType.GetConstructor(Array.Empty<Type>()));
                il.Emit(OpCodes.Stloc, operatorListLocal);

                foreach (var property in fmType.GetProperties().Where(x => x.GetCustomAttribute<FilterOperatorAttribute>() != null))
                {
                    var includedEndif = il.DefineLabel();

                    if (IsNullableType(property.PropertyType))
                    {
                        // get the first value and store it into a local
                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Ldflda, property.GetBackingField());
                        il.Emit(OpCodes.Call, property.PropertyType.GetProperty("HasValue").GetMethod);
                    }
                    else
                    {
                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Ldfld, property.GetBackingField());
                    }

                    il.Emit(OpCodes.Brfalse_S, includedEndif);
                    // is not default(Type)

                    var attribute = property.GetCustomAttribute<FilterOperatorAttribute>();
                    var column = metadata.Columns.First(x => x.ColumnName == attribute.ColumnName);

                    // Add the column name
                    il.Emit(OpCodes.Ldloc, columnNamesLocal);
                    il.Emit(OpCodes.Ldstr, column.ColumnName);
                    il.Emit(OpCodes.Call, columnNamesListType.GetMethod("Add"));
                    
                    #region AddTheParameter

                    il.Emit(OpCodes.Ldloc, parameterListLocal); // the parameter list

                    if (attribute.QueryOperatorType == QueryOperatorType.IsNull || attribute.QueryOperatorType == QueryOperatorType.IsNotNull)
                    {
                        il.Emit(OpCodes.Ldnull);
                    }
                    else
                    {
                        var npgsDataType = property.PropertyType.IsArray ? NpgsqlDbType.Array | column.NpgsDataType : column.NpgsDataType;
                        EmitNpgsqlParameter(il, property, npgsDataType, () => il.Emit(OpCodes.Ldarg_0));
                    }

                    il.Emit(OpCodes.Call, parameterListType.GetMethod("Add"));

                    #endregion

                    // Add the operator
                    il.Emit(OpCodes.Ldloc, operatorListLocal);
                    il.Emit(OpCodes.Ldc_I4, (int)attribute.QueryOperatorType);
                    il.Emit(OpCodes.Call, operatorListType.GetMethod("Add"));

                    // is default(Type)
                    il.MarkLabel(includedEndif);
                }

                il.Emit(OpCodes.Ldloc, columnNamesLocal);
                il.Emit(OpCodes.Ldloc, parameterListLocal);
                il.Emit(OpCodes.Ldloc, operatorListLocal);

                il.Emit(OpCodes.Newobj, tupleConstructor);

                il.Emit(OpCodes.Ret);
            });
        }

        /// <summary>
        /// Emits a reference to a newly created NpgsqlParameter(Of T) object 
        /// </summary>
        /// <param name="il">The generator.</param>
        /// <param name="property">The property from which to retrieve the value for the parameter.</param>
        /// <param name="npgsDataType">The NpgsqlDbType value for the parameter.</param>
        /// <param name="loadObject">A function that emits the code required to push the clr object to the stack.</param>
        private static void EmitNpgsqlParameter(ILGenerator il, PropertyInfo property, NpgsqlDbType npgsDataType, Action loadObject)
        {
            var genericParameterType = typeof(NpgsqlParameter<>);
            var dbNullValue = typeof(DBNull).GetField("Value");

            if (!property.PropertyType.IsValueType) { 

                var nullParameterType = genericParameterType.MakeGenericType(typeof(DBNull));
                var concreteParameterType = genericParameterType.MakeGenericType(property.PropertyType);

                loadObject();
                il.Emit(OpCodes.Ldfld, property.GetBackingField());

                var ifNotNullLabel = il.DefineLabel();
                var endifLabel = il.DefineLabel();

                il.Emit(OpCodes.Brtrue_S, ifNotNullLabel);

                // if null
                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Ldc_I4, (int)npgsDataType);
                il.Emit(OpCodes.Newobj, nullParameterType.GetConstructor(new[] { typeof(string), typeof(NpgsqlDbType) }));

                il.Emit(OpCodes.Dup);

                il.Emit(OpCodes.Ldsfld, dbNullValue);
                il.Emit(OpCodes.Stfld, nullParameterType.GetProperty("TypedValue").GetBackingField());

                il.Emit(OpCodes.Br_S, endifLabel);

                il.MarkLabel(ifNotNullLabel);

                // if not null
                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Ldc_I4, (int)npgsDataType);
                il.Emit(OpCodes.Newobj, concreteParameterType.GetConstructor(new[] { typeof(string), typeof(NpgsqlDbType) }));

                il.Emit(OpCodes.Dup);

                loadObject();
                il.Emit(OpCodes.Ldfld, property.GetBackingField());
                il.Emit(OpCodes.Stfld, concreteParameterType.GetProperty("TypedValue").GetBackingField());

                il.MarkLabel(endifLabel);
            }
            else if (IsNullableType(property.PropertyType))
            {
                var nullParameterType = genericParameterType.MakeGenericType(typeof(DBNull));
                var concreteParameterType = genericParameterType.MakeGenericType(Nullable.GetUnderlyingType(property.PropertyType));

                var ifNotNullLabel = il.DefineLabel();
                var endifLabel = il.DefineLabel();

                loadObject();
                il.Emit(OpCodes.Ldflda, property.GetBackingField());
                il.Emit(OpCodes.Call, property.PropertyType.GetProperty("HasValue").GetMethod);

                il.Emit(OpCodes.Brtrue_S, ifNotNullLabel);

                // if null
                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Ldc_I4, (int)npgsDataType);
                il.Emit(OpCodes.Newobj, nullParameterType.GetConstructor(new[] { typeof(string), typeof(NpgsqlDbType) }));

                il.Emit(OpCodes.Dup);

                il.Emit(OpCodes.Ldsfld, dbNullValue);
                il.Emit(OpCodes.Stfld, nullParameterType.GetProperty("TypedValue").GetBackingField());

                il.Emit(OpCodes.Br_S, endifLabel);

                il.MarkLabel(ifNotNullLabel);

                // if not null
                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Ldc_I4, (int)npgsDataType);
                il.Emit(OpCodes.Newobj, concreteParameterType.GetConstructor(new[] { typeof(string), typeof(NpgsqlDbType) }));

                il.Emit(OpCodes.Dup);

                loadObject();
                il.Emit(OpCodes.Ldflda, property.GetBackingField());
                il.Emit(OpCodes.Call, property.PropertyType.GetMethod("GetValueOrDefault", Array.Empty<Type>()));
                il.Emit(OpCodes.Stfld, concreteParameterType.GetProperty("TypedValue").GetBackingField());

                il.MarkLabel(endifLabel);
            }
            else
            {
                var concreteParameterType = genericParameterType.MakeGenericType(property.PropertyType);

                il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Ldc_I4, (int)npgsDataType);
                il.Emit(OpCodes.Newobj, concreteParameterType.GetConstructor(new[] { typeof(string), typeof(NpgsqlDbType) }));

                il.Emit(OpCodes.Dup);

                loadObject();
                il.Emit(OpCodes.Ldfld, property.GetBackingField());
                il.Emit(OpCodes.Stfld, concreteParameterType.GetProperty("TypedValue").GetBackingField());
            }
        }

        public static Dictionary<string, Action<T, object>> GenerateSetters<T>(Func<string, string> propertyNameToColumnName)
        {
            Dictionary<string, Action<T, object>> ValueFactory(Type type)
            {
                return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                           .ToDictionary(x => propertyNameToColumnName(x.Name), x => GetSetter<T>(x.Name));
            }

            return (Dictionary<string, Action<T, object>>)GenerateGettersCache.GetOrAdd(typeof(T), ValueFactory);
        }

        public static Dictionary<string, Func<T, object>> GenerateGetters<T>(Func<string, string> propertyNameToColumnName)
        {
            Dictionary<string, Func<T, object>> ValueFactory(Type type)
            {
                return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                           .ToDictionary(x => propertyNameToColumnName(x.Name), x => GetGetter<T>(x.Name));
            }

            return (Dictionary<string, Func<T, object>>)GenerateSettersCache.GetOrAdd(typeof(T), ValueFactory);
        }

        public static Dictionary<string, Action<T, object>> GenerateSetters<T>() => GenerateSetters<T>(DefaultPropertyNameToColumnName);

        public static Dictionary<string, Func<T, object>> GenerateGetters<T>() => GenerateGetters<T>(DefaultPropertyNameToColumnName);

        /// <summary>
        /// Converts `PascalCase` property names into `snake_case` column names.
        /// The conversion happens on Uppercase letter or the string `ID`.
        /// Examples:
        /// SystemSettingName => system_setting_name
        /// SystemSettingID => system_setting_id 
        /// </summary>
        public static string DefaultPropertyNameToColumnName(string propertyName)
        {
            var sb = new StringBuilder();

            sb.Append(char.ToLower(propertyName[0]));

            for (int i = 1; i < propertyName.Length; i++)
            {
                char c = propertyName[i];

                if (c == 'I' && i + 1 < propertyName.Length && propertyName[i + 1] == 'D')
                {
                    sb.Append("_id");
                    i++;
                }
                else if (char.IsUpper(c))
                {
                    sb.Append('_');
                    sb.Append(char.ToLower(c));
                }
                else
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        public static TableMetadataModel<TPoco> GetMetadata<TPoco>() where TPoco: IReadOnlyPoco<TPoco>
        {
            object ValueFactory(Type type)
            {
                var metadataProperty = type.GetProperty("Metadata", BindingFlags.Public | BindingFlags.Static);

                // ReSharper disable once PossibleNullReferenceException
                return metadataProperty.GetValue(null);
            }

            return (TableMetadataModel<TPoco>)GetMetadataCache.GetOrAdd(typeof(TPoco), ValueFactory);
        }
    }

    public static class PropertyInfoExtensions
    {
        public static FieldInfo GetBackingField(this PropertyInfo prop)
        {
            return prop?.DeclaringType?.GetField($"<{prop.Name}>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public static bool IsAutoImplemented(this PropertyInfo prop)
        {
            return prop.GetBackingField() != null;
        }
    }
}
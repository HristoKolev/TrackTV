﻿namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    using Npgsql;

    using NpgsqlTypes;

    public class DbServiceHelpers
    {
        public static T GenerateMethod<T>(Action<ILGenerator> generate)
            where T : Delegate
        {
            var type = typeof(T);

            var invoke = type.GetMethod("Invoke");

            var returnType = invoke.ReturnType;
            var parameterTypes = invoke.GetParameters().Select(x => x.ParameterType).ToArray();

            var dynamicMethod = new DynamicMethod("dynamic method", returnType, parameterTypes);

            var il = dynamicMethod.GetILGenerator();

            generate(il);

            var method = dynamicMethod.CreateDelegate(type);

            return (T)method;
        }

        public static Dictionary<string, Action<T, object>> GetSetters<T>(IReadOnlyDictionary<string, string> map)
        {
            return map.ToDictionary(x => x.Key, x => GetSetter<T>(x.Value));
        }

        public static Dictionary<string, Func<T, object>> GetGetters<T>(IReadOnlyDictionary<string, string> map)
        {
            return map.ToDictionary(x => x.Key, x => GetGetter<T>(x.Value));
        }

        private static Func<T, object> GetGetter<T>(string propertyName)
        {
            var instanceType = typeof(T);

            var property = instanceType.GetProperty(propertyName);

            return GenerateMethod<Func<T, object>>(il =>
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Call, property.GetMethod);

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

            var property = instanceType.GetProperty(propertyName);

            return GenerateMethod<Action<T, object>>(il =>
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldarg_1);

                if (property.PropertyType.IsValueType)
                {
                    il.Emit(OpCodes.Unbox, property.PropertyType);
                    il.Emit(OpCodes.Ldobj, property.PropertyType);
                }

                il.Emit(OpCodes.Call, property.SetMethod);
                il.Emit(OpCodes.Ret);
            });
        }

        public static Func<T, T> GetClone<T>()
        {
            var instanceType = typeof(T);
            
            return GenerateMethod<Func<T, T>>(il =>
            {
                il.DeclareLocal(instanceType);
                il.DeclareLocal(instanceType);

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Stloc_0);

                il.Emit(OpCodes.Newobj, instanceType.GetConstructor(Array.Empty<Type>()));
                il.Emit(OpCodes.Stloc_1);

                foreach (var propertyInfo in instanceType.GetProperties())
                {
                    il.Emit(OpCodes.Ldloc_1);

                    il.Emit(OpCodes.Ldloc_0);
                    il.Emit(OpCodes.Call, propertyInfo.GetMethod);

                    il.Emit(OpCodes.Call, propertyInfo.SetMethod);
                }

                il.Emit(OpCodes.Ldloc_1);
                
                il.Emit(OpCodes.Ret);
            });
        }

        public static Func<TPoco, TCM> GetMapToCM<TPoco, TCM>()
        {
            var pocoType = typeof(TPoco);
            var cmType = typeof(TCM);

            return GenerateMethod<Func<TPoco, TCM>>(il =>
            {
                il.DeclareLocal(pocoType);
                il.DeclareLocal(cmType);

                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Stloc_0);

                il.Emit(OpCodes.Newobj, cmType.GetConstructor(Array.Empty<Type>()));
                il.Emit(OpCodes.Stloc_1);

                foreach (var pocoProperty in pocoType.GetProperties())
                {
                    var cmProperty = cmType.GetProperty(pocoProperty.Name);

                    il.Emit(OpCodes.Ldloc_1);

                    il.Emit(OpCodes.Ldloc_0);
                    il.Emit(OpCodes.Call, pocoProperty.GetMethod);

                    il.Emit(OpCodes.Call, cmProperty.SetMethod);
                }

                il.Emit(OpCodes.Ldloc_1);

                il.Emit(OpCodes.Ret);
            });
        }

        public static Func<TPoco, NpgsqlParameter[]> GetGenerateParameters<TPoco>(TableMetadataModel<TPoco> metadata)
            where TPoco : IPoco<TPoco>
        {
            var pocoType = typeof(TPoco);
            var parameterType = typeof(NpgsqlParameter);
            var parameterValueProperty = parameterType.GetProperty("Value");
            var dbNullValue = typeof(DBNull).GetField("Value");

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
                    // load the array and the index
                    il.Emit(OpCodes.Ldloc_0);
                    il.Emit(OpCodes.Ldc_I4, i++);

                    // create the new parameter
                    il.Emit(OpCodes.Ldnull);
                    il.Emit(OpCodes.Ldc_I4, (int)column.NpgsDataType);
                    il.Emit(OpCodes.Newobj, parameterType.GetConstructor(new[] { typeof(string), typeof(NpgsqlDbType) }));

                    // set the param.Value property to the property value
                    var property = pocoType.GetProperty(column.PropertyName);

                    il.Emit(OpCodes.Dup);
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Call, property.GetMethod);

                    if (column.ClrType.IsValueType)
                    {
                        il.Emit(OpCodes.Box, column.ClrType);
                    }
                  
                    // if its null then set it to dbnull
                    var endif = il.DefineLabel();

                    il.Emit(OpCodes.Dup);

                    il.Emit(OpCodes.Brtrue_S, endif);

                    il.Emit(OpCodes.Pop);
                    il.Emit(OpCodes.Ldsfld, dbNullValue);

                    il.MarkLabel(endif);

                    // set the Value property of the NpgsqlParameter
                    il.Emit(OpCodes.Call, parameterValueProperty.SetMethod);

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
            var parameterValueProperty = parameterType.GetProperty("Value");
            var dbNullValue = typeof(DBNull).GetField("Value");

            var tupleConstructor = typeof(ValueTuple<string[], NpgsqlParameter[]>)
                .GetConstructor(new[] { typeof(string[]), typeof(NpgsqlParameter[]) });

            var nonPrimaryKeyColumns = metadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();

            return GenerateMethod<Func<TPoco, ValueTuple<string[], NpgsqlParameter[]>>>(il =>
            {
                il.DeclareLocal(typeof(string[]));
                il.DeclareLocal(typeof(NpgsqlParameter[]));

                // create the column name array and save it in local0
                il.Emit(OpCodes.Ldc_I4, nonPrimaryKeyColumns.Length);
                il.Emit(OpCodes.Newarr, typeof(string));
                il.Emit(OpCodes.Stloc_0);

                // create the parameter array and save it in local1
                il.Emit(OpCodes.Ldc_I4, nonPrimaryKeyColumns.Length);
                il.Emit(OpCodes.Newarr, parameterType);
                il.Emit(OpCodes.Stloc_1);

                int i = 0;

                foreach (var column in nonPrimaryKeyColumns)
                {
                    // add the column name to the array
                    il.Emit(OpCodes.Ldloc_0);
                    il.Emit(OpCodes.Ldc_I4, i);
                    il.Emit(OpCodes.Ldstr, column.ColumnName);
                    il.Emit(OpCodes.Stelem, typeof(string));

                    // add the parameter to the array
                    il.Emit(OpCodes.Ldloc_1);
                    il.Emit(OpCodes.Ldc_I4, i);

                    // create the new parameter
                    il.Emit(OpCodes.Ldnull);
                    il.Emit(OpCodes.Ldc_I4, (int)column.NpgsDataType);
                    il.Emit(OpCodes.Newobj, parameterType.GetConstructor(new[] { typeof(string), typeof(NpgsqlDbType) }));

                    // set the param.Value property to the property value
                    var property = pocoType.GetProperty(column.PropertyName);

                    il.Emit(OpCodes.Dup);
                    il.Emit(OpCodes.Ldarg_0);
                    il.Emit(OpCodes.Call, property.GetMethod);

                    if (column.ClrType.IsValueType)
                    {
                        il.Emit(OpCodes.Box, column.ClrType);
                    }

                    // if its null then set it to dbnull
                    var endif = il.DefineLabel();

                    il.Emit(OpCodes.Dup);

                    il.Emit(OpCodes.Brtrue_S, endif);

                    il.Emit(OpCodes.Pop);
                    il.Emit(OpCodes.Ldsfld, dbNullValue);

                    il.MarkLabel(endif);

                    // set the Value property of the NpgsqlParameter
                    il.Emit(OpCodes.Call, parameterValueProperty.SetMethod);

                    // set the parameter to the appropriate index
                    il.Emit(OpCodes.Stelem, parameterType);

                    i++;
                }

                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldloc_1);

                il.Emit(OpCodes.Newobj, tupleConstructor);

                il.Emit(OpCodes.Ret);
            });
        }

        public static Func<TPoco, TPoco, ValueTuple<List<string>, List<NpgsqlParameter>>> GetGetColumnChanges<TPoco>(TableMetadataModel<TPoco> metadata)
            where TPoco : IPoco<TPoco>
        {
            var pocoType = typeof(TPoco);
            var parameterType = typeof(NpgsqlParameter);
            var parameterValueProperty = parameterType.GetProperty("Value");
            var dbNullValue = typeof(DBNull).GetField("Value");

            var tupleConstructor = typeof(ValueTuple<List<string>, List<NpgsqlParameter>>)
                .GetConstructor(new[] { typeof(List<string>), typeof(List<NpgsqlParameter>) });

            var nonPrimaryKeyColumns = metadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();

            var stringListType = typeof(List<string>);
            var parameterList = typeof(List<NpgsqlParameter>);

            return GenerateMethod<Func<TPoco, TPoco, ValueTuple<List<string>, List<NpgsqlParameter>>>>(il =>
            {
                il.DeclareLocal(stringListType);
                il.DeclareLocal(parameterList);

                il.Emit(OpCodes.Newobj, stringListType.GetConstructor(Array.Empty<Type>()));
                il.Emit(OpCodes.Stloc_0);

                il.Emit(OpCodes.Newobj, parameterList.GetConstructor(Array.Empty<Type>()));
                il.Emit(OpCodes.Stloc_1);

                foreach (var column in nonPrimaryKeyColumns)
                {
                    var notChangedEndif = il.DefineLabel();

                    var property = pocoType.GetProperty(column.PropertyName);

                    var isNullableType = property.PropertyType.IsGenericType
                                    && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);

                    if (isNullableType)
                    {
                        var nullableType = property.PropertyType;
                        var getValueOrDefault = nullableType.GetMethod("GetValueOrDefault", Array.Empty<Type>());
                        var hasValue = nullableType.GetProperty("HasValue");

                        // check that the HasValue and Value properties are equal.

                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Call, property.GetMethod);
                        il.Emit(OpCodes.Call, hasValue.GetMethod);

                        il.Emit(OpCodes.Ldarg_1);
                        il.Emit(OpCodes.Call, property.GetMethod);
                        il.Emit(OpCodes.Call, hasValue.GetMethod);

                        il.Emit(OpCodes.Ceq);

                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Call, property.GetMethod);
                        il.Emit(OpCodes.Call, getValueOrDefault);

                        il.Emit(OpCodes.Ldarg_1);
                        il.Emit(OpCodes.Call, property.GetMethod);
                        il.Emit(OpCodes.Call, getValueOrDefault);

                        il.Emit(OpCodes.Ceq);

                        il.Emit(OpCodes.And);
                    }
                    else
                    {
                        // simple equality check

                        il.Emit(OpCodes.Ldarg_0);
                        il.Emit(OpCodes.Call, property.GetMethod);

                        il.Emit(OpCodes.Ldarg_1);
                        il.Emit(OpCodes.Call, property.GetMethod);

                        il.Emit(OpCodes.Ceq);
                    }

                    il.Emit(OpCodes.Brtrue_S, notChangedEndif);

                    il.Emit(OpCodes.Ldloc_0);
                    il.Emit(OpCodes.Ldstr, column.ColumnName);
                    il.Emit(OpCodes.Call, stringListType.GetMethod("Add"));

                    il.Emit(OpCodes.Ldloc_1);

                    // create the new parameter
                    il.Emit(OpCodes.Ldnull);
                    il.Emit(OpCodes.Ldc_I4, (int)column.NpgsDataType);
                    il.Emit(OpCodes.Newobj, parameterType.GetConstructor(new[] { typeof(string), typeof(NpgsqlDbType) }));

                    il.Emit(OpCodes.Dup);
                    il.Emit(OpCodes.Ldarg_1);
                    il.Emit(OpCodes.Call, property.GetMethod);

                    if (column.ClrType.IsValueType)
                    {
                        il.Emit(OpCodes.Box, column.ClrType);
                    }

                    // if its null then set it to dbnull
                    var endif = il.DefineLabel();

                    il.Emit(OpCodes.Dup);

                    il.Emit(OpCodes.Brtrue_S, endif);

                    il.Emit(OpCodes.Pop);
                    il.Emit(OpCodes.Ldsfld, dbNullValue);

                    il.MarkLabel(endif);

                    // set the Value property of the NpgsqlParameter
                    il.Emit(OpCodes.Call, parameterValueProperty.SetMethod);

                    il.Emit(OpCodes.Call, parameterList.GetMethod("Add"));

                    il.MarkLabel(notChangedEndif);
                }

                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldloc_1);

                il.Emit(OpCodes.Newobj, tupleConstructor);

                il.Emit(OpCodes.Ret);
            });
        }
    }
}
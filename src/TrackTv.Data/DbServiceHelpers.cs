namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection.Emit;

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
                il.Emit(OpCodes.Box, property.PropertyType);
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

                il.Emit(OpCodes.Unbox_Any, property.PropertyType);
                
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
    }
}
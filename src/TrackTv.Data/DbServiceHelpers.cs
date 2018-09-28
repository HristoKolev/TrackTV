namespace TrackTv.Data
{
    using System;
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

        private static Action<T, object> GetSetter<T>(string propertyName)
        {
            var instanceType = typeof(T);

            return GenerateMethod<Action<T, object>>(il =>
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldarg_1);

                il.Emit(OpCodes.Unbox_Any, typeof(int));

                il.Emit(OpCodes.Call, instanceType.GetProperty(propertyName).SetMethod);
                il.Emit(OpCodes.Ret);
            });
        }
    }
}
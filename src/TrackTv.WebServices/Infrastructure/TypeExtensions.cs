namespace TrackTv.WebServices.Infrastructure
{
    using System;

    public static class TypeExtensions
    {
        public static void AssertIs(this Type concreateType, Type abstractType)
        {
            if (!abstractType.IsAssignableFrom(concreateType))
            {
                throw new NotSupportedException($"The type {concreateType} is not a {abstractType}");
            }
        }

        public static void AssertIs<T>(this Type concreateType)
        {
            concreateType.AssertIs(typeof(T));
        }
    }
}
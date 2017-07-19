using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TrackTv.WebServices.Infrastructure
{
    public static class MethodInfoExtensions
    {
        private static readonly ConcurrentDictionary<MethodInfo, List<Attribute>> Cache
            = new ConcurrentDictionary<MethodInfo, List<Attribute>>(); 
        
        public static List<T> CachedAttributes<T>(this MethodInfo methodInfo)
            where T : Attribute
        {
            return Cache.GetOrAdd(methodInfo, info => info.GetCustomAttributes().ToList())
                        .Select(a => a as T)
                        .Where(a => a != null)
                        .ToList();
        }
        
        public static List<Attribute> CachedAttributes(this MethodInfo methodInfo)
        {
            return Cache.GetOrAdd(methodInfo, info => info.GetCustomAttributes().ToList()).ToList();
        }

        public static T FirstAttribute<T>(this MethodInfo methodInfo) where T : Attribute
        {
            return methodInfo.CachedAttributes<T>().FirstOrDefault();
        }
    }
}
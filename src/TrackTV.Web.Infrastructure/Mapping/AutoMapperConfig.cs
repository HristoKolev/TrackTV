namespace TrackTV.Web.Infrastructure.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using AutoMapper;

    using TrackTV.Web.Infrastructure.Mapping.Contracts;

    public class AutoMapperConfig
    {
        private readonly Assembly assembly;

        public AutoMapperConfig(Assembly assembly)
        {
            this.assembly = assembly;
        }

        public void Execute()
        {
            Type[] types = this.assembly.GetExportedTypes();

            LoadStandardMappings(types);

            LoadCustomMappings(types);
        }

        private static void LoadCustomMappings(IEnumerable<Type> types)
        {
            IEnumerable<IHaveCustomMappings> maps = from t in types
                                                    from i in t.GetInterfaces()
                                                    where typeof(IHaveCustomMappings).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface
                                                    select (IHaveCustomMappings)Activator.CreateInstance(t);

            foreach (IHaveCustomMappings map in maps)
            {
                map.CreateMappings(Mapper.Configuration);
            }
        }

        private static void LoadStandardMappings(IEnumerable<Type> types)
        {
            var maps = from t in types
                       from i in t.GetInterfaces()
                       where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>) && !t.IsAbstract && !t.IsInterface
                       select new
                       {
                           Source = i.GetGenericArguments()[0],
                           Destination = t
                       };

            foreach (var map in maps)
            {
                Mapper.CreateMap(map.Source, map.Destination);
                Mapper.CreateMap(map.Destination, map.Source);
            }
        }
    }
}
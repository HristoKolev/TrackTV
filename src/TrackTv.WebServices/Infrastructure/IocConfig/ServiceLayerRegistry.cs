namespace TrackTv.WebServices.Infrastructure.IocConfig
{
    using System;
    using System.Globalization;
    using System.Linq;

    using StructureMap;

    using TrackTv.Services.Calendar;
    using TrackTv.Services.Show;

    public class ServiceLayerRegistry : Registry
    {
        public ServiceLayerRegistry()
        {
            var types = typeof(ShowService).Assembly.DefinedTypes.Select(info => info.AsType())
                                           .Where(type =>
                                               type.IsClass && (type.Name.EndsWith("Repository") || type.Name.EndsWith("Service")))
                                           .ToList();

            foreach (Type type in types)
            {
                this.For(type).ContainerScoped();
            }

            this.For<Calendar>().Use<GregorianCalendar>().AlwaysUnique();
            this.For<EpisodeCalendarCalculator>().ContainerScoped();
        }
    }
}
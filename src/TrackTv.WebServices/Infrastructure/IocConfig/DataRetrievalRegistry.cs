namespace TrackTv.WebServices.Infrastructure.IocConfig
{
    using System;
    using System.Linq;

    using StructureMap;

    using TrackTv.DataRetrieval;

    public class DataRetrievalRegistry : Registry
    {
        public DataRetrievalRegistry()
        {
            var types = typeof(Fetcher).Assembly.DefinedTypes.Select(info => info.AsType())
                                       .Where(type =>
                                           type.IsClass && (type.Name.EndsWith("Repository") || type.Name.EndsWith("Service")
                                                                                             || type.Name.EndsWith("Fetcher")))
                                       .ToList();

            foreach (Type type in types)
            {
                this.For(type).ContainerScoped();
            }
        }
    }
}
namespace TrackTv.WebServices.Infrastructure.IocConfig
{
    using System.IO;
    using System.Reflection;

    using log4net;
    using log4net.Config;

    using StructureMap;

    public class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            // Log4Net
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.ConfigureAndWatch(logRepository, new FileInfo("log4net-config.xml"));
            this.For<ILog>().Use("Building log4net logger.", context => LogManager.GetLogger(Assembly.GetEntryAssembly(), "Global logger"));
        }
    }
}
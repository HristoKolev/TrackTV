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
            var assembly = Assembly.GetEntryAssembly();

            var logRepository = LogManager.GetRepository(assembly);
            XmlConfigurator.ConfigureAndWatch(logRepository, new FileInfo(Path.Combine(Global.ConfigDirectory, "log4net-config.xml")));
            this.For<ILog>().Use("Building log4net logger.", context => LogManager.GetLogger(assembly, "Global logger"));
        }
    }
}
namespace TrackTv.WebServices.Infrastructure.IocConfig
{
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using log4net;
    using log4net.Config;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    using StructureMap;

    public class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            // Log4Net
            var assembly = Assembly.GetEntryAssembly();
            var logRepository = LogManager.GetRepository(assembly);
            XmlConfigurator.ConfigureAndWatch(logRepository, new FileInfo(Path.Combine(Global.ConfigDirectory, "log4net-config.xml")));
            this.For<ILog>().Use("Building log4net logger.", context => LogManager.GetLogger(assembly, "Global logger")).Singleton();

            // Mishap
            MishapService CreateMishapService(IContext ctx)
            {
                return new MishapService(ctx.GetInstance<IConfiguration>()["MishapApiKey"]);
            }

            this.For<MishapService>().Use("Creating Mishap service.", CreateMishapService).Singleton();

            // Background tasks
            var baseClass = typeof(BackgroundTask);
            var backgroundServices = Assembly.GetEntryAssembly()
                                             .DefinedTypes.Select(info => info.AsType())
                                             .Where(type => type.IsClass && type != baseClass && baseClass.IsAssignableFrom(type))
                                             .ToList();

            foreach (var backgroundService in backgroundServices)
            {
                this.For(typeof(IHostedService)).Use(backgroundService).Singleton();
            }

            // ErrorHandler
            this.For<ErrorHandler>().Singleton();
        }
    }
}
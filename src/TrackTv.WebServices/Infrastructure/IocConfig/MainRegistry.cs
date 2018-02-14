namespace TrackTv.WebServices.Infrastructure.IocConfig
{
    using System;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using log4net;
    using log4net.Config;

    using LinqToDB.DataProvider;
    using LinqToDB.DataProvider.MySql;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Hosting;

    using MySql.Data.MySqlClient;

    using StructureMap;

    using TrackTv.Data;
    using TrackTv.Services;
    using TrackTv.Services.Calendar;
    using TrackTv.Services.Show;

    public class MainRegistry : Registry
    {
        public MainRegistry()
        {
            this.DataAccess();

            this.Infrastructure();

            this.ServiceLayer();
        }

        private void DataAccess()
        {
            this.For<MySqlConnection>()
                .Use("MySql connection.", ctx => new MySqlConnection(Global.AppConfig.GetConnectionString("DefaultConnection")))
                .ContainerScoped();

            this.For<IDbConnection>().Use("IDbConnection", ctx => ctx.GetInstance<MySqlConnection>());

            #if DEBUG
            this.For<IDataProvider>().Use<LoggigDataProviderWrapper>().Ctor<IDataProvider>().Is<MySqlDataProvider>();
            #else
            this.For<IDataProvider>().Use<MySqlDataProvider>();
            #endif
            this.For<IDbService>().Use<DbService>();
        }

        private void Infrastructure()
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

            // ErrorHandler
            this.For<ErrorHandler>().Singleton();
        }

        private void ServiceLayer()
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
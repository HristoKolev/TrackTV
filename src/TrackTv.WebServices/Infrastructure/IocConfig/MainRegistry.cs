namespace TrackTv.WebServices.Infrastructure.IocConfig
{
    using System;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using log4net;
    using log4net.Config;

    using LinqToDB.DataProvider;
    using LinqToDB.DataProvider.PostgreSQL;

    using Npgsql;

    using StructureMap;

    using TrackTv.Data;
    using TrackTv.Services;

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
            this.For<NpgsqlConnection>()
                .Use("Postgres connection.", ctx => new NpgsqlConnection(Global.AppConfig.ConnectionString))
                .ContainerScoped();

            this.For<IDbConnection>().Use("IDbConnection", ctx => ctx.GetInstance<NpgsqlConnection>());

            #if DEBUG
            this.For<IDataProvider>().Use<LoggingDataProviderWrapper>().Ctor<IDataProvider>().Is<PostgreSQLDataProvider>();
            #else
            this.For<IDataProvider>().Use<PostgreSQLDataProvider>();
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

            // ErrorHandler
            this.For<ErrorHandler>().Singleton();
        }

        private void ServiceLayer()
        {
            var types = typeof(ShowService).Assembly.DefinedTypes.Select(info => info.AsType())
                                           .Where(type => type.IsClass && (type.Name.EndsWith("Repository")
                                                                           || type.Name.EndsWith("Service")))
                                           .ToList();

            foreach (Type type in types)
            {
                this.For(type).ContainerScoped();
            }
        }
    }
}
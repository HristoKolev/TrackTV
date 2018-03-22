namespace TrackTv.Updater.Infrastructure
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;

    using log4net;

    using LinqToDB.DataProvider;
    using LinqToDB.DataProvider.PostgreSQL;

    using Npgsql;

    using StructureMap;

    using TrackTv.Data;
    using TrackTv.Services;

    using TvDbSharper;

    public class MainRegistry : Registry
    {
        public MainRegistry()
        {
            this.DataAccess();

            this.Infrastructure();

            this.ServiceLayer();

            this.TvDbClient();
        }

        private void DataAccess()
        {
            this.For<NpgsqlConnection>()
                .Use("Postgres connection.", ctx => new NpgsqlConnection(Global.AppConfig.ConnectionString))
                .ContainerScoped();

            this.For<IDbConnection>().Use("IDbConnection", ctx => ctx.GetInstance<NpgsqlConnection>());

            this.For<IDataProvider>().Use<LoggingDataProviderWrapper>().Ctor<IDataProvider>().Is<PostgreSQLDataProvider>();
            this.For<IDbService>().Use<DbService>();
        }
 
        private void Infrastructure()
        {
            this.For<ILog>().Use("Building log4net logger.", context => Global.Log);
            this.For<ErrorHandler>().Use("Global error handler", () => Global.ErrorHandler);
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
        }

        private void TvDbClient()
        {
            Expression<Action<IContext, TvDbClient>> authenticateClient = (context, client) =>
                client.Authentication.AuthenticateAsync(Global.AppConfig.TheTvDbApiKey).Wait();

            this.For<ITvDbClient>().Use<TvDbClient>().OnCreation(authenticateClient).Singleton();
        }
    }
}
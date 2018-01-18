namespace TrackTv.Updater
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;

    using log4net;

    using LinqToDB.DataProvider;
    using LinqToDB.DataProvider.MySql;

    using MySql.Data.MySqlClient;

    using StructureMap;

    using TrackTv.Data;
    using TrackTv.Services.Show;

    using TvDbSharper;

    public class MainRegistry : Registry
    {
        public MainRegistry()
        {
            this.DataAccess();

            this.DataRetrieval();

            this.Infrastructure();

            this.ServiceLayer();

            this.TvDbClient();
        }

        private void DataAccess()
        {
            this.For<MySqlConnection>()
                .Use("MySql connection.", ctx => new MySqlConnection(Global.AppConfig.ConnectionString))
                .ContainerScoped();

            this.For<IDbConnection>().Use("IDbConnection", ctx => ctx.GetInstance<MySqlConnection>());

            this.For<IDataProvider>().Use<DataProviderWrapper>().Ctor<IDataProvider>().Is<MySqlDataProvider>();
            this.For<IDbService>().Use<DbService>();
        }

        private void DataRetrieval()
        {
            this.For<DataSynchronizer>();
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

            this.For<ITvDbClient>().Use<TvDbClient>().OnCreation(authenticateClient).ContainerScoped();
        }
    }
}
namespace TrackTv.Updater.Infrastructure
{
    using System;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;

    using log4net;

    using LinqToDB.DataProvider;
    using LinqToDB.DataProvider.PostgreSQL;

    using Npgsql;

    using SharpRaven;

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

            this.For<IDataProvider>().Use<PostgreSQLDataProvider>();
            this.For<IDbService>().Use<DbService>();
        }
 
        private void Infrastructure()
        {
            this.For<IRavenClient>().Use("Building sentry client.", () => new RavenClient(Global.AppConfig.SentryUrl));
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
            async Task AuthenticateTvDbClient(IContext ctx, ITvDbClient tvDbClient)
            {
                var settingsService = ctx.GetInstance<SettingsService>();

                string key = await settingsService.GetSettingAsync(Setting.TheTvDbApiKey);

                await tvDbClient.Authentication.AuthenticateAsync(key);
            }

            this.For<ITvDbClient>()
                .Use<TvDbClient>()
                .OnCreation("TvDbClient", (ctx, obj) => AuthenticateTvDbClient(ctx, obj).Wait())
                .Singleton();
        }
    }
}
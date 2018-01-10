namespace TrackTv.WebServices.Infrastructure.IocConfig
{
    using System.Collections.Generic;
    using System.Data;

    using LinqToDB.Data;
    using LinqToDB.DataProvider;
    using LinqToDB.DataProvider.MySql;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    using MySql.Data.MySqlClient;

    using StructureMap;

    using TrackTv.Data;

    public class DataAccessRegistry : Registry
    {
        public DataAccessRegistry()
        {
            this.For<MySqlConnection>()
                .Use("MySql connection.", ctx => new MySqlConnection(Global.AppConfig.GetConnectionString("DefaultConnection")))
                .ContainerScoped();

            this.For<IDbConnection>().Use("IDbConnection", ctx => ctx.GetInstance<MySqlConnection>());

            this.For<IDataProvider>().Use<ProfilerDbProvider>();
            this.For<DbService>();
        }
    }

    public class ProfilerDbProvider : MySqlDataProvider
    {
        public ProfilerDbProvider(IHostingEnvironment environment)
        {
            this.Environment = environment;
        }

        private IHostingEnvironment Environment { get; }

        public override void InitCommand(
            DataConnection dataConnection,
            CommandType commandType,
            string commandText,
            DataParameter[] parameters)
        {
            if (this.Environment.IsDevelopment())
            {
                Global.SqlLog.Push(new KeyValuePair<string, object>(commandText, parameters));
            }

            base.InitCommand(dataConnection, commandType, commandText, parameters);
        }
    }
}
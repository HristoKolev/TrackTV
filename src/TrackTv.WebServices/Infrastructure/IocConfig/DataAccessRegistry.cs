namespace TrackTv.WebServices.Infrastructure.IocConfig
{
    using System.Data;

    using LinqToDB.DataProvider;
    using LinqToDB.DataProvider.MySql;

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

            this.For<IDataProvider>().Use<MySqlDataProvider>();
            this.For<DbService>();
        }
    }
}
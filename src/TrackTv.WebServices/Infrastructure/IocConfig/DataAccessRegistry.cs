namespace TrackTv.WebServices.Infrastructure.IocConfig
{
    using System.Data;

    using LinqToDB.DataProvider;
    using LinqToDB.DataProvider.MySql;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using MySql.Data.MySqlClient;

    using StructureMap;

    using TrackTv.Data;

    public class DataAccessRegistry : Registry
    {
        public DataAccessRegistry()
        {
            this.ForConcreteType<ApplicationDbContext>().Configure.ContainerScoped();

            this.Forward<ApplicationDbContext, TrackTvDbContext>();
            this.Forward<ApplicationDbContext, DbContext>();

            this.For<ITransactionScopeFactory>().Use<TransactionScopeFactory>().ContainerScoped();
            
            this.For<MySqlConnection>()
                .Use("MySql connection.", ctx => new MySqlConnection(Global.AppConfig.GetConnectionString("DefaultConnection")))
                .ContainerScoped();

            this.For<IDbConnection>().Use("IDbConnection", ctx => ctx.GetInstance<MySqlConnection>());

            this.For<IDataProvider>().Use<MySqlDataProvider>();
            this.For<DbService>();
        }
    }
}
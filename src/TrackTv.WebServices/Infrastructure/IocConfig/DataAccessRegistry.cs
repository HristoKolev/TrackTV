namespace TrackTv.WebServices.Infrastructure.IocConfig
{
    using Microsoft.EntityFrameworkCore;

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
        }
    }
}
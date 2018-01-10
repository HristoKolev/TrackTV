namespace TrackTv.WebServices.Infrastructure.IocConfig
{
    using StructureMap;

    using TrackTv.DataRetrieval;

    public class DataRetrievalRegistry : Registry
    {
        public DataRetrievalRegistry()
        {
            this.For<DataSynchronizer>();
        }
    }
}
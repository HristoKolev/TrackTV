namespace TrackTv.WebServices.Infrastructure.IocConfig
{
    using StructureMap;

    using TrackTv.DataRetrieval;
    using TrackTv.DataRetrieval.ClientExtensions;
    using TrackTv.DataRetrieval.Data;
    using TrackTv.DataRetrieval.Fetchers;
    using TrackTv.DataRetrieval.Services;

    public class DataRetrievalRegistry : Registry
    {
        public DataRetrievalRegistry()
        {
            this.For<ActorsRepository>().ContainerScoped();
            this.For<GenresRepository>().ContainerScoped();
            this.For<NetworkRepository>().ContainerScoped();
            this.For<ShowsRepository>().ContainerScoped();
            this.For<EpisodeRepository>().ContainerScoped();

            this.For<Fetcher>().ContainerScoped();
            this.For<EpisodeFetcher>().ContainerScoped();
            this.For<GenreFetcher>().ContainerScoped();
            this.For<ActorFetcher>().ContainerScoped();
            this.For<ShowFetcher>().ContainerScoped();
 
            this.For<ExternalShowsService>().ContainerScoped();
        }
    }
}
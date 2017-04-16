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
            this.For<IActorsRepository>().Use<ActorsRepository>().ContainerScoped();
            this.For<IGenresRepository>().Use<GenresRepository>().ContainerScoped();
            this.For<INetworkRepository>().Use<NetworkRepository>().ContainerScoped();
            this.For<IShowsRepository>().Use<ShowsRepository>().ContainerScoped();
            this.For<IEpisodeRepository>().Use<EpisodeRepository>().ContainerScoped();

            this.For<IFetcher>().Use<Fetcher>().ContainerScoped();
            this.For<IEpisodeFetcher>().Use<EpisodeFetcher>().ContainerScoped();
            this.For<IGenreFetcher>().Use<GenreFetcher>().ContainerScoped();
            this.For<IActorFetcher>().Use<ActorFetcher>().ContainerScoped();
            this.For<IShowFetcher>().Use<ShowFetcher>().ContainerScoped();

            this.For<IAdvancedEpisodeClient>().Use<AdvancedEpisodeClient>().ContainerScoped();
            this.For<IAdvancedSeriesClient>().Use<AdvancedSeriesClient>().ContainerScoped();

            this.For<IExternalShowsService>().Use<ExternalShowsService>().ContainerScoped();
        }
    }
}
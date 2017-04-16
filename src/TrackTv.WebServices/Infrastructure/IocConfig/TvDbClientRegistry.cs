namespace TrackTv.WebServices.Infrastructure.IocConfig
{
    using System;
    using System.Linq.Expressions;

    using Microsoft.Extensions.Configuration;

    using StructureMap;

    using TvDbSharper;
    using TvDbSharper.Clients.Authentication;
    using TvDbSharper.Clients.Episodes;
    using TvDbSharper.Clients.Languages;
    using TvDbSharper.Clients.Search;
    using TvDbSharper.Clients.Series;
    using TvDbSharper.Clients.Updates;
    using TvDbSharper.Clients.Users;

    public class TvDbClientRegistry : Registry
    {
        public TvDbClientRegistry()
        {
            Expression<Action<IContext, TvDbClient>> authenticateClient = (context, client) => client
                .Authentication.AuthenticateAsync(context.GetInstance<IConfigurationRoot>()["ApiKeys:TheTvDbApi"])
                .Wait();

            this.For<ITvDbClient>().Use<TvDbClient>().OnCreation(authenticateClient).TimeScoped();

            this.For<IAuthenticationClient>().Use(x => x.GetInstance<ITvDbClient>().Authentication);
            this.For<ISeriesClient>().Use(context => context.GetInstance<ITvDbClient>().Series);
            this.For<IEpisodesClient>().Use(context => context.GetInstance<ITvDbClient>().Episodes);
            this.For<ILanguagesClient>().Use(context => context.GetInstance<ITvDbClient>().Languages);
            this.For<ISearchClient>().Use(context => context.GetInstance<ITvDbClient>().Search);
            this.For<IUpdatesClient>().Use(context => context.GetInstance<ITvDbClient>().Updates);
            this.For<IUsersClient>().Use(context => context.GetInstance<ITvDbClient>().Users);
        }
    }
}
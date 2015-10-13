using Microsoft.Owin;

using TrackTV.WebServices;

[assembly: OwinStartup(typeof(Startup))]

namespace TrackTV.WebServices
{
    using System.Reflection;
    using System.Web.Http;

    using AutoMapper;

    using Newtonsoft.Json.Serialization;

    using Ninject;
    using Ninject.Web.Common.OwinHost;
    using Ninject.Web.WebApi.OwinHost;

    using Owin;

    using TrackTV.Logic.Calendar;
    using TrackTV.Models;

    public partial class Startup
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Episode, CalendarEpisode>()
                  .ForMember(model => model.SeasonNumber, expression => expression.MapFrom(episode => episode.Season.Number))
                  .ForMember(model => model.ShowName, expression => expression.MapFrom(episode => episode.Season.Show.Name))
                  .ForMember(model => model.UserFriendlyId, expression => expression.MapFrom(episode => episode.Season.Show.UserFriendlyId));
        }

        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);

            app.UseNinjectMiddleware(CreateKernel).UseNinjectWebApi(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            RegisterMappings();
        }

        private static StandardKernel CreateKernel()
        {
            StandardKernel kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel;
        }
    }
}
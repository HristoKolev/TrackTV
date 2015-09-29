using Microsoft.Owin;

using TrackTV.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace TrackTV.Web
{
    using System.Data.Entity;

    using AutoMapper;

    using Owin;

    using TrackTV.Data;
    using TrackTV.Data.Migrations;
    using TrackTV.Logic.Calendar;
    using TrackTV.Models;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
            this.RegisterMappings();

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public void RegisterMappings()
        {
            Mapper.CreateMap<Episode, CalendarEpisode>()
                  .ForMember(model => model.SeasonNumber, expression => expression.MapFrom(episode => episode.Season.Number))
                  .ForMember(model => model.ShowName, expression => expression.MapFrom(episode => episode.Season.Show.Name))
                  .ForMember(model => model.ShowStringId, expression => expression.MapFrom(episode => episode.Season.Show.StringId));
        }
    }
}
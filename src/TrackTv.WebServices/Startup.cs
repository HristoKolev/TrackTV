namespace TrackTv.WebServices
{
    using System;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using StructureMap;

    using TrackTv.WebServices.Infrastructure;
    using TrackTv.WebServices.Infrastructure.IocConfig;

    public class Startup
    {
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            LogLevel logLevel;
            if (!Enum.TryParse(Global.AppConfig.AspNetLoggingLevel ?? "Debug", out logLevel))
            {
                logLevel = LogLevel.Debug;
            }

            loggerFactory.AddConsole(logLevel);
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder => { builder.WithOrigins(Global.AppConfig.CorsUrls).AllowAnyHeader().AllowAnyMethod(); });

            app.UseUnconventionalAuth();

            app.UseStaticFiles();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715
            app.UseMvc(routes => routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}"));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add(new ServiceFilterAttribute(typeof(HandleExceptionFilterAttribute))));

            services.AddMemoryCache();

            services.AddUnconventionalAuth(Global.AppConfig.AuthAuthorityUrl);

            var container = new Container(config =>
            {
                // Populate the container using the service collection
                config.Populate(services);

                config.AddRegistry<MainRegistry>();
            });

            return container.GetInstance<IServiceProvider>();
        }
    }
}
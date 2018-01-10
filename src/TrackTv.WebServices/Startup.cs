namespace TrackTv.WebServices
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using StructureMap;

    using TrackTv.WebServices.Infrastructure;
    using TrackTv.WebServices.Infrastructure.IocConfig;

    public class Startup
    {
        public Startup()
        {
            this.Configuration = Global.AppConfig;
        }

        private IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(this.Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // Shows UseCors with CorsPolicyBuilder.
            app.UseCors(builder =>
            {
                var origins = this.Configuration["CorsUrls"].Split(',');

                builder.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod();
            });

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

            services.AddSingleton(this.Configuration);

            services.AddUnconventionalAuth(this.Configuration["Server:Urls"].Split(",").First());

            var container = new Container(config =>
            {
                // Populate the container using the service collection
                config.Populate(services);

                config.AddRegistry<ServiceLayerRegistry>();
                config.AddRegistry<DataAccessRegistry>();
                config.AddRegistry<TvDbClientRegistry>();
                config.AddRegistry<DataRetrievalRegistry>();
                config.AddRegistry<InfrastructureRegistry>();
            });

            Global.Container = container;
            Global.ErrorHandler = container.GetInstance<ErrorHandler>();

            return container.GetInstance<IServiceProvider>();
        }
    }
}
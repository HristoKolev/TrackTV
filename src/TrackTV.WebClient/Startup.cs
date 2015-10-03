namespace TrackTV.WebClient
{
    using Microsoft.AspNet.Builder;
    using Microsoft.Framework.DependencyInjection;

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseFileServer();

            app.Run(async context => { context.Response.Redirect("/"); });
        }

        public void ConfigureServices(IServiceCollection services)
        {
        }
    }
}
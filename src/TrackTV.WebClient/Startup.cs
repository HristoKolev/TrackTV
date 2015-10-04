namespace TrackTV.WebClient
{
    using Microsoft.AspNet.Builder;

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseFileServer();

            app.Run(async context => context.Response.Redirect("/"));
        }
    }
}
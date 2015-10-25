namespace TrackTV.WebClient
{
    using Microsoft.AspNet.Builder;
    using Microsoft.AspNet.Http;

    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.UseFileServer();

            app.Run(async context =>
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync("./wwwroot/index.html");
            });
        }
    }
}
namespace TrackTV.WebServices
{
    using System.Web.Http;
    using System.Web.Http.Cors;

    using Microsoft.Owin.Security.OAuth;

    using Newtonsoft.Json;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

            config.Routes.MapHttpRoute(name: "DefaultApi", routeTemplate: "api/{controller}/{id}", defaults: new
            {
                id = RouteParameter.Optional
            });

            config.Routes.MapHttpRoute(name: "Shows by genre", routeTemplate: "api/shows/genre/{genre}", defaults: new
            {
                controller = "Shows", 
                action = "Genre"
            });

            config.Routes.MapHttpRoute(name: "Serach shows", routeTemplate: "api/shows/search/{query}/{page}", defaults: new
            {
                controller = "Shows", 
                action = "Search"
            });

            config.Routes.MapHttpRoute(name: "Shows by Network", routeTemplate: "api/shows/network/{network}/{page}", defaults: new
            {
                controller = "Shows", 
                action = "Network"
            });

            config.Routes.MapHttpRoute(name: "Calendar", routeTemplate: "api/calendar/{year}/{month}", defaults: new
            {
                controller = "Calendar", 
                action = "Month"
            });

            config.Routes.MapHttpRoute(name: "My Shows", routeTemplate: "api/myshows/{action}/{page}", defaults: new
            {
                controller = "MyShows"
            });

            config.Routes.MapHttpRoute(name: "New DefaultApi", routeTemplate: "api/{controller}/{action}/{id}", defaults: new
            {
                id = RouteParameter.Optional
            });
        }
    }
}
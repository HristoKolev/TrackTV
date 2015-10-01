namespace TrackTV.Web
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Calendar",
                url: "Calendar/{year}/{month}",
                defaults: new
                {
                    controller = "Home",
                    action = "Calendar"
                });

            routes.MapRoute(
                name: "Shows by network",
                url: "Shows/Network/{userFriendlyId}",
                defaults: new
                {
                    controller = "Shows",
                    action = "ByNetwork"
                });

            routes.MapRoute(
                name: "Shows by genre",
                url: "Shows/Genre/{userFriendlyId}",
                defaults: new
                {
                    controller = "Shows",
                    action = "ByGenre"
                });

            routes.MapRoute(
                name: "Show Details",
                url: "Show/{userFriendlyId}",
                defaults: new
                {
                    controller = "ShowDetails",
                    action = "ById"
                });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                });
        }
    }
}
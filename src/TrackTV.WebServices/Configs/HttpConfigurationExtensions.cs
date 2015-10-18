namespace TrackTV.WebServices
{
    using System.Collections.Generic;
    using System.Web.Http;

    public static class HttpConfigurationExtensions
    {
        public static void RegisterRoutes(this HttpRouteCollection collection)
        {
            IEnumerable<Route> routes = new ApiRouteConfig().GetRoutes();

            foreach (Route route in routes)
            {
                collection.MapHttpRoute(route.Name, route.Template, route.Defaults, route.Constraints);
            }
        }
    }
}
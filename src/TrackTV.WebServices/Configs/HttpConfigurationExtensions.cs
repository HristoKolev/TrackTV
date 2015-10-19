namespace TrackTV.WebServices
{
    using System.Web.Http;

    using TrackTV.WebServices.Routing;

    public static class HttpConfigurationExtensions
    {
        public static void RegisterRoutes(this HttpRouteCollection collection)
        {
            foreach (Route route in new ApiRouteConfig().GetRoutes())
            {
                collection.MapHttpRoute(route.Name, route.Template, route.Defaults, route.Constraints);
            }
        }
    }
}
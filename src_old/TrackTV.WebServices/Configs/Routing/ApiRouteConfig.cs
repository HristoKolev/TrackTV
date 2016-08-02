namespace TrackTV.WebServices.Routing
{
    using System.Collections.Generic;
    using System.Web.Http;

    public class ApiRouteConfig
    {
        public IEnumerable<Route> GetRoutes()
        {
            IList<Route> routes = new List<Route>();

            routes.Add(new Route
            {
                Name = "Get by userFriendlyId",
                Template = "api/{controller}/{userFriendlyId}",
                Defaults = new
                {
                    userFriendlyId = RouteParameter.Optional
                }
            });


            routes.Add(new Route
            {
                Name = "DefaultApi", 
                Template = "api/{controller}/{id}", 
                Defaults = new
                {
                    id = RouteParameter.Optional
                }
            });

        
            routes.Add(new Route
            {
                Name = "Shows by genre", 
                Template = "api/shows/genre/{genre}", 
                Defaults = new
                {
                    controller = "Shows", 
                    action = "Genre"
                }
            });

            routes.Add(new Route
            {
                Name = "Serach shows", 
                Template = "api/shows/search/{query}/{page}", 
                Defaults = new
                {
                    controller = "Shows", 
                    action = "Search"
                }
            });

            routes.Add(new Route
            {
                Name = "Shows by Network", 
                Template = "api/shows/network/{network}/{page}", 
                Defaults = new
                {
                    controller = "Shows", 
                    action = "Network"
                }
            });

            routes.Add(new Route
            {
                Name = "Calendar", 
                Template = "api/calendar/{year}/{month}", 
                Defaults = new
                {
                    controller = "Calendar", 
                    action = "Month"
                }
            });

            routes.Add(new Route
            {
                Name = "My Shows", 
                Template = "api/myshows/{action}/{page}", 
                Defaults = new
                {
                    controller = "MyShows"
                }
            });

            routes.Add(new Route
            {
                Name = "New DefaultApi", 
                Template = "api/{controller}/{action}/{id}", 
                Defaults = new
                {
                    id = RouteParameter.Optional
                }
            });

            return routes;
        }
    }
}
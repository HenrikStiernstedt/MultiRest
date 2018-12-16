using multijson.Configuration;
using multijson.Models;
using System.Configuration;
using System.Web.Http;

namespace multijson
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.MapHttpAttributeRoutes(); // Minns ej om denna är nödvändig eller ej. Verkar gå bra utan.

            //string routeTemplate = ConfigurationManager.AppSettings.Get("routeTemplate") ?? "api/{*request}";

            var routingSetting = RoutingGroups.GetRoutingGroups();

            foreach (RoutingGroupElement rge in routingSetting)
            {
                if (rge.IsActive)
                {
                    config.Routes.MapHttpRoute(
                        name: rge.Name,
                        routeTemplate: rge.RouteTemplate,
                        constraints: null,
                        defaults: null,
                        handler: new DefaultHttpHandler(rge)
                    );
                }
            }
        }
    }
}

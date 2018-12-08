using multijson.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;

namespace multijson
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //config.MapHttpAttributeRoutes(); // Minns ej om denna är nödvändig eller ej. Verkar gå bra utan.

            string routeTemplate = ConfigurationManager.AppSettings.Get("routeTemplate") ?? "api/{*request}";

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: routeTemplate,
                constraints: null,
                defaults: null,
                handler: new DefaultHttpHandler()
            );
        }
    }
}

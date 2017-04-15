using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace finalTest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{uname}/{fname}/{option}/{id}/{recs}",
                defaults: new { uname = RouteParameter.Optional, fname = RouteParameter.Optional, option = RouteParameter.Optional, id = RouteParameter.Optional, recs = RouteParameter.Optional }
            );

           //remove the following two lines to go back to xml
           var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
           config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

        }
    }
}

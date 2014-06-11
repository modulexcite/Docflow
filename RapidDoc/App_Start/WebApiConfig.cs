using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using RapidDoc.App_Start;

namespace RapidDoc
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
                routeTemplate: "api/{controller}/{id}/{companyId}",
                defaults: new { id = RouteParameter.Optional, companyId = RouteParameter.Optional }
            );
        }
    }
}
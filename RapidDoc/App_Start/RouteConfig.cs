using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RapidDoc.App_Start;
using RapidDoc.Models.Repository;
using RapidDoc.Models.Services;

namespace RapidDoc
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreWindowsLoginRoute();
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Account",
                url: "Account/Login",
                defaults: new { controller = "Account", action = "Login" }
            ).RouteHandler = new DefaultMvcRouteHandler();

            routes.MapRoute(
                name: "AccountWindows",
                url: "Windows/Login"
            ).RouteHandler = new DefaultMvcRouteHandler();

            routes.MapRoute(
                name: "DocumentDownload",
                url: "Document/DownloadFile/{id}",
                defaults: new { controller = "Document", action = "DownloadFile", id = UrlParameter.Optional }
            ).RouteHandler = new DefaultMvcRouteHandler();

            routes.MapRoute(
                name: "DocumentDelete",
                url: "Document/DeleteFile/{id}",
                defaults: new { controller = "Document", action = "DeleteFile", id = UrlParameter.Optional }
            ).RouteHandler = new DefaultMvcRouteHandler();

            foreach (Route r in routes)
            {
                if (!(r.RouteHandler is DefaultMvcRouteHandler) && !(r.RouteHandler is System.Web.Http.WebHost.HttpControllerRouteHandler)
                    && !(r.RouteHandler is System.Web.Routing.StopRoutingHandler))
                {
                    r.RouteHandler = new AppMvcRouteHandler();
                    r.Url = "{company}/" + r.Url;
                    //Adding default culture 
                    if (r.Defaults == null)
                    {
                        r.Defaults = new RouteValueDictionary();
                    }

                    r.Defaults.Add("company", "ATK");

                    //Adding constraint for culture param
                    if (r.Constraints == null)
                    {
                        r.Constraints = new RouteValueDictionary();
                    }
                    r.Constraints.Add("company", new CompanyConstraint("ATK", "ATR"));
                }
            }
        }
    }
}

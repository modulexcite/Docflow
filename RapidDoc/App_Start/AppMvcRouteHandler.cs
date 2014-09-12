using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RapidDoc.Models.Services;

namespace RapidDoc.App_Start
{
    public class AppMvcRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return base.GetHttpHandler(requestContext);
        }
    }

    public class DefaultMvcRouteHandler : MvcRouteHandler { }
}
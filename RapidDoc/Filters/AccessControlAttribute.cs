using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Services;

namespace RapidDoc.Filters
{
    public class AccessControlAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            /*if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
               UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>());

                var user = UserManager.FindByName(HttpContext.Current.User.Identity.Name);
                if (user == null)
                {
                    HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
                }

                if (user.isEnable == false)
                {
                    HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
                }
            }*/
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}
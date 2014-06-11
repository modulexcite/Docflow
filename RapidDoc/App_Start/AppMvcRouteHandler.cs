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
            var companyId = requestContext.RouteData.Values["company"].ToString();

            IAccountService serviceAccount = DependencyResolver.Current.GetService<IAccountService>();
            ICompanyService serviceCompany = DependencyResolver.Current.GetService<ICompanyService>();

            var company = serviceCompany.FirstOrDefault(x => x.AliasCompanyName == companyId);
            var user = serviceAccount.FirstOrDefault(x => x.UserName == requestContext.HttpContext.User.Identity.Name);

            if (company != null && user != null)
            {
                if (user.CompanyTableId != company.Id)
                {
                    user.CompanyTableId = company.Id;
                    serviceAccount.SaveDomain(user);
                }
            }

            return base.GetHttpHandler(requestContext);
        }
    }

    public class DefaultMvcRouteHandler : MvcRouteHandler { }
}
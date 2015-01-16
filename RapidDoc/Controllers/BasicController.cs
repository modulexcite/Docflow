using Ninject;
using RapidDoc.Filters;
using RapidDoc.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.Services;
using System.IO;
using RapidDoc.Models.Repository;
using System.Reflection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RapidDoc.Models.DomainModels;
using Microsoft.AspNet.Identity;
using RapidDoc.Models.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RapidDoc.Controllers
{
    [Authorize(Roles = "ActiveUser")]
    [Culture]
    public class BasicController : AsyncController
    {
        protected readonly ICompanyService _CompanyService;
        protected readonly IAccountService _AccountService;
        protected readonly IUnitOfWork _uow;

        public BasicController(IUnitOfWork uow, ICompanyService companyService, IAccountService accountService)
        {
            _uow = uow;
            _CompanyService = companyService;
            _AccountService = accountService;
        }

        [Inject]
        public IMapper ModelMapper { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (filterContext.RouteData.Values.Any(x => x.Key == "company"))
            {
                var companyId = filterContext.RouteData.Values["company"].ToString();
                if (filterContext.RouteData.RouteHandler != null && !String.IsNullOrEmpty(companyId))
                {
                    ApplicationUser user = _AccountService.Find(User.Identity.GetUserId());
                    if (user != null)
                    {
                        if (user.AliasCompanyName != companyId)
                        {
                            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_uow.GetDbContext<ApplicationDbContext>()));

                            if (UserManager.IsInRole(user.Id, "ChangeCompany") || UserManager.IsInRole(user.Id, "Administrator"))
                            {
                                var companyList = _CompanyService.GetAll().ToList();
                                if (companyList != null)
                                {
                                    var company = companyList.FirstOrDefault(x => x.AliasCompanyName == companyId);
                                    if (company != null)
                                    {
                                        user.CompanyTableId = company.Id;
                                        _AccountService.SaveDomain(user);
                                    }
                                }
                            }
                            UserManager.Dispose();
                            UserManager = null;
                        }
                    }
                }
            }
        }

        public ActionResult ChangeCulture(string id)
        {
            string returnUrl = Request.UrlReferrer.PathAndQuery;

            List<string> cultures = Lang.GetISOCodes();
            if (!cultures.Contains(id))
            {
                id = Lang.DefaultLang();
            }

            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = id;
            else
            {

                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = id;
                cookie.Expires = DateTime.UtcNow.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }

        [Authorize(Roles = "ChangeCompany, Administrator")]
        public ActionResult ChangeCompany(string companyId, string returnUrl)
        {
            var company = _CompanyService.FirstOrDefault(x => x.AliasCompanyName == companyId);
            if (company == null)
                return HttpNotFound();

            ApplicationUser user = _AccountService.Find(User.Identity.GetUserId());
            if (user == null)
                return HttpNotFound();

            user.CompanyTableId = company.Id;
            _AccountService.SaveDomain(user);

            if (returnUrl.Length >= 4)
            {
                returnUrl = returnUrl.Substring(4);
            }
            return Redirect("/" + companyId.ToString());
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        protected string GetAttributeDisplayName(PropertyInfo property)
        {
            var atts = property.GetCustomAttributes(
                typeof(DisplayAttribute), true);
            if (atts.Length == 0)
                return null;
            return (atts[0] as DisplayAttribute).Name;
        }

        public Guid GuidNull2Guid(Guid? value)
        {
            return value ?? Guid.Empty;
        }

        public DateTime GetLocalTime(DateTime value, string timeZone)
        {
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            return TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(value), timeZoneInfo);
        }

        [AllowAnonymous]
        public ActionResult GetCompanyList()
        {
            return PartialView("_CompanyList", _CompanyService.GetAllView());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Services;

namespace RapidDoc.Controllers
{
    public class HomeController : BasicController
    {
        public HomeController(ICompanyService companyService, IAccountService accountService)
            : base(companyService, accountService)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult GetCompanyList()
        {
            return PartialView("~/Views/Home/CompanyList.cshtml", _CompanyService.GetAllView());
        }
    }
}
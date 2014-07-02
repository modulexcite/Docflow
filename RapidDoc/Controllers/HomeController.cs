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
        private readonly ICompanyService _CompanyService;
        private readonly IAccountService _AccountService;

        public HomeController(ICompanyService CompanyService, IAccountService AccountService)
        {
            _CompanyService = CompanyService;
            _AccountService = AccountService;
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Services;

namespace RapidDoc.Controllers
{
    public class HistoryUserController : BasicController
    {
        private readonly IHistoryUserService _HistoryUserService;

        public HistoryUserController(IHistoryUserService historyUserService, ICompanyService companyService, IAccountService accountService)
            : base(companyService, accountService)
        {
            _HistoryUserService = historyUserService;
        }

        //
        // GET: /HistoryUser/
        public ActionResult Index()
        {
            ApplicationUser userTable = _AccountService.FirstOrDefault(x => x.UserName == User.Identity.Name);
            var model = _HistoryUserService.GetPartialView(x => x.ApplicationUserCreatedId == userTable.Id).OrderByDescending(x => x.CreatedDate);

            return View(model);
        }
	}
}
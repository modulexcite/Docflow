using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Services;
using Microsoft.AspNet.Identity;
using RapidDoc.Models.Infrastructure;

namespace RapidDoc.Controllers
{
    public class HistoryUserController : BasicController
    {
        private readonly IHistoryUserService _HistoryUserService;

        public HistoryUserController(IUnitOfWork uow, IHistoryUserService historyUserService, ICompanyService companyService, IAccountService accountService)
            : base(uow, companyService, accountService)
        {
            _HistoryUserService = historyUserService;
        }

        //
        // GET: /HistoryUser/
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var model = _HistoryUserService.GetPartialView(x => x.ApplicationUserCreatedId == userId).OrderByDescending(x => x.CreatedDate);

            return View(model);
        }
	}
}
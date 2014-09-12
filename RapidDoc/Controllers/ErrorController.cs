using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.Services;

namespace RapidDoc.Controllers
{
    public class ErrorController : BasicController
    {
        public ErrorController(ICompanyService companyService, IAccountService accountService)
            : base(companyService, accountService)
        {
        }

        public ActionResult PageNotFound()
        {
            return View();
        }

        public ActionResult Exception()
        {
            var exception = Session["application_error"] as Exception;

            IEnumerable<String> model = new List<String>();

            if (exception != null)
            {
                model = GetExceptionDescription(exception);
            }

            return View(model);
        }

        private static IEnumerable<String> GetExceptionDescription(Exception ex)
        {
            var list = new List<String> { ex.Message };

            if (ex.InnerException != null)
            {
                list.AddRange(GetExceptionDescription(ex.InnerException));
            }

            return list;
        }
	}
}
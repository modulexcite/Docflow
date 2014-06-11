using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.Repository;

namespace RapidDoc.Filters
{
    public class CultureAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string cultureName = null;
            // Получаем куки из контекста, которые могут содержать установленную культуру
            HttpCookie cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
            {
                var userLanguages = HttpContext.Current.Request.UserLanguages;
                if (userLanguages[0] != null)
                {
                    cultureName = userLanguages[0];
                }
                else
                {
                    cultureName = Lang.DefaultLang();
                }
            }

            // Список культур
            List<string> cultures = Lang.GetISOCodes();
            if (!cultures.Contains(cultureName))
            {
                cultureName = Lang.DefaultLang();
            }

            CultureInfo ci = CultureInfo.GetCultureInfo(cultureName);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}
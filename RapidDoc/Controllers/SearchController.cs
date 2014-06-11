using AutoMapper;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Services;
using RapidDoc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Extensions;
using System.IO;

namespace RapidDoc.Controllers
{
    public class SearchController : BasicController
    {
        private readonly ISearchService _Service;

        public SearchController(ISearchService Service)
        {
            _Service = Service;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchFormView model)
        {
            string searchString = model.SearchText.Trim();

            if (searchString.Length > 3)
            {
                var resultText = _Service.GetPartialView(x => x.DocumentText.Contains(searchString));
                var resultNum = _Service.GetPartialView(x => x.DocumentTable.DocumentNum.Contains(searchString));
                var result = resultNum.Concat(resultText);
                ViewBag.SearchResult = result;
            }
            model.SearchText = String.Empty;
            return View(model);
        }
    }
}

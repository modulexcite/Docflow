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

        public SearchController(ISearchService Service, ICompanyService companyService, IAccountService accountService)
            : base(companyService, accountService)
        {
            _Service = Service;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchFormView model, string searchText)
        {
            if (!String.IsNullOrEmpty(searchText) && !String.IsNullOrWhiteSpace(searchText))
            {
                int blockSize = 5;
                var documents = _Service.GetDocuments(1, blockSize, searchText);

                ViewBag.SearchCount = _Service.GetPartialView(x => x.DocumentText.Contains(searchText)).Count() +
                    _Service.GetPartialView(x => x.DocumentTable.DocumentNum.Contains(searchText)).Count();
                ViewBag.SearchResult = documents;
            }

            return View(model);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InfinateScroll(DataCollection data)
        {
            int blockNumber = Convert.ToInt32(data.BlockSize);
            string searchText = data.SearchText;
            int blockSize = 5;

            var documents = _Service.GetDocuments(blockNumber, blockSize, searchText);
            SearchFormView searchFormView = new SearchFormView();
            searchFormView.NoMoreData = documents.Count < blockSize;

            ViewBag.SearchResult = documents;

            searchFormView.HTMLString = RenderPartialViewToString("_AjaxSearchDocument", documents);
            return Json(searchFormView);
        }

        public class DataCollection
        {
            public string BlockSize { get; set; }
            public string SearchText { get; set; }
        }
    }
}

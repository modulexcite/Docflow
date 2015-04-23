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
        private readonly IDepartmentService _DepartmentService;
        private readonly IEmplService _EmplService;
        private readonly IProcessService _ProcessService;

        public SearchController(ISearchService Service, IDepartmentService departmentService, IEmplService emplService, IProcessService processService, ICompanyService companyService, IAccountService accountService)
            : base(companyService, accountService)
        {
            _Service = Service;
            _DepartmentService = departmentService;
            _EmplService = emplService;
            _ProcessService = processService;
        }

        public ActionResult Index()
        {
            ViewBag.CompanyList = _CompanyService.GetDropListCompanyNull(null);
            ViewBag.EmplList = _EmplService.GetDropListEmplNull(null);
            ViewBag.ProcessList = _ProcessService.GetDropListProcessNull(null);
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchFormView model)
        {
            if ((!String.IsNullOrEmpty(model.SearchText) && !String.IsNullOrWhiteSpace(model.SearchText) || model.StartDate != null || model.EndDate != null || 
                model.CompanyTableId != null || model.CreatedEmplTableId != null || model.ProcessTableId != null))
            {
                int blockSize = 20;
                var documents = _Service.GetDocuments(1, blockSize, model);

                ViewBag.SearchCount = documents.Item1;
                ViewBag.SearchResult = documents.Item2;
            }

            ViewBag.CompanyList = _CompanyService.GetDropListCompanyNull(model.CompanyTableId);
            ViewBag.EmplList = _EmplService.GetDropListEmplNull(model.CreatedEmplTableId);
            ViewBag.ProcessList = _ProcessService.GetDropListProcessNull(model.ProcessTableId);
            return View(model);
        }

        [HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult InfinateScroll(DataCollection data)
        {
            int blockNumber = Convert.ToInt32(data.BlockSize);
            int blockSize = 20;
            var documents = _Service.GetDocuments(blockNumber, blockSize, new SearchFormView { SearchText = data.SearchText, StartDate = data.StartDate, EndDate = data.EndDate, CompanyTableId = data.CompanyTableId, ProcessTableId = data.ProcessTableId, CreatedEmplTableId = data.CreatedEmplTableId });
            SearchFormView searchFormView = new SearchFormView();
            searchFormView.NoMoreData = documents.Item1 < blockSize;
            ViewBag.SearchResult = documents.Item2;

            searchFormView.HTMLString = RenderPartialViewToString("_AjaxSearchDocument", documents.Item2);
            return Json(searchFormView);
        }

        public class DataCollection
        {
            public string BlockSize { get; set; }
            public string SearchText { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public Guid? CreatedEmplTableId { get; set; }
            public Guid? CompanyTableId { get; set; }
            public Guid? ProcessTableId { get; set; }
        }
    }
}

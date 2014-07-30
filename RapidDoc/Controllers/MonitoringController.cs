using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Repository;
using RapidDoc.Models.Services;

namespace RapidDoc.Controllers
{
    public class MonitoringController : BasicController
    {
        private readonly IDocumentService _DocumentService;

        public MonitoringController(IDocumentService documentService)
        {
            _DocumentService = documentService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult PerformanceDepartment()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var flatData = from wfTracker in context.WFTrackerTable
                join user in context.Users on wfTracker.SignUserId equals user.Id
                where wfTracker.ExecutionStep == true && wfTracker.SignUserId != null
                && wfTracker.TrackerType == TrackerType.Approved
                select new ReportPerformanceDepartmentModel
                {
                    UserName = user.UserName,
                    ActivityName = wfTracker.ActivityName,
                    SignDate = wfTracker.SignDate,
                    DocumentId = wfTracker.DocumentTableId,
                    Date = wfTracker.ModifiedDate,
                    SLAOffset = wfTracker.SLAOffset
                };

            foreach (var item in flatData.Where(x => x.SLAOffset > 0))
            {
                item.PerformDate = _DocumentService.GetSLAPerformDate(item.DocumentId, item.Date, item.SLAOffset);
            }

            var gridData = (from data in flatData.ToList()
                            group data by new
                            {
                                data.UserName
                            } into gflat
                            select new
                            {
                                UserName = gflat.Key.UserName,
                                Count = gflat.Count(),
                                CountError = gflat.Count(x => x.SignDate > x.PerformDate && x.PerformDate != null)
                            }).ToList();

            string[] emplList = gridData.Select(x => x.UserName).ToArray();
            int[] countList = gridData.Select(x => x.Count).ToArray();
            int[] countErrorList = gridData.Select(x => x.CountError).ToArray();

            return Json(new { labels = emplList, dataCount = countList, dataErrorCount = countErrorList }, JsonRequestBehavior.AllowGet);
        }
	}
}
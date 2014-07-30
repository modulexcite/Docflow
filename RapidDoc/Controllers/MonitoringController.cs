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
            DateTime startDate = DateTime.Now.AddDays(-30);
            DateTime endDate = DateTime.Now.AddDays(1);

            var flatData = (from wfTracker in context.WFTrackerTable
                /*join user in context.Users 
                            on wfTracker.SignUserId equals user.Id*/
                            from user in context.Users.Where(x => x.Id == wfTracker.SignUserId).DefaultIfEmpty()
                where wfTracker.ExecutionStep == true 
                && (wfTracker.SignDate >= startDate && wfTracker.SignDate <= endDate)/*&&  wfTracker.TrackerType == TrackerType.Approved*/
                select new ReportPerformanceDepartmentModel
                {
                    UserName = user.UserName,
                    ActivityName = wfTracker.ActivityName,
                    SignDate = wfTracker.SignDate,
                    DocumentId = wfTracker.DocumentTableId,
                    Date = wfTracker.StartDateSLA,
                    SLAOffset = wfTracker.SLAOffset,
                    TrackerType = wfTracker.TrackerType,
                    SignUserId = wfTracker != null ? wfTracker.SignUserId : null
                }).ToList();
         
            foreach (var item in flatData.Where(x => x.SLAOffset > 0))
            {
                item.PerformDate = _DocumentService.GetSLAPerformDate(item.DocumentId, item.Date, item.SLAOffset);
            }

            var barData = (from data in flatData.ToList()
                            where data.SignUserId != null &&
                            data.TrackerType == TrackerType.Approved
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

            var pieData = (from piedata in flatData.ToList()
                           group piedata by new
                           {
                               piedata.DocumentId,
                               piedata.SignUserId,
                               piedata.SignDate,
                               piedata.PerformDate
                           } into piegflat
                           select new
                           {                          
                               piegflat.Key.DocumentId,
                               piegflat.Key.SignUserId,
                               piegflat.Key.SignDate,
                               piegflat.Key.PerformDate
                           }).ToList();

            string[] barEmplList = barData.Select(x => x.UserName).ToArray();
            int[] barCountList = barData.Select(x => x.Count).ToArray();
            int[] barCountErrorList = barData.Select(x => x.CountError).ToArray();

            int pieCountList = pieData.Where(x => x.SignDate < x.PerformDate && x.PerformDate != null && x.SignUserId != null).Count();
            int pieCountErrorList = pieData.Where(x => x.SignDate > x.PerformDate && x.PerformDate != null && x.SignUserId != null).Count();
            int pieOpenCountList = pieData.Where(x => DateTime.Now < x.PerformDate && x.PerformDate != null && x.SignUserId == null).Count();
            int pieOpenCountErrorList = pieData.Where(x => DateTime.Now > x.PerformDate && x.PerformDate != null && x.SignUserId == null).Count();

            return Json(new { barLabels = barEmplList, barDataCount = barCountList, barDataErrorCount = barCountErrorList,
                              pieCountListValue = pieCountList, pieCountErrorListValue = pieCountErrorList,
                              pieOpenCountListValue = pieOpenCountList, pieOpenCountErrorListValue = pieOpenCountErrorList}, JsonRequestBehavior.AllowGet);
        }
	}
}
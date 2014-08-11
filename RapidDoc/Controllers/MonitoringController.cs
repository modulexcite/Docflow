using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Repository;
using RapidDoc.Models.Services;
using RapidDoc.Models.DomainModels;

namespace RapidDoc.Controllers
{
    public class MonitoringController : BasicController
    {
        private readonly IDocumentService _DocumentService;
        private readonly IDepartmentService _DepartmentService;
        private readonly IEmplService _EmplService;
        private readonly IAccountService _AccountService;

        public MonitoringController(IDocumentService documentService, IDepartmentService departmentService, IEmplService emplService, IAccountService accountService)
        {
            _DocumentService = documentService;
            _DepartmentService = departmentService;
            _EmplService = emplService;
            _AccountService = accountService;
        }

        public ActionResult Index()
        {
            ViewBag.DepartmentList = _DepartmentService.GetDropListDepartmentNull(null);
            return View();
        }

        public List<string> GetParentListDepartment(List<DepartmentTable> departmentList)
        {
            List<string> listdepartmentId = new List<string>();
            List<string> listdepartmentBufId = new List<string>();

            foreach (DepartmentTable depId in departmentList)
            {
                listdepartmentId.Add(depId.DepartmentName);
                List<DepartmentTable> departmentTable = _DepartmentService.GetPartial(x => x.ParentDepartmentId == depId.Id).ToList();
                listdepartmentBufId = this.GetParentListDepartment(departmentTable);
                listdepartmentId = listdepartmentId.Concat(listdepartmentBufId).Distinct().OrderBy(x => x).ToList();
            }
            return listdepartmentId;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult PerformanceDepartment(String departmentString)
        {
            List<string> listdepartmentId = new List<string>();
          
            Guid? departmentId = new Guid();
            if (departmentString == "")
            {
                string userId = _AccountService.FirstOrDefault(y => y.UserName == User.Identity.Name).Id;
                var empl = _EmplService.FirstOrDefault(x => x.ApplicationUserId == userId);
                if (empl != null && empl.DepartmentTable != null)
                {
                    departmentId = empl.DepartmentTable.Id;
                }
                else
                {
                    departmentId = _DepartmentService.FirstOrDefault(x => x.Id != null).Id;
                }
            }
            else
            {
                departmentId = new Guid(departmentString);
            }

            List<DepartmentTable> departmentTableList = _DepartmentService.GetPartial(x => x.Id == departmentId).ToList();
            listdepartmentId = this.GetParentListDepartment(departmentTableList);

            ApplicationDbContext context = new ApplicationDbContext();
            DateTime startDate = DateTime.Now.AddDays(-30);
            DateTime endDate = DateTime.Now.AddDays(1);

            var flatData = (from wfTracker in context.WFTrackerTable
                            from user in context.Users.Where(x => x.Id == wfTracker.SignUserId).DefaultIfEmpty()
                where wfTracker.ExecutionStep == true
                && (wfTracker.CreatedDate >= startDate && wfTracker.CreatedDate <= endDate)/*&&  wfTracker.TrackerType == TrackerType.Approved*/
                select new ReportPerformanceDepartmentModel
                {
                    UserName = user.UserName,
                    ActivityName = wfTracker.ActivityName,
                    SignDate = wfTracker.SignDate,
                    DocumentId = wfTracker.DocumentTableId,
                    Date = wfTracker.StartDateSLA,
                    SLAOffset = wfTracker.SLAOffset,
                    TrackerType = wfTracker.TrackerType,
                    SignUserId = wfTracker != null ? wfTracker.SignUserId : null,
                    WftId = wfTracker.Id
                }).ToList();
         
            foreach (var item in flatData.Where(x => x.SLAOffset > 0))
            {
                item.PerformDate = _DocumentService.GetSLAPerformDate(item.DocumentId, item.Date, item.SLAOffset);
            }

            var barData = (from data in flatData.ToList()
                           join users in context.Users
                                on data.SignUserId equals users.Id
                           join epml in context.EmplTable
                               on users.Id equals epml.ApplicationUserId
                           join department in context.DepartmentTable.Where(x => listdepartmentId.Contains(x.DepartmentName))
                               on epml.DepartmentTable.Id equals department.Id
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
            int pieOpenCountList = pieData.Where(x => DateTime.UtcNow < x.PerformDate && x.PerformDate != null && x.SignUserId == null).Count();
            int pieOpenCountErrorList = pieData.Where(x => DateTime.UtcNow > x.PerformDate && x.PerformDate != null && x.SignUserId == null).Count();

            return Json(new { barLabels = barEmplList, barDataCount = barCountList, barDataErrorCount = barCountErrorList,
                              pieCountListValue = pieCountList, pieCountErrorListValue = pieCountErrorList,
                              pieOpenCountListValue = pieOpenCountList, pieOpenCountErrorListValue = pieOpenCountErrorList}, JsonRequestBehavior.AllowGet);
        }
	}
}
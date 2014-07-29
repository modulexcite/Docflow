using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Services;
using RapidDoc.Models.ViewModels;

namespace RapidDoc.Controllers
{
    public class ReportController : BasicController
    {
        private readonly IWorkflowTrackerService _WorkflowTrackerService;
        private readonly IDocumentService _DocumentService;

        public ReportController(IWorkflowTrackerService workflowTrackerService, IDocumentService documentService)
        {
            _WorkflowTrackerService = workflowTrackerService;
            _DocumentService = documentService;
        }

        public ActionResult PerformanceDepartment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GenerateReport(ReportParametersBasicView model)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var flatData = from wfTracker in context.WFTrackerTable
                       join user in context.Users on wfTracker.SignUserId equals user.Id
                       join empl in context.EmplTable on user.Id equals empl.ApplicationUserId
                       join title in context.TitleTable on empl.TitleTableId equals title.Id
                       join department in context.DepartmentTable on empl.DepartmentTableId equals department.Id
                       join document in context.DocumentTable on wfTracker.DocumentTableId equals document.Id
                       join process in context.ProcessTable on document.ProcessTableId equals process.Id
                       where wfTracker.ExecutionStep == true && wfTracker.SignUserId != null
                       && (wfTracker.SignDate >= model.StartDate && wfTracker.SignDate <= model.EndDate)
                       select new ReportPerformanceDepartmentModel
                       {
                           FullName = empl.SecondName + " " + empl.FirstName + " " + empl.MiddleName,
                           UserName = user.UserName,
                           TitleName = title.TitleName,
                           DepartmentName = department.DepartmentName,
                           ProcessName = process.ProcessName,
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
                               data.FullName,
                               data.UserName,
                               data.TitleName,
                               data.DepartmentName,
                               data.ProcessName
                           } into gflat
                           select new
                           {
                               FullName = gflat.Key.FullName,
                               UserName = gflat.Key.UserName,
                               TitleName = gflat.Key.TitleName,
                               DepartmentName = gflat.Key.DepartmentName,
                               ProcessName = gflat.Key.ProcessName,
                               Count = gflat.Count(),
                               CountError = gflat.Count(x => x.SignDate > x.PerformDate && x.PerformDate != null)
                           }).ToList();

            System.Web.UI.WebControls.GridView gv = new System.Web.UI.WebControls.GridView();
            gv.DataSource = gridData;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Report.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

            return RedirectToAction("PerformanceDepartment");
        }
	}

    public class ReportPerformanceDepartmentModel
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string TitleName { get; set; }
        public string DepartmentName { get; set; }
        public string ProcessName { get; set; }
        public string ActivityName { get; set; }
        public Guid DocumentId { get; set; }
        public DateTime Date { get; set; }
        public int SLAOffset { get; set; }
        public DateTime? SignDate { get; set; }
        public DateTime? PerformDate { get; set; }
    }
}
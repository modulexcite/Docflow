using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Repository;
using RapidDoc.Models.Services;
using RapidDoc.Models.ViewModels;
using Excel = Microsoft.Office.Interop.Excel;
using RapidDoc.Models.DomainModels;
using System.Activities;
using RapidDoc.Activities;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Office.Interop.Excel;

namespace RapidDoc.Controllers
{
    public class ReportController : BasicController
    {
        private readonly IWorkflowTrackerService _WorkflowTrackerService;
        private readonly IDocumentService _DocumentService;
        private readonly IDepartmentService _DepartmentService;
        private readonly IProcessService _ProcessService;
        private readonly IEmplService _EmplService;
        private readonly IReportService _ReportService;

        public ReportController(IWorkflowTrackerService workflowTrackerService, IDocumentService documentService, IDepartmentService departmentService, ICompanyService companyService, IAccountService accountService, IProcessService processService, IEmplService emplService, IReportService reportService)
            : base(companyService, accountService)
        {
            _WorkflowTrackerService = workflowTrackerService;
            _DocumentService = documentService;
            _DepartmentService = departmentService;
            _ProcessService = processService;
            _EmplService = emplService;
            _ReportService = reportService;
        }

        public ActionResult PerformanceDepartment()
        {
            ViewBag.DepartmentList = _DepartmentService.GetDropListDepartmentNull(null);
            return View();
        }
        
        public ActionResult DetailReport()
        {
            ViewBag.DepartmentList = _DepartmentService.GetDropListDepartmentNull(null);
            return View();
        }

        public ActionResult ReportOfRoutes()
        {
            ViewBag.ProcessList = _ProcessService.GetDropListProcessNull(null);
            return View();
        }

        [HttpPost]
        public FileContentResult GenerateDetail(ReportParametersBasicView model)
        {
            WrapperImpersonationContext contextImpersonation = new WrapperImpersonationContext(ConfigurationManager.AppSettings["ReportAdminDomain"], ConfigurationManager.AppSettings["ReportAdminUser"], ConfigurationManager.AppSettings["ReportAdminPassword"]);
            contextImpersonation.Enter();

            ApplicationDbContext context = new ApplicationDbContext();

            Excel.Application excelAppl;
            Excel.Workbook excelWorkbook;
            Excel.Worksheet excelWorksheet;

            int rowCount = 3;

            model.EndDate = model.EndDate.AddDays(1);

            var detailData = (from wfTracker in context.WFTrackerTable
                              join document in context.DocumentTable on wfTracker.DocumentTableId equals document.Id
                              join process in context.ProcessTable on document.ProcessTableId equals process.Id
                         /*     join emplAuthor in context.EmplTable on document.ApplicationUserCreatedId equals emplAuthor.ApplicationUserId

                              join emplExecutor in context.EmplTable on wfTracker.SignUserId equals emplExecutor.ApplicationUserId into eA
                              from emplExecutor in eA.DefaultIfEmpty()*/

                              where wfTracker.CreatedDate >= model.StartDate && wfTracker.CreatedDate <= model.EndDate
                              select new DetailReportModel
                              {
                                  GroupProcessName = process.GroupProcessTable.GroupProcessName,
                                  ProcessName = process.ProcessName,
                                  DocumentNumber = document.DocumentNum,
                                  Author = document.ApplicationUserCreatedId,
                                  CreateDate = document.CreatedDate,
                                  TrackerType = wfTracker.TrackerType,
                                  ActivityName = wfTracker.ActivityName,
                                  UserExecuteName = wfTracker.SignUserId,
                                  SignDate = wfTracker.SignDate,
                                  SLAOffset = wfTracker.SLAOffset,
                                  DocumentId = wfTracker.DocumentTableId,
                                  Date = wfTracker.StartDateSLA
                              }).ToList();


            foreach (var item in detailData.Where(x => x.SLAOffset > 0))
            {
                item.PerformDate = _DocumentService.GetSLAPerformDate(item.DocumentId, item.Date, item.SLAOffset);
            }

            excelAppl = new Excel.Application();
            excelAppl.Visible = false;
            excelAppl.DisplayAlerts = false;
            excelWorkbook = excelAppl.Workbooks.Add(@"C:\Template\DetailReport.xlsx");
            excelWorksheet = (Excel.Worksheet)excelWorkbook.ActiveSheet;

            Excel.Range range = excelWorksheet.get_Range("ReportDate");
            range.Value = model.StartDate.ToShortDateString() + " - " + model.EndDate.AddDays(-1).ToShortDateString();

            ApplicationUser user = _AccountService.Find(User.Identity.GetUserId());
            var timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(user.TimeZoneId);
            
            foreach (var line in detailData)
            {
                rowCount++;

                EmplTable emplAuthor = _EmplService.FirstOrDefault(x => x.ApplicationUserId == line.Author);
                excelWorksheet.Cells[rowCount, 4] = emplAuthor.SecondName + " " + emplAuthor.FirstName + " " + emplAuthor.MiddleName;

                if (line.UserExecuteName == String.Empty)
                    excelWorksheet.Cells[rowCount, 8] = "";
                else
                {
                    EmplTable emplExecutor = _EmplService.FirstOrDefault(x => x.ApplicationUserId == line.UserExecuteName);
                    excelWorksheet.Cells[rowCount, 8] = emplExecutor.SecondName + " " + emplExecutor.FirstName + " " + emplExecutor.MiddleName;
                }
                excelWorksheet.Cells[rowCount, 1] = line.GroupProcessName.ToString();
                excelWorksheet.Cells[rowCount, 2] = line.ProcessName.ToString();
                excelWorksheet.Cells[rowCount, 3] = line.DocumentNumber.ToString();
                excelWorksheet.Cells[rowCount, 5] = line.CreateDate.ToString() == "" ? "" : TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(line.CreateDate), timeZoneInfo).ToString();
                excelWorksheet.Cells[rowCount, 6] = line.TrackerType.ToString();
                excelWorksheet.Cells[rowCount, 7] = line.ActivityName.ToString();
                excelWorksheet.Cells[rowCount, 9] = line.SignDate.ToString() == "" ? "" : TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(line.SignDate), timeZoneInfo).ToString();
                excelWorksheet.Cells[rowCount, 10] = line.PerformDate.ToString() == "" ? "" : TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(line.PerformDate), timeZoneInfo).ToString();
            }

            object misValue = System.Reflection.Missing.Value;
            string path = @"C:\Template\Result\" + Guid.NewGuid().ToString() + ".xlsx";
            excelWorkbook.SaveAs(path, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue,
                misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue,
                misValue, misValue, misValue, misValue);
            excelWorkbook.Close(true, misValue, misValue);
            excelAppl.Quit();
            FileInfo file = new FileInfo(path);

            byte[] buff = null;
            FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(file.FullName).Length;
            buff = br.ReadBytes((int)numBytes);

            contextImpersonation.Leave();

            return File(buff, "application/vnd.ms-excel", "DetailReport.xls");
        }

        [HttpPost]
        public FileContentResult GenerateReport(ReportParametersBasicView model)
        {
            List<string> listdepartmentId = new List<string>();
            WrapperImpersonationContext contextImpersonation = new WrapperImpersonationContext(ConfigurationManager.AppSettings["ReportAdminDomain"], ConfigurationManager.AppSettings["ReportAdminUser"], ConfigurationManager.AppSettings["ReportAdminPassword"]);
            contextImpersonation.Enter();

            List<DepartmentTable> departmentTableList = _DepartmentService.GetPartial(x => x.Id == model.DepartmentTableId).ToList();
            listdepartmentId = _ReportService.GetParentListDepartment(departmentTableList);

            ApplicationDbContext context = new ApplicationDbContext();

            Excel.Application excelAppl;
            Excel.Workbook excelWorkbook;
            Excel.Worksheet excelWorksheet;

            int rowCount = 3;

            model.EndDate = model.EndDate.AddDays(1);
            var flatData = (from wfTracker in context.WFTrackerTable
                       join user in context.Users on wfTracker.SignUserId equals user.Id
                       join empl in context.EmplTable on user.Id equals empl.ApplicationUserId
                       join title in context.TitleTable on empl.TitleTableId equals title.Id
                       join department in context.DepartmentTable.Where(x => listdepartmentId.Contains(x.DepartmentName)) on empl.DepartmentTableId equals department.Id
                       join document in context.DocumentTable on wfTracker.DocumentTableId equals document.Id
                       join process in context.ProcessTable on document.ProcessTableId equals process.Id
                       where wfTracker.ExecutionStep == true && wfTracker.SignUserId != null
                       && (wfTracker.SignDate >= model.StartDate && wfTracker.SignDate <= model.EndDate)
                       && wfTracker.TrackerType == TrackerType.Approved
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
                           Date = wfTracker.StartDateSLA,
                           SLAOffset = wfTracker.SLAOffset
                       }).ToList();

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

            excelAppl = new Excel.Application();
            excelAppl.Visible = false;
            excelAppl.DisplayAlerts = false;
            excelWorkbook = excelAppl.Workbooks.Add(@"C:\Template\ReportDeparmentTemplate.xlsx");
            excelWorksheet = (Excel.Worksheet)excelWorkbook.ActiveSheet;

            Excel.Range range = excelWorksheet.get_Range("ReportDate");
            range.Value = model.StartDate.ToShortDateString() + " - " + model.EndDate.AddDays(-1).ToShortDateString();

            foreach (var line in gridData)
            {
                rowCount++;
                excelWorksheet.Cells[rowCount, 1] = line.FullName.ToString();
                excelWorksheet.Cells[rowCount, 2] = line.UserName.ToString();
                excelWorksheet.Cells[rowCount, 3] = line.TitleName.ToString();
                excelWorksheet.Cells[rowCount, 4] = line.DepartmentName.ToString();
                excelWorksheet.Cells[rowCount, 5] = line.ProcessName.ToString();
                excelWorksheet.Cells[rowCount, 6] = line.Count.ToString();
                excelWorksheet.Cells[rowCount, 7] = line.CountError.ToString();
            }

            object misValue = System.Reflection.Missing.Value;
            string path = @"C:\Template\Result\" + Guid.NewGuid().ToString() + ".xlsx";
            excelWorkbook.SaveAs(path, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, 
                misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, 
                misValue, misValue, misValue, misValue);
            excelWorkbook.Close(true, misValue, misValue);
            excelAppl.Quit();
            FileInfo file = new FileInfo(path);

            byte[] buff = null;
            FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(file.FullName).Length;
            buff = br.ReadBytes((int)numBytes);

            contextImpersonation.Leave();

            return File(buff, "application/vnd.ms-excel", "ReportDepartment.xls");
        }

        public FileContentResult GetReportOfRoutes(Guid? processId)
        {
            List<ProcessTable> processList = new List<ProcessTable>();
            var rows = new List<ReportProcessesView>();
            string users;

            Dictionary<Type, int> typeDict = new Dictionary<Type, int>
            {
                {typeof(WFChooseUpManager),0},
                {typeof(WFChooseStaffStructure),1},
                {typeof(WFChooseSpecificUserFromService),2},
                {typeof(WFChooseSpecificUser),3},
                {typeof(WFChooseRoleUser),4},
                {typeof(WFChooseManualExecution),5},
                {typeof(WFChooseDocUsers),6},
                {typeof(WFChooseCreatedUser),7}
            };

            Excel.Application excelAppl;
            Excel.Workbook excelWorkbook;
            Excel.Worksheet excelWorksheet;

            int rowCount = 1;

            if (processId == null)
                processList = _ProcessService.GetPartial(x => x.isApproved == true).ToList();
            else
                processList.AddRange(_ProcessService.GetPartial(x => x.Id == processId).ToList());

            foreach (var process in processList)
            {
                rows = rows.Concat(_ReportService.GetActivityStages(typeDict, _ReportService.GetActivity(process), process)).ToList();
            }

            WrapperImpersonationContext contextImpersonation = new WrapperImpersonationContext(ConfigurationManager.AppSettings["ReportAdminDomain"], ConfigurationManager.AppSettings["ReportAdminUser"], ConfigurationManager.AppSettings["ReportAdminPassword"]);
            contextImpersonation.Enter();
            
            excelAppl = new Excel.Application();
            excelAppl.Visible = false;
            excelAppl.DisplayAlerts = false;
            excelWorkbook = excelAppl.Workbooks.Add(@"C:\Template\ProcessReport.xlsx");
            excelWorksheet = (Excel.Worksheet)excelWorkbook.ActiveSheet;


            foreach (var line in rows)
            {
                rowCount++;               
                excelWorksheet.Cells[rowCount, 1] = line.Process.ProcessName.ToString();
                excelWorksheet.Cells[rowCount, 2] = line.Process.TableName.ToString();
                excelWorksheet.Cells[rowCount, 3] = line.StageName.ToString();
                excelWorksheet.Cells[rowCount, 4] = line.FilterType.ToString();
                excelWorksheet.Cells[rowCount, 5] = line.FilterText.ToString();
                Range range = (Range)excelWorksheet.Cells[rowCount, 5];
                range.Interior.Color = System.Drawing.ColorTranslator.ToOle(line.Color);
                if (line.Names.Count != 0)
                {
                    if (line.Names.Count > 1)
                    {
                        users = String.Empty;
                        foreach (EmplTable user in line.Names)
                        {
                            users += user.FullName + ";";
                        }
                        excelWorksheet.Cells[rowCount, 6] = users.ToString();
                    }
                    else
                        excelWorksheet.Cells[rowCount, 6] = line.Names.FirstOrDefault().FullName;
                }
                else
                    excelWorksheet.Cells[rowCount, 6] = "";
            }


            object misValue = System.Reflection.Missing.Value;
            string path = @"C:\Template\Result\" + Guid.NewGuid().ToString() + ".xls";
            excelWorkbook.SaveAs(path, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue,
                misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue,
                misValue, misValue, misValue, misValue);
            excelWorkbook.Close(true, misValue, misValue);
            excelAppl.Quit();
            FileInfo file = new FileInfo(path);

            byte[] buff = null;
            FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(file.FullName).Length;
            buff = br.ReadBytes((int)numBytes);

            contextImpersonation.Leave();

            return File(buff, "application/vnd.ms-excel", "ReportRoute.xls");
        }
	}

    public class ProcessReportModel
    {
        public string ProcessName { get; set; }
        public string TableName { get; set; }
        public string StageName { get; set; }
        public FilterType FilterType { get; set; }
        public string Filter { get; set; }
        public string Users { get; set; }
        public System.Drawing.Color Color { get; set; }
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
        public DateTime? Date { get; set; }
        public int SLAOffset { get; set; }
        public DateTime? SignDate { get; set; }
        public DateTime? PerformDate { get; set; }
        public string SignUserId { get; set; }
        public TrackerType TrackerType { get; set; }
        public Guid? WftId { get; set; }
    }

    public class DetailReportModel
    {
        public string GroupProcessName { get; set; }
        public string ProcessName { get; set; }
        public string DocumentNumber { get; set; }
        public string Author { get; set; }
        public DateTime? CreateDate { get; set; }
        public TrackerType TrackerType { get; set; }
        public string ActivityName { get; set; }
        public string UserExecuteName { get; set; }
        public DateTime? SignDate { get; set; }
        public DateTime? PerformDate { get; set; }
        public int SLAOffset { get; set; }
        public Guid DocumentId { get; set; }
        public DateTime? Date { get; set; }
    }
}
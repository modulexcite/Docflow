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

        public ReportController(IUnitOfWork uow, IWorkflowTrackerService workflowTrackerService, IDocumentService documentService, IDepartmentService departmentService, ICompanyService companyService, IAccountService accountService, IProcessService processService, IEmplService emplService)
            : base(uow, companyService, accountService)
        {
            _WorkflowTrackerService = workflowTrackerService;
            _DocumentService = documentService;
            _DepartmentService = departmentService;
            _ProcessService = processService;
            _EmplService = emplService;
        }

        public ActionResult PerformanceDepartment()
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

        [HttpPost]
        public FileContentResult GenerateReport(ReportParametersBasicView model)
        {
            List<string> listdepartmentId = new List<string>();
            WrapperImpersonationContext contextImpersonation = new WrapperImpersonationContext(ConfigurationManager.AppSettings["ReportAdminDomain"], ConfigurationManager.AppSettings["ReportAdminUser"], ConfigurationManager.AppSettings["ReportAdminPassword"]);
            contextImpersonation.Enter();

            List<DepartmentTable> departmentTableList = _DepartmentService.GetPartial(x => x.Id == model.DepartmentTableId).ToList();
            listdepartmentId = this.GetParentListDepartment(departmentTableList);
            ApplicationDbContext context = _uow.GetDbContext<ApplicationDbContext>();

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

            return File(buff, "application/vnd.ms-excel", "ReportDepartment.xlsx");
        }

        public List<ProcessReportModel> printActivityTree(Dictionary<Type, int> dict, Activity activity, ProcessTable processTable)
        {
            var rows = new List<ProcessReportModel>();
            string stageName = "", endText = "", users = "";
            FilterType filterType = FilterType.Other;
            ApplicationDbContext contextDb = _uow.GetDbContext<ApplicationDbContext>();
            System.Drawing.Color color = System.Drawing.Color.LightGreen;

            if (activity.GetType() == typeof(WFChooseUpManager) ||
                activity.GetType() == typeof(WFChooseStaffStructure) ||
                activity.GetType() == typeof(WFChooseSpecificUserFromService) ||
                activity.GetType() == typeof(WFChooseSpecificUser) ||
                activity.GetType() == typeof(WFChooseRoleUser) ||
                activity.GetType() == typeof(WFChooseManualExecution) ||
                activity.GetType() == typeof(WFChooseDocUsers) ||
                activity.GetType() == typeof(WFChooseCreatedUser))
            {               
                switch (dict[activity.GetType()])
                {
                    case 0:
                    case 2:
                    case 5:
                    case 6:
                    case 7:
                        stageName = activity.DisplayName;
                        endText = "";
                        filterType = FilterType.Other;
                    break;
                    case 1:
                       var activityStaffStructure = activity as WFChooseStaffStructure;
                       var activityExpressionStaff = activityStaffStructure.inputPredicate.Expression as Microsoft.CSharp.Activities.CSharpValue<Expression<Func<EmplTable, bool>>>;
                       if (activityExpressionStaff != null)
                       {                          
                           stageName = activity.DisplayName;
                           endText = activityExpressionStaff.ExpressionText;                     
                           filterType = FilterType.Predicate;

                           System.Linq.Expressions.Expression expressionTree = activityExpressionStaff.GetExpressionTree();
                           dynamic dynamicExpression = expressionTree;
                           Expression<Func<EmplTable, bool>> expressionEmpl = dynamicExpression.Body.Operand;
                           if (expressionEmpl != null)
                           {
                               var empls = _EmplService.GetPartial(expressionEmpl).Select(x => x.FullName).ToList();
                               if (empls.Count > 0)
                               {
                                   foreach (string user in empls)
                                   {
                                       users += user + ";";
                                   }
                               }
                               else
                                   color = System.Drawing.Color.LightPink;
                           }
                           else
                               color = System.Drawing.Color.LightPink;
                       }
                    break;
                    case 3:
                        var activitySpecifyUser = activity as WFChooseSpecificUser;
                        var activityExpressionSpecific = activitySpecifyUser.inputUserName.Expression as System.Activities.Expressions.Literal<string>;
                        if (activityExpressionSpecific != null)
                        {
                            stageName = activity.DisplayName;
                            endText = activityExpressionSpecific.Value;
                            filterType = FilterType.Login;

                            ApplicationUser userTable = _AccountService.FirstOrDefault(x => x.UserName == endText);
                            if (userTable != null && endText.Length > 0)
                            {
                                users = _EmplService.FirstOrDefault(x => x.ApplicationUserId == userTable.Id).FullName;
                            }
                            else
                                color = System.Drawing.Color.LightPink;
                        }
                    break;
                    case 4:
                        var activityRoleUser = activity as WFChooseRoleUser;
                        var activityExpressionRole = activityRoleUser.inputRoleName.Expression as System.Activities.Expressions.Literal<string>;
                        if (activityExpressionRole != null)
                        {
                            stageName = activity.DisplayName;
                            endText = activityExpressionRole.Value;
                            filterType = FilterType.Role;


                            RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(contextDb));
                            if (RoleManager.RoleExists(endText))
                            {
                                var names = RoleManager.FindByName(endText).Users;
                                if (names != null && names.Count() > 0)
                                {
                                    foreach (IdentityUserRole name in names)
                                    {
                                        users += _EmplService.FirstOrDefault(x => x.ApplicationUserId == name.UserId).FullName + ";";
                                    }
                                }
                                else
                                    color = System.Drawing.Color.LightPink;
                            }
                            else
                                color = System.Drawing.Color.LightPink;
                        }
                    break;
                }

                rows.Add(new ProcessReportModel()
                {
                    ProcessName = processTable.ProcessName,
                    TableName = processTable.TableName,
                    StageName = stageName,
                    FilterType = filterType,
                    Filter = endText,
                    Users = users,
                    Color = color
                });

            }

            IEnumerator<Activity> list = WorkflowInspectionServices.GetActivities(activity).GetEnumerator();

            while (list.MoveNext())
            {
                var allStepsBuf = rows.Concat(printActivityTree(dict, list.Current, processTable));
                rows = allStepsBuf.ToList();
            }

            return rows;
        }

        public FileContentResult ReportOfRoutes()
        {
            var rows = new List<ProcessReportModel>();
            var finishRows = new List<ProcessReportModel>();
            FileTable fileWF;
            Activity activity;

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

            List<ProcessTable> processList = _ProcessService.GetPartial(x => x.isApproved == true).ToList();

            foreach (var process in processList)
            {
                fileWF = _DocumentService.GetAllXAMLDocument(process.Id).OrderByDescending(x => x.Version).FirstOrDefault();

                using (System.IO.Stream stream = new System.IO.MemoryStream(fileWF.Data))
                {
                    using (var xamlReader = new System.Xaml.XamlXmlReader(stream, new System.Xaml.XamlXmlReaderSettings { LocalAssembly = System.Reflection.Assembly.GetExecutingAssembly() }))
                    {
                        activity = System.Activities.XamlIntegration.ActivityXamlServices.Load(xamlReader, new System.Activities.XamlIntegration.ActivityXamlServicesSettings { CompileExpressions = true }) as DynamicActivity;
                    }
                }
                
                rows = printActivityTree(typeDict, activity, process);
                finishRows.AddRange(rows);
            }

            WrapperImpersonationContext contextImpersonation = new WrapperImpersonationContext(ConfigurationManager.AppSettings["ReportAdminDomain"], ConfigurationManager.AppSettings["ReportAdminUser"], ConfigurationManager.AppSettings["ReportAdminPassword"]);
            contextImpersonation.Enter();

            excelAppl = new Excel.Application();
            excelAppl.Visible = false;
            excelAppl.DisplayAlerts = false;
            excelWorkbook = excelAppl.Workbooks.Add(@"C:\Template\ProcessReport.xlsx");
            excelWorksheet = (Excel.Worksheet)excelWorkbook.ActiveSheet;


            foreach (var line in finishRows)
            {
                rowCount++;
                excelWorksheet.Cells[rowCount, 1] = line.ProcessName.ToString();
                excelWorksheet.Cells[rowCount, 2] = line.TableName.ToString();
                excelWorksheet.Cells[rowCount, 3] = line.StageName.ToString();
                excelWorksheet.Cells[rowCount, 4] = line.FilterType.ToString();
                excelWorksheet.Cells[rowCount, 5] = line.Filter.ToString();
                Range range = (Range)excelWorksheet.Cells[rowCount, 5];
                range.Interior.Color = System.Drawing.ColorTranslator.ToOle(line.Color);
                excelWorksheet.Cells[rowCount, 6] = line.Users.ToString();
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
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Activities;
using System.Activities.XamlIntegration;
using System.IO;
using System.Xaml;
using System.Reflection;
using System.Linq.Expressions;
using System.Drawing;
using RapidDoc.Models.ViewModels;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Repository;
using RapidDoc.Activities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace RapidDoc.Models.Services
{
    public interface IReportService
    {
        Activity GetActivity(ProcessTable processTable);
        List<ReportProcessesView> GetActivityStages(Dictionary<Type, int> codeActivitiesTypes, Activity activity,
            ProcessTable processTable);
        List<string> GetParentListDepartment(List<DepartmentTable> departmentList);
    }
    
    public class ReportService: IReportService
    {
        private IUnitOfWork _Uow;
        private readonly IDocumentService _DocumentService;
        private readonly IDepartmentService _DepartmentService;
        private readonly IEmplService _EmplService;
        private readonly IAccountService _AccountService;

        public ReportService(IUnitOfWork uow, IDocumentService documentService, IDepartmentService departmentService, IEmplService emplService, IAccountService accountService)
        {
            _Uow = uow;
            _DocumentService = documentService;
            _DepartmentService = departmentService;
            _EmplService = emplService;
            _AccountService = accountService;
        }
        
        public Activity GetActivity(ProcessTable processTable)
        {
            Activity activity;

            FileTable wfXamlFile = _DocumentService.GetAllXAMLDocument(processTable.Id).OrderByDescending(x => x.Version).FirstOrDefault();

            using (Stream stream = new MemoryStream(wfXamlFile.Data))
            {
                using(var xamlReader = new XamlXmlReader(stream, new XamlXmlReaderSettings{LocalAssembly = Assembly.GetExecutingAssembly()}))
	            {
		            activity = ActivityXamlServices.Load(xamlReader, new ActivityXamlServicesSettings { CompileExpressions = true }) as DynamicActivity;
	            }
            }

            return activity;
        }

        public List<string> GetParentListDepartment(List<DepartmentTable> departmentList)
        {
            List<string> listDepartmentId = new List<string>();
            List<string> listDepartmentBufId = new List<string>();

            foreach (DepartmentTable depId in departmentList)
            {
                listDepartmentId.Add(depId.DepartmentName);
                List<DepartmentTable> departmentTable = _DepartmentService.GetPartial(x => x.ParentDepartmentId == depId.Id).ToList();
                listDepartmentBufId = this.GetParentListDepartment(departmentTable);
                listDepartmentId = listDepartmentId.Concat(listDepartmentBufId).Distinct().OrderBy(x => x).ToList();
            }

            return listDepartmentId;
        }


        public List<ReportProcessesView> GetActivityStages(Dictionary<Type, int> codeActivitiesTypes, Activity activity, ProcessTable processTable)
        {
            List<ReportProcessesView> processesList = new List<ReportProcessesView>();
            List<EmplTable> namesList = new List<EmplTable>();
            string filterText = String.Empty, stageName = String.Empty;
            FilterType filterType = FilterType.Other;
            ApplicationDbContext contextDb = new ApplicationDbContext();
            Color color = Color.LightGreen;
            int item = 0;
            
            if (codeActivitiesTypes.TryGetValue(activity.GetType(), out item))
            {
                switch (item)
                {
                    case 0:
                    case 2:
                    case 5:
                    case 6:
                    case 7:
                        stageName = activity.DisplayName;
                        break;
                    case 1:
                        var activityStaffStructure = activity as WFChooseStaffStructure;
                        var activityExpressionStaff = activityStaffStructure.inputPredicate.Expression as Microsoft.CSharp.Activities.CSharpValue<Expression<Func<EmplTable, bool>>>;

                        if (activityExpressionStaff != null)
                        {                          
                            stageName = activity.DisplayName;
                            filterText = activityExpressionStaff.ExpressionText;                     
                            filterType = FilterType.Predicate;

                            System.Linq.Expressions.Expression expressionTree = activityExpressionStaff.GetExpressionTree();
                            dynamic dynamicExpression = expressionTree;
                            Expression<Func<EmplTable, bool>> expressionEmpl = dynamicExpression.Body.Operand;
                            if (expressionEmpl != null)
                            {
                                namesList = _EmplService.GetPartialIntercompany(expressionEmpl).ToList();
                                if (namesList.Count <= 0)
                                    color = Color.LightPink;
                            }
                            else
                                color = Color.LightPink;
                        }

                        break;
                    case 3:
                        var activitySpecifyUser = activity as WFChooseSpecificUser;
                        var activityExpressionSpecific = activitySpecifyUser.inputUserName.Expression as System.Activities.Expressions.Literal<string>;

                        if (activityExpressionSpecific != null)
                        {
                            stageName = activity.DisplayName;
                            filterText = activityExpressionSpecific.Value;
                            filterType = FilterType.Login;

                            ApplicationUser userTable = _AccountService.FirstOrDefault(x => x.UserName == filterText);
                            if (userTable != null && filterText.Length > 0)
                            {
                                namesList.Add(_EmplService.FirstOrDefault(x => x.ApplicationUserId == userTable.Id));
                            }
                            else
                                color = Color.LightPink;
                        }

                        break;
                    case 4:
                        var activityRoleUser = activity as WFChooseRoleUser;
                        var activityExpressionRole = activityRoleUser.inputRoleName.Expression as System.Activities.Expressions.Literal<string>;

                        if (activityExpressionRole != null)
                        {
                            stageName = activity.DisplayName;
                            filterText = activityExpressionRole.Value;
                            filterType = FilterType.Role;


                            RoleManager<ApplicationRole> RoleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(contextDb));
                            if (RoleManager.RoleExists(filterText))
                            {
                                var names = RoleManager.FindByName(filterText).Users;
                                if (names != null && names.Count() > 0)
                                {
                                    foreach (IdentityUserRole name in names)
                                    {
                                        namesList.Add(_EmplService.FirstOrDefault(x => x.ApplicationUserId == name.UserId));
                                    }
                                }
                                else
                                    color = Color.LightPink;
                            }
                            else
                                color = Color.LightPink;
                        }

                        break;
                }

                processesList.Add(new ReportProcessesView()
                {
                    Process = processTable,
                    StageName = stageName,
                    FilterType = filterType,
                    FilterText = filterText,
                    Names = namesList,
                    Color = color
                });
            }

            IEnumerator<Activity> list = WorkflowInspectionServices.GetActivities(activity).GetEnumerator();

            while (list.MoveNext())
            {
                var allStepsBuf = processesList.Concat(GetActivityStages(codeActivitiesTypes, list.Current, processTable));
                processesList = allStepsBuf.ToList();
            }

            return processesList;
        }
    }
}
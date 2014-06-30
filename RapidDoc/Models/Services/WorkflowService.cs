using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Security;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Activities;
using System.Activities.DurableInstancing;
using System.Activities.Statements;
using System.Runtime.DurableInstancing;
using System.Threading;
using System.Activities.Hosting;
using RapidDoc.Activities;
using System.ServiceModel.Activities.Description;
using RapidDoc.Models.Services;
using System.Configuration;

namespace RapidDoc.Models.Services
{
    public interface IWorkflowService
    {
        WFUserFunctionResult WFMatchingUpManager(Guid documentId, string currentUserName, int level = 1, string profileName = "");
        WFUserFunctionResult WFSpecificUser(Guid documentId, String userName);
        WFUserFunctionResult WFRoleUser(Guid documentId, String roleName);
        WFUserFunctionResult WFStaffStructure(Guid documentId, Expression<Func<EmplTable, bool>> predicate, string currentUserName);
        WFUserFunctionResult WFCreatedUser(Guid documentId);
        string WFChooseSpecificUserFromService(Guid serviceId);
        void RunWorkflow(Guid documentId, string TableName, IDictionary<string, object> documentData);
        void AgreementWorkflowApprove(Guid documentId, string TableName, IDictionary<string, object> documentData);
        void AgreementWorkflowReject(Guid documentId, string TableName, IDictionary<string, object> documentData);
        void CreateTrackerRecord(DocumentState step, Guid documentId, string bookmarkName, List<WFTrackerUsersTable> listUser, string currentUser, string workflowId, bool useManual, int slaOffset, bool executionStep);
        List<Array> printActivityTree(Activity activity, string _parallel = "");
    }

    public class WorkflowService : IWorkflowService
    {
        private IUnitOfWork _uow;
        private readonly IAccountService _AccountService;
        private readonly IDocumentService _DocumentService;
        private readonly IEmplService _EmplService;
        private readonly IWorkflowTrackerService _WorkflowTrackerService;
        private readonly IEmailService _EmailService;
        private readonly IHistoryUserService _HistoryUserService;
        private readonly IServiceIncidentService _ServiceIncidentService;
        
        private static SqlWorkflowInstanceStore instanceStore;
        private static AutoResetEvent instanceUnloaded = new AutoResetEvent(false);
        private static Activity activity;
        IDictionary<string, object> outputParameters;

        private static List<Array> allSteps = new List<Array>();
        private const string keyForStep = "<step>";


        public WorkflowService(IUnitOfWork uow, IAccountService accountService, IDocumentService documentService, IEmplService emplService, IWorkflowTrackerService workflowTrackerService, IEmailService emailService, IHistoryUserService historyUserService, IServiceIncidentService serviceIncidentService)
        {
            _uow = uow;
            _AccountService = accountService;
            _DocumentService = documentService;
            _EmplService = emplService;
            _WorkflowTrackerService = workflowTrackerService;
            _EmailService = emailService;
            _HistoryUserService = historyUserService;
            _ServiceIncidentService = serviceIncidentService;
        }

        public WFUserFunctionResult WFMatchingUpManager(Guid documentId, string currentUserName, int level = 1, string profileName = "")
        {
            bool skip = false;
            var documentTable = _DocumentService.Find(documentId);
            List<WFTrackerUsersTable> userList = new List<WFTrackerUsersTable>();
            EmplTable currentEmplUser = _EmplService.FirstOrDefault(x => x.ApplicationUserId == documentTable.ApplicationUserCreatedId);
            EmplTable manager = WFMatchingUpManagerFinder(currentEmplUser, level, currentUserName, profileName);

            if (manager != null)
                userList.Add(new WFTrackerUsersTable { UserId = manager.ApplicationUserId });

            if (!String.IsNullOrEmpty(profileName) && !String.IsNullOrWhiteSpace(profileName))
            {
                if(userList.Count == 0)
                {
                    skip = true;
                }
                else
                {
                    skip = checkSkipStep(userList, documentTable.ApplicationUserCreatedId);
                }
            }

            return new WFUserFunctionResult { Users = userList, Skip = skip };
        }

        private bool checkSkipStep(List<WFTrackerUsersTable> userlist, string createdBy)
        {
            foreach (var user in userlist)
            { 
                if(user.UserId == createdBy)
                {
                    return true;
                }
            }

            return false;
        }

        private EmplTable WFMatchingUpManagerFinder(EmplTable emplTable, int level, string currentUserName, string profileName = "")
        {
            if ((level == 0 && profileName == null) || (emplTable.ProfileName == profileName || emplTable.TitleTable.ProfileName == profileName)) return emplTable;
            EmplTable manager = _EmplService.Find(emplTable.ManageId, currentUserName);

            if(manager == null)
                return null;

            level--;
            return WFMatchingUpManagerFinder(manager, level, currentUserName, profileName);
        }


        public WFUserFunctionResult WFSpecificUser(Guid documentId, String userName)
        {
            var documentTable = _DocumentService.Find(documentId);
            List<WFTrackerUsersTable> userList = new List<WFTrackerUsersTable>();
            ApplicationUser userTable = _AccountService.FirstOrDefault(x => x.UserName == userName);
            userList.Add(new WFTrackerUsersTable { UserId = userTable.Id });

            return new WFUserFunctionResult { Users = userList, Skip = checkSkipStep(userList, documentTable.ApplicationUserCreatedId) };
        }

        public WFUserFunctionResult WFRoleUser(Guid documentId, String roleName)
        {
            var documentTable = _DocumentService.Find(documentId);
            List<WFTrackerUsersTable> userList = new List<WFTrackerUsersTable>();

            ApplicationDbContext context = new ApplicationDbContext();
            RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var users = RoleManager.FindByName(roleName).Users;

            foreach (IdentityUserRole user in users)
            {
                userList.Add(new WFTrackerUsersTable { UserId = user.UserId });
            }          
            return new WFUserFunctionResult { Users = userList, Skip = checkSkipStep(userList, documentTable.ApplicationUserCreatedId) };
        }

        public WFUserFunctionResult WFStaffStructure(Guid documentId, Expression<Func<EmplTable, bool>> predicate, string currentUserName)
        {
            var documentTable = _DocumentService.Find(documentId);
            List<WFTrackerUsersTable> userList = new List<WFTrackerUsersTable>();
            var empls = _EmplService.GetPartialIntercompany(predicate).Select(x => x.ApplicationUserId).ToList();

            foreach(string empl in empls)
            {
                userList.Add(new WFTrackerUsersTable { UserId = empl });
            }

            return new WFUserFunctionResult { Users = userList, Skip = checkSkipStep(userList, documentTable.ApplicationUserCreatedId) };
        }

        public WFUserFunctionResult WFCreatedUser(Guid documentId)
        {
            var documentTable = _DocumentService.Find(documentId);
            List<WFTrackerUsersTable> userList = new List<WFTrackerUsersTable>();
            userList.Add(new WFTrackerUsersTable { UserId = documentTable.ApplicationUserCreatedId });

            return new WFUserFunctionResult { Users = userList, Skip = false };
        }

        public string WFChooseSpecificUserFromService(Guid serviceId)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            IdentityRole identityRole = rm.FindById(_ServiceIncidentService.Find(serviceId).RoleTableId);

            return identityRole.Name;
        }

        public void RunWorkflow(Guid documentId, string TableName, IDictionary<string, object> documentData)
        {
            ChooseRightWorkflow(TableName);
            _WorkflowTrackerService.SaveTrackList(documentId, printActivityTree(activity));      
            SetupInstanceStore();
            StartAndPersistInstance(documentId, DocumentState.Agreement, documentData);
            DeleteInstanceStoreOwner();
            _EmailService.SendExecutorEmail(documentId);
        }

        public void AgreementWorkflowApprove(Guid documentId, string TableName, IDictionary<string, object> documentData)
        {
            ChooseRightWorkflow(TableName);
            this.printActivityTree(activity);
            SetupInstanceStore();
            LoadAOrCompleteInstance(documentId, DocumentState.Agreement, TrackerType.Approved, documentData);
            DeleteInstanceStoreOwner();
            _HistoryUserService.SaveDomain(new HistoryUserTable { DocumentTableId = documentId, HistoryType = Models.Repository.HistoryType.ApproveDocument });
            _EmailService.SendExecutorEmail(documentId);
        }

        public void AgreementWorkflowReject(Guid documentId, string TableName, IDictionary<string, object> documentData)
        {
            ChooseRightWorkflow(TableName);
            this.printActivityTree(activity);   
            SetupInstanceStore();
            LoadAOrCompleteInstance(documentId, DocumentState.Cancelled, TrackerType.Cancelled, documentData);
            DeleteInstanceStoreOwner();
            _HistoryUserService.SaveDomain(new HistoryUserTable { DocumentTableId = documentId, HistoryType = Models.Repository.HistoryType.CancelledDocument });
            _EmailService.SendInitiatorRejectEmail(documentId);
        }

        private static void SetupInstanceStore()
        {
            try
            {
                instanceStore =
                    new SqlWorkflowInstanceStore(ConfigurationManager.ConnectionStrings["WFConnection"].ToString());
                instanceStore.InstanceCompletionAction = InstanceCompletionAction.DeleteNothing;

                InstanceHandle handle = instanceStore.CreateInstanceHandle();
                InstanceView view = instanceStore.Execute(handle, new CreateWorkflowOwnerCommand(), TimeSpan.FromSeconds(30));
                handle.Free();

                instanceStore.DefaultInstanceOwner = view.InstanceOwner;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static void DeleteInstanceStoreOwner()
        {
            try
            {
                InstanceHandle handle = instanceStore.CreateInstanceHandle();
                InstanceView view = instanceStore.Execute(handle, new DeleteWorkflowOwnerCommand(), TimeSpan.FromSeconds(30));
                handle.Free();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void StartAndPersistInstance(Guid _documentId, DocumentState _state, IDictionary<string, object> documentData)
        {
            try
            {
                var documentTable = _DocumentService.Find(_documentId);
                IDictionary<string, object> inputArguments = new Dictionary<string, object>();
                inputArguments.Add("inputStep" ,  _state);
                inputArguments.Add("inputDocumentId" ,  _documentId);
                inputArguments.Add("inputCurrentUser", HttpContext.Current.User.Identity.Name);
                inputArguments.Add("documentData", documentData);

                WorkflowApplication application = new WorkflowApplication(activity, inputArguments);
                application.InstanceStore = instanceStore;
                application.Extensions.Add(new WFTrackingParticipant());

                #region Workflow Delegates

                application.PersistableIdle = (e) =>
                {
                    var ex = e.GetInstanceExtensions<WFTrackingParticipant>();
                    outputParameters = ex.First().Outputs;
                    //instanceUnloaded.Set();
                    return PersistableIdleAction.Unload;

                };
                application.Unloaded = (e) =>
                {
                    instanceUnloaded.Set();
                };

                #endregion Workflow Delegates

                application.Persist();
                application.Run();
                instanceUnloaded.WaitOne();

                documentTable.WWFInstanceId = application.Id;
                documentTable.DocumentState = (DocumentState)outputParameters["outputStep"];
                _DocumentService.UpdateDocument(documentTable);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void LoadAOrCompleteInstance(Guid _documentId, DocumentState _state, TrackerType _trackerType, IDictionary<string, object> documentData)
        {
            var documentTable = _DocumentService.Find(_documentId);
            IEnumerable<WFTrackerTable> bookmarks;

            IDictionary<string, object> inputArguments = new Dictionary<string, object>();
            inputArguments.Add("inputStep", _state);
            inputArguments.Add("inputCurrentUser", HttpContext.Current.User.Identity.Name);
            inputArguments.Add("documentData", documentData);

            WorkflowApplication application = new WorkflowApplication(activity);
            application.InstanceStore = instanceStore;
            application.Extensions.Add(new WFTrackingParticipant());

            #region Workflow Delegates

            application.PersistableIdle = (e) =>
            {
                var ex = e.GetInstanceExtensions<WFTrackingParticipant>();
                outputParameters = ex.Last().Outputs;
                //instanceUnloaded.Set();
                return PersistableIdleAction.Unload;
            };

            application.Completed = (e) =>
            {
                outputParameters = e.Outputs;
            };

            application.Unloaded = (workflowApplicationEventArgs) =>
            {
                instanceUnloaded.Set();

            };

            #endregion Workflow Delegates

            application.Load(documentTable.WWFInstanceId);

            bookmarks = _DocumentService.GetCurrentSignStep(_documentId, HttpContext.Current.User.Identity.Name);

            if (bookmarks != null)
            {
                foreach (var bookmark in bookmarks)
                {
                    application.ResumeBookmark(bookmark.ActivityName, inputArguments);

                    application.Persist();
                    instanceUnloaded.WaitOne();             
                }
            }
            if (((DocumentState)outputParameters["outputStep"] == DocumentState.Agreement) && (_trackerType == TrackerType.Cancelled))
                _DocumentService.SaveCanceledData(bookmarks);       
            else
                _DocumentService.SaveSignData(bookmarks, _trackerType);

            documentTable.WWFInstanceId = application.Id;
            documentTable.DocumentState = (DocumentState)outputParameters["outputStep"];
            _DocumentService.UpdateDocument(documentTable);

            if(documentTable.DocumentState == DocumentState.Closed)
            {
                _EmailService.SendInitiatorClosedEmail(documentTable.Id);
            }

        }

        public void ChooseRightWorkflow(string _tableName)
        {
            Type type = Type.GetType("RapidDoc.Activities." + _tableName);
            if (type != null)
                activity = Activator.CreateInstance(type) as Activity;
        }

        public void CreateTrackerRecord(DocumentState step, Guid documentId, string bookmarkName, List<WFTrackerUsersTable> listUser, string currentUser, string activityId, bool useManual, int slaOffset, bool executionStep)
        {
            WFTrackerTable trackerTable = _WorkflowTrackerService.FirstOrDefault(x => x.ActivityID == activityId && x.DocumentTableId == documentId);
            trackerTable.Users = null;
            _WorkflowTrackerService.SaveDomain(trackerTable, currentUser);
            

            if ((step != DocumentState.Cancelled) &&
                (step != DocumentState.Closed))
            {
               /* WFTrackerTable trackerTable = _WorkflowTrackerService.FirstOrDefault(x => x.ActivityID == activityId && x.DocumentTableId == documentId);*/
                trackerTable.ActivityName = bookmarkName.Replace(keyForStep, "");
                trackerTable.Users = listUser;
                trackerTable.TrackerType = TrackerType.Waiting;
                trackerTable.ManualExecutor = useManual;
                trackerTable.SLAOffset = slaOffset;
                trackerTable.ExecutionStep = executionStep;
                trackerTable.SignDate = null;
                trackerTable.SignUserId = null;
                _WorkflowTrackerService.SaveDomain(trackerTable, currentUser);
            }
        }

        public List<Array> printActivityTree(Activity activity, string _parallel = "")
        {
            string[] myIntArray = new string[3];
            List<Array> allSteps = new List<Array>();

            if ((activity is CodeActivity) && (activity.DisplayName.IndexOf(keyForStep) > 0))
            {
                myIntArray.SetValue(activity.DisplayName.Replace(keyForStep, ""), 0);
                myIntArray.SetValue(activity.Id, 1);
                myIntArray.SetValue(_parallel, 2);
                allSteps.Add(myIntArray);
            }
            if ((activity is Parallel))
                _parallel = activity.Id;

            IEnumerator<Activity> list = WorkflowInspectionServices.GetActivities(activity).GetEnumerator();

            while (list.MoveNext())
            {
                var allStepsBuf = allSteps.Concat(printActivityTree(list.Current, _parallel));
                allSteps = allStepsBuf.ToList();
            }
            if ((activity is Parallel) && (activity.Id == _parallel))
                _parallel = "";
            return allSteps;
        }
    }
}
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
using System.Text.RegularExpressions;

namespace RapidDoc.Models.Services
{
    public interface IWorkflowService
    {
        WFUserFunctionResult WFMatchingUpManager(Guid documentId, string currentUserId, int level = 1, string profileName = "");
        WFUserFunctionResult WFSpecificUser(Guid documentId, String userName);
        WFUserFunctionResult WFRoleUser(Guid documentId, String roleName);
        WFUserFunctionResult WFStaffStructure(Guid documentId, Expression<Func<EmplTable, bool>> predicate);
        WFUserFunctionResult WFCreatedUser(Guid documentId);
        WFUserFunctionResult WFUsersDocument(Guid documentId, string currentUserId);
        WFUserFunctionResult WFChooseManual(Guid documentId, Dictionary<string, Object> documentData, string manualKey);
        string WFChooseSpecificUserFromService(string serviceName, ServiceIncidientPriority priority, ServiceIncidientLevel level, ServiceIncidientLocation location);
        void RunWorkflow(Guid documentId, string TableName, IDictionary<string, object> documentData);
        void AgreementWorkflowApprove(Guid documentId, string TableName, IDictionary<string, object> documentData);
        void AgreementWorkflowReject(Guid documentId, string TableName, IDictionary<string, object> documentData);
        void CreateTrackerRecord(DocumentState step, Guid documentId, string bookmarkName, List<WFTrackerUsersTable> listUser, string currentUserId, string workflowId, bool useManual, int slaOffset, bool executionStep);
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
        private readonly IReviewDocLogService _ReviewDocLogService;
        private readonly IServiceIncidentService _ServiceIncidentService;
        
        IDictionary<string, object> outputParameters;
        private List<Array> allSteps = new List<Array>();
        private const string keyForStep = "<step>";

        public WorkflowService(IUnitOfWork uow, IAccountService accountService, IDocumentService documentService, IEmplService emplService, 
            IWorkflowTrackerService workflowTrackerService, IEmailService emailService, IHistoryUserService historyUserService,
            IServiceIncidentService serviceIncidentService, IReviewDocLogService reviewDocLogService)
        {
            _uow = uow;
            _AccountService = accountService;
            _DocumentService = documentService;
            _EmplService = emplService;
            _WorkflowTrackerService = workflowTrackerService;
            _EmailService = emailService;
            _HistoryUserService = historyUserService;
            _ReviewDocLogService = reviewDocLogService;
            _ServiceIncidentService = serviceIncidentService;
        }

        public WFUserFunctionResult WFMatchingUpManager(Guid documentId, string currentUserId, int level = 1, string profileName = "")
        {
            bool skip = false;
            var documentTable = _DocumentService.Find(documentId);
            List<WFTrackerUsersTable> userList = new List<WFTrackerUsersTable>();
            EmplTable currentEmplUser = _EmplService.FirstOrDefault(x => x.ApplicationUserId == documentTable.ApplicationUserCreatedId);
            EmplTable manager = WFMatchingUpManagerFinder(currentEmplUser, level, currentUserId, profileName);

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
        private EmplTable WFMatchingUpManagerFinder(EmplTable emplTable, int level, string currentUserId, string profileName = "")
        {
            if ((level == 0 && profileName == null) || (emplTable.ProfileName == profileName || emplTable.TitleTable.ProfileName == profileName)) return emplTable;
            EmplTable manager = _EmplService.Find(emplTable.ManageId ?? Guid.Empty, currentUserId);

            if(manager == null)
                return null;

            level--;
            return WFMatchingUpManagerFinder(manager, level, currentUserId, profileName);
        }
        public WFUserFunctionResult WFSpecificUser(Guid documentId, String userName)
        {
            var documentTable = _DocumentService.Find(documentId);
            List<WFTrackerUsersTable> userList = new List<WFTrackerUsersTable>();
            ApplicationUser userTable = _AccountService.FirstOrDefault(x => x.UserName == userName);
            if (userTable == null)
            {
                userTable = _AccountService.Find(userName);
            }
            userList.Add(new WFTrackerUsersTable { UserId = userTable.Id });

            return new WFUserFunctionResult { Users = userList, Skip = checkSkipStep(userList, documentTable.ApplicationUserCreatedId) };
        }
        public WFUserFunctionResult WFChooseManual(Guid documentId, Dictionary<string, Object> documentData, string manualKey)
        {

            var documentTable = _DocumentService.Find(documentId); 
            List<WFTrackerUsersTable> userList = new List<WFTrackerUsersTable>();
            string  userLis = (string)documentData[manualKey];
            
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
            RoleManager.Dispose();
            return new WFUserFunctionResult { Users = userList, Skip = checkSkipStep(userList, documentTable.ApplicationUserCreatedId) };
        }
        public WFUserFunctionResult WFStaffStructure(Guid documentId, Expression<Func<EmplTable, bool>> predicate)
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
        public WFUserFunctionResult WFUsersDocument(Guid documentId, string currentUserId)
        {
            var documentTable = _DocumentService.Find(documentId);
            List<WFTrackerUsersTable> userList = new List<WFTrackerUsersTable>();
            var domainTable = _DocumentService.RouteCustomRepository(documentTable.ProcessTable.TableName).GetById(documentTable.RefDocumentId);

            if (domainTable != null)
            {
                if(domainTable.Users != null)
                {
                    string users = domainTable.Users;
                    string[] array = users.Split(',');
                    Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
                    string[] result = array.Where(a => isGuid.IsMatch(a) == true).ToArray();

                    foreach(var item in result)
                    {
                        Guid emplId = Guid.Parse(item);
                        EmplTable empl = _EmplService.Find(emplId, currentUserId);

                        if (empl != null && empl.ApplicationUserId != null && empl.ApplicationUserId != documentTable.ApplicationUserCreatedId)
                        {
                            userList.Add(new WFTrackerUsersTable { UserId = empl.ApplicationUserId });
                        }
                    }
                }
            }

            userList.Add(new WFTrackerUsersTable { UserId = documentTable.ApplicationUserCreatedId });
            return new WFUserFunctionResult { Users = userList, Skip = false };
        }
        public string WFChooseSpecificUserFromService(string serviceName, ServiceIncidientPriority priority, ServiceIncidientLevel level, ServiceIncidientLocation location)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            ServiceIncidentTable incidentTable = _ServiceIncidentService.FirstOrDefault(x => x.ServiceName == serviceName && x.ServiceIncidientLevel == level && x.ServiceIncidientPriority == priority && x.ServiceIncidientLocation == location);

            if (incidentTable != null)
            {
                IdentityRole identityRole = rm.FindById(incidentTable.RoleTableId);
                return identityRole.Name;
            }

            return String.Empty;
        }
        public void RunWorkflow(Guid documentId, string TableName, IDictionary<string, object> documentData)
        {
            Activity activity = ChooseRightWorkflow(TableName);
            _WorkflowTrackerService.SaveTrackList(documentId, printActivityTree(activity));
            SqlWorkflowInstanceStore instanceStore = SetupInstanceStore();
            StartAndPersistInstance(documentId, DocumentState.Agreement, documentData, instanceStore, activity);
            DeleteInstanceStoreOwner(instanceStore);
            _EmailService.SendExecutorEmail(documentId);
        }
        public void AgreementWorkflowApprove(Guid documentId, string TableName, IDictionary<string, object> documentData)
        {
            Activity activity = ChooseRightWorkflow(TableName);
            this.printActivityTree(activity);
            SqlWorkflowInstanceStore instanceStore = SetupInstanceStore();
            LoadAOrCompleteInstance(documentId, DocumentState.Agreement, TrackerType.Approved, documentData, instanceStore, activity);
            DeleteInstanceStoreOwner(instanceStore);
            _HistoryUserService.SaveDomain(new HistoryUserTable { DocumentTableId = documentId, HistoryType = Models.Repository.HistoryType.ApproveDocument });
            _EmailService.SendExecutorEmail(documentId);
        }
        public void AgreementWorkflowReject(Guid documentId, string TableName, IDictionary<string, object> documentData)
        {
            Activity activity = ChooseRightWorkflow(TableName);
            this.printActivityTree(activity);
            SqlWorkflowInstanceStore instanceStore = SetupInstanceStore();
            LoadAOrCompleteInstance(documentId, DocumentState.Cancelled, TrackerType.Cancelled, documentData, instanceStore, activity);
            DeleteInstanceStoreOwner(instanceStore);
            _HistoryUserService.SaveDomain(new HistoryUserTable { DocumentTableId = documentId, HistoryType = Models.Repository.HistoryType.CancelledDocument });
            _EmailService.SendInitiatorRejectEmail(documentId);
        }
        private SqlWorkflowInstanceStore SetupInstanceStore()
        {
            SqlWorkflowInstanceStore instanceStore =
                new SqlWorkflowInstanceStore(ConfigurationManager.ConnectionStrings["WFConnection"].ToString());
            InstanceView view = instanceStore.Execute(instanceStore.CreateInstanceHandle(), new CreateWorkflowOwnerCommand(), TimeSpan.FromSeconds(30));
            instanceStore.DefaultInstanceOwner = view.InstanceOwner;

            return instanceStore;
        }
        private void DeleteInstanceStoreOwner(SqlWorkflowInstanceStore instanceStore)
        {
            InstanceView view = instanceStore.Execute(instanceStore.CreateInstanceHandle(instanceStore.DefaultInstanceOwner), new DeleteWorkflowOwnerCommand(), TimeSpan.FromSeconds(30));
        }
        public void StartAndPersistInstance(Guid _documentId, DocumentState _state, IDictionary<string, object> documentData, SqlWorkflowInstanceStore instanceStore, Activity activity)
        {
            AutoResetEvent instanceUnloaded = new AutoResetEvent(false);
            var documentTable = _DocumentService.Find(_documentId);
            IDictionary<string, object> inputArguments = new Dictionary<string, object>();
            string currentUserId = HttpContext.Current.User.Identity.GetUserId();

            inputArguments.Add("inputStep" ,  _state);
            inputArguments.Add("inputDocumentId" ,  _documentId);
            inputArguments.Add("inputCurrentUser", currentUserId);
            inputArguments.Add("documentData", documentData);

            WorkflowApplication application = new WorkflowApplication(activity, inputArguments);
            application.InstanceStore = instanceStore;
            application.Extensions.Add(new WFTrackingParticipant());

            #region Workflow Delegates

            application.PersistableIdle = (e) =>
            {
                var ex = e.GetInstanceExtensions<WFTrackingParticipant>();
                outputParameters = ex.First().Outputs;
                return PersistableIdleAction.Unload;

            };
            application.Unloaded = (e) =>
            {
                instanceUnloaded.Set();
            };

            application.OnUnhandledException = (e) =>
            {
                return UnhandledExceptionAction.Terminate;
            };

            #endregion Workflow Delegates

            application.Persist();
            application.Run();
            instanceUnloaded.WaitOne();

            documentTable.WWFInstanceId = application.Id;
            documentTable.DocumentState = (DocumentState)outputParameters["outputStep"];
            _DocumentService.UpdateDocument(documentTable, currentUserId);
        }
        public void LoadAOrCompleteInstance(Guid _documentId, DocumentState _state, TrackerType _trackerType, IDictionary<string, object> documentData, SqlWorkflowInstanceStore instanceStore, Activity activity)
        {
            try
            {
                AutoResetEvent instanceUnloaded = new AutoResetEvent(false);
                var documentTable = _DocumentService.Find(_documentId);
                IEnumerable<WFTrackerTable> bookmarks;
                string currentUserId = HttpContext.Current.User.Identity.GetUserId();

                IDictionary<string, object> inputArguments = new Dictionary<string, object>();
                inputArguments.Add("inputStep", _state);
                inputArguments.Add("inputCurrentUser", currentUserId);
                inputArguments.Add("documentData", documentData);

                WorkflowApplication application = new WorkflowApplication(activity);
                application.InstanceStore = instanceStore;
                application.Extensions.Add(new WFTrackingParticipant());

                #region Workflow Delegates

                application.PersistableIdle = (e) =>
                {
                    var ex = e.GetInstanceExtensions<WFTrackingParticipant>();
                    outputParameters = ex.Last().Outputs;
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

                application.OnUnhandledException = (e) =>
                {
                    return UnhandledExceptionAction.Terminate;
                };

                #endregion Workflow Delegates

                application.Load(documentTable.WWFInstanceId);

                bookmarks = _DocumentService.GetCurrentSignStep(_documentId, currentUserId).ToList();

                if (bookmarks != null)
                {
                    foreach (var bookmark in bookmarks)
                    {
                        application.ResumeBookmark(bookmark.ActivityName, inputArguments);

                        application.Persist();
                        instanceUnloaded.WaitOne();             
                    }
                }
                if (!(((DocumentState)outputParameters["outputStep"] == DocumentState.Agreement) && (_trackerType == TrackerType.Cancelled)))
                    _DocumentService.SaveSignData(bookmarks, _trackerType);

                documentTable.WWFInstanceId = application.Id;
                documentTable.DocumentState = (DocumentState)outputParameters["outputStep"];

                int retries = 3;
                while (retries > 0)
                {
                    try
                    {
                        _DocumentService.UpdateDocument(documentTable, currentUserId);
                        break;
                    }
                    catch
                    {
                        retries = retries - 1;
                        if (retries <= 0) throw;
                            Thread.Sleep(1000);
                    }
                }

                CustomParamUpdate(documentTable, documentData);

                if(documentTable.DocumentState == DocumentState.Closed)
                {
                    _EmailService.SendInitiatorClosedEmail(documentTable.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CustomParamUpdate(DocumentTable document, IDictionary<string, object> documentData)
        {
            if(document.ProcessTable.TableName == "USR_REQ_IT_CTP_IncidentIT")
            {
                if (document.ActivityName == "Исполнитель")
                {
                    ServiceIncidientPriority priority = ((ServiceIncidientPriority)documentData["ServiceIncidientPriority"]);
                    ServiceIncidientLevel level = ((ServiceIncidientLevel)documentData["ServiceIncidientLevel"]);
                    ServiceIncidientLocation location = ((ServiceIncidientLocation)documentData["ServiceIncidientLocation"]);

                    var serviceIncident = _ServiceIncidentService.GetAll().ToList().FirstOrDefault(x => x.ServiceName == ((string)documentData["ServiceName"]) && x.ServiceIncidientPriority == priority && x.ServiceIncidientLevel == level && x.ServiceIncidientLocation == location);
                    if(serviceIncident != null)
                    {
                        var items = _WorkflowTrackerService.GetPartial(x => x.DocumentTableId == document.Id && x.ActivityName == document.ActivityName).ToList();

                        foreach (var item in items)
                        {
                            item.SLAOffset = serviceIncident.SLAIncident;
                            _WorkflowTrackerService.SaveDomain(item);
                        }
                    }
                }
            }
        }
        public Activity ChooseRightWorkflow(string _tableName)
        {
            Type type = Type.GetType("RapidDoc.Activities." + _tableName);
            if (type != null)
                return Activator.CreateInstance(type) as Activity;

            return null;
        }
        public void CreateTrackerRecord(DocumentState step, Guid documentId, string bookmarkName, List<WFTrackerUsersTable> listUser, string currentUserId, string activityId, bool useManual, int slaOffset, bool executionStep)
        {
            if ((step != DocumentState.Cancelled) &&
                (step != DocumentState.Closed))
            {
                WFTrackerTable trackerTable = _WorkflowTrackerService.FirstOrDefault(x => x.ActivityID == activityId && x.DocumentTableId == documentId);
                trackerTable.ActivityName = bookmarkName.Replace(keyForStep, "");
                if (trackerTable.Users == null || trackerTable.Users.Count() == 0)
                    trackerTable.Users = listUser;
                trackerTable.TrackerType = TrackerType.Waiting;
                trackerTable.ManualExecutor = useManual;
                trackerTable.SLAOffset = slaOffset;
                trackerTable.StartDateSLA = DateTime.UtcNow;
                trackerTable.ExecutionStep = executionStep;
                trackerTable.SignDate = null;
                trackerTable.SignUserId = null;
                _WorkflowTrackerService.SaveDomain(trackerTable, currentUserId);

                foreach(var user in listUser)
                {
                    try
                    {
                        _ReviewDocLogService.Delete(documentId, user.UserId);
                    }
                    catch (Exception)
                    {
                    }
                }

                IEnumerable<WFTrackerTable> trackerTableCancel = _WorkflowTrackerService.GetPartial(x => x.DocumentTableId == trackerTable.DocumentTableId && x.LineNum > trackerTable.LineNum && x.ActivityID != trackerTable.ActivityID).ToList();
                foreach (var item in trackerTableCancel)
                {
                    item.TrackerType = TrackerType.NonActive;
                    item.SignDate = null;
                    item.SignUserId = null;
                    item.Users.Clear();
                    item.SLAOffset = 0;
                    item.StartDateSLA = null;
                    item.ManualExecutor = false;
                    _WorkflowTrackerService.SaveDomain(item, currentUserId);
                }
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
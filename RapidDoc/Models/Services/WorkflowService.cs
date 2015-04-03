using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Security;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
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
using System.Activities.XamlIntegration;
using System.Activities.Expressions;
using RapidDoc.Models.ViewModels;
using RapidDoc.Models.Repository;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using RapidDoc.Activities.CodeActivities;

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
        WFUserFunctionResult WFChooseManual(Guid documentId, Dictionary<string, Object> documentData, string manualKey, string currentUserId);
        string WFChooseSpecificUserFromService(string serviceName, ServiceIncidientPriority priority, ServiceIncidientLevel level, ServiceIncidientLocation location);
        void RunWorkflow(DocumentTable documentTable, string TableName, IDictionary<string, object> documentData);
        void AgreementWorkflowApprove(Guid documentId, string TableName, Guid WWFInstanceId, Guid processId, IDictionary<string, object> documentData);
        void AgreementWorkflowReject(Guid documentId, string TableName, Guid WWFInstanceId, Guid processId, IDictionary<string, object> documentData);
        void AgreementWorkflowWithdraw(Guid documentId, string TableName, Guid WWFInstanceId, Guid processId);
        void CreateTrackerRecord(DocumentState step, Guid documentId, string bookmarkName, List<WFTrackerUsersTable> listUser, string currentUserId, string workflowId, bool useManual, int slaOffset, bool executionStep);
        List<Array> GetRequestTree(Activity activity, string _parallel = "");
        List<Array> GetOfficeMemoTree(Activity activity, bool parallelSequence, List<string> userList, string _parallel = "", bool _cycle = false);
        List<Array> GetTrackerList(Activity activity, IDictionary<string, object> documentData, DocumentType documentType);
        List<string> GetUniqueUserList(IDictionary<string, object> documentData, string nameField);
    }

    public class WorkflowService : IWorkflowService
    {
        private IRepository<ApplicationUser> repoUser;
        private IRepository<ServiceIncidentTable> repoIncident;
        private IUnitOfWork _uow;
        private readonly IDocumentService _DocumentService;
        private readonly IEmplService _EmplService;
        private readonly IWorkflowTrackerService _WorkflowTrackerService;
        private readonly IEmailService _EmailService;
        private readonly IHistoryUserService _HistoryUserService;
        private readonly IReviewDocLogService _ReviewDocLogService;
        private readonly ICustomCheckDocument _CustomCheckDocument;
        
        IDictionary<string, object> outputParameters;              

        public WorkflowService(IUnitOfWork uow, IDocumentService documentService, IEmplService emplService, 
            IWorkflowTrackerService workflowTrackerService, IEmailService emailService, IHistoryUserService historyUserService,
            IReviewDocLogService reviewDocLogService, ICustomCheckDocument customCheckDocument)
        {
            repoUser = uow.GetRepository<ApplicationUser>();
            repoIncident = uow.GetRepository<ServiceIncidentTable>();
            _uow = uow;
            _DocumentService = documentService;
            _EmplService = emplService;
            _WorkflowTrackerService = workflowTrackerService;
            _EmailService = emailService;
            _HistoryUserService = historyUserService;
            _ReviewDocLogService = reviewDocLogService;
            _CustomCheckDocument = customCheckDocument;
        }

        public WFUserFunctionResult WFMatchingUpManager(Guid documentId, string currentUserId, int level = 1, string profileName = "")
        {
            bool skip = false;
            var documentTable = _DocumentService.Find(documentId);
            List<WFTrackerUsersTable> userList = new List<WFTrackerUsersTable>();
            EmplTable currentEmplUser = _EmplService.FirstOrDefault(x => x.ApplicationUserId == documentTable.ApplicationUserCreatedId && x.Enable == true);
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
                    skip = checkSkipStep(documentId, userList, documentTable.ApplicationUserCreatedId);
                }
            }

            return new WFUserFunctionResult { Users = userList, Skip = skip };
        }
        private bool checkSkipStep(Guid documentId, List<WFTrackerUsersTable> userlist, string createdBy)
        {
            var trackerTables = _WorkflowTrackerService.GetPartial(x => x.DocumentTableId == documentId && x.TrackerType == TrackerType.Approved).ToList();

            foreach (var user in userlist)
            {
                if (user.UserId == createdBy || (trackerTables != null && trackerTables.Any(x => x.SignUserId == user.UserId)))
                {
                    return true;
                }
            }

            return false;
        }
        private EmplTable WFMatchingUpManagerFinder(EmplTable emplTable, int level, string currentUserId, string profileName = "")
        {
            if (((level == 0 && profileName == null) || (emplTable.ProfileName == profileName || emplTable.TitleTable.ProfileName == profileName)) && emplTable.Enable == true) return emplTable;
            EmplTable manager = _EmplService.FindIntercompany(emplTable.ManageId ?? Guid.Empty);

            if(manager == null || manager.Id == manager.ManageId)
                return null;

            level--;
            return WFMatchingUpManagerFinder(manager, level, currentUserId, profileName);
        }
        public WFUserFunctionResult WFSpecificUser(Guid documentId, String userName)
        {
            var documentTable = _DocumentService.Find(documentId);
            List<WFTrackerUsersTable> userList = new List<WFTrackerUsersTable>();
            ApplicationUser userTable = repoUser.Find(x => (x.UserName == userName || x.Id == userName) && x.Enable == true);

            if (userTable != null)
            {
                userList.Add(new WFTrackerUsersTable { UserId = userTable.Id });
            }

            return new WFUserFunctionResult { Users = userList, Skip = checkSkipStep(documentId, userList, documentTable.ApplicationUserCreatedId) };
        }
        public WFUserFunctionResult WFChooseManual(Guid documentId, Dictionary<string, Object> documentData, string manualKey, string currentUserId)
        {
            var documentTable = _DocumentService.Find(documentId);
            List<WFTrackerUsersTable> userList = new List<WFTrackerUsersTable>();
            
            if ((string)documentData[manualKey] != "" )
            {              
                string users = (string)documentData[manualKey];
                string[] array = users.Split(',');
                Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
                string[] result = array.Where(a => isGuid.IsMatch(a) == true).ToArray();

                foreach (var item in result)
                {
                    Guid emplId = Guid.Parse(item);
                    EmplTable empl = _EmplService.Find(emplId, currentUserId);

                    if (empl.Enable == true)
                        userList.Add(new WFTrackerUsersTable { UserId = empl.ApplicationUserId });
                }
            }
            return new WFUserFunctionResult { Users = userList, Skip = checkSkipStep(documentId, userList, documentTable.ApplicationUserCreatedId) }; 
        }
        public WFUserFunctionResult WFRoleUser(Guid documentId, String roleName)
        {
            var documentTable = _DocumentService.Find(documentId);
            List<WFTrackerUsersTable> userList = new List<WFTrackerUsersTable>();

            if (!String.IsNullOrEmpty(roleName))
            {
                RoleManager<ApplicationRole> RoleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(_uow.GetDbContext<ApplicationDbContext>()));

                if (RoleManager.RoleExists(roleName))
                {
                    var users = RoleManager.FindByName(roleName).Users;
                    foreach (IdentityUserRole user in users)
                    {
                        userList.Add(new WFTrackerUsersTable { UserId = user.UserId });
                    }
                    RoleManager.Dispose();
                }
            }
            return new WFUserFunctionResult { Users = userList, Skip = checkSkipStep(documentId, userList, documentTable.ApplicationUserCreatedId) };
        }
        public WFUserFunctionResult WFStaffStructure(Guid documentId, Expression<Func<EmplTable, bool>> predicate)
        {
            var documentTable = _DocumentService.Find(documentId);
            List<WFTrackerUsersTable> userList = new List<WFTrackerUsersTable>();
            var empls = _EmplService.GetPartialIntercompany(predicate).Where(x => x.Enable == true).Select(x => x.ApplicationUserId).ToList();

            foreach(string empl in empls)
            {
                userList.Add(new WFTrackerUsersTable { UserId = empl });
            }

            return new WFUserFunctionResult { Users = userList, Skip = checkSkipStep(documentId, userList, documentTable.ApplicationUserCreatedId) };
        }
        public WFUserFunctionResult WFCreatedUser(Guid documentId)
        {
            var documentTable = _DocumentService.Find(documentId);
            List<WFTrackerUsersTable> userList = new List<WFTrackerUsersTable>();

            ApplicationUser userTable = repoUser.Find(x => x.Id == documentTable.ApplicationUserCreatedId && x.Enable == true);

            if (userTable != null)
                userList.Add(new WFTrackerUsersTable { UserId = userTable.Id });

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
                        EmplTable empl = _EmplService.FindIntercompany(emplId);

                        if (empl != null && empl.Enable == true && empl.ApplicationUserId != null && empl.ApplicationUserId != documentTable.ApplicationUserCreatedId)
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
            var rm = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(_uow.GetDbContext<ApplicationDbContext>()));
            ServiceIncidentTable incidentTable = repoIncident.Find(x => x.ServiceName == serviceName && x.ServiceIncidientLevel == level && x.ServiceIncidientPriority == priority && x.ServiceIncidientLocation == location);

            if (incidentTable != null)
            {
                ApplicationRole identityRole = rm.FindById(incidentTable.RoleTableId);
                return identityRole.Name;
            }

            return String.Empty;
        }
        public void RunWorkflow(DocumentTable documentTable, string TableName, IDictionary<string, object> documentData)
        {        
            SqlWorkflowInstanceStore instanceStore = SetupInstanceStore();
            FileTable fileTableWF = GetActualFileWF(TableName, documentTable);
            Activity activity = ChooseActualWorkflow(TableName, fileTableWF);
            _WorkflowTrackerService.SaveTrackList(documentTable.Id, this.GetTrackerList(activity, documentData, documentTable.DocType));        
            StartAndPersistInstance(documentTable.Id, DocumentState.Agreement, documentData, instanceStore, activity, fileTableWF); 
            DeleteInstanceStoreOwner(instanceStore);
            _EmailService.SendExecutorEmail(documentTable.Id);
        }
        public void AgreementWorkflowApprove(Guid documentId, string TableName, Guid WWFInstanceId, Guid processId, IDictionary<string, object> documentData)
        {
            SqlWorkflowInstanceStore instanceStore = SetupInstanceStore();

            WorkflowApplicationInstance instanceInfo =
                    WorkflowApplication.GetInstance(WWFInstanceId, instanceStore);

            FileTable fileTableWF = GetRightFileWF(TableName, processId, instanceInfo);
            Activity activity = ChooseActualWorkflow(TableName, fileTableWF, instanceInfo.DefinitionIdentity != null);
            LoadAOrCompleteInstance(documentId, DocumentState.Agreement, TrackerType.Approved, documentData, instanceStore, activity, instanceInfo);
            DeleteInstanceStoreOwner(instanceStore);
            _HistoryUserService.SaveDomain(new HistoryUserTable { DocumentTableId = documentId, HistoryType = Models.Repository.HistoryType.ApproveDocument }, HttpContext.Current.User.Identity.GetUserId());
            _EmailService.SendExecutorEmail(documentId);
        }
        public void AgreementWorkflowReject(Guid documentId, string TableName, Guid WWFInstanceId, Guid processId, IDictionary<string, object> documentData)
        {
            SqlWorkflowInstanceStore instanceStore = SetupInstanceStore();

            WorkflowApplicationInstance instanceInfo =
                    WorkflowApplication.GetInstance(WWFInstanceId, instanceStore);

            FileTable fileTableWF = GetRightFileWF(TableName, processId, instanceInfo);
            Activity activity = ChooseActualWorkflow(TableName, fileTableWF, instanceInfo.DefinitionIdentity != null);
            LoadAOrCompleteInstance(documentId, DocumentState.Cancelled, TrackerType.Cancelled, documentData, instanceStore, activity, instanceInfo);
            DeleteInstanceStoreOwner(instanceStore);
            _HistoryUserService.SaveDomain(new HistoryUserTable { DocumentTableId = documentId, HistoryType = Models.Repository.HistoryType.CancelledDocument }, HttpContext.Current.User.Identity.GetUserId());
            _EmailService.SendInitiatorRejectEmail(documentId);
        }
        public void AgreementWorkflowWithdraw(Guid documentId, string tableName, Guid WWFInstanceId, Guid processId)
        {
            SqlWorkflowInstanceStore instanceStore = SetupInstanceStore();
            WorkflowApplicationInstance instanceInfo = WorkflowApplication.GetInstance(WWFInstanceId, instanceStore);

            FileTable fileTableWF = GetRightFileWF(tableName, processId, instanceInfo);
            Activity activity = ChooseActualWorkflow(tableName, fileTableWF, instanceInfo.DefinitionIdentity != null);
            WithdrawInstance(documentId, DocumentState.Cancelled, TrackerType.Cancelled, instanceStore, activity, instanceInfo);
            DeleteInstanceStoreOwner(instanceStore);
            _HistoryUserService.SaveDomain(new HistoryUserTable { DocumentTableId = documentId, HistoryType = Models.Repository.HistoryType.Withdraw }, HttpContext.Current.User.Identity.GetUserId());
        }
        private SqlWorkflowInstanceStore SetupInstanceStore()
        {
            SqlWorkflowInstanceStore instanceStore =
                new SqlWorkflowInstanceStore(ConfigurationManager.ConnectionStrings["WFConnection"].ToString());
            InstanceView view = instanceStore.Execute(instanceStore.CreateInstanceHandle(), new CreateWorkflowOwnerCommand(), TimeSpan.FromSeconds(40));
            instanceStore.DefaultInstanceOwner = view.InstanceOwner;

            return instanceStore;
        }
        private void DeleteInstanceStoreOwner(SqlWorkflowInstanceStore instanceStore)
        {
            InstanceView view = instanceStore.Execute(instanceStore.CreateInstanceHandle(instanceStore.DefaultInstanceOwner), new DeleteWorkflowOwnerCommand(), TimeSpan.FromSeconds(40));
        }

        public void StartAndPersistInstance(Guid _documentId, DocumentState _state, IDictionary<string, object> documentData, SqlWorkflowInstanceStore instanceStore, Activity activity, FileTable fileTableWF)
        {
            AutoResetEvent instanceUnloaded = new AutoResetEvent(false);
            var documentTable = _DocumentService.Find(_documentId);
            IDictionary<string, object> inputArguments = new Dictionary<string, object>();
            string currentUserId = HttpContext.Current.User.Identity.GetUserId();

            inputArguments.Add("inputStep" ,  _state);
            inputArguments.Add("inputDocumentId" ,  _documentId);
            inputArguments.Add("inputCurrentUser", currentUserId);
            inputArguments.Add("documentData", documentData);

            WorkflowIdentity workflowIdentity = new WorkflowIdentity
            {
                Name = fileTableWF.VersionName,
                Version = new Version(0, 0, 0, Convert.ToInt32(fileTableWF.Version)) 
            };
            WorkflowApplication application = new WorkflowApplication(activity, inputArguments, workflowIdentity);
            application.InstanceStore = instanceStore;
            application.Extensions.Add(new WFTrackingParticipant());

            #region Workflow Delegates

            application.PersistableIdle = (e) =>
            {
                var ex = e.GetInstanceExtensions<WFTrackingParticipant>();
                outputParameters = ex.First().Outputs;
                return PersistableIdleAction.Unload;

            };

            application.Completed = (e) =>
            {
                outputParameters = e.Outputs;
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

            //application.Persist();
            application.Run();
            instanceUnloaded.WaitOne();

            documentTable.WWFInstanceId = application.Id;
            documentTable.DocumentState = (DocumentState)outputParameters["outputStep"];
            _DocumentService.UpdateDocument(documentTable, currentUserId);
        }
        public void LoadAOrCompleteInstance(Guid _documentId, DocumentState _state, TrackerType _trackerType, IDictionary<string, object> documentData, SqlWorkflowInstanceStore instanceStore, Activity activity, WorkflowApplicationInstance instanceInfo)
        {
            try
            {
                AutoResetEvent instanceUnloaded = new AutoResetEvent(false);
                IEnumerable<WFTrackerTable> bookmarks;
                string currentUserId = HttpContext.Current.User.Identity.GetUserId();

                IDictionary<string, object> inputArguments = new Dictionary<string, object>();
                inputArguments.Add("inputStep", _state);
                inputArguments.Add("inputCurrentUser", currentUserId);
                inputArguments.Add("documentData", documentData);

                WorkflowApplication application = new WorkflowApplication(activity, instanceInfo.DefinitionIdentity);
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

                application.Load(instanceInfo);

                bookmarks = _DocumentService.GetCurrentSignStep(_documentId, currentUserId).ToList();
                _DocumentService.SaveSignData(bookmarks, _trackerType);

                if (bookmarks != null)
                {
                    foreach (var bookmark in bookmarks)
                    {
                        application.ResumeBookmark(bookmark.ActivityName, inputArguments);

                        //application.Persist();
                        instanceUnloaded.WaitOne();             
                    }
                }

                DocumentTable documentTable = _DocumentService.Find(_documentId);
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

                _CustomCheckDocument.UpdateDocumentData(documentTable, documentData);

                if (documentTable.DocumentState == DocumentState.Closed)
                {
                    _EmailService.SendInitiatorClosedEmail(documentTable.Id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void WithdrawInstance(Guid _documentId, DocumentState _state, TrackerType _trackerType,  SqlWorkflowInstanceStore instanceStore, Activity activity, WorkflowApplicationInstance instanceInfo)
        {
            try
            {
                AutoResetEvent instanceUnloaded = new AutoResetEvent(false);
                IEnumerable<WFTrackerTable> bookmarks;
                string currentUserId = HttpContext.Current.User.Identity.GetUserId();

                IDictionary<string, object> inputArguments = new Dictionary<string, object>();
                inputArguments.Add("inputStep", _state);
                inputArguments.Add("inputCurrentUser", currentUserId);
                inputArguments.Add("documentData", null);

                WorkflowApplication application = new WorkflowApplication(activity, instanceInfo.DefinitionIdentity);
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

                application.Load(instanceInfo);

                bookmarks = _DocumentService.GetCurrentSignStep(_documentId, currentUserId).ToList();
                _DocumentService.SaveSignData(bookmarks, _trackerType);

                if (bookmarks != null)
                {
                    foreach (var bookmark in bookmarks)
                    {
                        application.ResumeBookmark(bookmark.ActivityName, inputArguments);

                        //application.Persist();
                        instanceUnloaded.WaitOne();
                    }
                }

                DocumentTable documentTable = _DocumentService.Find(_documentId);
                documentTable.WWFInstanceId = Guid.Empty;
                documentTable.DocumentState = DocumentState.Created;
                documentTable.ActivityName = String.Empty;

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
                
                IEnumerable<WFTrackerTable> wftrackers = _WorkflowTrackerService.GetPartial(x => x.DocumentTableId == _documentId).ToList();
                foreach (var item in wftrackers)
                {
                    item.Users.Clear();
                    _WorkflowTrackerService.SaveDomain(item, currentUserId);
                }

                _WorkflowTrackerService.DeleteAll(_documentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FileTable GetActualFileWF(string _tableName, DocumentTable documentTable)
        {
            FileTable fileWF = _DocumentService.GetAllXAMLDocument(documentTable.ProcessTableId).OrderByDescending(x => x.Version).FirstOrDefault();
            return fileWF;
        }
        public Activity ChooseActualWorkflow(string _tableName, FileTable fileWF, bool flag = true)
        {
            if (flag == true)
            {
                using (System.IO.Stream stream = new System.IO.MemoryStream(fileWF.Data))
                {
                    using (var xamlReader = new System.Xaml.XamlXmlReader(stream, new System.Xaml.XamlXmlReaderSettings { LocalAssembly = System.Reflection.Assembly.GetExecutingAssembly() }))
                    {
                        Activity activity = System.Activities.XamlIntegration.ActivityXamlServices.Load(xamlReader, 
                            new System.Activities.XamlIntegration.ActivityXamlServicesSettings { CompileExpressions = true }) as DynamicActivity;
                        return activity;
                    }
                }
            }
            else
            { 
                Type type = Type.GetType("RapidDoc.Activities." + _tableName);
                if (type != null)
                    return Activator.CreateInstance(type) as Activity;

                return null;
            }
        }

        public FileTable GetRightFileWF(string _tableName, Guid processId, WorkflowApplicationInstance instanceInfo)
        {
            FileTable fileWF;

            if (instanceInfo != null && instanceInfo.DefinitionIdentity != null)
            {
                Int32 revision = instanceInfo.DefinitionIdentity.Version.Revision;
                return fileWF = _DocumentService.GetAllXAMLDocument(processId).Where(x => x.Version == revision.ToString()).FirstOrDefault();
            }

            return null;
        }

        public void CreateTrackerRecord(DocumentState step, Guid documentId, string bookmarkName, List<WFTrackerUsersTable> listUser, string currentUserId, string activityId, bool useManual, int slaOffset, bool executionStep)
        {
            if ((step != DocumentState.Cancelled) &&
                (step != DocumentState.Closed))
            {
                WFTrackerTable trackerTable = _WorkflowTrackerService.FirstOrDefault(x => x.ActivityID == activityId && x.DocumentTableId == documentId);
                trackerTable.ActivityName = bookmarkName;
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
                if (trackerTable.ParallelID == String.Empty)
                {
                    IEnumerable<WFTrackerTable> trackerTableCancel = _WorkflowTrackerService.GetPartial(x => x.DocumentTableId == trackerTable.DocumentTableId && x.LineNum > trackerTable.LineNum && x.ActivityID != trackerTable.ActivityID && x.TrackerType != TrackerType.NonActive).ToList();
                    foreach (var item in trackerTableCancel)
                    {
                        item.TrackerType = TrackerType.NonActive;
                        item.SignDate = null;
                        item.SignUserId = null;
                        if (item.Users != null)
                            item.Users.Clear();
                        item.SLAOffset = 0;
                        item.StartDateSLA = null;
                        item.ManualExecutor = false;
                        _WorkflowTrackerService.SaveDomain(item, currentUserId);
                    }
                }
            }
        }     

        public List<Array> GetRequestTree(Activity activity, string _parallel = "")
        {
            string[] myIntArray = new string[3];
            List<Array> allSteps = new List<Array>();

            if (activity.GetType() == typeof(WFChooseUpManager) ||
                activity.GetType() == typeof(WFChooseStaffStructure) ||
                activity.GetType() == typeof(WFChooseSpecificUserFromService) ||
                activity.GetType() == typeof(WFChooseSpecificUser) ||
                activity.GetType() == typeof(WFChooseRoleUser) ||
                activity.GetType() == typeof(WFChooseManualExecution) ||
                activity.GetType() == typeof(WFChooseDocUsers) ||
                activity.GetType() == typeof(WFChooseCreatedUser))
            {
                myIntArray.SetValue(activity.DisplayName, 0);
                myIntArray.SetValue(activity.Id, 1);
                myIntArray.SetValue(_parallel, 2);
                allSteps.Add(myIntArray);
            }
            if ((activity is Parallel))
                _parallel = activity.Id;

            IEnumerator<Activity> list = WorkflowInspectionServices.GetActivities(activity).GetEnumerator();

            while (list.MoveNext())
            {
                var allStepsBuf = allSteps.Concat(GetRequestTree(list.Current, _parallel));
                allSteps = allStepsBuf.ToList();
            }
            if ((activity is Parallel) && (activity.Id == _parallel))
                _parallel = "";
            return allSteps;
        }

        public List<Array> GetOfficeMemoTree(Activity activity, bool parallelSequence, List<string> userList, string _parallel = "", bool _cycle = false)
        {
            string[] myIntArray = new string[3];
            List<Array> allSteps = new List<Array>();

            if (activity.GetType() == typeof(WFChooseOfficeMemoSpecificUser) ||
                activity.GetType() == typeof(WFChooseUpManager))
            {
                if (_cycle == false && _parallel == String.Empty)
                {
                    myIntArray.SetValue(activity.DisplayName, 0);
                    myIntArray.SetValue(activity.Id, 1);
                    myIntArray.SetValue(_parallel, 2);
                    allSteps.Add(myIntArray);
                }
                else if (parallelSequence == true && _parallel != String.Empty)
                {
                    foreach (string item in userList)
                    {
                        myIntArray.SetValue(item, 0);
                        myIntArray.SetValue(activity.Id + item, 1);
                        myIntArray.SetValue(_parallel, 2);
                        allSteps.Add(myIntArray);
                        myIntArray = new string[3];
                    }
                }
                else if (parallelSequence == false && _parallel == String.Empty && _cycle == true)
                {
                    foreach (string item in userList)
                    {
                        myIntArray.SetValue(item, 0);
                        myIntArray.SetValue(activity.Id + item, 1);
                        myIntArray.SetValue(_parallel, 2);
                        allSteps.Add(myIntArray);
                        myIntArray = new string[3];
                    } 
                }
            }

            if (activity is ParallelForEach<string>)
                _parallel = activity.Id;
            else if (activity is DoWhile)
                _cycle = true;

            IEnumerator<Activity> list = WorkflowInspectionServices.GetActivities(activity).GetEnumerator();

            while (list.MoveNext())
            {
                var allStepsBuf = allSteps.Concat(GetOfficeMemoTree(list.Current, parallelSequence, userList, _parallel, _cycle));
                allSteps = allStepsBuf.ToList();
            }

            if ((activity is ParallelForEach<string>) && (activity.Id == _parallel))
                _parallel = "";
            if ((activity is DoWhile) && (_cycle == true))
                _cycle = false;

            return allSteps;
        }

        public List<Array> GetTrackerList(Activity activity, IDictionary<string, object> documentData, DocumentType documentType)
        {
            List<Array> allSteps = new List<Array>();
            allSteps = this.GetRequestTree(activity);

            /*
            switch (documentType)
            {
                case DocumentType.Request:
                    allSteps = this.GetRequestTree(activity);
                    break;
                case DocumentType.OfficeMemo:
                    List<string> users = this.GetUniqueUserList(documentData, "DocumentWhom");
                    allSteps = this.GetOfficeMemoTree(activity, (bool)documentData["Parallel"], users);
                    documentData["DocumentWhom"] = users;
                    break;
            }
            */
            return allSteps;
        }


        public List<string> GetUniqueUserList(IDictionary<string, object> documentData, string nameField)
        {
            List<string> ofmList = new List<string>();
            string initailStructure = (string)documentData[nameField];
            string[] arrayTempStructrue = initailStructure.Split(',');
            ofmList.Clear();

            Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
            string[] arrayStructure = arrayTempStructrue.Where(a => isGuid.IsMatch(a) == true).ToArray();
            EmplTable emplTable;

            foreach (string item in arrayStructure)
            {
                Guid applicationUserId = Guid.Parse(item);
                emplTable = _EmplService.FirstOrDefault(x => x.Id == applicationUserId && x.Enable == true);

                if (emplTable != null && !ofmList.Exists(x => x == emplTable.UserName))
                {
                    ofmList.Add(emplTable.ApplicationUserId);
                }
                else
                {
                    RoleManager<ApplicationRole> RoleManager = new RoleManager<ApplicationRole>(new RoleStore<ApplicationRole>(_uow.GetDbContext<ApplicationDbContext>()));

                    var names = RoleManager.FindById(item).Users;
                    if (names != null && names.Count() > 0)
                    {
                        foreach (IdentityUserRole name in names)
                        {
                            emplTable = _EmplService.FirstOrDefault(x => x.ApplicationUserId == name.UserId && x.Enable == true);
                            if (emplTable != null && !ofmList.Exists(x => x == emplTable.UserName))
                            {
                                ofmList.Add(emplTable.ApplicationUserId);
                            }
                        }
                    }
                }
            }

            return ofmList;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.CSharp.RuntimeBinder;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Interfaces;
using RapidDoc.Models.Repository;
using RapidDoc.Models.ViewModels;

namespace RapidDoc.Models.Services
{
    public interface IDocumentService
    {
        Guid SaveDocument(dynamic model, string tableName, Guid processId, Guid fileId, string currentUserName = "");
        IEnumerable<DocumentTable> GetAll();
        IQueryable<DocumentTable> GetAllView();
        IQueryable<DocumentTable> GetArchiveView();
        IEnumerable<DocumentTable> GetPartial(Expression<Func<DocumentTable, bool>> predicate);
        IQueryable<DocumentTable> GetAgreedDocument();
        DocumentTable Find(Guid? id);
        dynamic GetDocument(Guid documentId, string tableName = "");
        dynamic GetDocumentView(Guid documentId);
        dynamic RouteCustomModelView(string customModel);
        dynamic RouteCustomModelDomain(string customModel);
        dynamic RouteCustomRepository(string customModel);
        void UpdateDocument(DocumentTable domainTable, string currentUserName = "");
        void UpdateDocumentFields(dynamic viewTable, Guid processId);
        void SaveDocumentText(DocumentTable domainTable, string currentUserName = "");
        bool isShowDocument(Guid documentId, Guid ProcessId, string currentUserName = "", bool isAfterView = false, ApplicationUser user = null, DocumentTable documentTable = null);
        bool isSignDocument(Guid documentId, Guid ProcessId, string currentUserName = "");
        IEnumerable<WFTrackerTable> GetCurrentSignStep(Guid documentId, string currentUserName = "", ApplicationUser user = null);
        SLAStatusList SLAStatus(Guid documentId, string currentUserName = "", ApplicationUser user = null);
        void SaveSignData(IEnumerable<WFTrackerTable> trackerTables, TrackerType trackerType);
        Guid SaveFile(FileTable file);
        FileTable GetFile(Guid Id);
        IEnumerable<FileTable> GetAllFilesDocument(Guid documentFileId);
        string DeleteFile(Guid Id);
        List<ApplicationUser> GetSignUsers(DocumentTable docuTable);
        List<WFTrackerUsersTable> GetUsersSLAStatus(DocumentTable docuTable, SLAStatusList status);
        DateTime? GetSLAPerformDate(Guid DocumentId, DateTime? CreatedDate, double SLAOffset);
    }

    public class DocumentService : IDocumentService
    {
        private IRepository<ProcessTable> repoProcess;
        private IRepository<DocumentTable> repoDocument;
        private IRepository<FileTable> repoFile;
        private IUnitOfWork _uow;
        private readonly INumberSeqService _NumberSeqService;
        private readonly IProcessService _ProcessService;
        private readonly IAccountService _AccountService;
        private readonly IEmplService _EmplService;
        private readonly IWorkflowTrackerService _WorkflowTrackerService;
        private readonly IDelegationService _DelegationService;
        private readonly IDocumentReaderService _DocumentReaderService;
        private readonly IWorkScheduleService _WorkScheduleService;
        private readonly IReviewDocLogService _ReviewDocLogService;

        public DocumentService(IUnitOfWork uow, INumberSeqService numberSeqService, IProcessService processService, 
            IAccountService accountService, IEmplService emplService, IWorkflowTrackerService workflowTrackerService,
            IDelegationService delegationService, IDocumentReaderService documentReaderService, IWorkScheduleService workScheduleService,
            IReviewDocLogService reviewDocLogService)
        {
            _uow = uow;
            repoProcess = uow.GetRepository<ProcessTable>();
            repoDocument = uow.GetRepository<DocumentTable>();
            repoFile = uow.GetRepository<FileTable>();
            _NumberSeqService = numberSeqService;
            _ProcessService = processService;
            _AccountService = accountService;
            _EmplService = emplService;
            _WorkflowTrackerService = workflowTrackerService;
            _DelegationService = delegationService;
            _DocumentReaderService = documentReaderService;
            _WorkScheduleService = workScheduleService;
            _ReviewDocLogService = reviewDocLogService;
        }

        public Guid SaveDocument(dynamic viewTable, string tableName, Guid processId, Guid fileId, string currentUserName = "")
        {
            string localUserName = getCurrentUserName(currentUserName);

            var docuTable = new DocumentTable();
            docuTable.ProcessTableId = processId;
            docuTable.CreatedDate = DateTime.UtcNow;
            docuTable.ModifiedDate = docuTable.CreatedDate;
            docuTable.DocumentState = DocumentState.Created;
            docuTable.FileId = fileId;

            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
            docuTable.CompanyTableId = user.CompanyTableId;
            docuTable.ApplicationUserCreatedId = user.Id;
            docuTable.ApplicationUserModifiedId = user.Id;

            EmplTable emplTable = _EmplService.FirstOrDefault(x => x.ApplicationUserId == user.Id);
            docuTable.EmplTableId = emplTable.Id;

            Guid numberSeqId = _ProcessService.Find(processId).GroupProcessTable.NumberSeriesTableId ?? Guid.Empty;
            docuTable.DocumentNum = _NumberSeqService.GetDocumentNum(numberSeqId);

            while(_uow.GetRepository<DocumentTable>().Contains(x => x.DocumentNum == docuTable.DocumentNum))
            {
                docuTable.DocumentNum = _NumberSeqService.GetDocumentNum(numberSeqId);
            }

            _uow.GetRepository<DocumentTable>().Add(docuTable);
            _uow.Save();

            var domainTable = RouteCustomModelDomain(tableName);
            Mapper.Map(viewTable, domainTable);
            domainTable.DocumentTableId = docuTable.Id;
            domainTable.CreatedDate = DateTime.UtcNow;
            domainTable.ModifiedDate = domainTable.CreatedDate;
            RouteCustomRepository(tableName).Add(domainTable);
            _uow.Save();

            docuTable.RefDocumentId = domainTable.Id;
            _uow.GetRepository<DocumentTable>().Update(docuTable);
            _uow.Save();

            return docuTable.Id;
        }

        public void UpdateDocumentFields(dynamic viewTable, Guid processId)
        {
            ProcessTable process = _ProcessService.Find(processId);

            try
            {
                if (viewTable.Id != null)
                {
                    var domainTable = RouteCustomRepository(process.TableName).GetById(viewTable.Id);

                    if (domainTable != null)
                    {
                        Type typeDomain = Type.GetType("RapidDoc.Models.DomainModels." + process.TableName + "_Table");
                        Type typeDomainView = Type.GetType("RapidDoc.Models.ViewModels." + process.TableName + "_View");
                        Mapper.Map(viewTable, domainTable, typeDomainView, typeDomain);

                        domainTable.ModifiedDate = DateTime.UtcNow;
                        RouteCustomRepository(process.TableName).Update(domainTable);
                        _uow.Save();
                    }
                }
            }
            catch
            {
                return;
            }
        }

        public IEnumerable<DocumentTable> GetAll()
        {
            return repoDocument.All();
        }

        public IQueryable<DocumentTable> GetAllView()
        {
            string localUserName = getCurrentUserName();
            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
            DateTime currentDate = DateTime.UtcNow;
            IQueryable<DocumentTable> items = null;

            if(user == null)
            {
                List<DocumentTable> errorData = new List<DocumentTable>();
                return errorData.AsQueryable();
            }

            ApplicationDbContext contextQuery = new ApplicationDbContext();
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(contextQuery));

            if (UserManager.IsInRole(user.Id, "Administrator"))
            {
                items = from document in contextQuery.DocumentTable
                        where !(contextQuery.ReviewDocLogTable.Any(x => x.ApplicationUserCreatedId == user.Id && x.DocumentTableId == document.Id && x.isArchive == true))
                        orderby document.CreatedDate descending
                        select document;
            }
            else
            {
                items = from document in contextQuery.DocumentTable
                        where
                            (document.ApplicationUserCreatedId == user.Id ||
                                contextQuery.WFTrackerTable.Any(x => x.DocumentTableId == document.Id && x.SignUserId == null && x.TrackerType == TrackerType.Waiting && x.Users.Any(b => b.UserId == user.Id)) ||
                                contextQuery.DocumentReaderTable.Any(r => r.DocumentTableId == document.Id && r.UserId == user.Id) ||
                                (contextQuery.DelegationTable.Any(d => d.EmplTableTo.ApplicationUserId == user.Id && d.DateFrom <= currentDate && d.DateTo >= currentDate && d.isArchive == false
                                && d.CompanyTableId == user.CompanyTableId
                                && (d.GroupProcessTableId == document.ProcessTable.Id || d.GroupProcessTableId == null)
                                && (d.ProcessTableId == document.ProcessTableId || d.ProcessTableId == null
                                && contextQuery.WFTrackerTable.Any(w => w.DocumentTableId == document.Id && w.SignUserId == null && w.TrackerType == TrackerType.Waiting && w.Users.Any(b => b.UserId == d.EmplTableFrom.ApplicationUserId))
                                )))
                            )
                            &&
                            !(contextQuery.ReviewDocLogTable.Any(x => x.ApplicationUserCreatedId == user.Id && x.DocumentTableId == document.Id && x.isArchive == true))
                        orderby document.CreatedDate descending
                        select document;
            }

            return items;
        }

        public IQueryable<DocumentTable> GetArchiveView()
        {
            string localUserName = getCurrentUserName();
            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
            DateTime currentDate = DateTime.UtcNow;
            IQueryable<DocumentTable> items = null;

            if (user == null)
            {
                List<DocumentTable> errorData = new List<DocumentTable>();
                return errorData.AsQueryable();
            }

            ApplicationDbContext contextQuery = new ApplicationDbContext();
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(contextQuery));

            if (UserManager.IsInRole(user.Id, "Administrator"))
            {
                items = from document in contextQuery.DocumentTable
                        where (contextQuery.ReviewDocLogTable.Any(x => x.ApplicationUserCreatedId == user.Id && x.DocumentTableId == document.Id && x.isArchive == true))
                        orderby document.CreatedDate descending
                        select document;
            }
            else
            {
                items = from document in contextQuery.DocumentTable
                    where
                        (document.ApplicationUserCreatedId == user.Id ||
                            contextQuery.WFTrackerTable.Any(x => x.DocumentTableId == document.Id && x.SignUserId == null && x.TrackerType == TrackerType.Waiting && x.Users.Any(b => b.UserId == user.Id)) ||
                            contextQuery.DocumentReaderTable.Any(r => r.DocumentTableId == document.Id && r.UserId == user.Id) ||
                            (contextQuery.DelegationTable.Any(d => d.EmplTableTo.ApplicationUserId == user.Id && d.DateFrom <= currentDate && d.DateTo >= currentDate && d.isArchive == false
                            && d.CompanyTableId == user.CompanyTableId
                            && (d.GroupProcessTableId == document.ProcessTable.Id || d.GroupProcessTableId == null)
                            && (d.ProcessTableId == document.ProcessTableId || d.ProcessTableId == null
                            && contextQuery.WFTrackerTable.Any(w => w.DocumentTableId == document.Id && w.SignUserId == null && w.TrackerType == TrackerType.Waiting && w.Users.Any(b => b.UserId == d.EmplTableFrom.ApplicationUserId))
                            )))
                        )
                        && (contextQuery.ReviewDocLogTable.Any(x => x.ApplicationUserCreatedId == user.Id && x.DocumentTableId == document.Id && x.isArchive == true))
                    orderby document.CreatedDate descending
                    select document;
            }
         
            return items;
        }

        public IQueryable<DocumentTable> GetAgreedDocument()
        {
            string localUserName = getCurrentUserName();
            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
            IQueryable<DocumentTable> items = null;
            ApplicationDbContext contextQuery = new ApplicationDbContext();

            items = from document in contextQuery.DocumentTable
                where
                    (contextQuery.WFTrackerTable.Any(x => x.DocumentTableId == document.Id && x.SignUserId == user.Id && x.TrackerType == TrackerType.Approved))
                    &&
                    !(contextQuery.ReviewDocLogTable.Any(x => x.ApplicationUserCreatedId == user.Id && x.DocumentTableId == document.Id && x.isArchive == true))
                orderby document.CreatedDate descending
                select document;

            return items;
        }

        public IEnumerable<DocumentTable> GetPartial(Expression<Func<DocumentTable, bool>> predicate)
        {
            return repoDocument.FindAll(predicate);
        }

        public DocumentTable Find(Guid? id)
        {
            return repoDocument.Find(a => a.Id == id);
        }

        public dynamic GetDocument(Guid documentId, string tableName = "")
        {
            var documentTable = Find(documentId);

            if (tableName == String.Empty)
            {
                var processTable = repoProcess.Find(x => x.Id == documentTable.ProcessTableId);
                tableName = processTable.TableName;
            }

            var domainModel = RouteCustomRepository(tableName).GetById(documentTable.RefDocumentId);

            return domainModel;
        }

        public dynamic GetDocumentView(Guid documentId)
        {
            var documentTable = Find(documentId);
            var processTable = repoProcess.Find(x => x.Id == documentTable.ProcessTableId);

            var viewTable = RouteCustomModelView(processTable.TableName);
            var domainTable = GetDocument(documentId, processTable.TableName);
            Mapper.Map(domainTable, viewTable);
            return viewTable;
        }

        public void UpdateDocument(DocumentTable domainTable, string currentUserName = "")
        {
            string localUserName = getCurrentUserName(currentUserName);
            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
            domainTable.ApplicationUserModifiedId = user.Id;

            if (domainTable.DocumentState == DocumentState.Agreement || domainTable.DocumentState == DocumentState.Execution)
            {
                IEnumerable<WFTrackerTable> items = _WorkflowTrackerService.GetCurrentStep(x => x.DocumentTableId == domainTable.Id && x.TrackerType == TrackerType.Waiting);
                string currentName = String.Empty;

                if (items != null)
                {
                    foreach (var item in items)
                    {
                        currentName += item.ActivityName + "/";
                    }
                }

                if (currentName != String.Empty)
                {
                    currentName = currentName.Remove(currentName.Length - 1);
                }

                domainTable.ActivityName = currentName;
            }
            else if (domainTable.DocumentState == DocumentState.Closed || domainTable.DocumentState == DocumentState.Cancelled)
            {
                domainTable.ActivityName = "";
            }

            _uow.GetRepository<DocumentTable>().Update(domainTable);
            _uow.Save();
        }

        public void SaveDocumentText(DocumentTable domainTable, string currentUserName = "")
        {
            _uow.GetRepository<DocumentTable>().Update(domainTable);
            _uow.Save();
        }

        public bool isShowDocument(Guid documentId, Guid ProcessId, string currentUserName = "", bool isAfterView = false, ApplicationUser user = null, DocumentTable documentTable = null)
        {
            if (user == null)
            {
                string localUserName = getCurrentUserName(currentUserName);
                user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
            }

            if (documentTable == null)
                documentTable = Find(documentId);

            if (user.Id == documentTable.ApplicationUserCreatedId)
            {
                return true;
            }

            IEnumerable<WFTrackerTable> trackerTables = null;

            if (isAfterView == false)
            {
                trackerTables = _WorkflowTrackerService.GetCurrentStep(x => x.DocumentTableId == documentId && x.SignUserId == null);
            }
            else
            {
                trackerTables = _WorkflowTrackerService.GetPartial(x => x.DocumentTableId == documentId);
            }

            if (checkTrackUsers(trackerTables, user.Id))
            {
                return true;
            }

            ApplicationDbContext context = new ApplicationDbContext();
            UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (UserManager.IsInRole(user.Id, "Administrator"))
            {
                return true;
            }

            var delegationItems = _DelegationService.GetPartial(x => x.EmplTableTo.ApplicationUserId == user.Id
                && x.DateFrom <= DateTime.UtcNow && x.DateTo >= DateTime.UtcNow
                && x.isArchive == false && x.CompanyTableId == user.CompanyTableId);

            foreach (var delegationItem in delegationItems)
            {
                if (delegationItem.GroupProcessTableId != null || delegationItem.ProcessTableId != null)
                {
                    if (delegationItem.ProcessTableId == ProcessId)
                    {
                        if (checkTrackUsers(trackerTables, delegationItem.EmplTableFrom.ApplicationUserId))
                        {
                            return true;
                        }
                    }
                    else if (_ProcessService.Find(ProcessId).GroupProcessTableId == delegationItem.GroupProcessTableId)
                    {
                        if (checkTrackUsers(trackerTables, delegationItem.EmplTableFrom.ApplicationUserId))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if (checkTrackUsers(trackerTables, delegationItem.EmplTableFrom.ApplicationUserId))
                    {
                        return true;
                    }
                }
            }

            if(_DocumentReaderService.Contains(x => x.DocumentTableId == documentId && x.UserId == user.Id))
            {
                return true;
            }


            return false;
        }

        public bool isSignDocument(Guid documentId, Guid ProcessId, string currentUserName = "")
        {
            string localUserName = getCurrentUserName(currentUserName);
            IEnumerable<WFTrackerTable> trackerTables = _WorkflowTrackerService.GetCurrentStep(x => x.DocumentTableId == documentId && x.SignUserId == null);
            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);

            if(checkTrackUsers(trackerTables, user.Id))
            {
                return true;
            }

            var delegationItems = _DelegationService.GetPartial(x => x.EmplTableTo.ApplicationUserId == user.Id 
                && x.DateFrom <= DateTime.UtcNow && x.DateTo >= DateTime.UtcNow
                && x.isArchive == false && x.CompanyTableId == user.CompanyTableId);

            foreach (var delegationItem in delegationItems)
            {
                if (delegationItem.GroupProcessTableId != null || delegationItem.ProcessTableId != null)
                {
                    if (delegationItem.ProcessTableId == ProcessId)
                    {
                        if (checkTrackUsers(trackerTables, delegationItem.EmplTableFrom.ApplicationUserId))
                        {
                            return true;
                        }
                    }
                    else if (_ProcessService.Find(ProcessId).GroupProcessTableId == delegationItem.GroupProcessTableId)
                    {
                        if (checkTrackUsers(trackerTables, delegationItem.EmplTableFrom.ApplicationUserId))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if (checkTrackUsers(trackerTables, delegationItem.EmplTableFrom.ApplicationUserId))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public List<ApplicationUser> GetSignUsers(DocumentTable docuTable)
        {
            List<ApplicationUser> signUsers = new List<ApplicationUser>();

            if(docuTable != null)
            {
                IEnumerable<WFTrackerTable> trackerTables = _WorkflowTrackerService.GetCurrentStep(x => x.DocumentTableId == docuTable.Id && x.SignUserId == null);

                foreach (var trackerTable in trackerTables)
                {
                    if (trackerTable.Users != null)
                    {
                        foreach (var trackUser in trackerTable.Users)
                        {
                            ApplicationUser user = _AccountService.Find(trackUser.UserId);
                            if (user != null)
                                signUsers.Add(user);
                        }
                    }
                }

                List<ApplicationUser> delegationUserCheck = signUsers.ToList();

                foreach (var user in delegationUserCheck)
                {
                    var delegationItems = _DelegationService.GetPartial(x => x.EmplTableFrom.ApplicationUserId == user.Id
                        && x.DateFrom <= DateTime.UtcNow && x.DateTo >= DateTime.UtcNow
                        && x.isArchive == false && x.CompanyTableId == user.CompanyTableId);

                    foreach (var delegationItem in delegationItems)
                    {
                        if (delegationItem.GroupProcessTableId != null || delegationItem.ProcessTableId != null)
                        {
                            if (delegationItem.ProcessTableId == docuTable.ProcessTableId)
                            {
                                signUsers.Add(_AccountService.Find(delegationItem.EmplTableTo.ApplicationUserId));
                            }
                            else if (_ProcessService.Find(docuTable.ProcessTableId).GroupProcessTableId == delegationItem.GroupProcessTableId)
                            {
                                signUsers.Add(_AccountService.Find(delegationItem.EmplTableTo.ApplicationUserId));
                            }
                        }
                        else
                        {
                            signUsers.Add(_AccountService.Find(delegationItem.EmplTableTo.ApplicationUserId));
                        }
                    }
                }
            }

            return signUsers;
        }

        public SLAStatusList SLAStatus(Guid documentId, string currentUserName = "", ApplicationUser user = null)
        {
            IEnumerable<WFTrackerTable> items = _WorkflowTrackerService.GetCurrentStep(x => x.DocumentTableId == documentId && x.TrackerType == TrackerType.Waiting && x.SLAOffset > 0);

            if (items != null)
            {
                if (user == null)
                {
                    string localUserName = getCurrentUserName(currentUserName);
                    user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
                }
                if (items.Any(x => x.Users.Any(a => a.UserId == user.Id)))
                {
                    WFTrackerTable item = items.FirstOrDefault();
                    DateTime? date = GetSLAPerformDate(documentId, item.StartDateSLA, item.SLAOffset);

                    if (date != null)
                    {
                        if (date < DateTime.UtcNow)
                        {
                            return SLAStatusList.Disturbance;
                        }
                        else if (date >= DateTime.UtcNow && date.Value.AddHours(-1) <= DateTime.UtcNow)
                        {
                            return SLAStatusList.Warning;
                        }
                    }
                }
            }

            return SLAStatusList.NoWarning;
        }

        private bool checkTrackUsers(IEnumerable<WFTrackerTable> trackerTables, string userId)
        {
            if (trackerTables != null)
            {
                foreach (var trackerTable in trackerTables)
                {
                    if (trackerTable.Users != null)
                    {
                        if (trackerTable.Users.Exists(x => x.UserId == userId))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public IEnumerable<WFTrackerTable> GetCurrentSignStep(Guid documentId, string currentUserName = "", ApplicationUser user = null)
        {
            if (user == null)
            {
                string localUserName = getCurrentUserName(currentUserName);
                user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
            }
            IEnumerable<WFTrackerTable> trackerTables = _WorkflowTrackerService.GetCurrentStep(x => x.DocumentTableId == documentId && x.TrackerType == TrackerType.Waiting);
            DocumentTable document = Find(documentId);
            List<WFTrackerTable> signStep = new List<WFTrackerTable>();

            if (trackerTables != null)
            {
                foreach (var trackerTable in trackerTables)
                {
                    if (trackerTable.Users != null)
                    {
                        if (trackerTable.Users.Exists(x => x.UserId == user.Id))
                        {
                            signStep.Add(trackerTable);
                        }
                    }
                }
            }

            var delegationItems = _DelegationService.GetPartial(x => x.EmplTableTo.ApplicationUserId == user.Id
                && x.DateFrom <= DateTime.UtcNow && x.DateTo >= DateTime.UtcNow
                && x.isArchive == false && x.CompanyTableId == user.CompanyTableId);

            foreach (var delegationItem in delegationItems)
            {
                if (delegationItem.GroupProcessTableId != null || delegationItem.ProcessTableId != null)
                {
                    if (delegationItem.ProcessTableId == document.ProcessTableId)
                    {
                        foreach (var trackerTable in trackerTables)
                        {
                            if (trackerTable.Users != null)
                            {
                                if (trackerTable.Users.Exists(x => x.UserId == delegationItem.EmplTableFrom.ApplicationUserId))
                                {
                                    signStep.Add(trackerTable);
                                }
                            }
                        }
                    }
                    else if (_ProcessService.Find(document.ProcessTableId).GroupProcessTableId == delegationItem.GroupProcessTableId)
                    {
                        foreach (var trackerTable in trackerTables)
                        {
                            if (trackerTable.Users != null)
                            {
                                if (trackerTable.Users.Exists(x => x.UserId == delegationItem.EmplTableFrom.ApplicationUserId))
                                {
                                    signStep.Add(trackerTable);
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (var trackerTable in trackerTables)
                    {
                        if (trackerTable.Users != null)
                        {
                            if (trackerTable.Users.Exists(x => x.UserId == delegationItem.EmplTableFrom.ApplicationUserId))
                            {
                                signStep.Add(trackerTable);
                            }
                        }
                    }
                }
            }

            return signStep;
        }

        public void SaveSignData(IEnumerable<WFTrackerTable> trackerTables, TrackerType trackerType)
        {
            ApplicationUser userTable = _AccountService.FirstOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name);

            foreach (var trackerTable in trackerTables)
            {
                try
                {
                    trackerTable.SignDate = DateTime.UtcNow;
                    trackerTable.SignUserId = userTable.Id;
                    trackerTable.TrackerType = trackerType;
                    _WorkflowTrackerService.SaveDomain(trackerTable);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
          
        public Guid SaveFile(FileTable file)
        {
            string localUserName = getCurrentUserName();

            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
            file.ApplicationUserCreatedId = user.Id;
            file.ApplicationUserModifiedId = user.Id;
            file.CreatedDate = DateTime.UtcNow;
            file.ModifiedDate = file.CreatedDate;

            _uow.GetRepository<FileTable>().Add(file);
            _uow.Save();

            return file.Id;
        }

        public FileTable GetFile(Guid Id)
        {
            return repoFile.GetById(Id);
        }

        public IEnumerable<FileTable> GetAllFilesDocument(Guid documentFileId)
        {
            return repoFile.FindAll(x => x.DocumentFileId == documentFileId);
        }

        public string DeleteFile(Guid Id)
        {
            string fileName = repoFile.GetById(Id).FileName;
            repoFile.Delete(a => a.Id == Id);
            _uow.Save();

            return fileName;
        }

        public List<WFTrackerUsersTable> GetUsersSLAStatus(DocumentTable docuTable, SLAStatusList status)
        {
            List<WFTrackerUsersTable> users = new List<WFTrackerUsersTable>();
            IEnumerable<WFTrackerTable> items = _WorkflowTrackerService.GetCurrentStep(x => x.DocumentTableId == docuTable.Id && x.TrackerType == TrackerType.Waiting && x.SLAOffset > 0);

            if (items != null)
            {
                foreach (var item in items)
                {
                    DateTime? date = GetSLAPerformDate(docuTable.Id, item.StartDateSLA, item.SLAOffset);

                    if (date != null)
                    {
                        if (SLAStatusList.Disturbance == status && date < DateTime.UtcNow)
                        {
                            users.AddRange(item.Users);
                        }

                        if (SLAStatusList.Warning == status && date >= DateTime.UtcNow && item.ModifiedDate.AddMinutes(2) < date.Value.AddHours(-1) && date.Value.AddHours(-1) <= DateTime.UtcNow)
                        {
                            users.AddRange(item.Users);
                        }
                    }
                }
            }

            return users;
        }

        public DateTime? GetSLAPerformDate(Guid DocumentId, DateTime? CreatedDate, double SLAOffset)
        {
            if (CreatedDate != null)
            {
                DocumentTable documentTable = Find(DocumentId);

                if (documentTable != null && documentTable.ProcessTable != null)
                {
                    WorkScheduleTable scheduleTable = _WorkScheduleService.Find(documentTable.ProcessTable.WorkScheduleTableId);
                    if (scheduleTable != null)
                    {
                        CreatedDate = GetWorkStartDate(CreatedDate, scheduleTable);
                        double SLAMinutes = (SLAOffset * 60);

                        return GetSLAAddOffset(scheduleTable, CreatedDate, SLAMinutes);
                    }
                }
            }

            return null;
        }

        private DateTime GetSLAAddOffset(WorkScheduleTable scheduleTable, DateTime? CreatedDate, double SLAMinutes)
        {
            DateTime tempCreatedDate = CreatedDate ?? DateTime.MinValue;

            if ((scheduleTable.WorkEndTime - tempCreatedDate.TimeOfDay).TotalMinutes >= SLAMinutes)
            {
                return tempCreatedDate.AddMinutes(SLAMinutes);
            }
            else
            {
                SLAMinutes = SLAMinutes - (scheduleTable.WorkEndTime - tempCreatedDate.TimeOfDay).TotalMinutes;
                tempCreatedDate = GetWorkStartDate(tempCreatedDate.Date.AddDays(1), scheduleTable);

                return GetSLAAddOffset(scheduleTable, tempCreatedDate, SLAMinutes);
            }
        }

        private DateTime GetWorkStartDate(DateTime? CreatedDate, WorkScheduleTable scheduleTable)
        {
            DateTime tempCreatedDate = CreatedDate ?? DateTime.MinValue;

            tempCreatedDate = FindWorkDay(scheduleTable.Id, tempCreatedDate);

            if (tempCreatedDate.Hour > scheduleTable.WorkEndTime.Hours)
            {
                tempCreatedDate = FindWorkDay(scheduleTable.Id, tempCreatedDate);
                tempCreatedDate = tempCreatedDate.Date + scheduleTable.WorkStartTime;
            }
            else if (tempCreatedDate.Hour < scheduleTable.WorkStartTime.Hours)
            {
                tempCreatedDate = tempCreatedDate.Date + scheduleTable.WorkStartTime;
            }

            return tempCreatedDate;
        }

        private DateTime FindWorkDay(Guid scheduleId, DateTime createdDate)
        {
            DateTime tempCreatedDate = createdDate;

            while (_WorkScheduleService.CheckDayType(scheduleId, tempCreatedDate) == true)
            {
                tempCreatedDate = tempCreatedDate.AddDays(1);
            }

            return tempCreatedDate;
        }

        private string getCurrentUserName(string currentUserName = "")
        {
            if (HttpContext.Current == null && currentUserName != string.Empty)
            {
                return currentUserName;
            }
            else
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }

        public dynamic RouteCustomModelView(string customModel)
        {
            Type type = Type.GetType("RapidDoc.Models.ViewModels." + customModel + "_View");
            if (type != null)
                return Activator.CreateInstance(type) as IDocument;

            return null;
        }

        public dynamic RouteCustomModelDomain(string customModel)
        {
            Type type = Type.GetType("RapidDoc.Models.DomainModels." + customModel + "_Table");
            if (type != null)
                return Activator.CreateInstance(type) as IDocument;
            return null;
        }

        public dynamic RouteCustomRepository(string customModel)
        {
            Type type = Type.GetType("RapidDoc.Models.DomainModels." + customModel + "_Table");
            if (type != null)
            {
                var method = typeof(RapidDoc.Models.Infrastructure.IUnitOfWork).GetMethod("GetRepository");
                var methodGeneric = method.MakeGenericMethod(type);
                return methodGeneric.Invoke(_uow, null);
            }
            return null;
        }
    }
}
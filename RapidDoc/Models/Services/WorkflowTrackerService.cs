using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Repository;
using RapidDoc.Models.ViewModels;
using Microsoft.AspNet.Identity;

namespace RapidDoc.Models.Services
{
    public interface IWorkflowTrackerService
    {
        IEnumerable<WFTrackerTable> GetAll();
        IEnumerable<WFTrackerTable> GetPartial(Expression<Func<WFTrackerTable, bool>> predicate);
        IEnumerable<WFTrackerListView> GetPartialView(Expression<Func<WFTrackerTable, bool>> predicate);
        bool Contains(Expression<Func<WFTrackerTable, bool>> predicate);
        WFTrackerTable FirstOrDefault(Expression<Func<WFTrackerTable, bool>> predicate);
        void SaveDomain(WFTrackerTable domainTable, string currentUserId = "");
        void SaveTrackList(Guid documentId, List<Array> allSteps);
        WFTrackerTable Find(Guid id);
        IEnumerable<WFTrackerTable> GetCurrentStep(Expression<Func<WFTrackerTable, bool>> predicate);
        void DeleteAll(Guid documentId);
    }

    public class WorkflowTrackerService : IWorkflowTrackerService
    {
        private IRepository<WFTrackerTable> repo;
        private IUnitOfWork _uow;
        private readonly IAccountService _AccountService;
        private readonly IEmplService _EmplService;
        private readonly IDelegationService _DelegationService;

        public WorkflowTrackerService(IUnitOfWork uow, IAccountService accountService, IEmplService emplService, IDelegationService delegationService)
        {
            _uow = uow;
            repo = uow.GetRepository<WFTrackerTable>();
            _AccountService = accountService;
            _EmplService = emplService;
            _DelegationService = delegationService;
        }
        public IEnumerable<WFTrackerTable> GetAll()
        {
            return repo.All();
        }
        public IEnumerable<WFTrackerTable> GetPartial(Expression<Func<WFTrackerTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public IEnumerable<WFTrackerListView> GetPartialView(Expression<Func<WFTrackerTable, bool>> predicate)
        {
            IEnumerable<WFTrackerTable> trackerDomainItems = GetPartial(predicate).OrderBy(x => x.LineNum);
            List<WFTrackerListView> trackerViewItems = new List<WFTrackerListView>();
            int rowNum = 0;
            int tmpNum;
            string workers;
            string prevActivityID = String.Empty;

            foreach (var item in trackerDomainItems)
            {
                workers = String.Empty;
               
                foreach(var userItem in item.Users)
                {
                    var empl = _EmplService.FirstOrDefault(x => x.ApplicationUserId == userItem.UserId);

                    if (workers != String.Empty)
                    {
                        workers += ", ";
                    }
                    
                    workers += string.Format("({0}) {1} - {2}", empl.CompanyTable.AliasCompanyName, empl.FullName, empl.TitleTable.TitleName);

                    var delegationItems = _DelegationService.GetPartial(x => x.EmplTableFromId == empl.Id
                        && x.DateFrom <= DateTime.UtcNow && x.DateTo >= DateTime.UtcNow
                        && x.isArchive == false && x.CompanyTableId == empl.CompanyTable.Id);

                    bool addUser;
                    foreach (var delegationItem in delegationItems)
                    {
                        addUser = false;
                        if (delegationItem.GroupProcessTableId != null || delegationItem.ProcessTableId != null)
                        {
                            if (delegationItem.ProcessTableId == item.DocumentTable.ProcessTableId)
                            {
                                addUser = true;
                            }
                            else if (delegationItem.GroupProcessTableId == item.DocumentTable.ProcessTable.GroupProcessTableId)
                            {
                                addUser = true;
                            }
                        }
                        else
                        {
                            addUser = true;
                        }

                        if(addUser == true)
                        {
                            workers += string.Format(" ==> ({0}) {1} - {2}", delegationItem.EmplTableTo.CompanyTable.AliasCompanyName, delegationItem.EmplTableTo.FullName, delegationItem.EmplTableTo.TitleTable.TitleName);
                        }
                    }
                }

                if(item.ParallelID != String.Empty)
                {
                    if (prevActivityID != item.ParallelID)
                    {
                        rowNum = rowNum + 1;
                    }
                    tmpNum = rowNum;

                    if (prevActivityID != item.ParallelID)
                        prevActivityID = item.ParallelID;
                }
                else
                {
                    rowNum = rowNum + 1;
                    tmpNum = rowNum;
                    prevActivityID = String.Empty;
                }

                if (item.SignDate != null)
                {
                    var signEmpl = _EmplService.FirstOrDefault(x => x.ApplicationUserId == item.SignUserId);
                    trackerViewItems.Add(new WFTrackerListView { ActivityName = item.ActivityName, RowNum = tmpNum, Executors = signEmpl.FullName, SignDate = item.SignDate, TrackerType = item.TrackerType, ActivityID = item.ActivityID, SLAOffset = item.SLAOffset, CreatedDate = item.CreatedDate, PerformToDate = item.PerformToDate() });
                }
                else
                {
                    trackerViewItems.Add(new WFTrackerListView { ActivityName = item.ActivityName, RowNum = tmpNum, Executors = workers, TrackerType = item.TrackerType, ManualExecutor = item.ManualExecutor, ActivityID = item.ActivityID, SLAOffset = item.SLAOffset, CreatedDate = item.CreatedDate, PerformToDate = item.PerformToDate() });
                }
            }

            return trackerViewItems;
        }
        public WFTrackerTable FirstOrDefault(Expression<Func<WFTrackerTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }
        public bool Contains(Expression<Func<WFTrackerTable, bool>> predicate)
        {
            return repo.Contains(predicate);
        }
        public IEnumerable<WFTrackerTable> GetCurrentStep(Expression<Func<WFTrackerTable, bool>> predicate)
        {
            var endSteps = repo.FindAll(predicate);

            if (endSteps != null && endSteps.Count() > 0)
            {
                WFTrackerTable endStep = endSteps.OrderByDescending(x => x.LineNum).FirstOrDefault();

                if (endStep != null)
                {
                    if (endStep.ParallelID != String.Empty)
                    {
                        IEnumerable<WFTrackerTable> alltrack = repo.FindAll(x => x.DocumentTableId == endStep.DocumentTableId).Where(b => b.ParallelID == endStep.ParallelID);
                        if (alltrack.Any(x => x.TrackerType == TrackerType.Cancelled) && endStep.DocumentTable.DocumentState != DocumentState.Agreement)
                        {
                            return null;
                        }

                        IEnumerable<WFTrackerTable> trackers = repo.FindAll(predicate).Where(b => b.ParallelID == endStep.ParallelID).OrderByDescending(x => x.LineNum);
                        return trackers;
                    }
                    else
                    {
                        return repo.FindAll(predicate).Where(b => b.TrackerType == TrackerType.Waiting).OrderByDescending(x => x.LineNum);
                    }
                }
            }

            return null;
        }
        public void SaveTrackList(Guid documentId, List<Array> allSteps)
        {
            foreach(string[] step in allSteps)
            {
                WFTrackerTable trackerTable     = new WFTrackerTable();
                trackerTable.ActivityName       = step[0];
                trackerTable.ActivityID         = step[1];
                trackerTable.ParallelID         = step[2];
                trackerTable.DocumentTableId    = documentId;
                trackerTable.TrackerType        = TrackerType.NonActive;
                SaveDomain(trackerTable);
            }
        }
        public void SaveDomain(WFTrackerTable domainTable, string currentUserId = "")
        {
            ApplicationUser user = getCurrentUserId(currentUserId);
            if (domainTable.Id == Guid.Empty)
            {
                domainTable.CreatedDate = DateTime.UtcNow;
                domainTable.ModifiedDate = domainTable.CreatedDate;
                domainTable.ApplicationUserCreatedId = user.Id;
                domainTable.ApplicationUserModifiedId = user.Id;
                repo.Add(domainTable);
            }
            else
            {
                domainTable.ModifiedDate = DateTime.UtcNow;
                domainTable.ApplicationUserModifiedId = user.Id;
                repo.Update(domainTable);
            }
            _uow.Save();
        }
        public WFTrackerTable Find(Guid id)
        {
            return repo.GetById(id);
        }
        private ApplicationUser getCurrentUserId(string currentUserId = "")
        {
            if (currentUserId != string.Empty)
            {
                return _AccountService.Find(currentUserId);
            }
            else
            {
                return _AccountService.Find(HttpContext.Current.User.Identity.GetUserId());
            }
        }
        public void DeleteAll(Guid documentId)
        {
            repo.Delete(a => a.DocumentTableId == documentId);
            _uow.Save();
        }
    }
}
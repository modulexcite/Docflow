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
        IEnumerable<WFTrackerListView> GetPartialView(Expression<Func<WFTrackerTable, bool>> predicate, TimeZoneInfo currentTimeZoneInfo);
        bool Contains(Expression<Func<WFTrackerTable, bool>> predicate);
        WFTrackerTable FirstOrDefault(Expression<Func<WFTrackerTable, bool>> predicate);
        void SaveDomain(WFTrackerTable domainTable, string currentUserId = "");
        void SaveTrackList(Guid documentId, List<Array> allSteps);
        WFTrackerTable Find(Guid id);
        List<WFTrackerTable> GetCurrentStep(Expression<Func<WFTrackerTable, bool>> predicate);
        void DeleteAll(Guid documentId);
    }

    public class WorkflowTrackerService : IWorkflowTrackerService
    {
        private IRepository<WFTrackerTable> repo;
        private IRepository<ApplicationUser> repoUser;
        private IRepository<EmplTable> repoEmpl;
        private IRepository<DelegationTable> repoDelegation;
        private IUnitOfWork _uow;

        public WorkflowTrackerService(IUnitOfWork uow)
        {
            _uow = uow;
            repo = uow.GetRepository<WFTrackerTable>();
            repoUser = uow.GetRepository<ApplicationUser>();
            repoEmpl = uow.GetRepository<EmplTable>();
            repoDelegation = uow.GetRepository<DelegationTable>();
        }
        public IEnumerable<WFTrackerTable> GetAll()
        {
            return repo.All();
        }
        public IEnumerable<WFTrackerTable> GetPartial(Expression<Func<WFTrackerTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public IEnumerable<WFTrackerListView> GetPartialView(Expression<Func<WFTrackerTable, bool>> predicate, TimeZoneInfo currentTimeZoneInfo)
        {
            IEnumerable<WFTrackerTable> trackerDomainItems = GetPartial(predicate).OrderBy(x => x.LineNum);
            List<WFTrackerListView> trackerViewItems = new List<WFTrackerListView>();
            int rowNum = 0;
            int tmpNum;
            string workers;
            string prevActivityID = String.Empty;
            List<EmplTable> cacheEmplList = new List<EmplTable>();

            foreach (var item in trackerDomainItems)
            {
                workers = String.Empty;
               
                foreach(var userItem in item.Users)
                {
                    EmplTable empl = null;
                    if (cacheEmplList.Any(x => x.ApplicationUserId == userItem.UserId))
                    {
                        empl = cacheEmplList.FirstOrDefault(x => x.ApplicationUserId == userItem.UserId);
                    }
                    else
                    {
                        empl = repoEmpl.Find(x => x.ApplicationUserId == userItem.UserId);
                        cacheEmplList.Add(empl);
                    }

                    if (workers != String.Empty)
                    {
                        workers += ", ";
                    }
                    
                    workers += string.Format("({0}) {1} - {2}", empl.CompanyTable.AliasCompanyName, empl.FullName, empl.TitleTable.TitleName);

                    var delegationItems = repoDelegation.FindAll(x => x.EmplTableFromId == empl.Id
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
                    EmplTable signEmpl = null;
                    if (cacheEmplList.Any(x => x.ApplicationUserId == item.SignUserId))
                    {
                        signEmpl = cacheEmplList.FirstOrDefault(x => x.ApplicationUserId == item.SignUserId);
                    }
                    else
                    {
                        signEmpl = repoEmpl.Find(x => x.ApplicationUserId == item.SignUserId);
                        cacheEmplList.Add(signEmpl);
                    }

                    WFTrackerListView model = new WFTrackerListView
                    {
                        ActivityName = item.ActivityName,
                        RowNum = tmpNum,
                        Executors = signEmpl.FullName,
                        TrackerType = item.TrackerType,
                        ActivityID = item.ActivityID,
                        SLAOffset = item.SLAOffset,
                        CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(item.CreatedDate, currentTimeZoneInfo)
                    };
                    model.SignDate = TimeZoneInfo.ConvertTimeFromUtc(item.SignDate ?? DateTime.MinValue, currentTimeZoneInfo);
                    DateTime? performToDate = item.PerformToDate();
                    if (performToDate != null)
                        model.PerformToDate = TimeZoneInfo.ConvertTimeFromUtc(performToDate ?? DateTime.MinValue, currentTimeZoneInfo);
                    
                    trackerViewItems.Add(model);
                }
                else
                {
                    WFTrackerListView model = new WFTrackerListView
                    {
                        ActivityName = item.ActivityName,
                        RowNum = tmpNum,
                        Executors = workers,
                        TrackerType = item.TrackerType,
                        ManualExecutor = item.ManualExecutor,
                        ActivityID = item.ActivityID,
                        SLAOffset = item.SLAOffset,
                        CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(item.CreatedDate, currentTimeZoneInfo)
                    };
                    DateTime? performToDate = item.PerformToDate();
                    if (performToDate != null)
                        model.PerformToDate = TimeZoneInfo.ConvertTimeFromUtc(performToDate ?? DateTime.MinValue, currentTimeZoneInfo);

                    trackerViewItems.Add(model);
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
        public List<WFTrackerTable> GetCurrentStep(Expression<Func<WFTrackerTable, bool>> predicate)
        {
            var endSteps = repo.FindAll(predicate);

            if (endSteps != null && endSteps.Count() > 0)
            {
                WFTrackerTable endStep = endSteps.Where(b => b.TrackerType == TrackerType.Waiting).OrderByDescending(x => x.LineNum).FirstOrDefault();

                if (endStep != null)
                {
                    if (endStep.ParallelID != String.Empty)
                    {
                        List<WFTrackerTable> alltrack = repo.FindAll(x => x.DocumentTableId == endStep.DocumentTableId).Where(b => b.ParallelID == endStep.ParallelID).ToList();
                        if (alltrack.Any(x => x.TrackerType == TrackerType.Cancelled) && endStep.DocumentTable.DocumentState != DocumentState.Agreement)
                        {
                            return null;
                        }

                        List<WFTrackerTable> trackers = repo.FindAll(predicate).Where(b => b.ParallelID == endStep.ParallelID).OrderByDescending(x => x.LineNum).ToList();
                        return trackers;
                    }
                    else
                    {
                        return repo.FindAll(predicate).Where(b => b.TrackerType == TrackerType.Waiting).OrderByDescending(x => x.LineNum).ToList();
                    }
                }
            }
            return null;
        }
        public void SaveTrackList(Guid documentId, List<Array> allSteps)
        {
            string userid = getCurrentUserId(String.Empty);
            DateTime createdDate = DateTime.UtcNow;

            foreach (string[] step in allSteps)
            {
                WFTrackerTable trackerTable = new WFTrackerTable();
                trackerTable.ActivityName = step[0];
                trackerTable.ActivityID = step[1];
                trackerTable.ParallelID = step[2];
                trackerTable.DocumentTableId = documentId;
                trackerTable.TrackerType = TrackerType.NonActive;

                trackerTable.CreatedDate = createdDate;
                trackerTable.ModifiedDate = trackerTable.CreatedDate;
                trackerTable.ApplicationUserCreatedId = userid;
                trackerTable.ApplicationUserModifiedId = userid;
                repo.Add(trackerTable);
            }
            _uow.Commit();
        }
        public void SaveDomain(WFTrackerTable domainTable, string currentUserId = "")
        {
            string userid = getCurrentUserId(currentUserId);
            if (domainTable.Id == Guid.Empty)
            {
                domainTable.CreatedDate = DateTime.UtcNow;
                domainTable.ModifiedDate = domainTable.CreatedDate;
                domainTable.ApplicationUserCreatedId = userid;
                domainTable.ApplicationUserModifiedId = userid;
                repo.Add(domainTable);
            }
            else
            {
                domainTable.ModifiedDate = DateTime.UtcNow;
                domainTable.ApplicationUserModifiedId = userid;
                repo.Update(domainTable);
            }
            _uow.Commit();
        }

        public WFTrackerTable Find(Guid id)
        {
            return repo.GetById(id);
        }
        private string getCurrentUserId(string currentUserId = "")
        {
            if (currentUserId != string.Empty)
            {
                return currentUserId;
            }
            else
            {
                return HttpContext.Current.User.Identity.GetUserId();
            }
        }
        public void DeleteAll(Guid documentId)
        {
            repo.Delete(a => a.DocumentTableId == documentId);
            _uow.Commit();
        }
    }
}
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
using System.Data;
using System.ComponentModel;

namespace RapidDoc.Models.Services
{
    public interface IWorkflowTrackerService
    {
        IEnumerable<WFTrackerTable> GetAll();
        IEnumerable<WFTrackerTable> GetPartial(Expression<Func<WFTrackerTable, bool>> predicate);
        IEnumerable<WFTrackerListView> GetPartialView(Expression<Func<WFTrackerTable, bool>> predicate, TimeZoneInfo currentTimeZoneInfo, DocumentType documentType);
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
        public IEnumerable<WFTrackerListView> GetPartialView(Expression<Func<WFTrackerTable, bool>> predicate, TimeZoneInfo currentTimeZoneInfo, DocumentType documentType)
        {
            List<WFTrackerTable> trackerDomainItems = GetPartial(predicate).OrderBy(x => x.LineNum).ToList();
            List<EmplTable> cacheEmplList = repoEmpl.All().ToList();
            List<WFTrackerListView> trackerViewItems = new List<WFTrackerListView>();

            int rowNum = 0;
            string workers;
            string prevActivityID = String.Empty;

            foreach (var item in trackerDomainItems)
            {
                workers = String.Empty;
               
                foreach(var userItem in item.Users)
                {
                    EmplTable empl = cacheEmplList.FirstOrDefault(x => x.ApplicationUserId == userItem.UserId);

                    if (workers != String.Empty)
                    {
                        workers += ", ";
                    }
                    
                    workers += string.Format("({0}) {1} - {2}", empl.CompanyTable.AliasCompanyName, empl.FullName, empl.TitleTable.TitleName);

                    var delegationItems = repoDelegation.FindAll(x => x.EmplTableFromId == empl.Id
                        && x.DateFrom <= DateTime.UtcNow && x.DateTo >= DateTime.UtcNow
                        && x.isArchive == false && x.CompanyTableId == empl.CompanyTable.Id).ToList();

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
                        prevActivityID = item.ParallelID;
                    }
                }
                else
                {
                    rowNum = rowNum + 1;
                    prevActivityID = String.Empty;
                }

                if (item.SignDate != null)
                {
                    EmplTable signEmpl = cacheEmplList.FirstOrDefault(x => x.ApplicationUserId == item.SignUserId);
                    EmplTable createdEmpl = cacheEmplList.FirstOrDefault(x => x.ApplicationUserId == item.ApplicationUserCreatedId);

                    WFTrackerListView model = new WFTrackerListView
                    {
                        ActivityName = item.ActivityName,
                        RowNum = rowNum,
                        Executors = signEmpl.FullName,
                        SignUserId = item.SignUserId,
                        TrackerType = item.TrackerType,
                        ActivityID = item.ActivityID,
                        ParallelID = item.ParallelID,
                        SLAOffset = item.SLAOffset,
                        CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(item.CreatedDate, currentTimeZoneInfo),
                        Comments = item.Comments,
                        CreatedBy = createdEmpl != null ? createdEmpl.FullName : item.CreatedBy
                    };
                    model.SignDate = TimeZoneInfo.ConvertTimeFromUtc(item.SignDate ?? DateTime.MinValue, currentTimeZoneInfo);
                    DateTime? performToDate = item.PerformToDate();
                    if (performToDate != null)
                        model.PerformToDate = TimeZoneInfo.ConvertTimeFromUtc(performToDate ?? DateTime.MinValue, currentTimeZoneInfo);

                    if (documentType == DocumentType.Request || (documentType == DocumentType.Task && item.TrackerType != TrackerType.NonActive) || (!trackerViewItems.Any(x => x.SignUserId == item.SignUserId) && documentType == DocumentType.OfficeMemo))
                        trackerViewItems.Add(model);
                }
                else
                {
                    EmplTable createdEmpl = cacheEmplList.FirstOrDefault(x => x.ApplicationUserId == item.ApplicationUserCreatedId);

                    WFTrackerListView model = new WFTrackerListView
                    {
                        ActivityName = item.ActivityName,
                        RowNum = rowNum,
                        Executors = workers,
                        TrackerType = item.TrackerType,
                        ManualExecutor = item.ManualExecutor,
                        ActivityID = item.ActivityID,
                        ParallelID = item.ParallelID,
                        SLAOffset = item.SLAOffset,
                        CreatedDate = TimeZoneInfo.ConvertTimeFromUtc(item.CreatedDate, currentTimeZoneInfo),
                        Comments = item.Comments,
                        CreatedBy = createdEmpl != null ? createdEmpl.FullName : item.CreatedBy
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
                    if (endStep.ParallelID != String.Empty && endStep.DocumentTable.DocType != DocumentType.OfficeMemo)
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

            using(var bcp = new System.Data.SqlClient.SqlBulkCopy(System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                bcp.BatchSize = allSteps.Count;
                bcp.DestinationTableName = "[dbo].[WFTrackerTable]";
                DataTable table = new DataTable();
                table.Columns.Add("Id", typeof(Guid));
                table.Columns.Add("LineNum", typeof(int));
                table.Columns.Add("DocumentTableId", typeof(Guid));
                table.Columns.Add("ActivityName", typeof(string));
                table.Columns.Add("ActivityID", typeof(string));
                table.Columns.Add("ParallelID", typeof(string));
                table.Columns.Add("SignUserId", typeof(string));
                table.Columns.Add("SignDate", typeof(DateTime));
                table.Columns.Add("TrackerType", typeof(RapidDoc.Models.Repository.TrackerType));
                table.Columns.Add("ManualExecutor", typeof(Boolean));
                table.Columns.Add("SLAOffset", typeof(int));
                table.Columns.Add("ExecutionStep", typeof(Boolean));
                table.Columns.Add("TimeStamp", typeof(Byte[]));
                table.Columns.Add("CreatedDate", typeof(DateTime));
                table.Columns.Add("ModifiedDate", typeof(DateTime));
                table.Columns.Add("ApplicationUserCreatedId", typeof(string));
                table.Columns.Add("ApplicationUserModifiedId", typeof(string));
                table.Columns.Add("StartDateSLA", typeof(DateTime));

                foreach (string[] step in allSteps)
                {
                    DataRow row = table.NewRow();
                    row["Id"] = Guid.NewGuid();
                    row["LineNum"] = DBNull.Value;
                    row["DocumentTableId"] = documentId;
                    row["ActivityName"] = step[0];
                    row["ActivityID"] = step[1];
                    row["ParallelID"] = step[2];
                    row["SignUserId"] = DBNull.Value;
                    row["SignDate"] = DBNull.Value;
                    row["TrackerType"] = TrackerType.NonActive;
                    row["ManualExecutor"] = 0;
                    row["SLAOffset"] = 0;
                    row["ExecutionStep"] = 0;
                    row["TimeStamp"] = DBNull.Value;
                    row["CreatedDate"] = createdDate;
                    row["ModifiedDate"] = createdDate;
                    row["ApplicationUserCreatedId"] = userid;
                    row["ApplicationUserModifiedId"] = userid;
                    row["StartDateSLA"] = DBNull.Value;

                    table.Rows.Add(row);
                }

                bcp.WriteToServer(table);
                _uow.Commit();
            }
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
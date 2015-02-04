using AutoMapper;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.Repository;
using RapidDoc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RapidDoc.Models.Services
{
    public interface IDelegationService
    {
        IEnumerable<DelegationTable> GetAll();
        IEnumerable<DelegationView> GetAllView();
        IEnumerable<DelegationTable> GetPartial(Expression<Func<DelegationTable, bool>> predicate);
        IEnumerable<DelegationView> GetPartialView(Expression<Func<DelegationTable, bool>> predicate);
        IEnumerable<DelegationTable> GetPartialIntercompany(Expression<Func<DelegationTable, bool>> predicate);
        IEnumerable<DelegationView> GetPartialIntercompanyView(Expression<Func<DelegationTable, bool>> predicate);
        DelegationTable FirstOrDefault(Expression<Func<DelegationTable, bool>> predicate);
        DelegationView FirstOrDefaultView(Expression<Func<DelegationTable, bool>> predicate);
        void Save(DelegationView viewTable);
        void SaveDomain(DelegationTable domainTable);
        void Delete(Guid id);
        DelegationTable Find(Guid id);
        DelegationView FindView(Guid id);
        List<WFTrackerTable> GetDelegationUsers(DocumentTable document, ApplicationUser currentUser, IEnumerable<WFTrackerTable> trackerTables);
        List<ApplicationUser> GetDelegationUsers(DocumentTable document, List<ApplicationUser> users);
        bool CheckDelegation(DocumentTable document, ApplicationUser user, ProcessTable process, IEnumerable<WFTrackerTable> trackerTables);
        bool CheckTrackerUsers(IEnumerable<WFTrackerTable> trackerTables, string userId);
        bool CheckTrackerUsers(WFTrackerTable trackerTable, string userId);
    }

    public class DelegationService : IDelegationService
    {
        private IRepository<DelegationTable> repo;
        private IRepository<ApplicationUser> repoUser;
        private IUnitOfWork _uow;

        public DelegationService(IUnitOfWork uow)
        {
            _uow = uow;
            repo = uow.GetRepository<DelegationTable>();
            repoUser = uow.GetRepository<ApplicationUser>();
        }
        public IEnumerable<DelegationTable> GetAll()
        {
            ApplicationUser user = repoUser.GetById(HttpContext.Current.User.Identity.GetUserId());
            return repo.FindAll(x => x.CompanyTableId == user.CompanyTableId).OrderByDescending(x => x.CreatedDate);
        }
        public IEnumerable<DelegationView> GetAllView()
        {
            var items = Mapper.Map<IEnumerable<DelegationTable>, IEnumerable<DelegationView>>(GetAll());
            return items;
        }
        public IEnumerable<DelegationTable> GetPartial(Expression<Func<DelegationTable, bool>> predicate)
        {
            ApplicationUser user = repoUser.GetById(HttpContext.Current.User.Identity.GetUserId());
            return repo.FindAll(predicate).Where(x => x.CompanyTableId == user.CompanyTableId);
        }
        public IEnumerable<DelegationView> GetPartialView(Expression<Func<DelegationTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<DelegationTable>, IEnumerable<DelegationView>>(GetPartial(predicate));
            return items;
        }
        public IEnumerable<DelegationTable> GetPartialIntercompany(Expression<Func<DelegationTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public IEnumerable<DelegationView> GetPartialIntercompanyView(Expression<Func<DelegationTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<DelegationTable>, IEnumerable<DelegationView>>(GetPartialIntercompany(predicate));
            return items;
        }
        public DelegationTable FirstOrDefault(Expression<Func<DelegationTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }
        public DelegationView FirstOrDefaultView(Expression<Func<DelegationTable, bool>> predicate)
        {
            return Mapper.Map<DelegationTable, DelegationView>(FirstOrDefault(predicate));
        }
        public void Save(DelegationView viewTable)
        {
            if (viewTable.Id == null)
            {
                var domainTable = new DelegationTable();
                Mapper.Map(viewTable, domainTable);
                SaveDomain(domainTable);
            }
            else
            {
                var domainTable = Find(viewTable.Id ?? Guid.Empty);
                Mapper.Map(viewTable, domainTable);
                SaveDomain(domainTable);
            }
        }
        public void SaveDomain(DelegationTable domainTable)
        {
            ApplicationUser user = repoUser.GetById(HttpContext.Current.User.Identity.GetUserId());
            if (domainTable.Id == Guid.Empty)
            {
                domainTable.CreatedDate = DateTime.UtcNow;
                domainTable.ModifiedDate = domainTable.CreatedDate;
                domainTable.CompanyTableId = user.CompanyTableId;
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
        public void Delete(Guid id)
        {
            repo.Delete(a => a.Id == id);
            _uow.Save();
        }
        public DelegationTable Find(Guid id)
        {
            ApplicationUser user = repoUser.GetById(HttpContext.Current.User.Identity.GetUserId());
            return repo.Find(a => a.Id == id && a.CompanyTableId == user.CompanyTableId);
        }
        public DelegationView FindView(Guid id)
        {
            return Mapper.Map<DelegationTable, DelegationView>(Find(id));
        }

        public List<WFTrackerTable> GetDelegationUsers(DocumentTable document, ApplicationUser user, IEnumerable<WFTrackerTable> trackerTables)
        {
            List<WFTrackerTable> result = new List<WFTrackerTable>();

            var delegationItems = GetPartial(x => x.EmplTableTo.ApplicationUserId == user.Id
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
                            if (CheckTrackerUsers(trackerTable, delegationItem.EmplTableFrom.ApplicationUserId))
                            {
                                result.Add(trackerTable);
                            }
                        }
                    }
                    else if (document.ProcessTable.GroupProcessTableId == delegationItem.GroupProcessTableId)
                    {
                        foreach (var trackerTable in trackerTables)
                        {
                            if (CheckTrackerUsers(trackerTable, delegationItem.EmplTableFrom.ApplicationUserId))
                            {
                                result.Add(trackerTable);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var trackerTable in trackerTables)
                    {
                        if (CheckTrackerUsers(trackerTable, delegationItem.EmplTableFrom.ApplicationUserId))
                        {
                            result.Add(trackerTable);
                        }
                    }
                }
            }

            return result;
        }

        public List<ApplicationUser> GetDelegationUsers(DocumentTable document, List<ApplicationUser> users)
        {
            List<ApplicationUser> result = new List<ApplicationUser>();

            foreach (var user in users)
            {
                var delegationItems = GetPartialIntercompany(x => x.EmplTableFrom.ApplicationUserId == user.Id
                    && x.DateFrom <= DateTime.UtcNow && x.DateTo >= DateTime.UtcNow
                    && x.isArchive == false && x.CompanyTableId == user.CompanyTableId);

                foreach (var delegationItem in delegationItems)
                {
                    if (delegationItem.GroupProcessTableId != null || delegationItem.ProcessTableId != null)
                    {
                        if (delegationItem.ProcessTableId == document.ProcessTableId)
                        {
                            result.Add(repoUser.GetById(delegationItem.EmplTableTo.ApplicationUserId));
                        }
                        else if (document.ProcessTable.GroupProcessTableId == delegationItem.GroupProcessTableId)
                        {
                            result.Add(repoUser.GetById(delegationItem.EmplTableTo.ApplicationUserId));
                        }
                    }
                    else
                    {
                        result.Add(repoUser.GetById(delegationItem.EmplTableTo.ApplicationUserId));
                    }
                }
            }

            return result;
        }

        public bool CheckDelegation(DocumentTable document, ApplicationUser user, ProcessTable process, IEnumerable<WFTrackerTable> trackerTables)
        {
            var delegationItems = GetPartial(x => x.EmplTableTo.ApplicationUserId == user.Id
                && x.DateFrom <= DateTime.UtcNow && x.DateTo >= DateTime.UtcNow
                && x.isArchive == false && x.CompanyTableId == user.CompanyTableId);

            foreach (var delegationItem in delegationItems)
            {
                if (delegationItem.GroupProcessTableId != null || delegationItem.ProcessTableId != null)
                {
                    if (delegationItem.ProcessTableId == process.Id)
                    {
                        if (CheckTrackerUsers(trackerTables, delegationItem.EmplTableFrom.ApplicationUserId))
                        {
                            return true;
                        }
                    }
                    else if (process.GroupProcessTableId == delegationItem.GroupProcessTableId)
                    {
                        if (CheckTrackerUsers(trackerTables, delegationItem.EmplTableFrom.ApplicationUserId))
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if (CheckTrackerUsers(trackerTables, delegationItem.EmplTableFrom.ApplicationUserId))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CheckTrackerUsers(IEnumerable<WFTrackerTable> trackerTables, string userId)
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

        public bool CheckTrackerUsers(WFTrackerTable trackerTable, string userId)
        {
            if (trackerTable.Users != null)
            {
                if (trackerTable.Users.Exists(x => x.UserId == userId))
                    return true;
            }

            return false;
        }
    }
}
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
    public interface IProcessService
    {
        IEnumerable<ProcessTable> GetAll(string currentUserName = "");
        IEnumerable<ProcessView> GetAllView();
        IEnumerable<ProcessTable> GetPartial(Expression<Func<ProcessTable, bool>> predicate, string currentUserName = "");
        IEnumerable<ProcessView> GetPartialView(Expression<Func<ProcessTable, bool>> predicate);
        IEnumerable<ProcessTable> GetPartialIntercompany(Expression<Func<ProcessTable, bool>> predicate);
        IEnumerable<ProcessView> GetPartialIntercompanyView(Expression<Func<ProcessTable, bool>> predicate);
        ProcessTable FirstOrDefault(Expression<Func<ProcessTable, bool>> predicate);
        ProcessView FirstOrDefaultView(Expression<Func<ProcessTable, bool>> predicate);
        void Save(ProcessView viewTable);
        void SaveDomain(ProcessTable domainTable, string currentUserName = "");
        void Delete(Guid id);
        ProcessTable Find(Guid? id, string currentUserName = "");
        ProcessView FindView(Guid id);
        SelectList GetDropListProcessNull(Guid? id);
        SelectList GetDropListProcess(Guid? id);
    }

    public class ProcessService : IProcessService
    {
        private IRepository<ProcessTable> repo;
        private IUnitOfWork _uow;
        private readonly IAccountService _AccountService;

        public ProcessService(IUnitOfWork uow, IAccountService accountService)
        {
            _uow = uow;
            repo = uow.GetRepository<ProcessTable>();
            _AccountService = accountService;
        }

        public IEnumerable<ProcessTable> GetAll(string currentUserName = "")
        {
            string localUserName = getCurrentUserName(currentUserName);
            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
            return repo.FindAll(x => x.CompanyTableId == user.CompanyTableId);
        }

        public IEnumerable<ProcessTable> GetPartial(Expression<Func<ProcessTable, bool>> predicate, string currentUserName = "")
        {
            string localUserName = getCurrentUserName(currentUserName);
            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
            return repo.FindAll(predicate).Where(x => x.CompanyTableId == user.CompanyTableId);
        }

        public IEnumerable<ProcessView> GetPartialView(Expression<Func<ProcessTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<ProcessTable>, IEnumerable<ProcessView>>(GetPartial(predicate));
            return items;
        }

        public IEnumerable<ProcessTable> GetPartialIntercompany(Expression<Func<ProcessTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }

        public IEnumerable<ProcessView> GetPartialIntercompanyView(Expression<Func<ProcessTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<ProcessTable>, IEnumerable<ProcessView>>(GetPartialIntercompany(predicate));
            return items;
        }

        public IEnumerable<ProcessView> GetAllView()
        {
            var items = Mapper.Map<IEnumerable<ProcessTable>, IEnumerable<ProcessView>>(GetAll());
            return items;
        }

        public ProcessTable FirstOrDefault(Expression<Func<ProcessTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }

        public ProcessView FirstOrDefaultView(Expression<Func<ProcessTable, bool>> predicate)
        {
            return Mapper.Map<ProcessTable, ProcessView>(FirstOrDefault(predicate));
        }

        public void Save(ProcessView viewTable)
        {
            if (viewTable.Id == null)
            {
                var domainTable = new ProcessTable();
                Mapper.Map(viewTable, domainTable);
                SaveDomain(domainTable);
            }
            else
            {
                var domainTable = Find(viewTable.Id);
                Mapper.Map(viewTable, domainTable);
                SaveDomain(domainTable);
            }
        }

        public void SaveDomain(ProcessTable domainTable, string currentUserName = "")
        {
            string localUserName = getCurrentUserName(currentUserName);
            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);

            if (domainTable.Id == Guid.Empty)
            {
                domainTable.Id = Guid.NewGuid();
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

        public ProcessTable Find(Guid? id, string currentUserName = "")
        {
            string localUserName = getCurrentUserName(currentUserName);
            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
            return repo.Find(a => a.Id == id && a.CompanyTableId == user.CompanyTableId);
        }

        public ProcessView FindView(Guid id)
        {
            return Mapper.Map<ProcessTable, ProcessView>(Find(id));
        }

        public SelectList GetDropListProcessNull(Guid? id)
        {
            var items = GetAllView().ToList();
            items.Insert(0, new ProcessView { ProcessName = UIElementRes.UIElement.NoValue, Id = null });
            return new SelectList(items, "Id", "ProcessName", id);
        }

        public SelectList GetDropListProcess(Guid? id)
        {
            var items = GetAllView().ToList();
            return new SelectList(items, "Id", "ProcessName", id);
        }

        private string getCurrentUserName(string currentUserName = "")
        {
            if ((HttpContext.Current == null || HttpContext.Current.User.Identity.Name == String.Empty) && currentUserName != string.Empty)
            {
                return currentUserName;
            }
            else
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }
    }
}
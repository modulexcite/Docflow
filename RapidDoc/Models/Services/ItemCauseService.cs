using System;
using System.Web;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNet.Identity;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using RapidDoc.Models.Repository;
using RapidDoc.Models.Infrastructure;
using System.Web.Mvc;


namespace RapidDoc.Models.Services
{
    public interface IItemCauseService
    {
        IEnumerable<ItemCauseTable> GetAll();
        IEnumerable<ItemCauseView> GetAllView();
        IEnumerable<ItemCauseTable> GetPartial(Expression<Func<ItemCauseTable, bool>> predicate);
        IEnumerable<ItemCauseView> GetPartialView(Expression<Func<ItemCauseTable, bool>> predicate);
        IEnumerable<ItemCauseTable> GetPartialIntercompany(Expression<Func<ItemCauseTable, bool>> predicate);
        IEnumerable<ItemCauseView> GetPartialIntercompanyView(Expression<Func<ItemCauseTable, bool>> predicate);
        ItemCauseTable FirstOrDefault(Expression<Func<ItemCauseTable, bool>> predicate);
        ItemCauseView FirstOrDefaultView(Expression<Func<ItemCauseTable, bool>> prediacate);
        bool Contains(Expression<Func<ItemCauseTable, bool>> predicate);
        void Save(ItemCauseView viewTable);
        void SaveDomain(ItemCauseTable domainTable);
        void Delete(Guid id);
        ItemCauseTable Find(Guid id);
        ItemCauseView FindView(Guid id);
        List<ItemCauseView> GetCurrentUserItemsCause(List<ItemCauseView> list, DepartmentTable departmentTable, Guid companyId);
    }

    public class ItemCauseService : IItemCauseService
    {
        private IRepository<ItemCauseTable> repo;
        private IRepository<ApplicationUser> repoUser;
        private IRepository<DepartmentTable> repoDepartment;
        private IUnitOfWork uow;

        public ItemCauseService(IUnitOfWork _uow)
        {
            uow = _uow;
            repo = uow.GetRepository<ItemCauseTable>();
            repoUser = uow.GetRepository<ApplicationUser>();
            repoDepartment = uow.GetRepository<DepartmentTable>();
        }

        public IEnumerable<ItemCauseTable> GetAll()
        {
            ApplicationUser user = repoUser.GetById(HttpContext.Current.User.Identity.GetUserId());
            return repo.FindAll(x => x.CompanyTableId == user.CompanyTableId);
        }

        public IEnumerable<ItemCauseView> GetAllView()
        {
            return Mapper.Map<IEnumerable<ItemCauseTable>, IEnumerable<ItemCauseView>>(GetAll());
        }

        public IEnumerable<ItemCauseTable> GetPartial(Expression<Func<ItemCauseTable, bool>> predicate)
        {
            ApplicationUser user = repoUser.GetById(HttpContext.Current.User.Identity.GetUserId());
            return repo.FindAll(predicate).Where(x => x.CompanyTableId == user.CompanyTableId);
        }

        public IEnumerable<ItemCauseView> GetPartialView(Expression<Func<ItemCauseTable, bool>> predicate)
        {
            return Mapper.Map<IEnumerable<ItemCauseTable>, IEnumerable<ItemCauseView>>(GetPartial(predicate));
        }

        public IEnumerable<ItemCauseTable> GetPartialIntercompany(Expression<Func<ItemCauseTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public IEnumerable<ItemCauseView> GetPartialIntercompanyView(Expression<Func<ItemCauseTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<ItemCauseTable>, IEnumerable<ItemCauseView>>(GetPartialIntercompany(predicate));
            return items;
        }

        public ItemCauseTable FirstOrDefault(Expression<Func<ItemCauseTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }

        public ItemCauseView FirstOrDefaultView(Expression<Func<ItemCauseTable, bool>> prediacate)
        {
            return Mapper.Map<ItemCauseTable, ItemCauseView>(FirstOrDefault(prediacate));
        }

        public bool Contains(Expression<Func<ItemCauseTable, bool>> predicate)
        {
            return repo.Contains(predicate);
        }

        public void Save(ItemCauseView viewTable)
        {
            if (viewTable.Id == null)
            {
                var domainTable = new ItemCauseTable();
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

        public void SaveDomain(ItemCauseTable domainTable)
        {
            string userId = HttpContext.Current.User.Identity.GetUserId();
            ApplicationUser user = repoUser.GetById(userId);
            if (domainTable.Id == Guid.Empty)
            {
                domainTable.Id = Guid.NewGuid();
                domainTable.CreatedDate = DateTime.UtcNow;
                domainTable.ModifiedDate = domainTable.CreatedDate;
                domainTable.ApplicationUserCreatedId = userId;
                domainTable.ApplicationUserModifiedId = userId;
                domainTable.CompanyTableId = user.CompanyTableId;
                repo.Add(domainTable);
            }
            else
            {
                domainTable.ModifiedDate = DateTime.UtcNow;
                domainTable.ApplicationUserModifiedId = userId;
                repo.Update(domainTable);
            }
            uow.Commit();
        }

        public void Delete(Guid id)
        {
            repo.Delete(a => a.Id == id);
            uow.Commit();
        }

        public ItemCauseTable Find(Guid id)
        {
            ApplicationUser user = repoUser.GetById(HttpContext.Current.User.Identity.GetUserId());
            return repo.Find(a => a.Id == id && a.CompanyTableId == user.CompanyTableId);
        }

        public ItemCauseView FindView(Guid id)
        {
            return Mapper.Map<ItemCauseTable, ItemCauseView>(Find(id));
        }

        public List<ItemCauseView> GetCurrentUserItemsCause(List<ItemCauseView> list, DepartmentTable departmentTable, Guid companyId)
        {
            if (departmentTable == null) return list;
            if (list.Exists(item => item.DepartmentTableId == departmentTable.Id))
            {
                list.Where(item => item.DepartmentTableId == departmentTable.Id).ToList().ForEach(x => x.IsCurrentUserDepartment = true);
                return list;
            }
            else
            {
                return this.GetCurrentUserItemsCause(list, repoDepartment.Find(depr => depr.Id == departmentTable.ParentDepartmentId && depr.CompanyTableId == companyId), companyId);
            }
        }
    }
}
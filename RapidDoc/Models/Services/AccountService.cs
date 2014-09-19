using AutoMapper;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RapidDoc.Models.Repository;
using System.Linq.Expressions;

namespace RapidDoc.Models.Services
{
    public interface IAccountService
    {
        IEnumerable<ApplicationUser> GetAll();
        IEnumerable<UserViewModel> GetAllView();
        IEnumerable<ApplicationUser> GetPartial(Expression<Func<ApplicationUser, bool>> predicate);
        IEnumerable<UserViewModel> GetPartialView(Expression<Func<ApplicationUser, bool>> predicate);
        ApplicationUser FirstOrDefault(Expression<Func<ApplicationUser, bool>> predicate);
        UserViewModel FirstOrDefaultView(Expression<Func<ApplicationUser, bool>> predicate);
        ApplicationUser Find(string id);
        UserViewModel FindView(string id);
        void Save(UserViewModel viewTable);
        void SaveDomain(ApplicationUser domainTable);
        SelectList GetDropListUserNull(string id);
        SelectList GetDropListUser(string id);
        SelectList GetTimeZoneList(string id);
    }

    public class AccountService : IAccountService
    {
        private IRepository<ApplicationUser> repo;
        private IUnitOfWork _uow;

        public AccountService(IUnitOfWork uow)
        {
            _uow = uow;
            repo = uow.GetRepository<ApplicationUser>();
        }
        public IEnumerable<ApplicationUser> GetAll()
        {
            return repo.All();
        }
        public IEnumerable<UserViewModel> GetAllView()
        {
            var items = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserViewModel>>(GetAll());
            return items;
        }
        public IEnumerable<ApplicationUser> GetPartial(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public IEnumerable<UserViewModel> GetPartialView(Expression<Func<ApplicationUser, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<UserViewModel>>(GetPartial(predicate));
            return items;
        }
        public ApplicationUser FirstOrDefault(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return repo.Find(predicate);
        }
        public UserViewModel FirstOrDefaultView(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return Mapper.Map<ApplicationUser, UserViewModel>(FirstOrDefault(predicate));
        }
        public ApplicationUser Find(string id)
        {
            return repo.Find(a => a.Id == id);
        }
        public UserViewModel FindView(string id)
        {
            return Mapper.Map<ApplicationUser, UserViewModel>(Find(id));
        }
        public void Save(UserViewModel viewTable)
        {
            var domainTable = Find(viewTable.Id);
            Mapper.Map(viewTable, domainTable);
            SaveDomain(domainTable);
        }
        public void SaveDomain(ApplicationUser domainTable)
        {
            repo.Update(domainTable);
            _uow.Save();
        }
        public SelectList GetDropListUserNull(string id)
        {
            var items = GetAllView().ToList();
            items.Insert(0, new UserViewModel { UserName = UIElementRes.UIElement.NoValue, Id = "" });
            return new SelectList(items, "Id", "UserName", id);
        }
        public SelectList GetDropListUser(string id)
        {
            var items = GetAllView().ToList();
            return new SelectList(items, "Id", "UserName", id);
        }
        public SelectList GetTimeZoneList(string id)
        {
            var items = TimeZoneInfo.GetSystemTimeZones().ToList();
            return new SelectList(items, "Id", "DisplayName", id);
        }
    }
}
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

namespace RapidDoc.Models.Services
{
    public interface IProfileService
    {
        IEnumerable<ProfileTable> GetAll();
        IEnumerable<ProfileView> GetAllView();
        IEnumerable<ProfileTable> GetPartial(Expression<Func<ProfileTable, bool>> predicate);
        IEnumerable<ProfileView> GetPartialView(Expression<Func<ProfileTable, bool>> predicate);
        ProfileTable FirstOrDefault(Expression<Func<ProfileTable, bool>> predicate);
        ProfileView FirstOrDefaultView(Expression<Func<ProfileTable, bool>> predicate);
        void Save(ProfileView viewTable);
        void SaveDomain(ProfileTable domainTable);
        void Delete(Guid id);
        ProfileTable Find(Guid id);
        ProfileView FindView(Guid id);
        SelectList GetDropListProfileNull(Guid? id);
        SelectList GetDropListProfile(Guid? id);
    }

    public class ProfileService : IProfileService
    {
        private IRepository<ProfileTable> repo;
        private IUnitOfWork _uow;
        private readonly IAccountService _AccountService;

        public ProfileService(IUnitOfWork uow, IAccountService accountService)
        {
            _uow = uow;
            repo = uow.GetRepository<ProfileTable>();
            _AccountService = accountService;
        }
        public IEnumerable<ProfileTable> GetAll()
        {
            return repo.All();
        }
        public IEnumerable<ProfileView> GetAllView()
        {
            var items = Mapper.Map<IEnumerable<ProfileTable>, IEnumerable<ProfileView>>(GetAll());
            return items;
        }
        public IEnumerable<ProfileTable> GetPartial(Expression<Func<ProfileTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public IEnumerable<ProfileView> GetPartialView(Expression<Func<ProfileTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<ProfileTable>, IEnumerable<ProfileView>>(GetPartial(predicate));
            return items;
        }
        public ProfileTable FirstOrDefault(Expression<Func<ProfileTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }
        public ProfileView FirstOrDefaultView(Expression<Func<ProfileTable, bool>> predicate)
        {
            return Mapper.Map<ProfileTable, ProfileView>(FirstOrDefault(predicate));
        }
        public void Save(ProfileView viewTable)
        {
            if (viewTable.Id == null)
            {
                var domainTable = new ProfileTable();
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
        public void SaveDomain(ProfileTable domainTable)
        {
            string userId = HttpContext.Current.User.Identity.GetUserId();
            if (domainTable.Id == Guid.Empty)
            {
                domainTable.Id = Guid.NewGuid();
                domainTable.CreatedDate = DateTime.UtcNow;
                domainTable.ModifiedDate = domainTable.CreatedDate;
                domainTable.ApplicationUserCreatedId = userId;
                domainTable.ApplicationUserModifiedId = userId;
                repo.Add(domainTable);
            }
            else
            {
                domainTable.ModifiedDate = DateTime.UtcNow;
                domainTable.ApplicationUserModifiedId = userId;
                repo.Update(domainTable);
            }
            _uow.Save();
        }
        public void Delete(Guid id)
        {
            repo.Delete(a => a.Id == id);
            _uow.Save();
        }
        public ProfileTable Find(Guid id)
        {
            return repo.GetById(id);
        }
        public ProfileView FindView(Guid id)
        {
            return Mapper.Map<ProfileTable, ProfileView>(Find(id));
        }
        public SelectList GetDropListProfileNull(Guid? id)
        {
            var items = GetAllView().ToList();
            items.Insert(0, new ProfileView { ProfileName = UIElementRes.UIElement.NoValue, Id = null });
            return new SelectList(items, "Id", "ProfileName", id);
        }
        public SelectList GetDropListProfile(Guid? id)
        {
            var items = GetAllView().ToList();
            return new SelectList(items, "Id", "ProfileName", id);
        }
    }
}
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
    public interface IModificationUsersService
    {
        IEnumerable<ModificationUsersTable> GetAll();
        IEnumerable<ModificationUsersTable> GetPartial(Expression<Func<ModificationUsersTable, bool>> predicate);
        ModificationUsersTable FirstOrDefault(Expression<Func<ModificationUsersTable, bool>> predicate);
        bool Contains(Expression<Func<ModificationUsersTable, bool>> predicate);
        void SaveDomain(ModificationUsersTable domainTable);
        void Delete(Guid Id);
        ModificationUsersTable Find(Guid id);
        void DeleteAll(Guid documenId);
        bool ContainDocumentUser(Guid documentId, string user);
    }

    public class ModificationUsersService : IModificationUsersService
    {
        private IRepository<ModificationUsersTable> repo;
        private IRepository<ApplicationUser> repoUser;
        private IUnitOfWork _uow;

        public ModificationUsersService(IUnitOfWork uow)
        {
            _uow = uow;
            repo = uow.GetRepository<ModificationUsersTable>();
            repoUser = uow.GetRepository<ApplicationUser>();
        }

        public IEnumerable<ModificationUsersTable> GetAll()
        {
            return repo.All();
        }

        public IEnumerable<ModificationUsersTable> GetPartial(Expression<Func<ModificationUsersTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }

        public ModificationUsersTable FirstOrDefault(Expression<Func<ModificationUsersTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }

        public bool Contains(Expression<Func<ModificationUsersTable, bool>> predicate)
        {
            return repo.Contains(predicate);
        }

        public void SaveDomain(ModificationUsersTable domainTable)
        {
            ApplicationUser user = repoUser.GetById(HttpContext.Current.User.Identity.GetUserId());
            domainTable.CreatedDate = DateTime.UtcNow;
            domainTable.ModifiedDate = domainTable.CreatedDate;
            domainTable.ApplicationUserCreatedId = user.Id;
            domainTable.ApplicationUserModifiedId = user.Id;
            repo.Add(domainTable);
            _uow.Commit();
        }

        public void Delete(Guid Id)
        {
            repo.Delete(x => x.Id == Id);
            _uow.Commit();
        }

        public ModificationUsersTable Find(Guid id)
        {
            return repo.GetById(id);
        }

        public void DeleteAll(Guid documenId)
        {
            repo.Delete(x => x.DocumentTableId == documenId);
            _uow.Commit();
        }


        public bool ContainDocumentUser(Guid documentId, string user)
        {
            return repo.Contains(x => x.UserId == user && x.DocumentTableId == documentId);
        }
    }
}
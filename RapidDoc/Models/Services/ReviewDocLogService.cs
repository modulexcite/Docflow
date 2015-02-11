using AutoMapper;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Data;
using System.Web.Mvc;
using RapidDoc.Models.Repository;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity;

namespace RapidDoc.Models.Services
{
    public interface IReviewDocLogService
    {
        IEnumerable<ReviewDocLogTable> GetAll();
        IEnumerable<ReviewDocLogTable> GetPartial(Expression<Func<ReviewDocLogTable, bool>> predicate);
        ReviewDocLogTable FirstOrDefault(Expression<Func<ReviewDocLogTable, bool>> predicate);
        void SaveDomain(ReviewDocLogTable domainTable, string currentUserName = "", ApplicationUser user = null);
        ReviewDocLogTable Find(Guid id);
        bool isNotReviewDocCurrentUser(Guid documentId, string currentUserName = "", ApplicationUser user = null);
        bool isArchive(Guid documentId, string currentUserName = "", ApplicationUser user = null);
        void Delete(Guid documentId, string userId);
        void DeleteAll(Guid documenId);

    }

    public class ReviewDocLogService : IReviewDocLogService
    {
        private IRepository<ReviewDocLogTable> repo;
        private IRepository<ApplicationUser> repoUser;
        private IUnitOfWork _uow;

        public ReviewDocLogService(IUnitOfWork uow)
        {
            _uow = uow;
            repo = uow.GetRepository<ReviewDocLogTable>();
            repoUser = uow.GetRepository<ApplicationUser>();
        }
        public IEnumerable<ReviewDocLogTable> GetAll()
        {
            return repo.All();
        }
        public IEnumerable<ReviewDocLogTable> GetPartial(Expression<Func<ReviewDocLogTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public ReviewDocLogTable FirstOrDefault(Expression<Func<ReviewDocLogTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }
        public void SaveDomain(ReviewDocLogTable domainTable, string currentUserName = "", ApplicationUser user = null)
        {
            if(user == null)
                user = getCurrentUserName(currentUserName);
            if (repo.Contains(x => x.ApplicationUserCreatedId == user.Id && x.DocumentTableId == domainTable.DocumentTableId) == false)
            {
                domainTable.CreatedDate = DateTime.UtcNow;
                domainTable.ModifiedDate = domainTable.CreatedDate;
                domainTable.ApplicationUserCreatedId = user.Id;
                domainTable.ApplicationUserModifiedId = user.Id;
                repo.Add(domainTable);
                _uow.Commit();
            }
            else if (domainTable.Id != Guid.Empty)
            {
                domainTable.ModifiedDate = DateTime.UtcNow;
                domainTable.ApplicationUserModifiedId = user.Id;
                repo.Update(domainTable);
                _uow.Commit();
            }
        }
        public ReviewDocLogTable Find(Guid id)
        {
            return repo.GetById(id);
        }
        public bool isNotReviewDocCurrentUser(Guid documentId, string currentUserName = "", ApplicationUser user = null)
        {
            if (user == null)
            {
                user = getCurrentUserName(currentUserName);
            }

            return repo.Contains(x => x.ApplicationUserCreatedId == user.Id && x.DocumentTableId == documentId);
        }
        public bool isArchive(Guid documentId, string currentUserName = "", ApplicationUser user = null)
        {
            if (user == null)
            {
                user = getCurrentUserName(currentUserName);
            }

            return repo.Contains(x => x.ApplicationUserCreatedId == user.Id && x.DocumentTableId == documentId && x.isArchive == true);
        }
        public void Delete(Guid documentId, string userId)
        {
            repo.Delete(x => x.DocumentTableId == documentId && x.ApplicationUserCreatedId == userId);
            _uow.Commit();
        }
        public void DeleteAll(Guid documentId)
        {
            repo.Delete(x => x.DocumentTableId == documentId);
            _uow.Commit();
        }

        private ApplicationUser getCurrentUserName(string currentUserName = "")
        {
            if ((HttpContext.Current == null || HttpContext.Current.User.Identity.Name == String.Empty) && currentUserName != string.Empty)
            {
                return repoUser.Find(x => x.UserName == currentUserName);
            }
            else
            {
                return repoUser.GetById(HttpContext.Current.User.Identity.GetUserId());
            }
        }
    }
}
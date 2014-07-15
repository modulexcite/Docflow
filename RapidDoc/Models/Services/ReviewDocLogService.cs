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

namespace RapidDoc.Models.Services
{
    public interface IReviewDocLogService
    {
        IEnumerable<ReviewDocLogTable> GetAll();
        IEnumerable<ReviewDocLogTable> GetPartial(Expression<Func<ReviewDocLogTable, bool>> predicate);
        ReviewDocLogTable FirstOrDefault(Expression<Func<ReviewDocLogTable, bool>> predicate);
        void SaveDomain(ReviewDocLogTable domainTable, string currentUserName = "");
        ReviewDocLogTable Find(Guid? id);
        bool isNotReviewDocCurrentUser(Guid documentId, string currentUserName = "", ApplicationUser user = null);
        bool isArchive(Guid documentId, string currentUserName = "", ApplicationUser user = null);
        void Delete(Guid documentId, string userId);
    }

    public class ReviewDocLogService : IReviewDocLogService
    {
        private IRepository<ReviewDocLogTable> repo;
        private IUnitOfWork _uow;
        private readonly IAccountService _AccountService;

        public ReviewDocLogService(IUnitOfWork uow, IAccountService accountService)
        {
            _uow = uow;
            repo = uow.GetRepository<ReviewDocLogTable>();
            _AccountService = accountService;
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

        public void SaveDomain(ReviewDocLogTable domainTable, string currentUserName = "")
        {
            string localUserName = getCurrentUserName(currentUserName);
            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);

            if (repo.Contains(x => x.ApplicationUserCreatedId == user.Id && x.DocumentTableId == domainTable.DocumentTableId) == false)
            {
                domainTable.Id = Guid.NewGuid();
                domainTable.CreatedDate = DateTime.UtcNow;
                domainTable.ModifiedDate = domainTable.CreatedDate;
                domainTable.ApplicationUserCreatedId = user.Id;
                domainTable.ApplicationUserModifiedId = user.Id;
                repo.Add(domainTable);
                _uow.Save();
            }
            else if (domainTable.Id != Guid.Empty)
            {
                domainTable.ModifiedDate = DateTime.UtcNow;
                domainTable.ApplicationUserModifiedId = user.Id;
                repo.Update(domainTable);
                _uow.Save();
            }
        }

        public ReviewDocLogTable Find(Guid? id)
        {
            return repo.Find(a => a.Id == id);
        }

        public bool isNotReviewDocCurrentUser(Guid documentId, string currentUserName = "", ApplicationUser user = null)
        {
            if (user == null)
            {
                string localUserName = getCurrentUserName(currentUserName);
                user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
            }

            return repo.Contains(x => x.ApplicationUserCreatedId == user.Id && x.DocumentTableId == documentId);
        }

        public bool isArchive(Guid documentId, string currentUserName = "", ApplicationUser user = null)
        {
            if (user == null)
            {
                string localUserName = getCurrentUserName(currentUserName);
                user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
            }

            return repo.Contains(x => x.ApplicationUserCreatedId == user.Id && x.DocumentTableId == documentId && x.isArchive == true);
        }

        public void Delete(Guid documentId, string userId)
        {
            repo.Delete(x => x.DocumentTableId == documentId && x.ApplicationUserCreatedId == userId);
            _uow.Save();
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
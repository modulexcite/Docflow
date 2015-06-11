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
    public interface INotificationUsersService
    {
        IEnumerable<NotificationUsersTable> GetAll();
        IEnumerable<NotificationUsersTable> GetPartial(Expression<Func<NotificationUsersTable, bool>> predicate);
        NotificationUsersTable FirstOrDefault(Expression<Func<NotificationUsersTable, bool>> predicate);
        bool Contains(Expression<Func<NotificationUsersTable, bool>> predicate);
        void SaveDomain(NotificationUsersTable domainTable);
        void Delete(Guid Id);
        NotificationUsersTable Find(Guid id);
        void DeleteAll(Guid documenId);
        bool ContainDocumentUser(Guid documentId, string fromUser);
        void CreateNotifyForUser(Guid documentId, string toUser, string fromUser);
        void SetNotifyForUser(Guid documentId, string fromUser);
    }

    public class NotificationUsersService: INotificationUsersService
    {
        private IRepository<NotificationUsersTable> repo;
        private IRepository<ApplicationUser> repoUser;
        private IUnitOfWork _uow;

        public NotificationUsersService(IUnitOfWork uow)
        {
            _uow = uow;
            repo = uow.GetRepository<NotificationUsersTable>();
            repoUser = uow.GetRepository<ApplicationUser>();
        }

        public IEnumerable<NotificationUsersTable> GetAll()
        {
            return repo.All();
        }

        public IEnumerable<NotificationUsersTable> GetPartial(Expression<Func<NotificationUsersTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }

        public NotificationUsersTable FirstOrDefault(Expression<Func<NotificationUsersTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }

        public bool Contains(Expression<Func<NotificationUsersTable, bool>> predicate)
        {
            return repo.Contains(predicate);
        }

        public void SaveDomain(NotificationUsersTable domainTable)
        {
            string userId = HttpContext.Current.User.Identity.GetUserId();
            if (domainTable.Id == Guid.Empty)
            {
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
            _uow.Commit();
        }

        public void Delete(Guid Id)
        {
            repo.Delete(x => x.Id == Id);
            _uow.Commit();
        }

        public NotificationUsersTable Find(Guid id)
        {
            return repo.GetById(id);
        }

        public void DeleteAll(Guid documenId)
        {
            repo.Delete(x => x.DocumentTableId == documenId);
            _uow.Commit();
        }


        public bool ContainDocumentUser(Guid documentId, string fromUser)
        {
            return repo.Contains(x => x.FromUserId == fromUser && x.DocumentTableId == documentId && x.IsNotify == false);
        }


        public void CreateNotifyForUser(Guid documentId, string toUser, string fromUser)
        {
            NotificationUsersTable notificationUsersTable = new NotificationUsersTable();

            notificationUsersTable.DocumentTableId = documentId;
            notificationUsersTable.FromUserId = fromUser;
            notificationUsersTable.ToUserId = toUser;
            notificationUsersTable.IsNotify = false;

            this.SaveDomain(notificationUsersTable);
        }


        public void SetNotifyForUser(Guid documentId, string fromUser)
        {
            List<NotificationUsersTable> notificationUsersTables = this.GetPartial(x => x.DocumentTableId == documentId && x.FromUserId == fromUser && x.IsNotify == false).ToList();
            notificationUsersTables.ForEach(x => { x.IsNotify = true; this.SaveDomain(x); });
        }
    }
}
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
        bool ContainDocumentUser(Guid? documentId, string user);
        Guid? GetParentDocument(Guid? documentId);
        List<ModificationDocumentView> GetHierarchyModification(Guid? parentDocumentId);
        string GetModificationUserNamesFromDocument(Guid? documentId, string currentWokerUser);
    }

    public class ModificationUsersService : IModificationUsersService
    {        
        private IRepository<ModificationUsersTable> repo;
        private IRepository<ApplicationUser> repoUser;
        private IUnitOfWork _uow;
        private readonly IEmplService _EmplService;

        public ModificationUsersService(IUnitOfWork uow, IEmplService emplService)
        {
            _uow = uow;
            repo = uow.GetRepository<ModificationUsersTable>();
            repoUser = uow.GetRepository<ApplicationUser>();
            _EmplService = emplService;
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


        public bool ContainDocumentUser(Guid? documentId, string user)
        {
            return repo.Contains(x => x.UserId == user && x.DocumentTableId == documentId);
        }


        public Guid? GetParentDocument(Guid? documentId)
        {
            ModificationUsersTable modificationTable = this.FirstOrDefault(x => x.DocumentTableId == documentId);
            Guid? docId = modificationTable != null && modificationTable.OriginalDocumentId != null ? modificationTable.OriginalDocumentId : null;

            return docId != null && docId != Guid.Empty ?
                this.GetParentDocument(docId) : documentId;
        }

        public List<ModificationDocumentView> GetHierarchyModification(Guid? parentDocumentId)
        {
            List<ModificationDocumentView> listModificationHierarchy = new List<ModificationDocumentView>();
            string currentUserId = HttpContext.Current.User.Identity.GetUserId();
            foreach (var item in this.GetPartial(x => x.OriginalDocumentId == parentDocumentId))
            {
                DocumentTable docTable = _uow.GetRepository<DocumentTable>().GetById(item.DocumentTableId);

                listModificationHierarchy.Add(new ModificationDocumentView { DocumentId = item.DocumentTableId, DocumentNum = docTable.DocumentNum, ParentDocumentId = parentDocumentId, Name = _EmplService.FirstOrDefault(x => x.ApplicationUserId == item.UserId).FullName, CreateDateTime = docTable.CreatedDate, Enable = (currentUserId == docTable.ApplicationUserCreatedId || item.UserId == currentUserId) ? true : false, NamesTo = this.GetModificationUserNamesFromDocument(item.DocumentTableId, item.UserId) });
                listModificationHierarchy.AddRange(this.GetHierarchyModification(item.DocumentTableId));    
            }

            return listModificationHierarchy;
        }


        public string GetModificationUserNamesFromDocument(Guid? documentId, string currentWokerUser)
        {
            string names = "";

            this.GetPartial(x => x.DocumentTableId == documentId).ToList().ForEach(x => names += !this.GetAll().ToList().Any(z => z.OriginalDocumentId == documentId && z.UserId == x.UserId) && x.UserId != currentWokerUser ? _EmplService.FirstOrDefault(y => y.ApplicationUserId == x.UserId).FullName + ";" : "");

            return names;
        }
    }
}
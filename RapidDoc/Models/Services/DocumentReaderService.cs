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
using System.Transactions;
using Microsoft.AspNet.Identity;

namespace RapidDoc.Models.Services
{
    public interface IDocumentReaderService
    {
        IEnumerable<DocumentReaderTable> GetAll();
        IEnumerable<DocumentReaderTable> GetPartial(Expression<Func<DocumentReaderTable, bool>> predicate);
        DocumentReaderTable FirstOrDefault(Expression<Func<DocumentReaderTable, bool>> predicate);
        bool Contains(Expression<Func<DocumentReaderTable, bool>> predicate);
        List<string> SaveReader(Guid documentId, string[] listdata);
        void SaveDomain(DocumentReaderTable domainTable);
        void Delete(Guid documentId);
        void Delete(Expression<Func<DocumentReaderTable, bool>> predicate);
        DocumentReaderTable Find(Guid id);
    }

    public class DocumentReaderService : IDocumentReaderService
    {
        private IRepository<DocumentReaderTable> repo;
        private IUnitOfWork _uow;
        private readonly IAccountService _AccountService;
        private readonly IEmplService _EmplService;
        private readonly IHistoryUserService _HistoryUserService;

        public DocumentReaderService(IUnitOfWork uow, IAccountService accountService, IEmplService emplService, IHistoryUserService historyUserService)
        {
            _uow = uow;
            repo = uow.GetRepository<DocumentReaderTable>();
            _AccountService = accountService;
            _EmplService = emplService;
            _HistoryUserService = historyUserService;
        }
        public IEnumerable<DocumentReaderTable> GetAll()
        {
            return repo.All();
        }
        public IEnumerable<DocumentReaderTable> GetPartial(Expression<Func<DocumentReaderTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public DocumentReaderTable FirstOrDefault(Expression<Func<DocumentReaderTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }
        public bool Contains(Expression<Func<DocumentReaderTable, bool>> predicate)
        {
            return repo.Contains(predicate);
        }
        public List<string> SaveReader(Guid documentId, string[] listdata)
        {
            List<string> newReader = new List<string>();
            string addReadersDescription = String.Empty;
            string removeReadersDescription = String.Empty;
            ApplicationUser user = _AccountService.Find(HttpContext.Current.User.Identity.GetUserId());

            if (listdata != null)
            {
                foreach (string userId in listdata)
                {
                    if (Contains(x => x.DocumentTableId == documentId && x.UserId == userId) == false)
                    {
                        newReader.Add(userId);
                        var empl = _EmplService.GetEmployer(userId, user.CompanyTableId);
                        addReadersDescription += empl.FullName + "; ";

                        DocumentReaderTable reader = new DocumentReaderTable();
                        reader.DocumentTableId = documentId;
                        reader.UserId = userId;
                        SaveDomain(reader);
                    }
                }
            }

            var currentReaders = GetPartial(x => x.DocumentTableId == documentId).ToList();
            if(listdata == null)
                Delete(documentId);

            foreach (var item in currentReaders)
            {
                if(listdata != null)
                {
                    if (listdata.Contains(item.UserId) == false)
                    {
                        var empl = _EmplService.GetEmployer(item.UserId, user.CompanyTableId);
                        removeReadersDescription += empl.FullName + "; ";
                        Delete(x => x.DocumentTableId == documentId && x.UserId == item.UserId);
                    }
                }
                else
                {
                    var empl = _EmplService.GetEmployer(item.UserId, user.CompanyTableId);
                    removeReadersDescription += empl.FullName + "; ";
                }
            }

            if (addReadersDescription.Length > 0)
            {
                _HistoryUserService.SaveDomain(new HistoryUserTable { DocumentTableId = documentId, HistoryType = HistoryType.AddReader, Description = addReadersDescription }, user.Id);
            }
            if (removeReadersDescription.Length > 0)
            {
                _HistoryUserService.SaveDomain(new HistoryUserTable { DocumentTableId = documentId, HistoryType = HistoryType.RemoveReader, Description = removeReadersDescription }, user.Id);
            }

            return newReader;
        }
        public void SaveDomain(DocumentReaderTable domainTable)
        {
            string userId = HttpContext.Current.User.Identity.GetUserId();
            domainTable.CreatedDate = DateTime.UtcNow;
            domainTable.ModifiedDate = domainTable.CreatedDate;
            domainTable.ApplicationUserCreatedId = userId;
            domainTable.ApplicationUserModifiedId = userId;
            repo.Add(domainTable);
            _uow.Save();
        }
        public void Delete(Guid documentId)
        {
            repo.Delete(x => x.DocumentTableId == documentId);
            _uow.Save();
        }
        public void Delete(Expression<Func<DocumentReaderTable, bool>> predicate)
        {
            repo.Delete(predicate);
            _uow.Save();
        }
        public DocumentReaderTable Find(Guid id)
        {
            return repo.GetById(id);
        }
    }
}
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

namespace RapidDoc.Models.Services
{
    public interface IDocumentReaderService
    {
        IEnumerable<DocumentReaderTable> GetAll();
        IEnumerable<DocumentReaderTable> GetPartial(Expression<Func<DocumentReaderTable, bool>> predicate);
        DocumentReaderTable FirstOrDefault(Expression<Func<DocumentReaderTable, bool>> predicate);
        bool Contains(Expression<Func<DocumentReaderTable, bool>> predicate);
        List<string> SaveReader(Guid documentId, string[] listdata, string currentUserName = "");
        void SaveDomain(DocumentReaderTable domainTable, string currentUserName = "");
        void Delete(Guid documentId);
        DocumentReaderTable Find(Guid? id);
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

        public List<string> SaveReader(Guid documentId, string[] listdata, string currentUserName = "")
        {
            List<string> newReader = new List<string>();
            string historyDescription = String.Empty;

            if (listdata != null)
            {
                foreach (string emplId in listdata)
                {
                    if (Contains(x => x.DocumentTableId == documentId && x.UserId == emplId) == false)
                    {
                        newReader.Add(emplId);
                        var empl = _EmplService.FirstOrDefault(x => x.ApplicationUserId == emplId);
                        historyDescription += empl.FullName;
                    }
                }
            }

            _HistoryUserService.SaveDomain(new HistoryUserTable { DocumentTableId = documentId, HistoryType = HistoryType.AddReader, Description = historyDescription });

            Delete(documentId);

            if (listdata != null)
            {
                foreach (string emplId in listdata)
                {
                    DocumentReaderTable reader = new DocumentReaderTable();
                    reader.DocumentTableId = documentId;
                    reader.UserId = emplId;
                    SaveDomain(reader, currentUserName);
                }
            }

            return newReader;
        }

        public void SaveDomain(DocumentReaderTable domainTable, string currentUserName = "")
        {
            string localUserName = getCurrentUserName(currentUserName);
            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);

            domainTable.Id = Guid.NewGuid();
            domainTable.CreatedDate = DateTime.UtcNow;
            domainTable.ModifiedDate = domainTable.CreatedDate;
            domainTable.ApplicationUserCreatedId = user.Id;
            domainTable.ApplicationUserModifiedId = user.Id;
            repo.Add(domainTable);

            _uow.Save();
        }

        public void Delete(Guid documentId)
        {
            repo.Delete(x => x.DocumentTableId == documentId);
            _uow.Save();
        }

        public DocumentReaderTable Find(Guid? id)
        {
            return repo.Find(a => a.Id == id);
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
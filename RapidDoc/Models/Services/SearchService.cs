using AutoMapper;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RapidDoc.Models.Repository;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace RapidDoc.Models.Services
{
    public interface ISearchService
    {
        IEnumerable<SearchTable> GetPartial(Expression<Func<SearchTable, bool>> predicate);
        IEnumerable<SearchView> GetPartialView(Expression<Func<SearchTable, bool>> predicate);
        SearchTable FirstOrDefault(Expression<Func<SearchTable, bool>> predicate);
        SearchView FirstOrDefaultView(Expression<Func<SearchTable, bool>> predicate);
        void SaveDomain(SearchTable domainTable, string currentUserName = "");
        List<SearchView> GetDocuments(int blockNumber, int blockSize, string searchText);
    }

    public class SearchService : ISearchService
    {
        private IRepository<SearchTable> repo;
        private IUnitOfWork _uow;
        private readonly IAccountService _AccountService;
        private readonly IDocumentService _DocumentService;
        private readonly IEmplService _EmplService;
        private readonly ISystemService _SystemService;

        public SearchService(IUnitOfWork uow, IAccountService accountService, IDocumentService documentService, IEmplService emplService, ISystemService systemService)
        {
            _uow = uow;
            repo = uow.GetRepository<SearchTable>();
            _AccountService = accountService;
            _DocumentService = documentService;
            _EmplService = emplService;
            _SystemService = systemService;
        }

        public IEnumerable<SearchTable> GetPartial(Expression<Func<SearchTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }

        public IEnumerable<SearchView> GetPartialView(Expression<Func<SearchTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<SearchTable>, IEnumerable<SearchView>>(GetPartial(predicate));
            ApplicationUser currentUser = _AccountService.FirstOrDefault(x => x.UserName == HttpContext.Current.User.Identity.Name);

            foreach (var item in items)
            {
                DocumentTable docuTable = _DocumentService.Find(item.DocumentTableId);
                item.isShow = _DocumentService.isShowDocument(docuTable.Id, docuTable.ProcessTableId, "", true);

                ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == item.CreatedBy);
                EmplView empl = _EmplService.FirstOrDefaultView(x => x.ApplicationUserId == user.Id);
                if (empl != null)
                    item.CreatedUserName = "(" + empl.AliasCompanyName + ") " + empl.FullName + " " + empl.TitleName + " " + empl.DepartmentName + " "+ _SystemService.ConvertDateTimeToLocal(currentUser, item.CreatedDate);
                else
                    item.CreatedUserName = String.Empty;
            }

            return items;
        }

        public SearchTable FirstOrDefault(Expression<Func<SearchTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }

        public SearchView FirstOrDefaultView(Expression<Func<SearchTable, bool>> predicate)
        {
            var item = Mapper.Map<SearchTable, SearchView>(FirstOrDefault(predicate));
            DocumentTable docuTable = _DocumentService.Find(item.DocumentTableId);
            item.isShow = _DocumentService.isShowDocument(docuTable.Id, docuTable.ProcessTableId, "", true);

            return item;
        }

        public void SaveDomain(SearchTable domainTable, string currentUserName = "")
        {
            string localUserName = getCurrentUserName(currentUserName);
            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);

            if (domainTable.Id == Guid.Empty)
            {
                domainTable.Id = Guid.NewGuid();
                domainTable.CreatedDate = DateTime.UtcNow;
                domainTable.ModifiedDate = domainTable.CreatedDate;
                domainTable.ApplicationUserCreatedId = user.Id;
                domainTable.ApplicationUserModifiedId = user.Id;
                repo.Add(domainTable);
            }
            else
            {
                domainTable.ModifiedDate = DateTime.UtcNow;
                domainTable.ApplicationUserModifiedId = user.Id;
                repo.Update(domainTable);
            }
            _uow.Save();
        }

        public List<SearchView> GetDocuments(int blockNumber, int blockSize, string searchText)
        {
            int startIndex = (blockNumber - 1) * blockSize;
            string searchString = searchText.Trim();

            var resultText = this.GetPartialView(x => x.DocumentText.Contains(searchString)).Skip(startIndex).Take(blockSize).ToList();
            var resultNum = this.GetPartialView(x => x.DocumentTable.DocumentNum.Contains(searchString)).Skip(startIndex).Take(blockSize).ToList();
            var result = resultNum.Concat(resultText).ToList();
            return result;
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
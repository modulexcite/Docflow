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
using Microsoft.AspNet.Identity;

namespace RapidDoc.Models.Services
{
    public interface ISearchService
    {
        IEnumerable<SearchTable> GetPartial(Expression<Func<SearchTable, bool>> predicate);
        IEnumerable<SearchView> GetPartialView(Expression<Func<SearchTable, bool>> predicate);
        SearchTable FirstOrDefault(Expression<Func<SearchTable, bool>> predicate);
        SearchView FirstOrDefaultView(Expression<Func<SearchTable, bool>> predicate);
        void SaveDomain(SearchTable domainTable);
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
            ApplicationUser currentUser = _AccountService.Find(HttpContext.Current.User.Identity.GetUserId());

            foreach (var item in items)
            {
                DocumentTable docuTable = _DocumentService.Find(item.DocumentTableId);
                item.isShow = _DocumentService.isShowDocument(docuTable, docuTable.ProcessTableId, currentUser, true);

                ApplicationUser user = _AccountService.Find(item.ApplicationUserCreatedId);
                EmplView empl = _EmplService.FirstOrDefaultView(x => x.ApplicationUserId == user.Id && x.CompanyTableId == user.CompanyTableId);
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
            ApplicationUser currentUser = _AccountService.Find(HttpContext.Current.User.Identity.GetUserId());
            item.isShow = _DocumentService.isShowDocument(docuTable, docuTable.ProcessTableId, currentUser, true);

            return item;
        }
        public void SaveDomain(SearchTable domainTable)
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
            _uow.Save();
        }
        public List<SearchView> GetDocuments(int blockNumber, int blockSize, string searchText)
        {
            List<SearchView> result = new List<SearchView>();
            if (!String.IsNullOrEmpty(searchText))
            {
                int startIndex = (blockNumber - 1) * blockSize;
                string searchString = searchText.Trim();

                if (!String.IsNullOrEmpty(searchString))
                {
                    var resultText = this.GetPartialView(x => x.DocumentText.Contains(searchString)).OrderByDescending(x => x.CreatedDate).Skip(startIndex).Take(blockSize).ToList();
                    var resultNum = this.GetPartialView(x => x.DocumentTable.DocumentNum.Contains(searchString)).Skip(startIndex).Take(blockSize).ToList();
                    result = resultNum.Concat(resultText).ToList();
                }
            }
            return result;
        }
    }
}
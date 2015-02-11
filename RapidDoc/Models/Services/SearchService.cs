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
using System.Reflection;
using System.Text.RegularExpressions;

namespace RapidDoc.Models.Services
{
    public interface ISearchService
    {
        IEnumerable<SearchTable> GetPartial(Expression<Func<SearchTable, bool>> predicate);
        IEnumerable<SearchView> GetPartialView(Expression<Func<SearchTable, bool>> predicate);
        SearchTable FirstOrDefault(Expression<Func<SearchTable, bool>> predicate);
        SearchView FirstOrDefaultView(Expression<Func<SearchTable, bool>> predicate);
        bool Contains(Expression<Func<SearchTable, bool>> predicate);
        void SaveDomain(SearchTable domainTable);
        List<SearchView> GetDocuments(int blockNumber, int blockSize, string searchText);
        void SaveSearchData(Guid id, string searchString);
        void SaveSearchData(Guid id, dynamic docModel, string actionModelName);
        string PrepareSearchString(dynamic docModel, string actionModelName);
        void Delete(Guid Id);
    }

    public class SearchService : ISearchService
    {
        private IRepository<SearchTable> repo;
        private IRepository<ApplicationUser> repoUser;
        private IRepository<EmplTable> repoEmpl;
        private IUnitOfWork _uow;
        private readonly IDocumentService _DocumentService;
        private readonly ISystemService _SystemService;

        public SearchService(IUnitOfWork uow, IDocumentService documentService,  ISystemService systemService)
        {
            _uow = uow;
            repo = uow.GetRepository<SearchTable>();
            repoUser = uow.GetRepository<ApplicationUser>();
            repoEmpl = uow.GetRepository<EmplTable>();
            _DocumentService = documentService;
            _SystemService = systemService;
        }
        public IEnumerable<SearchTable> GetPartial(Expression<Func<SearchTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public IEnumerable<SearchView> GetPartialView(Expression<Func<SearchTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<SearchTable>, IEnumerable<SearchView>>(GetPartial(predicate));
            ApplicationUser currentUser = repoUser.GetById(HttpContext.Current.User.Identity.GetUserId());

            foreach (var item in items)
            {
                DocumentTable docuTable = _DocumentService.Find(item.DocumentTableId);
                item.isShow = _DocumentService.isShowDocument(docuTable, currentUser, true);

                ApplicationUser user = repoUser.GetById(item.ApplicationUserCreatedId);
                EmplTable empl = repoEmpl.Find(x => x.ApplicationUserId == user.Id && x.CompanyTableId == user.CompanyTableId);
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
            ApplicationUser currentUser = repoUser.GetById(HttpContext.Current.User.Identity.GetUserId());
            item.isShow = _DocumentService.isShowDocument(docuTable, currentUser, true);

            return item;
        }
        public bool Contains(Expression<Func<SearchTable, bool>> predicate)
        {
            return repo.Contains(predicate);
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
            _uow.Commit();
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

        public string PrepareSearchString(dynamic docModel, string actionModelName)
        {
            Type type = Type.GetType("RapidDoc.Models.ViewModels." + actionModelName + "_View");
            var properties = type.GetProperties().Where(x => x.PropertyType == typeof(string));
            string allStringData = String.Empty;
            string regex = @"(<.+?>|&nbsp;)";
            string regexGuid = @"([a-z0-9]{8}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{12})";

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    var value = property.GetValue(docModel, null);

                    if (!String.IsNullOrEmpty(value) && !String.IsNullOrWhiteSpace(value))
                    {
                        string stringWithoutTags = Regex.Replace(value, regex, "").Trim();

                        if (!String.IsNullOrEmpty(stringWithoutTags))
                        {
                            List<string> guidList = new List<string>();
                            guidList = Regex.Matches(stringWithoutTags, regexGuid)
                                .Cast<Match>()
                                .Select(m => m.Groups[0].Value)
                                .ToList();

                            foreach (string guid in guidList)
                            {
                                stringWithoutTags = stringWithoutTags.Replace(guid + ",", "");
                                stringWithoutTags = stringWithoutTags.Replace(guid, "");
                            }

                            allStringData = allStringData + stringWithoutTags + "|";
                        }
                    }
                }
            }

            return allStringData;
        }

        public void SaveSearchData(Guid id, string searchString)
        {
            if (String.IsNullOrEmpty(searchString))
                return;

            DocumentTable document = _DocumentService.Find(id);

            if (document == null)
                return;

            document.DocumentText = searchString;
            _DocumentService.SaveDocumentText(document);

            if (!Contains(x => x.DocumentTableId == document.Id))
                SaveDomain(new SearchTable { DocumentText = searchString, DocumentTableId = document.Id });
            else
            {
                SearchTable searchTable = FirstOrDefault(x => x.DocumentTableId == document.Id);
                searchTable.DocumentText = searchString;
                SaveDomain(searchTable);
            }
        }

        public void SaveSearchData(Guid id, dynamic docModel, string actionModelName)
        {
            string searchString = PrepareSearchString(docModel, actionModelName);
            if (!String.IsNullOrEmpty(searchString))
                SaveSearchData(id, searchString);
        }
        public void Delete(Guid Id)
        {
            repo.Delete(x => x.Id == Id);
            _uow.Commit();
        }
    }
}
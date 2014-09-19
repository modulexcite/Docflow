using AutoMapper;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RapidDoc.Models.Repository;
using System.Web.Mvc;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity;

namespace RapidDoc.Models.Services
{
    public interface ICompanyService
    {
        IEnumerable<CompanyTable> GetAll();
        IEnumerable<CompanyView> GetAllView();
        IEnumerable<CompanyTable> GetPartial(Expression<Func<CompanyTable, bool>> predicate);
        IEnumerable<CompanyView> GetPartialView(Expression<Func<CompanyTable, bool>> predicate);
        CompanyTable FirstOrDefault(Expression<Func<CompanyTable, bool>> predicate);
        CompanyView FirstOrDefaultView(Expression<Func<CompanyTable, bool>> predicate);
        void Save(CompanyView viewTable);
        void SaveDomain(CompanyTable domainTable);
        void Delete(Guid id);
        CompanyTable Find(Guid id);
        CompanyView FindView(Guid id);
        SelectList GetDropListCompanyNull(Guid? id);
        SelectList GetDropListCompany(Guid? id);
    }

    public class CompanyService : ICompanyService
    {
        private IRepository<CompanyTable> repo;
        private IUnitOfWork _uow;
        private readonly IAccountService _AccountService;

        public CompanyService(IUnitOfWork uow, IAccountService accountService)
        {
            _uow = uow;
            repo = uow.GetRepository<CompanyTable>();
            _AccountService = accountService;
        }
        public IEnumerable<CompanyTable> GetAll()
        {
            return repo.All();
        }
        public IEnumerable<CompanyView> GetAllView()
        {
            var items = Mapper.Map<IEnumerable<CompanyTable>, IEnumerable<CompanyView>>(GetAll());
            return items;
        }
        public IEnumerable<CompanyTable> GetPartial(Expression<Func<CompanyTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public IEnumerable<CompanyView> GetPartialView(Expression<Func<CompanyTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<CompanyTable>, IEnumerable<CompanyView>>(GetPartial(predicate));
            return items;
        }
        public CompanyTable FirstOrDefault(Expression<Func<CompanyTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }
        public CompanyView FirstOrDefaultView(Expression<Func<CompanyTable, bool>> predicate)
        {
            return Mapper.Map<CompanyTable, CompanyView>(FirstOrDefault(predicate));
        }
        public void Save(CompanyView viewTable)
        {
            if (viewTable.Id == null)
            {
                var domainTable = new CompanyTable();
                Mapper.Map(viewTable, domainTable);
                SaveDomain(domainTable);
            }
            else
            {
                var domainTable = Find(viewTable.Id ?? Guid.Empty);
                Mapper.Map(viewTable, domainTable);
                SaveDomain(domainTable);
            }
        }
        public void SaveDomain(CompanyTable domainTable)
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
        public void Delete(Guid id)
        {
            repo.Delete(a => a.Id == id);
            _uow.Save();
        }
        public CompanyTable Find(Guid id)
        {
            return repo.GetById(id);
        }
        public CompanyView FindView(Guid id)
        {
            return Mapper.Map<CompanyTable, CompanyView>(Find(id));
        }
        public SelectList GetDropListCompanyNull(Guid? id)
        {
            var items = GetAllView().ToList();
            items.Insert(0, new CompanyView { CompanyName = UIElementRes.UIElement.NoValue, Id = null });
            return new SelectList(items, "Id", "CompanyName", id);
        }
        public SelectList GetDropListCompany(Guid? id)
        {
            var items = GetAllView().ToList();
            return new SelectList(items, "Id", "CompanyName", id);
        }
    }
}
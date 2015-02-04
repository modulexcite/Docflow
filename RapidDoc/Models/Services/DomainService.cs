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
    public interface IDomainService
    {
        IEnumerable<DomainTable> GetAll();
        IEnumerable<DomainView> GetAllView();
        IEnumerable<DomainTable> GetPartial(Expression<Func<DomainTable, bool>> predicate);
        IEnumerable<DomainView> GetPartialView(Expression<Func<DomainTable, bool>> predicate);
        DomainTable FirstOrDefault(Expression<Func<DomainTable, bool>> predicate);
        DomainView FirstOrDefaultView(Expression<Func<DomainTable, bool>> predicate);
        DomainView GetNewModel();
        void Save(DomainView viewTable);
        void SaveDomain(DomainTable domainTable);
        void Delete(Guid id);
        DomainTable Find(Guid id);
        DomainView FindView(Guid id);
        SelectList GetDropListDomainNull(Guid? id);
        SelectList GetDropListDomain(Guid? id);
    }

    public class DomainService : IDomainService
    {
        private IRepository<DomainTable> repo;
        private IUnitOfWork _uow;

        public DomainService(IUnitOfWork uow)
        {
            _uow = uow;
            repo = uow.GetRepository<DomainTable>();
        }
        public IEnumerable<DomainTable> GetAll()
        {
            return repo.All();
        }
        public IEnumerable<DomainView> GetAllView()
        {
            var items = Mapper.Map<IEnumerable<DomainTable>, IEnumerable<DomainView>>(GetAll());
            return items;
        }
        public IEnumerable<DomainTable> GetPartial(Expression<Func<DomainTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public IEnumerable<DomainView> GetPartialView(Expression<Func<DomainTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<DomainTable>, IEnumerable<DomainView>>(GetPartial(predicate));
            return items;
        }
        public DomainTable FirstOrDefault(Expression<Func<DomainTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }
        public DomainView FirstOrDefaultView(Expression<Func<DomainTable, bool>> predicate)
        {
            return Mapper.Map<DomainTable, DomainView>(FirstOrDefault(predicate));
        }
        public DomainView GetNewModel()
        {
            var model = new DomainView();
            model.LDAPPort = Convert.ToInt32(ConfigurationManager.AppSettings.Get("LDAPDefaultPort"));
            return model;
        }
        public void Save(DomainView viewTable)
        {
            if (viewTable.Id == null)
            {
                var domainTable = new DomainTable();
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
        public void SaveDomain(DomainTable domainTable)
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
        public DomainTable Find(Guid id)
        {
            return repo.GetById(id);
        }
        public DomainView FindView(Guid id)
        {
            return Mapper.Map<DomainTable, DomainView>(Find(id));
        }
        public SelectList GetDropListDomainNull(Guid? id)
        {
            var items = GetAllView().ToList();
            items.Insert(0, new DomainView { DomainName = UIElementRes.UIElement.NoValue, Id = null });
            return new SelectList(items, "Id", "DomainName", id);
        }
        public SelectList GetDropListDomain(Guid? id)
        {
            var items = GetAllView().ToList();
            return new SelectList(items, "Id", "DomainName", id);
        }
    }
}
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
using Microsoft.AspNet.Identity.EntityFramework;

namespace RapidDoc.Models.Services
{
    public interface ITripSettingsService
    {
        IEnumerable<TripSettingsTable> GetAll();
        IEnumerable<TripSettingsView> GetAllView();
        IEnumerable<TripSettingsTable> GetPartial(Expression<Func<TripSettingsTable, bool>> predicate);
        IEnumerable<TripSettingsView> GetPartialView(Expression<Func<TripSettingsTable, bool>> predicate);
        TripSettingsTable FirstOrDefault(Expression<Func<TripSettingsTable, bool>> predicate);
        TripSettingsView FirstOrDefaultView(Expression<Func<TripSettingsTable, bool>> predicate);
        void Save(TripSettingsView viewTable);
        void SaveDomain(TripSettingsTable domainTable);
        void Delete(Guid id);
        TripSettingsTable Find(Guid id);
        TripSettingsView FindView(Guid id);
    }

    public class TripSettingsService : ITripSettingsService
    {
        private IRepository<TripSettingsTable> repo;
        private IUnitOfWork _uow;

        public TripSettingsService(IUnitOfWork uow)
        {
            _uow = uow;
            repo = uow.GetRepository<TripSettingsTable>();
        }
        public IEnumerable<TripSettingsTable> GetAll()
        {
            return repo.All();
        }
        public IEnumerable<TripSettingsView> GetAllView()
        {
            var items = Mapper.Map<IEnumerable<TripSettingsTable>, IEnumerable<TripSettingsView>>(GetAll());
            return items;
        }
        public IEnumerable<TripSettingsTable> GetPartial(Expression<Func<TripSettingsTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public IEnumerable<TripSettingsView> GetPartialView(Expression<Func<TripSettingsTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<TripSettingsTable>, IEnumerable<TripSettingsView>>(GetPartial(predicate));
            return items;
        }
        public TripSettingsTable FirstOrDefault(Expression<Func<TripSettingsTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }
        public TripSettingsView FirstOrDefaultView(Expression<Func<TripSettingsTable, bool>> predicate)
        {
            return Mapper.Map<TripSettingsTable, TripSettingsView>(FirstOrDefault(predicate));
        }
        public void Save(TripSettingsView viewTable)
        {
            if (viewTable.Id == null)
            {
                var domainTable = new TripSettingsTable();
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
        public void SaveDomain(TripSettingsTable domainTable)
        {
            string userId = HttpContext.Current.User.Identity.GetUserId();
            if (domainTable.Id == Guid.Empty)
            {
                domainTable.Id = Guid.NewGuid();
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
        public TripSettingsTable Find(Guid id)
        {
            return repo.GetById(id);
        }
        public TripSettingsView FindView(Guid id)
        {
            return Mapper.Map<TripSettingsTable, TripSettingsView>(Find(id));
        }
      
        public void Delete(Guid id)
        {
            repo.Delete(a => a.Id == id);
            _uow.Commit();
        }
    }
}
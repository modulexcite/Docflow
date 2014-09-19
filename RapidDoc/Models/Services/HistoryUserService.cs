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
    public interface IHistoryUserService
    {
        IEnumerable<HistoryUserTable> GetAll();
        IEnumerable<HistoryUserView> GetAllView();
        IEnumerable<HistoryUserTable> GetPartial(Expression<Func<HistoryUserTable, bool>> predicate);
        IEnumerable<HistoryUserView> GetPartialView(Expression<Func<HistoryUserTable, bool>> predicate);
        HistoryUserTable FirstOrDefault(Expression<Func<HistoryUserTable, bool>> predicate);
        HistoryUserView FirstOrDefaultView(Expression<Func<HistoryUserTable, bool>> predicate);
        void SaveDomain(HistoryUserTable domainTable);
        HistoryUserTable Find(Guid id);
        HistoryUserView FindView(Guid id);
    }

    public class HistoryUserService : IHistoryUserService
    {
        private IRepository<HistoryUserTable> repo;
        private IUnitOfWork _uow;
        private readonly IAccountService _AccountService;

        public HistoryUserService(IUnitOfWork uow, IAccountService accountService)
        {
            _uow = uow;
            repo = uow.GetRepository<HistoryUserTable>();
            _AccountService = accountService;
        }
        public IEnumerable<HistoryUserTable> GetAll()
        {
            return repo.All();
        }
        public IEnumerable<HistoryUserView> GetAllView()
        {
            var items = Mapper.Map<IEnumerable<HistoryUserTable>, IEnumerable<HistoryUserView>>(GetAll());
            return items;
        }
        public IEnumerable<HistoryUserTable> GetPartial(Expression<Func<HistoryUserTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public IEnumerable<HistoryUserView> GetPartialView(Expression<Func<HistoryUserTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<HistoryUserTable>, IEnumerable<HistoryUserView>>(GetPartial(predicate));
            return items;
        }        
        public HistoryUserTable FirstOrDefault(Expression<Func<HistoryUserTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }
        public HistoryUserView FirstOrDefaultView(Expression<Func<HistoryUserTable, bool>> predicate)
        {
            return Mapper.Map<HistoryUserTable, HistoryUserView>(FirstOrDefault(predicate));
        }
        public void SaveDomain(HistoryUserTable domainTable)
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
        public HistoryUserTable Find(Guid id)
        {
            return repo.GetById(id);
        }
        public HistoryUserView FindView(Guid id)
        {
            return Mapper.Map<HistoryUserTable, HistoryUserView>(Find(id));
        }
    }
}
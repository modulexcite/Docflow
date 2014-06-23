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
        void SaveDomain(HistoryUserTable domainTable, string currentUserName = "");
        HistoryUserTable Find(Guid? id);
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

        public void SaveDomain(HistoryUserTable domainTable, string currentUserName = "")
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

        public void Delete(Guid id)
        {
            repo.Delete(a => a.Id == id);
            _uow.Save();
        }

        public HistoryUserTable Find(Guid? id)
        {
            return repo.Find(a => a.Id == id);
        }

        public HistoryUserView FindView(Guid id)
        {
            return Mapper.Map<HistoryUserTable, HistoryUserView>(Find(id));
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
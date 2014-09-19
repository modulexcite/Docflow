using AutoMapper;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.Infrastructure;
using RapidDoc.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Data;
using System.Web.Mvc;
using RapidDoc.Models.Repository;
using System.Linq.Expressions;
using Microsoft.AspNet.Identity;

namespace RapidDoc.Models.Services
{
    public interface ITitleService
    {
        IEnumerable<TitleTable> GetAll();
        IEnumerable<TitleView> GetAllView();
        IEnumerable<TitleTable> GetPartial(Expression<Func<TitleTable, bool>> predicate);
        IEnumerable<TitleView> GetPartialView(Expression<Func<TitleTable, bool>> predicate);
        IEnumerable<TitleTable> GetPartialIntercompany(Expression<Func<TitleTable, bool>> predicate);
        IEnumerable<TitleView> GetPartialIntercompanyView(Expression<Func<TitleTable, bool>> predicate);
        TitleTable FirstOrDefault(Expression<Func<TitleTable, bool>> predicate);
        TitleView FirstOrDefaultView(Expression<Func<TitleTable, bool>> predicate);
        bool Contains(Expression<Func<TitleTable, bool>> predicate);
        void Save(TitleView viewTable);
        void SaveDomain(TitleTable domainTable, string currentUserName = "");
        void Delete(Guid id);
        TitleTable Find(Guid id);
        TitleView FindView(Guid id);
        SelectList GetDropListTitleNull(Guid? id);
        SelectList GetDropListTitle(Guid? id);
    }

    public class TitleService : ITitleService
    {
        private IRepository<TitleTable> repo;
        private IUnitOfWork _uow;
        private readonly IAccountService _AccountService;

        public TitleService(IUnitOfWork uow, IAccountService accountService)
        {
            _uow = uow;
            repo = uow.GetRepository<TitleTable>();
            _AccountService = accountService;
        }
        public IEnumerable<TitleTable> GetAll()
        {
            return repo.All();
        }
        public IEnumerable<TitleView> GetAllView()
        {
            var items = Mapper.Map<IEnumerable<TitleTable>, IEnumerable<TitleView>>(GetAll());
            return items;
        }
        public IEnumerable<TitleTable> GetPartial(Expression<Func<TitleTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public IEnumerable<TitleView> GetPartialView(Expression<Func<TitleTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<TitleTable>, IEnumerable<TitleView>>(GetPartial(predicate));
            return items;
        }
        public IEnumerable<TitleTable> GetPartialIntercompany(Expression<Func<TitleTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public IEnumerable<TitleView> GetPartialIntercompanyView(Expression<Func<TitleTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<TitleTable>, IEnumerable<TitleView>>(GetPartialIntercompany(predicate));
            return items;
        }
        public TitleTable FirstOrDefault(Expression<Func<TitleTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }
        public TitleView FirstOrDefaultView(Expression<Func<TitleTable, bool>> predicate)
        {
            return Mapper.Map<TitleTable, TitleView>(FirstOrDefault(predicate));
        }
        public bool Contains(Expression<Func<TitleTable, bool>> predicate)
        {
            return repo.Contains(predicate);
        }
        public void Save(TitleView viewTable)
        {
            if (viewTable.Id == null)
            {
                var domainTable = new TitleTable();
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
        public void SaveDomain(TitleTable domainTable, string currentUserName = "")
        {
            ApplicationUser user = getCurrentUserName(currentUserName);
            if (domainTable.Id == Guid.Empty)
            {
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
        public TitleTable Find(Guid id)
        {
            return repo.GetById(id);
        }
        public TitleView FindView(Guid id)
        {
            return Mapper.Map<TitleTable, TitleView>(Find(id));
        }
        public SelectList GetDropListTitleNull(Guid? id)
        {
            var items = GetAllView().ToList();
            items.Insert(0, new TitleView { TitleName = UIElementRes.UIElement.NoValue, Id = null });
            return new SelectList(items, "Id", "TitleName", id);
        }
        public SelectList GetDropListTitle(Guid? id)
        {
            var items = GetAllView().ToList();
            return new SelectList(items, "Id", "TitleName", id);
        }
        private ApplicationUser getCurrentUserName(string currentUserName = "")
        {
            if ((HttpContext.Current == null || HttpContext.Current.User.Identity.Name == String.Empty) && currentUserName != string.Empty)
            {
                return _AccountService.FirstOrDefault(x => x.UserName == currentUserName);
            }
            else
            {
                return _AccountService.Find(HttpContext.Current.User.Identity.GetUserId());
            }
        }
    }
}
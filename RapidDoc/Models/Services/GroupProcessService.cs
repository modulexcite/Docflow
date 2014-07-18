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
    public interface IGroupProcessService
    {
        IEnumerable<GroupProcessTable> GetAll();
        IEnumerable<GroupProcessView> GetAllView();
        IEnumerable<GroupProcessTable> GetPartial(Expression<Func<GroupProcessTable, bool>> predicate);
        IEnumerable<GroupProcessView> GetPartialView(Expression<Func<GroupProcessTable, bool>> predicate);
        GroupProcessTable FirstOrDefault(Expression<Func<GroupProcessTable, bool>> predicate);
        GroupProcessView FirstOrDefaultView(Expression<Func<GroupProcessTable, bool>> predicate);
        void Save(GroupProcessView viewTable);
        void SaveDomain(GroupProcessTable domainTable, string currentUserName = "");
        void Delete(Guid id);
        GroupProcessTable Find(Guid? id);
        GroupProcessView FindView(Guid id);
        SelectList GetDropListGroupProcessNull(Guid? id);
        SelectList GetDropListGroupProcess(Guid? id);
    }

    public class GroupProcessService : IGroupProcessService
    {
        private IRepository<GroupProcessTable> repo;
        private IUnitOfWork _uow;
        private readonly IAccountService _AccountService;

        public GroupProcessService(IUnitOfWork uow, IAccountService accountService)
        {
            _uow = uow;
            repo = uow.GetRepository<GroupProcessTable>();
            _AccountService = accountService;
        }

        public IEnumerable<GroupProcessTable> GetAll()
        {
            return repo.All();
        }

        public IEnumerable<GroupProcessView> GetAllView()
        {
            var items = Mapper.Map<IEnumerable<GroupProcessTable>, IEnumerable<GroupProcessView>>(GetAll());
            return items;
        }

        public IEnumerable<GroupProcessTable> GetPartial(Expression<Func<GroupProcessTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }

        public IEnumerable<GroupProcessView> GetPartialView(Expression<Func<GroupProcessTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<GroupProcessTable>, IEnumerable<GroupProcessView>>(GetPartial(predicate));
            return items;
        }

        public GroupProcessTable FirstOrDefault(Expression<Func<GroupProcessTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }

        public GroupProcessView FirstOrDefaultView(Expression<Func<GroupProcessTable, bool>> predicate)
        {
            return Mapper.Map<GroupProcessTable, GroupProcessView>(FirstOrDefault(predicate));
        }

        public void Save(GroupProcessView viewTable)
        {
            if (viewTable.Id == null)
            {
                var domainTable = new GroupProcessTable();
                Mapper.Map(viewTable, domainTable);
                SaveDomain(domainTable);
            }
            else
            {
                var domainTable = Find(viewTable.Id);
                Mapper.Map(viewTable, domainTable);
                SaveDomain(domainTable);
            }
        }

        public void SaveDomain(GroupProcessTable domainTable, string currentUserName = "")
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

        public GroupProcessTable Find(Guid? id)
        {
            return repo.Find(a => a.Id == id);
        }

        public GroupProcessView FindView(Guid id)
        {
            return Mapper.Map<GroupProcessTable, GroupProcessView>(Find(id));
        }

        public SelectList GetDropListGroupProcessNull(Guid? id)
        {
            var items = GetAllView().ToList();
            items.Insert(0, new GroupProcessView { GroupProcessName = UIElementRes.UIElement.NoValue, Id = null });
            return new SelectList(items, "Id", "GroupProcessName", id);
        }

        public SelectList GetDropListGroupProcess(Guid? id)
        {
            var items = GetAllView().ToList();
            return new SelectList(items, "Id", "GroupProcessName", id);
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
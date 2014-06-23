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
using Microsoft.AspNet.Identity.EntityFramework;

namespace RapidDoc.Models.Services
{
    public interface IEmplService
    {
        IEnumerable<EmplTable> GetAll(string currentUserName = "");
        IEnumerable<EmplView> GetAllView();
        IEnumerable<EmplTable> GetPartial(Expression<Func<EmplTable, bool>> predicate, string currentUserName = "");
        IEnumerable<EmplView> GetPartialView(Expression<Func<EmplTable, bool>> predicate);
        IEnumerable<EmplTable> GetPartialIntercompany(Expression<Func<EmplTable, bool>> predicate);
        IEnumerable<EmplView> GetPartialIntercompanyView(Expression<Func<EmplTable, bool>> predicate);
        EmplTable FirstOrDefault(Expression<Func<EmplTable, bool>> predicate);
        EmplView FirstOrDefaultView(Expression<Func<EmplTable, bool>> predicate);
        bool Contains(Expression<Func<EmplTable, bool>> predicate);
        EmplView GetNewModel();
        void Save(EmplView viewTable);
        void SaveDomain(EmplTable domainTable, string currentUserName = "");
        void Delete(Guid id);
        EmplTable Find(Guid? id, string currentUserName = "");
        EmplView FindView(Guid id);
        SelectList GetDropListEmplNull(Guid? id);
        object GetJsonEmpl();
    }
    //4753254
    public class EmplService : IEmplService
    {
        private IRepository<EmplTable> repo;
        private IUnitOfWork _uow;
        private readonly IAccountService _AccountService;

        public EmplService(IUnitOfWork uow, IAccountService accountService)
        {
            _uow = uow;
            repo = uow.GetRepository<EmplTable>();
            _AccountService = accountService;
        }

        public IEnumerable<EmplTable> GetAll(string currentUserName = "")
        {
            string localUserName = getCurrentUserName(currentUserName);
            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
            return repo.FindAll(x => x.CompanyTableId == user.CompanyTableId);
        }

        public IEnumerable<EmplView> GetAllView()
        {
            var items = Mapper.Map<IEnumerable<EmplTable>, IEnumerable<EmplView>>(GetAll());
            return items;
        }

        public IEnumerable<EmplTable> GetPartial(Expression<Func<EmplTable, bool>> predicate, string currentUserName = "")
        {
            string localUserName = getCurrentUserName(currentUserName);
            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
            return repo.FindAll(predicate).Where(x => x.CompanyTableId == user.CompanyTableId);
        }

        public IEnumerable<EmplView> GetPartialView(Expression<Func<EmplTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<EmplTable>, IEnumerable<EmplView>>(GetPartial(predicate));
            return items;
        }

        public IEnumerable<EmplTable> GetPartialIntercompany(Expression<Func<EmplTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }

        public IEnumerable<EmplView> GetPartialIntercompanyView(Expression<Func<EmplTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<EmplTable>, IEnumerable<EmplView>>(GetPartialIntercompany(predicate));
            return items;
        }

        public EmplTable FirstOrDefault(Expression<Func<EmplTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }

        public EmplView FirstOrDefaultView(Expression<Func<EmplTable, bool>> predicate)
        {
            return Mapper.Map<EmplTable, EmplView>(FirstOrDefault(predicate));
        }

        public bool Contains(Expression<Func<EmplTable, bool>> predicate)
        {
            return repo.Contains(predicate);
        }

        public EmplView GetNewModel()
        {
            var model = new EmplView();
            return model;
        }

        public void Save(EmplView viewTable)
        {
            if (viewTable.Id == null)
            {
                var domainTable = new EmplTable();
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

        public void SaveDomain(EmplTable domainTable, string currentUserName = "")
        {
            string localUserName = getCurrentUserName(currentUserName);
            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);

            if (domainTable.Id == Guid.Empty)
            {
                domainTable.Id = Guid.NewGuid();
                domainTable.CreatedDate = DateTime.UtcNow;
                domainTable.ModifiedDate = domainTable.CreatedDate;
                domainTable.CompanyTableId = user.CompanyTableId;
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

        public EmplTable Find(Guid? id, string currentUserName = "")
        {
            string localUserName = getCurrentUserName(currentUserName);
            ApplicationUser user = _AccountService.FirstOrDefault(x => x.UserName == localUserName);
            return repo.Find(a => a.Id == id && a.CompanyTableId == user.CompanyTableId);
        }

        public EmplView FindView(Guid id)
        {
            return Mapper.Map<EmplTable, EmplView>(Find(id));
        }

        public SelectList GetDropListEmplNull(Guid? id)
        {
            var items = GetAllView().ToList();
            items.Insert(0, new EmplView { FirstName = UIElementRes.UIElement.NoValue, Id = null });
            return new SelectList(items, "Id", "FullName", id);
        }

        public object GetJsonEmpl()
        {
            var jsondata = from c in GetAllView()
                           select new
                           {
                               value = string.Format("{0},({1}) {2} - {3}", c.Id, c.AliasCompanyName, c.FullName, c.TitleName),
                               text = string.Format("({0}) {1} - {2}", c.AliasCompanyName, c.FullName, c.TitleName)
                           };

            return jsondata;
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
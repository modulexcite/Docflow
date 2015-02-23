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
        IEnumerable<EmplTable> GetAll();
        IEnumerable<EmplView> GetAllView();
        IEnumerable<EmplTable> GetAllIntercompany();
        IEnumerable<EmplView> GetAllIntercompanyView();
        IEnumerable<EmplTable> GetPartial(Expression<Func<EmplTable, bool>> predicate);
        IEnumerable<EmplView> GetPartialView(Expression<Func<EmplTable, bool>> predicate);
        IEnumerable<EmplTable> GetPartialIntercompany(Expression<Func<EmplTable, bool>> predicate);
        IEnumerable<EmplView> GetPartialIntercompanyView(Expression<Func<EmplTable, bool>> predicate);
        EmplTable FirstOrDefault(Expression<Func<EmplTable, bool>> predicate);
        EmplView FirstOrDefaultView(Expression<Func<EmplTable, bool>> predicate);
        bool Contains(Expression<Func<EmplTable, bool>> predicate);
        void Save(EmplView viewTable);
        void SaveDomain(EmplTable domainTable, string currentUserName = "", Guid? companyId = null);
        void Delete(Guid id);
        EmplTable Find(Guid id, string currentUserId = "");
        EmplTable FindIntercompany(Guid id);
        EmplView FindView(Guid id);
        SelectList GetDropListEmplNull(Guid? id);
        object GetJsonEmpl();
        object GetJsonEmplIntercompany();
        EmplTable GetEmployer(string userId, Guid? companyId);
    }
    public class EmplService : IEmplService
    {
        private IRepository<EmplTable> repo;
        private IRepository<ApplicationUser> repoUser;
        private IUnitOfWork _uow;

        public EmplService(IUnitOfWork uow)
        {
            _uow = uow;
            repo = uow.GetRepository<EmplTable>();
            repoUser = uow.GetRepository<ApplicationUser>();
        }
        public IEnumerable<EmplTable> GetAll()
        {
            ApplicationUser user = repoUser.GetById(HttpContext.Current.User.Identity.GetUserId());
            return repo.FindAll(x => x.CompanyTableId == user.CompanyTableId);
        }
        public IEnumerable<EmplView> GetAllView()
        {
            var items = Mapper.Map<IEnumerable<EmplTable>, IEnumerable<EmplView>>(GetAll());
            return items;
        }
        public IEnumerable<EmplTable> GetAllIntercompany()
        {
            return repo.All();
        }
        public IEnumerable<EmplView> GetAllIntercompanyView()
        {
            var items = Mapper.Map<IEnumerable<EmplTable>, IEnumerable<EmplView>>(GetAllIntercompany());
            return items;
        }
        public IEnumerable<EmplTable> GetPartial(Expression<Func<EmplTable, bool>> predicate)
        {
            ApplicationUser user = repoUser.GetById(HttpContext.Current.User.Identity.GetUserId());
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
                var domainTable = Find(viewTable.Id ?? Guid.Empty);
                Mapper.Map(viewTable, domainTable);
                SaveDomain(domainTable);
            }
        }
        public void SaveDomain(EmplTable domainTable, string currentUserName = "", Guid? companyId = null)
        {
            ApplicationUser user = getCurrentUserName(currentUserName);

            if (domainTable.Id == Guid.Empty)
            {
                domainTable.CreatedDate = DateTime.UtcNow;
                domainTable.ModifiedDate = domainTable.CreatedDate;
                if (companyId == null)
                    domainTable.CompanyTableId = user.CompanyTableId;
                else
                    domainTable.CompanyTableId = companyId;
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
            _uow.Commit();
        }
        public void Delete(Guid id)
        {
            repo.Delete(a => a.Id == id);
            _uow.Commit();
        }
        public EmplTable Find(Guid id, string currentUserId = "")
        {
            ApplicationUser user = getCurrentUserId(currentUserId);
            return repo.Find(a => a.Id == id && a.CompanyTableId == user.CompanyTableId);
        }
        public EmplTable FindIntercompany(Guid id)
        {
            return repo.Find(a => a.Id == id);
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
            var jsondata = from c in GetPartial(x => x.Enable == true)
                           select new
                           {
                               value = string.Format("{0},({1}) {2} - {3}", c.Id, c.AliasCompanyName, c.FullName, c.TitleName),
                               text = string.Format("({0}) {1} - {2}", c.AliasCompanyName, c.FullName, c.TitleName)
                           }; 

            return jsondata;
        }
        public object GetJsonEmplIntercompany()
        {
            var jsondata = from c in GetPartialIntercompany(x => x.Enable == true)
                           select new
                           {
                               value = string.Format("{0},({1}) {2} - {3}", c.Id, c.AliasCompanyName, c.FullName, c.TitleName),
                               text = string.Format("({0}) {1} - {2}", c.AliasCompanyName, c.FullName, c.TitleName)
                           };

            return jsondata;
        }
        public EmplTable GetEmployer(string userId, Guid? companyId)
        {
            var empls = GetPartialIntercompany(x => x.ApplicationUserId == userId && x.CompanyTableId == companyId);

            if(empls != null)
            {
                EmplTable emplTable = empls.OrderByDescending(x => x.Enable).FirstOrDefault();

                if (emplTable == null)
                {
                    empls = GetPartialIntercompany(x => x.ApplicationUserId == userId);
                    if(empls != null)
                    {
                        return empls.OrderByDescending(x => x.Enable).FirstOrDefault();
                    }
                }
                return emplTable;
            }

            return null;
        }
        private ApplicationUser getCurrentUserName(string currentUserName = "")
        {
            if ((HttpContext.Current == null || HttpContext.Current.User.Identity.Name == String.Empty) && currentUserName != string.Empty)
            {
                return repoUser.Find(x => x.UserName == currentUserName);
            }
            else
            {
                return repoUser.GetById(HttpContext.Current.User.Identity.GetUserId());
            }
        }
        private ApplicationUser getCurrentUserId(string currentUserId = "")
        {
            if (currentUserId != string.Empty)
            {
                return repoUser.GetById(currentUserId);
            }
            else
            {
                return repoUser.GetById(HttpContext.Current.User.Identity.GetUserId());
            }
        }
    }
}
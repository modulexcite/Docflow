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
    public interface IServiceIncidentService
    {
        IEnumerable<ServiceIncidentTable> GetAll();
        IEnumerable<ServiceIncidentView> GetAllView();
        IEnumerable<ServiceIncidentTable> GetPartial(Expression<Func<ServiceIncidentTable, bool>> predicate);
        IEnumerable<ServiceIncidentView> GetPartialView(Expression<Func<ServiceIncidentTable, bool>> predicate);
        ServiceIncidentTable FirstOrDefault(Expression<Func<ServiceIncidentTable, bool>> predicate);
        ServiceIncidentView FirstOrDefaultView(Expression<Func<ServiceIncidentTable, bool>> predicate);
        void Save(ServiceIncidentView viewTable);
        void SaveDomain(ServiceIncidentTable domainTable);
        void Delete(Guid id);
        ServiceIncidentTable Find(Guid id);
        ServiceIncidentView FindView(Guid id);
        SelectList GetDropListServiceIncident(string id);
        SelectList GetDropListRole(string id);
        SelectList GetDropListRoleNull(Guid? id);
    }

    public class ServiceIncidentService : IServiceIncidentService
    {
        private IRepository<ServiceIncidentTable> repo;
        private IUnitOfWork _uow;
        private readonly IAccountService _AccountService;

        public ServiceIncidentService(IUnitOfWork uow, IAccountService accountService)
        {
            _uow = uow;
            repo = uow.GetRepository<ServiceIncidentTable>();
            _AccountService = accountService;
        }
        public IEnumerable<ServiceIncidentTable> GetAll()
        {
            return repo.All();
        }
        public IEnumerable<ServiceIncidentView> GetAllView()
        {
            var items = Mapper.Map<IEnumerable<ServiceIncidentTable>, IEnumerable<ServiceIncidentView>>(GetAll());
            return items;
        }
        public IEnumerable<ServiceIncidentTable> GetPartial(Expression<Func<ServiceIncidentTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public IEnumerable<ServiceIncidentView> GetPartialView(Expression<Func<ServiceIncidentTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<ServiceIncidentTable>, IEnumerable<ServiceIncidentView>>(GetPartial(predicate));
            return items;
        }
        public ServiceIncidentTable FirstOrDefault(Expression<Func<ServiceIncidentTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }
        public ServiceIncidentView FirstOrDefaultView(Expression<Func<ServiceIncidentTable, bool>> predicate)
        {
            return Mapper.Map<ServiceIncidentTable, ServiceIncidentView>(FirstOrDefault(predicate));
        }
        public void Save(ServiceIncidentView viewTable)
        {
            if (viewTable.Id == null)
            {
                var domainTable = new ServiceIncidentTable();
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
        public void SaveDomain(ServiceIncidentTable domainTable)
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
            _uow.Save();
        }
        public ServiceIncidentTable Find(Guid id)
        {
            return repo.GetById(id);
        }
        public ServiceIncidentView FindView(Guid id)
        {
            return Mapper.Map<ServiceIncidentTable, ServiceIncidentView>(Find(id));
        }
        public SelectList GetDropListRole(string id)
        {
            RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_uow.GetDbContext<ApplicationDbContext>()));
            var roles = RoleManager.Roles.ToList();
            return new SelectList(roles, "Id", "Name", id);
        }
        public SelectList GetDropListRoleNull(Guid? id)
        {
            RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_uow.GetDbContext<ApplicationDbContext>()));
            var roles = RoleManager.Roles.ToList();
            roles.Insert(0, new IdentityRole { Name = UIElementRes.UIElement.NoValue, Id = null });
            return new SelectList(roles, "Id", "Name", id);
        }
        public SelectList GetDropListServiceIncident(string id)
        {
            var items = GetAllView().GroupBy(x => new { x.ServiceName, x.Description }).Select(x => new ServiceIncidentList { Id = x.Key.ServiceName, Description = x.Key.Description }).ToList();
            return new SelectList(items, "Id", "Description", id);
        }
        public void Delete(Guid id)
        {
            repo.Delete(a => a.Id == id);
            _uow.Save();
        }
    }

    public class ServiceIncidentList
    {
        public string Id { get; set; }
        public string Description { get; set; }
    }
}
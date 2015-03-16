using System;
using System.Web;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNet.Identity;
using RapidDoc.Models.DomainModels;
using RapidDoc.Models.ViewModels;
using RapidDoc.Models.Repository;
using RapidDoc.Models.Infrastructure;
using System.Web.Mvc;


namespace RapidDoc.Models.Services
{
    public interface IItemCauseService
    {
        IEnumerable<ItemCauseTable> GetAll();
        IEnumerable<ItemCauseView> GetAllView();
        IEnumerable<ItemCauseTable> GetPartial(Expression<Func<ItemCauseTable, bool>> predicate);
        IEnumerable<ItemCauseView> GetPartialView(Expression<Func<ItemCauseTable, bool>> predicate);
        ItemCauseTable FirstOrDefault(Expression<Func<ItemCauseTable, bool>> predicate);
        ItemCauseView FIrstOrDefaultView(Expression<Func<ItemCauseTable, bool>> prediacate);
        void Save(ItemCauseView viewTable);
        void SaveDomain(ItemCauseTable domainTable);
        void Delete(Guid id);
        ItemCauseTable Find(Guid id);
        ItemCauseView FindView(Guid id);
        List<ItemCauseView> GetCurrentUserItemsCause(List<ItemCauseView> list, DepartmentTable departmentTable);
    }

    public class ItemCauseService : IItemCauseService
    {
        private IRepository<ItemCauseTable> repo;
        private IUnitOfWork uow;
        private readonly IDepartmentService _DepartmentService;

        public ItemCauseService(IUnitOfWork _uow, IDepartmentService departmentService)
        {
            uow = _uow;
            repo = uow.GetRepository<ItemCauseTable>();
            _DepartmentService = departmentService;
        }

        public IEnumerable<ItemCauseTable> GetAll()
        {
            return repo.All();
        }

        public IEnumerable<ItemCauseView> GetAllView()
        {
            return Mapper.Map<IEnumerable<ItemCauseTable>, IEnumerable<ItemCauseView>>(GetAll());
        }

        public IEnumerable<ItemCauseTable> GetPartial(Expression<Func<ItemCauseTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }

        public IEnumerable<ItemCauseView> GetPartialView(Expression<Func<ItemCauseTable, bool>> predicate)
        {
            return Mapper.Map<IEnumerable<ItemCauseTable>, IEnumerable<ItemCauseView>>(GetPartial(predicate));
        }

        public ItemCauseTable FirstOrDefault(Expression<Func<ItemCauseTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }

        public ItemCauseView FIrstOrDefaultView(Expression<Func<ItemCauseTable, bool>> prediacate)
        {
            return Mapper.Map<ItemCauseTable, ItemCauseView>(FirstOrDefault(prediacate));
        }

        public void Save(ItemCauseView viewTable)
        {
            if (viewTable.Id == null)
            {
                var domainTable = new ItemCauseTable();
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

        public void SaveDomain(ItemCauseTable domainTable)
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
            uow.Commit();
        }

        public void Delete(Guid id)
        {
            repo.Delete(a => a.Id == id);

            uow.Commit();
        }

        public ItemCauseTable Find(Guid id)
        {
            return repo.Find(a => a.Id == id);
        }

        public ItemCauseView FindView(Guid id)
        {
            return Mapper.Map<ItemCauseTable, ItemCauseView>(Find(id));
        }


        public List<ItemCauseView> GetCurrentUserItemsCause(List<ItemCauseView> list, DepartmentTable departmentTable)
        {
            if (departmentTable == null) return list;
            if (list.Exists(item => item.DepartmentTableId == departmentTable.Id))
            {
                list.Where(item => item.DepartmentTableId == departmentTable.Id).ToList().ForEach(x => x.IsCurrentUserDepartment = true);

                return list;
            }
            else
            {
                return this.GetCurrentUserItemsCause(list, _DepartmentService.FirstOrDefault(depr => depr.Id == departmentTable.ParentDepartmentId));
            }
        }
    }
}
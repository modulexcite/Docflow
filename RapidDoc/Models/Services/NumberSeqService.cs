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
using System.Data.Entity.Core;
using System.Transactions;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity;

namespace RapidDoc.Models.Services
{
    public interface INumberSeqService
    {
        IEnumerable<NumberSeriesTable> GetAll();
        IEnumerable<NumberSeriesView> GetAllView();
        IEnumerable<NumberSeriesTable> GetPartial(Expression<Func<NumberSeriesTable, bool>> predicate);
        IEnumerable<NumberSeriesView> GetPartialView(Expression<Func<NumberSeriesTable, bool>> predicate);
        NumberSeriesTable FirstOrDefault(Expression<Func<NumberSeriesTable, bool>> predicate);
        NumberSeriesView FirstOrDefaultView(Expression<Func<NumberSeriesTable, bool>> predicate);
        void Save(NumberSeriesView viewTable);
        void SaveDomain(NumberSeriesTable domainTable);
        void Delete(Guid id);
        NumberSeriesTable Find(Guid id);
        NumberSeriesView FindView(Guid id);
        SelectList GetDropListNumberSeqNull(Guid? id);
        SelectList GetDropListNumberSeq(Guid? id);
        string GetDocumentNum(Guid id);
    }

    public class NumberSeqService : INumberSeqService
    {
        private IRepository<NumberSeriesTable> repo;
        private IUnitOfWork _uow;
        private readonly IAccountService _AccountService;

        public NumberSeqService(IUnitOfWork uow, IAccountService accountService)
        {
            _uow = uow;
            repo = uow.GetRepository<NumberSeriesTable>();
            _AccountService = accountService;
        }
        public IEnumerable<NumberSeriesTable> GetAll()
        {
            return repo.All();
        }
        public IEnumerable<NumberSeriesView> GetAllView()
        {
            var items = Mapper.Map<IEnumerable<NumberSeriesTable>, IEnumerable<NumberSeriesView>>(GetAll());
            return items;
        }
        public IEnumerable<NumberSeriesTable> GetPartial(Expression<Func<NumberSeriesTable, bool>> predicate)
        {
            return repo.FindAll(predicate);
        }
        public IEnumerable<NumberSeriesView> GetPartialView(Expression<Func<NumberSeriesTable, bool>> predicate)
        {
            var items = Mapper.Map<IEnumerable<NumberSeriesTable>, IEnumerable<NumberSeriesView>>(GetPartial(predicate));
            return items;
        }
        public NumberSeriesTable FirstOrDefault(Expression<Func<NumberSeriesTable, bool>> predicate)
        {
            return repo.Find(predicate);
        }
        public NumberSeriesView FirstOrDefaultView(Expression<Func<NumberSeriesTable, bool>> predicate)
        {
            return Mapper.Map<NumberSeriesTable, NumberSeriesView>(FirstOrDefault(predicate));
        }
        public void Save(NumberSeriesView viewTable)
        {
            if (viewTable.Id == null)
            {
                var domainTable = new NumberSeriesTable();
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
        public void SaveDomain(NumberSeriesTable domainTable)
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
        public NumberSeriesTable Find(Guid id)
        {
            return repo.GetById(id);
        }
        public NumberSeriesView FindView(Guid id)
        {
            return Mapper.Map<NumberSeriesTable, NumberSeriesView>(Find(id));
        }
        public SelectList GetDropListNumberSeqNull(Guid? id)
        {
            var items = GetAllView().ToList();
            items.Insert(0, new NumberSeriesView { NumberSeriesName = UIElementRes.UIElement.NoValue, Id = null });
            return new SelectList(items, "Id", "NumberSeriesName", id);
        }
        public SelectList GetDropListNumberSeq(Guid? id)
        {
            var items = GetAllView().ToList();
            return new SelectList(items, "Id", "NumberSeriesName", id);
        }
        public string GetDocumentNum(Guid id)
        {
            var numberSeq = Find(id);
            do
            {
                try
                {
                    numberSeq = Find(id);
                    numberSeq.LastNum++;
                    SaveDomain(numberSeq);
                    string num = numberSeq.Prefix + numberSeq.LastNum.ToString("D" + numberSeq.Size.ToString());
                    return num;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var databaseValues = (NumberSeriesTable)entry.GetDatabaseValues().ToObject();
                    var clientValues = (NumberSeriesTable)entry.Entity;
                    numberSeq.TimeStamp = databaseValues.TimeStamp;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } while (true);
        }
    }
}
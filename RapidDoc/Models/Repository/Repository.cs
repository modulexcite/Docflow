using RapidDoc.Models.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Linq.Expressions;
using AutoMapper;
using RapidDoc.Models.DomainModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RapidDoc.Models.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbContext _context;
        private readonly IDbSet<T> _dbset;

        public Repository(IDbContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Deleted;
            _dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            var entry = FindAll(predicate);
            Delete(entry);
        }

        public virtual void Delete(IEnumerable<T> entity)
        {
            foreach (var ent in entity)
            {
                var entry = _context.Entry(ent);
                entry.State = EntityState.Deleted;
                _dbset.Remove(ent);
            }
        }

        public virtual void Update(T entity)
        {
            var entry = _context.Entry(entity);
            _dbset.Attach(entity);
            entry.State = EntityState.Modified;
        }

        public virtual T GetById(Guid id)
        {
            return _dbset.Find(id);
        }

        public virtual IEnumerable<T> All()
        {
            return _dbset;
        }

        public virtual T Find(Expression<Func<T, bool>> predicate)
        {
            return _dbset.FirstOrDefault(predicate);
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Where(predicate);
        }

        public virtual bool Any()
        {
            return _dbset.Any();
        }

        public bool Contains(Expression<Func<T, bool>> predicate)
        {
            return _dbset.Count(predicate) > 0;
        }

        public virtual int Count
        {
            get
            {
                return _dbset.Count();
            }
        }
    }
}
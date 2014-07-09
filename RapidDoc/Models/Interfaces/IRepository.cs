using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RapidDoc.Models.Repository
{
    public interface IRepository<T> where T : class
    {
        //Search Operations
        T GetById(Guid Id);
        IEnumerable<T> All();
        T Find(Expression<Func<T, bool>> predicate);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate);
        bool Any();
        bool Any(Expression<Func<T, bool>> predicate);
        bool Contains(Expression<Func<T, bool>> predicate);
        int Count { get; }
        IQueryable<T> AllQuery();

        //--CRUD Operations
        void Add(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> predicate);
        void Delete(IEnumerable<T> entity);
        void Update(T entity);
    }
}

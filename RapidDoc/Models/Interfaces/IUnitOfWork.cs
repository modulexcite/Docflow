using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RapidDoc.Models.Repository;

namespace RapidDoc.Models.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        TContext GetDbContext<TContext>() where TContext : DbContext, IDbContext;
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        void Save();
    }
}

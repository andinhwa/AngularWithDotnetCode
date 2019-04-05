using WebApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Core.DBContexts.Repositories.Impl
{
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        protected readonly WebAppContext context;

        protected readonly DbSet<TEntity> dbSet;

        public GenericRepository(WebAppContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public virtual Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.AllAsync(predicate, cancellationToken);

        public virtual Task<bool> AnyAsync(CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.AnyAsync();

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.AnyAsync(predicate, cancellationToken);

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.CountAsync(predicate, cancellationToken);

        public virtual Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.MaxAsync(selector, cancellationToken);

        public virtual Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.MinAsync(selector, cancellationToken);

        public virtual TEntity Find(params object[] keyValues)
            => dbSet.Find(keyValues);

        public virtual Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.FindAsync(keyValues, cancellationToken);

        public virtual Task<TEntity> FindAsync(params object[] keyValues)
            => dbSet.FindAsync(keyValues);

        public virtual Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
            => dbSet.Where(predicate).ToListAsync();

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
            => dbSet.FirstOrDefault(predicate);

        public virtual TEntity FirstOrDefault()
            => dbSet.FirstOrDefault();

        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.FirstOrDefaultAsync(predicate, cancellationToken);

        public virtual Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.FirstOrDefaultAsync(cancellationToken);

        public virtual Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.ToListAsync(cancellationToken);

        public virtual EntityEntry<TEntity> Add(TEntity entity)
            => dbSet.Add(entity);

        public virtual Task<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.AddAsync(entity, cancellationToken);

        public virtual void AddRange(IEnumerable<TEntity> entities)
            => dbSet.AddRange(entities);

        public virtual void AddRange(params TEntity[] entities)
            => dbSet.AddRange(entities);

        public virtual Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.AddRangeAsync(entities, cancellationToken);

        public virtual Task AddRangeAsync(params TEntity[] entities)
            => dbSet.AddRangeAsync(entities);

        public virtual EntityEntry<TEntity> Attach(TEntity entity)
            => dbSet.Attach(entity);

        public virtual void AttachRange(params TEntity[] entities)
            => dbSet.AttachRange(entities);

        public virtual void AttachRange(IEnumerable<TEntity> entities)
            => dbSet.AttachRange(entities);

        public virtual EntityEntry<TEntity> Update(TEntity entity)
            => dbSet.Update(entity);

        public virtual void UpdateRange(params TEntity[] entities)
            => dbSet.UpdateRange(entities);

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
            => dbSet.UpdateRange(entities);

        public virtual EntityEntry<TEntity> Remove(TEntity entity)
            => dbSet.Remove(entity);

        public virtual void RemoveRange(params TEntity[] entities)
            => RemoveRange(entities.ToList());

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
            => entities.ToList().ForEach(entity => Remove(entity));

        public IQueryable<TEntity> Skip(int count)
             => dbSet.Skip(count);

        public Task<List<TEntity>> SkipAndTakeAsync(int skip, int take)
            => dbSet.Skip(skip).Take(take).ToListAsync();
    }
}

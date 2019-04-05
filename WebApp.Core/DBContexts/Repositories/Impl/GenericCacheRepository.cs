using WebApp.Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace WebApp.Core.DBContexts.Repositories.Impl
{
    internal class GenericCacheRepository<TEntity> : GenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly IDistributedCache _distributedCache;

        public GenericCacheRepository(
            WebAppContext context,
            IDistributedCache distributedCache
            ) : base(context)
        {
            _distributedCache = distributedCache;
        }

        protected string GetCacheKeyWithPrefix(string key)
        {
               return $"{GetType().FullName}_{typeof(TEntity).FullName}_{key}";
        }

        protected void RemoveCache()
        {
            _distributedCache.Remove(DbSetCacheKey);
        }

        protected IList<TEntity> GetCachedEntities()
        {            
            var value = _distributedCache.Get<IList<TEntity>>(DbSetCacheKey);
            if (value != null)
            {
                return value;
            }

            var entities = dbSet.ToList();
            _distributedCache.Set<IList<TEntity>>(DbSetCacheKey, entities);

            return entities;
        }

        protected async Task<IList<TEntity>> GetCachedEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var value = await _distributedCache.GetAsync<IList<TEntity>>(DbSetCacheKey, cancellationToken);
            if (value != null)
            {
                return value;
            }

            var data = await base.ToListAsync();
            await _distributedCache.SetAsync<IList<TEntity>>(DbSetCacheKey, data, cancellationToken);

            return data;
        }

        public string DbSetCacheKey => GetCacheKeyWithPrefix(nameof(DbSetCacheKey));

        public override async Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await GetCachedEntitiesAsync(cancellationToken);
            return entities.All(predicate.Compile());
        }

        public override async Task<bool> AnyAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await GetCachedEntitiesAsync(cancellationToken);
            return entities.Any();
        }

        public override async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await GetCachedEntitiesAsync(cancellationToken);
            return entities.Any(predicate.Compile());
        }

        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await GetCachedEntitiesAsync(cancellationToken);
            return entities.Count(predicate.Compile());
        }

        public override async Task<TResult> MaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await GetCachedEntitiesAsync(cancellationToken);
            return entities.Max(selector.Compile());
        }

        public override async Task<TResult> MinAsync<TResult>(Expression<Func<TEntity, TResult>> selector, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await GetCachedEntitiesAsync(cancellationToken);
            return entities.Min(selector.Compile());
        }

        public override TEntity Find(params object[] keyValues)
        {
            if (keyValues.Length > 1)
            {
                return base.Find(keyValues);
            }

            var entities = GetCachedEntities();
            return entities.FirstOrDefault(e =>
            {
                var properties = e.GetType().GetProperties();
                var property = properties.FirstOrDefault(p => nameof(BaseEntity.Id).Equals(p.Name, StringComparison.InvariantCultureIgnoreCase));
                var value = property.GetValue(e);
                return keyValues.Any(kv => kv.Equals(value));
            });
        }

        public override async Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (keyValues.Length > 1)
            {
                return await base.FindAsync(keyValues, cancellationToken);
            }

            var entities = await GetCachedEntitiesAsync();
            return entities.FirstOrDefault(e =>
            {
                var properties = e.GetType().GetProperties();
                var property = properties.FirstOrDefault(p => nameof(BaseEntity.Id).Equals(p.Name, StringComparison.InvariantCultureIgnoreCase));
                var value = property.GetValue(e);
                return keyValues.Any(kv => kv.Equals(value));
            });
        }

        public override Task<TEntity> FindAsync(params object[] keyValues)
        {
            return FindAsync(keyValues, default(CancellationToken));
        }

        public override async Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await GetCachedEntitiesAsync();
            return entities.Where(predicate.Compile()).ToList();
        }

        public override TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = GetCachedEntities();
            return entities.FirstOrDefault(predicate.Compile());
        }

        public override TEntity FirstOrDefault()
        {
            var entities = GetCachedEntities();
            return entities.FirstOrDefault();
        }

        public override async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await GetCachedEntitiesAsync(cancellationToken);
            return entities.FirstOrDefault(predicate.Compile());
        }

        public override async Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await GetCachedEntitiesAsync(cancellationToken);
            return entities.FirstOrDefault();
        }

        public override async Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return (await GetCachedEntitiesAsync(cancellationToken)).ToList();
        }

        public override EntityEntry<TEntity> Add(TEntity entity)
        {
            RemoveCache();
            return base.Add(entity);
        }

        public override Task<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            RemoveCache();
            return base.AddAsync(entity, cancellationToken);
        }

        public override void AddRange(IEnumerable<TEntity> entities)
        {
            RemoveCache();
            base.AddRange(entities);
        }

        public override void AddRange(params TEntity[] entities)
        {
            RemoveCache();
            base.AddRange(entities);
        }

        public override Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            RemoveCache();
            return base.AddRangeAsync(entities, cancellationToken);
        }

        public override Task AddRangeAsync(params TEntity[] entities)
        {
            RemoveCache();
            return base.AddRangeAsync(entities);
        }

        public override EntityEntry<TEntity> Attach(TEntity entity)
        {
            RemoveCache();
            return base.Attach(entity);
        }

        public override void AttachRange(params TEntity[] entities)
        {
            RemoveCache();
            base.AttachRange(entities);
        }

        public override void AttachRange(IEnumerable<TEntity> entities)
        {
            RemoveCache();
            base.AttachRange(entities);
        }

        public override EntityEntry<TEntity> Update(TEntity entity)
        {
            RemoveCache();
            return base.Update(entity);
        }

        public override void UpdateRange(params TEntity[] entities)
        {
            RemoveCache();
            base.UpdateRange(entities);
        }

        public override void UpdateRange(IEnumerable<TEntity> entities)
        {
            RemoveCache();
            base.UpdateRange(entities);
        }

        public override EntityEntry<TEntity> Remove(TEntity entity)
        {
            RemoveCache();
            return base.Remove(entity);
        }

        public override void RemoveRange(params TEntity[] entities)
        {
            RemoveCache();
            base.RemoveRange(entities);
        }

        public override void RemoveRange(IEnumerable<TEntity> entities)
        {
            RemoveCache();
            base.RemoveRange(entities);
        }
    }
}

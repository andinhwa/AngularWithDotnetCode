using WebApp.Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.Core.DBContexts.Repositories.Impl
{
    internal class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private readonly WebAppContext _context;

        public UnitOfWorkRepository(WebAppContext context)
        {
            _context = context;
        }

        public EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class, IBaseEntity
        {
            return _context.Remove(entity);
        }

        public EntityEntry Remove(object entity)
        {
            return _context.Remove(entity);
        }

        public void RemoveRange(IEnumerable<object> entities)
        {
            _context.RemoveRange(entities);
        }

        public void RemoveRange(params object[] entities)
        {
            _context.RemoveRange(entities);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}

using WebApp.Core.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.Core.DBContexts.Repositories
{
    internal interface IUnitOfWorkRepository
    {
        EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity : class, IBaseEntity;

        EntityEntry Remove(object entity);

        void RemoveRange(IEnumerable<object> entities);

        void RemoveRange(params object[] entities);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}

using WebApp.Core.Models.Identity;
using Microsoft.Extensions.Caching.Distributed;

namespace WebApp.Core.DBContexts.Repositories.Impl
{
    internal class UserRoleRepository : GenericCacheRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(WebAppContext context, IDistributedCache distributedCache) : base(context, distributedCache)
        {
        }
    }
}

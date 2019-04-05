using WebApp.Core.Models.Identity;

namespace WebApp.Core.DBContexts.Repositories.Impl
{
    internal class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(WebAppContext context) : base(context)
        {
        }
    }
}

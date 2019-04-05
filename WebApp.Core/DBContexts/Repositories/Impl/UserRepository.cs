using WebApp.Core.Models.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Core.DBContexts.Repositories.Impl
{
    internal class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(WebAppContext context) : base(context)
        {
        }

        public User GetUserWithRolesByUserName(string normalizeUserName)
        {
            return dbSet
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.NormalizedUserName == normalizeUserName);
        }
    }
}

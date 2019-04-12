using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Core.Models.Identity;

namespace WebApp.Core.DBContexts.Repositories.Impl {
    internal class UserRepository : GenericRepository<User>, IUserRepository {
        public UserRepository (WebAppContext context) : base (context) { }

        public async Task<User> GetUserWithRolesByUserNameAsync (string userName) {
            return await dbSet
                .Include (u => u.UserRoles)
                    .ThenInclude (ur => ur.Role)
                .FirstOrDefaultAsync (u => u.UserName == userName);
        }
    }
}
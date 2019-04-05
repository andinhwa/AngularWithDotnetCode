using WebApp.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp.Core.DBContexts.Repositories
{
    internal interface IUserRepository : IGenericRepository<User>
    {
        User GetUserWithRolesByUserName(string username);
    }
}

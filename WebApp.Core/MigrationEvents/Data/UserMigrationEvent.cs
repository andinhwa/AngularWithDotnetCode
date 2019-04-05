using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Core.DBContexts.Repositories;
using WebApp.Core.Models.Identity;

namespace WebApp.Core.MigrationEvents.Data
{
    internal class UserMigrationEvent : IMigrationEvent
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;

        public int Order => UserRoleMigrationEvent.CurrentOrder + 1;

        public bool IsCreateSchemaEvent => false;

        IEnumerable<(string UserName, string FirstName, string LastName)> _users
            = new List<(string UserName, string FirstName, string LastName)> {
            ("Admin", "Admin", ""),
        };

        readonly IEnumerable<string> _superUsers = new[]
        {
            "Admin"
        };

        public UserMigrationEvent(
            IUnitOfWorkRepository unitOfWorkRepository,
            UserManager<User> userManager
            )
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _userManager = userManager;
        }

        private async Task CreateUser()
        {
            var password = "Abc123456@";
            foreach (var (userName, firstName, lastName) in _users)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user != null)
                {
                    if (!await _userManager.IsInRoleAsync(user, "USER"))
                    {
                        await _userManager.AddToRoleAsync(user, "USER");
                    }

                    continue;
                }
                user = new User
                {
                    UserName = userName,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = $"{userName}@mail.com",
                };
                var identityResult = await _userManager.CreateAsync(user, password);
                if (!identityResult.Succeeded)
                {
                    var error = identityResult.Errors.FirstOrDefault();
                    throw new Exception($"{error?.Code} {error?.Description}");
                }

                identityResult = await _userManager.AddToRoleAsync(user, "USER");
                if (!identityResult.Succeeded)
                {
                    var error = identityResult.Errors.FirstOrDefault();
                    throw new Exception($"{error?.Code} {error?.Description}");
                }
            }

        }

        private async Task CreateSuperUsers()
        {
            foreach (var userName in _superUsers)
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (!await _userManager.IsInRoleAsync(user, "SUPER_USER"))
                {
                    await _userManager.AddToRoleAsync(user, "SUPER_USER");
                }
            }
        }

        public async Task ExecuteAsync()
        {
            await CreateUser();
            await CreateSuperUsers();
        }
    }
}

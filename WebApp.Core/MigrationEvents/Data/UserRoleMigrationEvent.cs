using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApp.Core.Models.Identity;

namespace WebApp.Core.MigrationEvents.Data {
    internal class UserRoleMigrationEvent : IMigrationEvent {
        private readonly RoleManager<Role> _roleManager;

        public UserRoleMigrationEvent (
            RoleManager<Role> roleManager
        ) {
            _roleManager = roleManager;
        }

        public int Order => CurrentOrder;

        public static int CurrentOrder => default (int);

        public bool IsCreateSchemaEvent => false;

        public async Task ExecuteAsync () {
            var roles = new [] {
                "USER",
                "SUPER_USER",
            };

            foreach (var role in roles) {
                if (!await _roleManager.RoleExistsAsync (role)) {
                    var result = await _roleManager.CreateAsync (new Role (role));
                }
            }
        }
    }
}
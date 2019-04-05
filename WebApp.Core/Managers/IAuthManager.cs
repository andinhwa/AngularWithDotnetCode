using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApp.Core.Models;
using WebApp.Core.Models.Identity;

namespace WebApp.Core.Managers {
    public interface IAuthManager {

        Task<IList<User>> GetAllUsersAsync();
        Task<IdentityResult> RegisterUser (User user, string password);
        Task<User> FindUser (string userName, string password);
        Task<User> FindUser (string userName);
        Client FindClient (string clientId);
        Task<bool> AddRefreshToken (RefreshToken token);
        Task<bool> RemoveRefreshToken (string refreshTokenId);
        Task<bool> RemoveRefreshToken (RefreshToken refreshToken);
        Task<RefreshToken> FindRefreshToken (string refreshTokenId);
        Task<List<RefreshToken>> GetAllRefreshTokens ();
        Task<User> FindAsync (UserLoginInfo loginInfo);
        Task<IdentityResult> CreateAsync (User user, string password);
        Task<IdentityResult> AddLoginAsync (User user, UserLoginInfo login);
        Task<IdentityResult> UpdateProfileWithPassword (User user, string password, string newPassword);
        Task<IdentityResult> Update (User user);
        Task<bool> IsUserInRoleAsync (string userName, string role);
        bool IsUserInRoles (string userName, string[] roles);
    }
}
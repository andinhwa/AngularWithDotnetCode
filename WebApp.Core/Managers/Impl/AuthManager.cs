using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApp.Core.DBContexts.Repositories;
using WebApp.Core.Models;
using WebApp.Core.Models.Identity;

namespace WebApp.Core.Managers.Impl
{
    internal class AuthManager : IAuthManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IUserRepository _userRepository;

        public AuthManager(UserManager<User> userManager,
            IUnitOfWorkRepository unitOfWorkRepository,
            IRefreshTokenRepository refreshTokenRepository,
            IClientRepository clientRepository,
            IUserRepository userRepository)
        {
            _userManager = userManager;
            _unitOfWorkRepository = unitOfWorkRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _clientRepository = clientRepository;
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo login)
        {
            var result = await _userManager.AddLoginAsync(user, login);
            if (result.Succeeded)
            {
                await _unitOfWorkRepository.SaveChangesAsync();
            }
            return result;
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            var existingTokens = await _refreshTokenRepository.GetBySubjectAndClientId(token.Subject, token.Client.ClientId);
            if (existingTokens != null && existingTokens.Count > 0)
            {
                existingTokens.Clear();
            }
            _refreshTokenRepository.Add(token);
            await _unitOfWorkRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _unitOfWorkRepository.SaveChangesAsync();
            }
            return result;
        }

        public async Task<User> FindAsync(UserLoginInfo loginInfo)
        {
            var user = await _userManager.FindByLoginAsync(loginInfo.LoginProvider, loginInfo.ProviderKey);
            return user;
        }

        public Client FindClient(string clientId)
        {
            var client = _clientRepository.FirstOrDefault(cl => cl.ClientId == clientId);
            return client;

        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _refreshTokenRepository.FirstOrDefaultAsync(rf => rf.RefreshTokenId == refreshTokenId);

            return refreshToken;
        }

        public async Task<User> FindUser(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return null;
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                return null;
            }

            return (await _userManager.CheckPasswordAsync(user, password)) ? user : null;
        }

        public async Task<User> FindUser(string userName)
        {
            return await _userManager.FindByNameAsync(userName);

        }

        public async Task<List<RefreshToken>> GetAllRefreshTokens()
        {
            return await _refreshTokenRepository.ToListAsync();
        }

        public async Task<IList<User>> GetAllUsersAsync()
        {
            return await _userRepository.ToListAsync();
        }

        public async Task<bool> IsUserInRoleAsync(string userName, string role)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(role))
            {
                return false;
            }
            var user = await _userManager.FindByNameAsync(userName);
            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public bool IsUserInRoles(string userName, string[] roles)
        {
            if (string.IsNullOrEmpty(userName) || roles.Count() == 0)
            {
                return false;
            }

            var normalizedUserName = _userManager.NormalizeKey(userName);
            var user = _userRepository.GetUserWithRolesByUserName(normalizedUserName);
            if (user == null)
            {
                return false;
            }

            foreach (var roleName in roles)
            {
                var normalizedRoleName = _userManager.NormalizeKey(roleName);
                if (user.UserRoles.Select(ur => ur.Role.NormalizedName).Contains(normalizedRoleName))
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<IdentityResult> RegisterUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _unitOfWorkRepository.SaveChangesAsync();
            }
            return result;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _refreshTokenRepository.FirstOrDefaultAsync(rf => rf.RefreshTokenId == refreshTokenId);

            if (refreshToken != null)
            {
                _refreshTokenRepository.Remove(refreshToken);
                await _unitOfWorkRepository.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _refreshTokenRepository.Remove(refreshToken);
            await _unitOfWorkRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IdentityResult> Update(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _unitOfWorkRepository.SaveChangesAsync();
            }
            return result;
        }

        public async Task<IdentityResult> UpdateProfileWithPassword(User user, string password, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, password, newPassword);
            if (result.Succeeded)
            {
                await _unitOfWorkRepository.SaveChangesAsync();
            }
            return result;
        }
    }
}
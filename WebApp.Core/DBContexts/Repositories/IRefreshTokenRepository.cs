using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Core.Models;
using WebApp.Core.Models.Identity;

namespace WebApp.Core.DBContexts.Repositories {
    internal interface IRefreshTokenRepository : IGenericRepository<RefreshToken> {
        Task<IList<RefreshToken>> GetBySubjectAndClientId (string subject, string clientId);
    }
}
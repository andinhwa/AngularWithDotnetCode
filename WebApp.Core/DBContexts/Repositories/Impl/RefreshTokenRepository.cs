using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Core.Models;
using WebApp.Core.Models.Identity;

namespace WebApp.Core.DBContexts.Repositories.Impl
{
    internal class RefreshTokenRepository:  GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(WebAppContext context) : base(context)
        {
        }

        public async Task<IList<RefreshToken>> GetBySubjectAndClientId(string subject, string clientId)
        {
            return await dbSet.Where(r => r.Subject == subject && r.Client.ClientId == clientId).ToListAsync();
        }
    }
}

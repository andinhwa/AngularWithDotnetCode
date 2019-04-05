using WebApp.Core.Models;

namespace WebApp.Core.DBContexts.Repositories.Impl
{
    internal class ClientRepository:  GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(WebAppContext context) : base(context)
        {
        }
    }
}

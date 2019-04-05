using System;
using System.Threading.Tasks;
using WebApp.Core.Models;
using WebApp.Core.DBContexts.Repositories;
using WebApp.Core.Enums;

namespace WebApp.Core.MigrationEvents.Data
{
    internal class ClientMigrationEvent : IMigrationEvent
    {
        public bool IsCreateSchemaEvent => false;

        public int Order => UserRoleMigrationEvent.CurrentOrder + 1;

        private readonly IClientRepository _clientRepository;

        private readonly IUnitOfWorkRepository _unitOfWorkRepository;


        public ClientMigrationEvent(IClientRepository clientRepository, IUnitOfWorkRepository unitOfWorkRepository)
        {
            _clientRepository = clientRepository;
            _unitOfWorkRepository = unitOfWorkRepository;
        }

        public async Task ExecuteAsync()
        {
            if (!await _clientRepository.AnyAsync(c =>c.ClientId.Equals("ngAuthApp", StringComparison.CurrentCultureIgnoreCase)))
            {
                _clientRepository.Add(new Client
                {
                    ClientId = "ngAuthApp",
                    Secret = Helper.GetHash("abc@123"),
                    Name = "AngularJS front-end Application",
                    ApplicationType = ApplicationTypes.JavaScript,
                    Active = true,
                    RefreshTokenLifeTime = 14400,
                    AllowedOrigin = "http://localhost:51132/"
                });
                await _unitOfWorkRepository.SaveChangesAsync();
            }
           
        }

    }
}

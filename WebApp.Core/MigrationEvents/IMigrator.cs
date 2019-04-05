using System.Threading.Tasks;

namespace WebApp.Core.MigrationEvents
{
    internal interface IMigrator
    {
        Task MigrateSchema();

        Task MigrateData();
    }
}

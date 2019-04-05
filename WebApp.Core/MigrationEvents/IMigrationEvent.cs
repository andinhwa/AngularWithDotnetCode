using System.Threading.Tasks;

namespace WebApp.Core.MigrationEvents
{
    internal interface IMigrationEvent
    {
        bool IsCreateSchemaEvent { get; }

        int Order { get; }

        Task ExecuteAsync();
    }
}

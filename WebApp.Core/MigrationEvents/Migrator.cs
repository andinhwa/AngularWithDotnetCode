using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Core.MigrationEvents
{
    internal class Migrator : IMigrator
    {
        private readonly IEnumerable<IMigrationEvent> _migrationEvents;

        public Migrator(
            IEnumerable<IMigrationEvent> migrationEvents
            )
        {
            _migrationEvents = migrationEvents;
        }

        public async Task MigrateSchema()
        {
            var migrationEvents = _migrationEvents.Where(me => me.IsCreateSchemaEvent).OrderBy(me => me.Order);
            foreach (var migrationEvent in migrationEvents)
            {
                await migrationEvent.ExecuteAsync();
            }
        }

        public async Task MigrateData()
        {
            var migrationEvents = _migrationEvents.Where(me => !me.IsCreateSchemaEvent).OrderBy(me => me.Order);
            foreach (var migrationEvent in migrationEvents)
            {
                await migrationEvent.ExecuteAsync();
            }
        }
    }
}

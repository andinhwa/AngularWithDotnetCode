using System;
using System.Threading.Tasks;
using WebApp.Core.Managers;

namespace WebApp.Core.MigrationEvents.Data {
    internal class ScheduleJobMigrationEvent : IMigrationEvent {
        public int Order => int.MaxValue;

        public bool IsCreateSchemaEvent => false;

        public ScheduleJobMigrationEvent () {

        }

        public async Task ExecuteAsync () {
              await Task.FromResult(0);
        }
    }
}
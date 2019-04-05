using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebApp.Core.MigrationEvents.Schema
{
    internal class DatabaseSchemaMigrationEvent : IMigrationEvent
    {
        private readonly WebAppContext _context;

        public int Order => CurrentOrder;

        public static int CurrentOrder => default(int);

        public bool IsCreateSchemaEvent => true;

        public DatabaseSchemaMigrationEvent(WebAppContext context)
        {
           
            _context = context;
        }

        public async Task ExecuteAsync()
        {
#if !DEBUG
           await _context.Database.MigrateAsync();
#else
            await Task.FromResult(0);
            
#endif
        }
    }
}

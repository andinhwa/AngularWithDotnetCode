using System;
using System.Linq;
using System.Threading.Tasks;
using LightInject;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Core.MigrationEvents;

namespace WebApp.Core.StartupExtensions
{
   internal static class DataMigrationEventCollectionExtensions
    {
        public static IServiceCollection RegisterMigrationEvents(this IServiceCollection services)
        {
            services.AddSingleton<IMigrator, Migrator>();

            var migrationType = typeof(IMigrationEvent);
            var implementedTypes = migrationType.Assembly.DefinedTypes.Where(t => migrationType.IsAssignableFrom(t) && t != migrationType);

            foreach (var implementedType in implementedTypes)
            {
                services.AddSingleton(migrationType, implementedType);
            }

            return services;
        }

        public static IServiceContainer RunMigrationEvents(this IServiceContainer container, IServiceProvider serviceProvider)
        {
            using (container.BeginScope())
            {
                Task.Run(async () =>
                {
                    var migrator = serviceProvider.GetService<IMigrator>();
                    await migrator.MigrateSchema();
                    await migrator.MigrateData();
                }).GetAwaiter().GetResult();
            }

            return container;
        }
    }
}
using LightInject;
using LightInject.Microsoft.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using WebApp.Core.StartupExtensions;

namespace WebApp.Core
{
    public static class LicensingAppBuilderExtensions
    {
        public static void UseCoreAsync(this IServiceContainer container, Assembly assembly, IConfiguration configuration, IServiceCollection services)
        {
            services
                .RegisterAppContextServices(configuration)
                .RegisterTypeLoaderServices()
                .RegisterCommonServices()
                .RegisterDataServices()
                .RegisterManagerServices()
                .RegisterMapperServices(assembly)
                .RegisterMigrationEvents()
                //.RegisterHangFireServices(configuration)
                .RegisterCachingServices();

            container.RegisterAssembly(assembly);
            var serviceProvider = container.CreateServiceProvider(services);

            //container.RegisterHangFireJob(serviceProvider);
            container.RunMigrationEvents(serviceProvider);

        }

    }
}

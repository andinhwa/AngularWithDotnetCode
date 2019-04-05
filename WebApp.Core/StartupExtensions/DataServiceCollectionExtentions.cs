using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Core.DBContexts.Repositories;
using WebApp.Core.DBContexts.Repositories.Impl;
using WebApp.Core.Models;

namespace WebApp.Core.StartupExtensions
{
  internal static class DataServiceCollectionExtentions
    {
        public static IServiceCollection RegisterDataServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWorkRepository, UnitOfWorkRepository>();

            var interfaceTypes = GetImplementedOfGenericTypes(typeof(IGenericRepository<>));
            var implementedTypes = GetImplementedOfGenericTypes(typeof(GenericRepository<>));
            var cacheImplementedTypes = GetImplementedOfGenericTypes(typeof(GenericCacheRepository<>));

            foreach (var interfaceType in interfaceTypes)
            {
                var typeName = interfaceType.Name.Substring(1, interfaceType.Name.Length - 1);
                var implementType = implementedTypes.FirstOrDefault(t => interfaceType.IsAssignableFrom(t) && t.Name.Equals(typeName, StringComparison.InvariantCultureIgnoreCase));
                if (implementType == null)
                {
                    implementType = cacheImplementedTypes.FirstOrDefault(t => interfaceType.IsAssignableFrom(t) && t.Name.Equals(typeName, StringComparison.InvariantCultureIgnoreCase));
                }

                if (implementType == null)
                {
                    throw new Exception($"Missing implementation of the {interfaceType.FullName}");
                }

                services.AddScoped(interfaceType, implementType);
            }

            return services;
        }

        private static IEnumerable<Type> GetImplementedOfGenericTypes(Type genericType)
        {
            var baseEntity = typeof(IBaseEntity);

            return typeof(DataServiceCollectionExtentions)
                .Assembly
                .DefinedTypes
                .Where(t =>
                    t.IsInterface == genericType.IsInterface &&
                    t != genericType &&
                    ((genericType.IsInterface && t.GetInterfaces().Any(i =>
                        i.Namespace.Equals(genericType.Namespace, StringComparison.InvariantCultureIgnoreCase) &&
                        i.Name.Equals(genericType.Name, StringComparison.InvariantCultureIgnoreCase) &&
                        baseEntity.IsAssignableFrom(i.GenericTypeArguments.FirstOrDefault())))
                        || (!genericType.IsInterface &&
                            t.BaseType.Namespace.Equals(genericType.Namespace, StringComparison.InvariantCultureIgnoreCase) &&
                            t.BaseType.Name.Equals(genericType.Name, StringComparison.InvariantCultureIgnoreCase) &&
                            baseEntity.IsAssignableFrom(t.BaseType.GenericTypeArguments.FirstOrDefault()))
                        ));
        }
    }
}
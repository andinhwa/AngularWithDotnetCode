using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Core.StartupExtensions
{
    internal static class MapperServiceCollectionExtensions
    {
        public static IServiceCollection RegisterMapperServices(this IServiceCollection services, Assembly assembly)
        {
            services.AddAutoMapper(assembly, typeof(MapperServiceCollectionExtensions).Assembly);

            return services;
        }
    }
}
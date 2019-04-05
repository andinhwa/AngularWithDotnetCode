using Microsoft.Extensions.DependencyInjection;
using WebApp.Core.ReflectionLoader;
using WebApp.Core.ReflectionLoader.Impl;

namespace WebApp.Core.StartupExtensions
{
    internal static class AssemblyLoaderCollectionExtentions
    {
        public static IServiceCollection RegisterTypeLoaderServices(this IServiceCollection services)
        {
            services.AddSingleton<ISystemWebTypeLoader, SystemWebTypeLoader>();
            services.AddSingleton<IAssemblyLoader, AssemblyLoader>();

            return services;
        }
    }
}
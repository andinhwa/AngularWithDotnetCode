using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Core.StartupExtensions {
    internal static class CachingServiceCollectionExtentions {
        public static IServiceCollection RegisterCachingServices (this IServiceCollection services) {
            services.AddDistributedMemoryCache ();

            return services;
        }
    }
}
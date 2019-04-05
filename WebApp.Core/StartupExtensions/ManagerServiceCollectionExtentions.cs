using Microsoft.Extensions.DependencyInjection;
using WebApp.Core.Managers;
using WebApp.Core.Managers.Impl;

namespace WebApp.Core.StartupExtensions
{
   internal static class ManagerServiceCollectionExtentions
    {
        public static IServiceCollection RegisterManagerServices(this IServiceCollection services)
        {
            services.AddTransient<IAuthManager, AuthManager>();
            return services;
        }
    }
}
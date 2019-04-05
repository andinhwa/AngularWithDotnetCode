using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WebApp.Core.StartupExtensions
{
   internal static class CommonServiceCollectionExtentions
    {
        public static IServiceCollection RegisterCommonServices(this IServiceCollection services)
        {
            services.AddSingleton(new HttpClient());
            services.AddSingleton(new Random());
            //services.AddScoped<IPrincipalService, PrincipalService>();
            return services;
        }
    }
}
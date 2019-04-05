using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Core.Models.Identity;

namespace WebApp.Core.StartupExtensions {
   internal static class AppContextServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAppContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebAppContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("WebAppDbContext"));
                options.EnableSensitiveDataLogging();
            });

            services
                .AddIdentity<User, Role>()
                .AddEntityFrameworkStores<WebAppContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;                

                // User settings
                options.User.RequireUniqueEmail = true;
            });            

            return services;
        }
    }
}
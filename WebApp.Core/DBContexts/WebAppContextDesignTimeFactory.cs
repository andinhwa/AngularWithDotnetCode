using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WebApp.Core {
    public class DesignTimeFactory : IDesignTimeDbContextFactory<WebAppContext> {
        public WebAppContext CreateDbContext (string[] args) {
            var configuration = new ConfigurationBuilder ()
                .SetBasePath (AppContext.BaseDirectory)
                .AddJsonFile ("appsettings.json", optional : false, reloadOnChange : true)
                .Build ();

            var builder = new DbContextOptionsBuilder<WebAppContext> ();
            var connectionString = configuration.GetConnectionString("WebAppDbContext");
            builder.UseSqlite (connectionString);

            return new WebAppContext (builder.Options);
        }
    }
}
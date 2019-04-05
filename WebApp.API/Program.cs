using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LightInject.Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApp.API {
    public class Program {

        public static void Main (string[] args) {
            var host = CreateWebHostBuilder (args).Build ();
            // host.BuildAppCore();
            host.Run ();
        }

        public static IWebHostBuilder CreateWebHostBuilder (string[] args) {
            var config = new ConfigurationBuilder ()
                .SetBasePath (Directory.GetCurrentDirectory ())
                .AddJsonFile ("appsettings.json", optional : true)
                .AddCommandLine (args)
                .Build ();

            return WebHost.CreateDefaultBuilder (args)
                .UseUrls ("http://*:5000")
                .UseConfiguration (config)
                .UseLightInject ()
                .UseStartup<Startup> ();
        }

    }
}
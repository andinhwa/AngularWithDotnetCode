using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using LightInject;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApp.API.Providers;
using WebApp.Core;

namespace WebApp.API {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IServiceCollection Services { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddCors ();
            services.RegisterAuthorization ();
            services.AddMvc ()
                .SetCompatibilityVersion (CompatibilityVersion.Version_2_1)
                .AddControllersAsServices ();

            Services = services;
        }

        public void ConfigureContainer (IServiceContainer container) {
            container.UseCoreAsync (typeof (Startup).Assembly, Configuration, this.Services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseHsts ();
            }

            app.UseCors (
                options => options.AllowAnyOrigin () //options.WithOrigins("http://localhost:1280") //.AllowAnyOrigin()//
                .AllowAnyMethod ()
                .AllowAnyHeader ()
                .AllowCredentials()
            );
            app.UseAuthentication ();
            //app.UseHttpsRedirection();

            app.UseMvc ();
            app.UseStaticFiles ();
            app.UseWelcomePage ();

        }
    }
}
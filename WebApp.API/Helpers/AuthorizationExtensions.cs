using System;
using System.Net;
using System.Threading.Tasks;
using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebApp.API.Providers;

namespace WebApp.API {
    public static class AuthorizationExtensions {

        public static IServiceCollection RegisterAuthorization (this IServiceCollection services) {

            services
                .AddAuthentication (options => {
                    options.DefaultScheme = "ServerCookie"; //OAuthValidationDefaults.AuthenticationScheme; //"ServerCookie";
                    options.DefaultChallengeScheme = OAuthValidationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = OAuthValidationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie ("ServerCookie", options => {
                    options.Cookie.Name = CookieAuthenticationDefaults.CookiePrefix + "ServerCookie";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes (5);
                    options.LoginPath = new PathString ("/signin");
                    options.LogoutPath = new PathString ("/signout");
                })
                .AddOAuthValidation ()
                .AddOpenIdConnectServer (options => {
                    options.ProviderType = typeof (AuthorizationProvider);
                    options.AuthorizationEndpointPath = "/connect/authorize";
                    options.LogoutEndpointPath = "/connect/logout";
                    options.TokenEndpointPath = "/connect/token";
                    options.UserinfoEndpointPath = "/connect/userinfo";
                    // Note: see AuthorizationController.cs for more
                    // information concerning ApplicationCanDisplayErrors.
                    options.ApplicationCanDisplayErrors = true;
                    options.AllowInsecureHttp = true;
                    // options.Events = new OpenIdConnectEvents {
                    //     OnUserInformationReceived = usr => {
                    //         return Task.FromResult (usr);
                    //     }
                    // };

                    // Note: to override the default access token format and use JWT, assign AccessTokenHandler:
                    //
                    // options.AccessTokenHandler = new JwtSecurityTokenHandler
                    // {
                    //     InboundClaimTypeMap = new Dictionary<string, string>(),
                    //     OutboundClaimTypeMap = new Dictionary<string, string>()
                    // };
                    //
                    // Note: when using JWT as the access token format, you have to register a signing key.
                    //
                    // You can register a new ephemeral key, that is discarded when the application shuts down.
                    // Tokens signed using this key are automatically invalidated and thus this method
                    // should only be used during development:
                    //
                    // options.SigningCredentials.AddEphemeralKey();
                    //
                    // On production, using a X.509 certificate stored in the machine store is recommended.
                    // You can generate a self-signed certificate using Pluralsight's self-cert utility:
                    // https://s3.amazonaws.com/pluralsight-free/keith-brown/samples/SelfCert.zip
                    //
                    // options.SigningCredentials.AddCertificate("7D2A741FE34CC2C7369237A5F2078988E17A6A75");
                    //
                    // Alternatively, you can also store the certificate as an embedded .pfx resource
                    // directly in this assembly or in a file published alongside this project:
                    //
                    // options.SigningCredentials.AddCertificate(
                    //     assembly: typeof(Startup).GetTypeInfo().Assembly,
                    //     resource: "Mvc.Server.Certificate.pfx",
                    //     password: "Owin.Security.OpenIdConnect.Server");
                });

            services.AddScoped<AuthorizationProvider> ();
            return services;
        }

    }
}
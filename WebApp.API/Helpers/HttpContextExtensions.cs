using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
namespace WebApp.API.Helpers {
    public static class HttpContextExtensions {
        public static async Task<AuthenticationScheme[]> GetExternalProvidersAsync (this HttpContext context) {
            if (context == null) {
                throw new ArgumentNullException (nameof (context));
            }

            var schemes = context.RequestServices.GetRequiredService<IAuthenticationSchemeProvider> ();

            return (from scheme in await schemes.GetAllSchemesAsync () where!string.IsNullOrEmpty (scheme.DisplayName) select scheme).ToArray ();
        }

        public static async Task<bool> IsProviderSupportedAsync (this HttpContext context, string provider) {
            if (context == null) {
                throw new ArgumentNullException (nameof (context));
            }

            var datas = await context.GetExternalProvidersAsync ();
            Debug.WriteLine (datas);

            return (from scheme in await context.GetExternalProvidersAsync () where string.Equals (scheme.Name, provider, StringComparison.OrdinalIgnoreCase) select scheme).Any ();
        }
    }
}
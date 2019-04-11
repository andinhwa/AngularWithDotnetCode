using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.API {
    public class AppAuthorizeAttribute : AuthorizeAttribute {
        public AppAuthorizeAttribute () {
            this.AuthenticationSchemes = OAuthValidationDefaults.AuthenticationScheme;
            //this.ActiveAuthenticationSchemes
        }
    }
}
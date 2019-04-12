using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using WebApp.Core.Managers;

namespace WebApp.API.Providers {
    public sealed class AuthorizationProvider : OpenIdConnectServerProvider {

        private readonly IAuthManager _authManager;
        public AuthorizationProvider (IAuthManager authManager) {
            _authManager = authManager;
        }

        public override Task ValidateAuthorizationRequest (ValidateAuthorizationRequestContext context) {
            // Note: the OpenID Connect server middleware supports the authorization code, implicit and hybrid flows
            // but this authorization provider only accepts response_type=code authorization/authentication requests.
            // You may consider relaxing it to support the implicit or hybrid flows. In this case, consider adding
            // checks rejecting implicit/hybrid authorization requests when the client is a confidential application.
            if (!context.Request.IsAuthorizationCodeFlow ()) {
                context.Reject (
                    error: OpenIdConnectConstants.Errors.UnsupportedResponseType,
                    description: "Only the authorization code flow is supported by this authorization server.");

                return Task.FromResult (0);
            }

            // Note: to support custom response modes, the OpenID Connect server middleware doesn't
            // reject unknown modes before the ApplyAuthorizationResponse event is invoked.
            // To ensure invalid modes are rejected early enough, a check is made here.
            if (!string.IsNullOrEmpty (context.Request.ResponseMode) && !context.Request.IsFormPostResponseMode () &&
                !context.Request.IsFragmentResponseMode () &&
                !context.Request.IsQueryResponseMode ()) {
                context.Reject (
                    error: OpenIdConnectConstants.Errors.InvalidRequest,
                    description: "The specified 'response_mode' is unsupported.");

                return Task.FromResult (0);
            }

            // Ensure the client_id parameter corresponds to the Postman client.
            if (!string.Equals (context.Request.ClientId, "postman", StringComparison.Ordinal)) {
                context.Reject (
                    error: OpenIdConnectConstants.Errors.InvalidClient,
                    description: "The specified client identifier is invalid.");

                return Task.FromResult (0);
            }

            // Ensure the redirect_uri parameter corresponds to the Postman client.
            if (!string.Equals (context.Request.RedirectUri, "https://www.getpostman.com/oauth2/callback", StringComparison.Ordinal)) {
                context.Reject (
                    error: OpenIdConnectConstants.Errors.InvalidClient,
                    description: "The specified 'redirect_uri' is invalid.");

                return Task.FromResult (0);
            }

            context.Validate ();

            return Task.FromResult (0);
        }

        public override Task ValidateTokenRequest (ValidateTokenRequestContext context) {
            // Reject the token request if it doesn't specify grant_type=authorization_code,
            // grant_type=password or grant_type=refresh_token.
            if (!context.Request.IsAuthorizationCodeGrantType () &&
                !context.Request.IsPasswordGrantType () &&
                !context.Request.IsRefreshTokenGrantType ()) {
                context.Reject (
                    error: OpenIdConnectConstants.Errors.UnsupportedGrantType,
                    description: "Only grant_type=authorization_code, grant_type=password or " +
                    "grant_type=refresh_token are accepted by this server.");

                return Task.FromResult (0);
            }

            // Since there's only one application and since it's a public client
            // (i.e a client that cannot keep its credentials private), call Skip()
            // to inform the server the request should be accepted without
            // enforcing client authentication.
            context.Skip ();
            return Task.FromResult (0);
        }

        public override async  Task HandleUserinfoRequest (HandleUserinfoRequestContext context) {             
             var user = await _authManager.FindUser(context.Ticket.Principal.Identity.Name);
             foreach(var claim in context.Ticket.Principal.Claims){
                 context.Claims.Add(claim.Type, claim.Value);
             }
        }

        public override async Task HandleTokenRequest (HandleTokenRequestContext context) {
            //HttpContext.RequestServices.GetRequiredService
            // Only handle grant_type=password token requests and let the
            // OpenID Connect server middleware handle the other grant types.
            if (context.Request.IsPasswordGrantType ()) {

                var user = await _authManager.FindUser (context.Request.Username, context.Request.Password);

                if (user == null) {
                    context.Reject (
                        error: OpenIdConnectConstants.Errors.InvalidGrant,
                        description: "The specified user credentials are invalid.");
                    return;
                }
                // Create a new ClaimsIdentity containing the claims that
                // will be used to create an id_token and/or an access token.
                var identity = new ClaimsIdentity (
                    OpenIdConnectServerDefaults.AuthenticationScheme,
                    OpenIdConnectConstants.Claims.Name,
                    OpenIdConnectConstants.Claims.Role);

                identity.AddClaim (
                    new Claim (OpenIdConnectConstants.Claims.Subject, user.Id.ToString ())
                    .SetDestinations (OpenIdConnectConstants.Destinations.AccessToken,
                        OpenIdConnectConstants.Destinations.IdentityToken));

                identity.AddClaim (
                    new Claim (OpenIdConnectConstants.Claims.Name, user.UserName)
                    .SetDestinations (OpenIdConnectConstants.Destinations.AccessToken,
                        OpenIdConnectConstants.Destinations.IdentityToken));

                identity.AddClaim (
                    new Claim (OpenIdConnectConstants.Claims.Email, user.Email)
                    .SetDestinations (OpenIdConnectConstants.Destinations.AccessToken,
                        OpenIdConnectConstants.Destinations.IdentityToken));

                identity.AddClaim (
                    new Claim (OpenIdConnectConstants.Claims.Role, "['a','b','c']")
                    .SetDestinations (OpenIdConnectConstants.Destinations.AccessToken,
                        OpenIdConnectConstants.Destinations.IdentityToken));

                var ticket = new AuthenticationTicket (
                    new ClaimsPrincipal (identity),
                    new AuthenticationProperties (),
                    OpenIdConnectServerDefaults.AuthenticationScheme);

                ticket.SetScopes (new [] {
                    /* openid: */
                    OpenIdConnectConstants.Scopes.OpenId,
                        /* email: */
                        OpenIdConnectConstants.Scopes.Email,
                        /* profile: */
                        OpenIdConnectConstants.Scopes.Profile,
                        /* offline_access: */
                        OpenIdConnectConstants.Scopes.OfflineAccess
                }.Intersect (context.Request.GetScopes ()));
                ticket.SetResources ("resource_server");
                context.Validate (ticket);
            }
        }
    }
}
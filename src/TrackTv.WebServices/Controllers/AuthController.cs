namespace TrackTv.WebServices.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Threading.Tasks;

    using AspNet.Security.OpenIdConnect.Extensions;
    using AspNet.Security.OpenIdConnect.Primitives;
    using AspNet.Security.OpenIdConnect.Server;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Http.Authentication;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using OpenIddict.Core;

    using TrackTv.Services.Data;
    using TrackTv.WebServices.Infrastructure;

    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        public AuthController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IProfilesRepository profilesRepository,
            ITransactionScopeFactory transactionScopeFactory)
        {
            this.SignInManager = signInManager;
            this.UserManager = userManager;
            this.ProfilesRepository = profilesRepository;
            this.TransactionScopeFactory = transactionScopeFactory;
        }

        private IProfilesRepository ProfilesRepository { get; }

        private SignInManager<ApplicationUser> SignInManager { get; }

        private ITransactionScopeFactory TransactionScopeFactory { get; }

        private UserManager<ApplicationUser> UserManager { get; }

        [HttpPost("~/connect/token")]
        [Produces("application/json")]
        public async Task<IActionResult> Exchange(OpenIdConnectRequest request)
        {
            Debug.Assert(request.IsTokenRequest(),
                "The OpenIddict binder for ASP.NET Core MVC is not registered. "
                + "Make sure services.AddOpenIddict().AddMvcBinders() is correctly called.");

            if (request.IsPasswordGrantType())
            {
                var user = await this.UserManager.FindByNameAsync(request.Username).ConfigureAwait(false);

                if (user == null)
                {
                    return this.BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The username/password couple is invalid."
                    });
                }

                // Ensure the user is allowed to sign in.
                if (!await this.SignInManager.CanSignInAsync(user).ConfigureAwait(false))
                {
                    return this.BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The specified user is not allowed to sign in."
                    });
                }

                // Reject the token request if two-factor authentication has been enabled by the user.
                if (this.UserManager.SupportsUserTwoFactor && await this.UserManager.GetTwoFactorEnabledAsync(user).ConfigureAwait(false))
                {
                    return this.BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The specified user is not allowed to sign in."
                    });
                }

                // Ensure the user is not already locked out.
                if (this.UserManager.SupportsUserLockout && await this.UserManager.IsLockedOutAsync(user).ConfigureAwait(false))
                {
                    return this.BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The username/password couple is invalid."
                    });
                }

                // Ensure the password is valid.
                if (!await this.UserManager.CheckPasswordAsync(user, request.Password).ConfigureAwait(false))
                {
                    if (this.UserManager.SupportsUserLockout)
                    {
                        await this.UserManager.AccessFailedAsync(user).ConfigureAwait(false);
                    }

                    return this.BadRequest(new OpenIdConnectResponse
                    {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The username/password couple is invalid."
                    });
                }

                if (this.UserManager.SupportsUserLockout)
                {
                    await this.UserManager.ResetAccessFailedCountAsync(user).ConfigureAwait(false);
                }

                // Create a new authentication ticket.
                var ticket = await this.CreateTicketAsync(request, user).ConfigureAwait(false);

                return this.SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
            }

            return this.BadRequest(new OpenIdConnectResponse
            {
                Error = OpenIdConnectConstants.Errors.UnsupportedGrantType,
                ErrorDescription = "The specified grant type is not supported."
            });
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            using (var scope = await this.TransactionScopeFactory.CreateScopeAsync().ConfigureAwait(false))
            {
                int profileId = await this.ProfilesRepository.CreateProfileAsync(model.Email).ConfigureAwait(false);

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    ProfileId = profileId
                };

                var result = await this.UserManager.CreateAsync(user, model.Password).ConfigureAwait(false);

                if (result.Succeeded)
                {
                    scope.Complete();

                    return this.Ok();
                }

                return this.BadRequest(result.Errors);
            }
        }

        private static void AddCustomClaims(ApplicationUser user, IPrincipal principal)
        {
            var identity = (ClaimsIdentity)principal.Identity;

            identity.AddClaim(new Claim(nameof(user.ProfileId), user.ProfileId.ToString()));
        }

        private async Task<AuthenticationTicket> CreateTicketAsync(OpenIdConnectRequest request, ApplicationUser user)
        {
            // Create a new ClaimsPrincipal containing the claims that
            // will be used to create an id_token, a token or a code.
            var principal = await this.SignInManager.CreateUserPrincipalAsync(user).ConfigureAwait(false);

            AddCustomClaims(user, principal);

            // Note: by default, claims are NOT automatically included in the access and identity tokens.
            // To allow OpenIddict to serialize them, you must attach them a destination, that specifies
            // whether they should be included in access tokens, in identity tokens or in both.
            foreach (var claim in principal.Claims)
            {
                // In this sample, every claim is serialized in both the access and the identity tokens.
                // In a real world application, you'd probably want to exclude confidential claims
                // or apply a claims policy based on the scopes requested by the client application.
                claim.SetDestinations(OpenIdConnectConstants.Destinations.AccessToken, OpenIdConnectConstants.Destinations.IdentityToken);
            }

            // Create a new authentication ticket holding the user identity.
            var ticket = new AuthenticationTicket(principal, new AuthenticationProperties(),
                OpenIdConnectServerDefaults.AuthenticationScheme);

            // Set the list of scopes granted to the client application.
            // Note: the offline_access scope must be granted
            // to allow OpenIddict to return a refresh token.
            ticket.SetScopes(new[]
            {
                OpenIdConnectConstants.Scopes.OpenId,
                OpenIdConnectConstants.Scopes.Email,
                OpenIdConnectConstants.Scopes.Profile,
                OpenIdConnectConstants.Scopes.OfflineAccess,
                OpenIddictConstants.Scopes.Roles
            }.Intersect(request.GetScopes()));

            ticket.SetResources("TrackTv_Api");

            return ticket;
        }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
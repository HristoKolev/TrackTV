namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.Security.Claims;
    using System.Text.Encodings.Web;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authentication;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class CustomTokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string SchemeName = "JWT";

        private const string AuthorizationHeaderName = "Authorization";

        public CustomTokenAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            SessionService sessionService)
            : base(options, logger, encoder, clock)
        {
            this.SessionService = sessionService;
        }

        private SessionService SessionService { get; }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string headerValue = this.Request.Headers[AuthorizationHeaderName];

            if (string.IsNullOrWhiteSpace(headerValue))
            {
                // Invalid authorization header
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var parts = headerValue.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
            {
                // Invalid authorization header
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            string scheme = parts[0];

            if (scheme != SchemeName)
            {
                // Unsupported scheme.
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            string token = parts[1];

            var publicSession = this.SessionService.DecodeSession(token);

            if (publicSession == null)
            {
                // Invalid/Expired token.
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, $"User profile {publicSession.ProfileID}"),
                new Claim("ProfileID", publicSession.ProfileID.ToString()),
            };

            var identity = new ClaimsIdentity(claims, this.Scheme.Name);

            var principal = new ClaimsPrincipal(identity);

            var ticket = new AuthenticationTicket(principal, this.Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

    public static class CustomTokenAuthenticationExtensions
    {
        public static AuthenticationBuilder AddCustomTokenAuthentication(this IServiceCollection services)
        {
            return services.AddAuthentication(CustomTokenAuthenticationHandler.SchemeName)
                           .AddScheme<AuthenticationSchemeOptions, CustomTokenAuthenticationHandler>(
                               CustomTokenAuthenticationHandler.SchemeName, null);
        }
    }
}
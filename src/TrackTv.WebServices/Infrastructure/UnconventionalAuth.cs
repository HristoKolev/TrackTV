namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography.X509Certificates;
    using System.Threading.Tasks;

    using IdentityModel;

    using IdentityServer4.Models;
    using IdentityServer4.Services;
    using IdentityServer4.Stores;
    using IdentityServer4.Validation;

    using LinqToDB;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;

    using TrackTv.Data;

    public static class AppRoles
    {
        public const string Admin = nameof(Admin);

        public const string User = nameof(User);
    }

    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        public ResourceOwnerPasswordValidator(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        // ReSharper disable once StyleCop.SA1202
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await this.DbService.Users.FirstOrDefaultAsync(u => u.Username == context.UserName).ConfigureAwait(false);

            if (user != null)
            {
                if (user.Password == context.Password.Sha512())
                {
                    context.Result = new GrantValidationResult(user.UserID.ToString(), "custom");
                }
                else
                {
                    context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Incorrect password");
                }
            }
            else
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
            }
        }
    }

    public class IdentityServerProfileService : IProfileService
    {
        public IdentityServerProfileService(IDbService dbService)
        {
            this.DbService = dbService;
        }

        private IDbService DbService { get; }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var claim = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub") ?? new Claim(string.Empty, string.Empty);

            if (int.TryParse(claim.Value, out int userId))
            {
                // get user from db (find user by user id)
                var user = await this.DbService.Users.FirstOrDefaultAsync(poco => poco.UserID == userId).ConfigureAwait(false);

                // issue the claims for the user
                if (user != null)
                {
                    context.IssuedClaims = GetUserClaims(user).ToList();
                }
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
        }

        // build claims array from user data
        private static IEnumerable<Claim> GetUserClaims(UserPoco user)
        {
            return new[]
            {
                new Claim(JwtClaimTypes.Name, user.Username),
                new Claim(JwtClaimTypes.GivenName, user.Username),
                new Claim(JwtClaimTypes.FamilyName, user.Username),
                new Claim(JwtClaimTypes.Email, user.Username),
                new Claim("ProfileID", user.ProfileID.ToString()),
                new Claim(JwtClaimTypes.Role, user.IsAdmin ? AppRoles.Admin : AppRoles.User)
            };
        }
    }

    public class PersistentGrantStore : IPersistedGrantStore
    {
        private static readonly ConcurrentDictionary<string, PersistedGrant> PersistedGrants =
            new ConcurrentDictionary<string, PersistedGrant>();

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            return PersistedGrants.Values.Where(grant => grant.SubjectId == subjectId);
        }

        public async Task<PersistedGrant> GetAsync(string key)
        {
            return PersistedGrants.Values.FirstOrDefault(grant => grant.Key == key);
        }

        public async Task RemoveAllAsync(string subjectId, string clientId)
        {
            foreach (var persistedGrant in PersistedGrants.Values.Where(grant => grant.SubjectId == subjectId && grant.ClientId == clientId)
            )
            {
                PersistedGrants.TryRemove(persistedGrant.Key, out var p);
            }
        }

        public async Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            foreach (var persistedGrant in PersistedGrants.Values.Where(grant =>
                grant.SubjectId == subjectId && grant.ClientId == clientId
                                             && grant.Type == type))
            {
                PersistedGrants.TryRemove(persistedGrant.Key, out var p);
            }
        }

        public async Task RemoveAsync(string key)
        {
            PersistedGrants.TryRemove(key, out var p);
        }

        public async Task StoreAsync(PersistedGrant grant)
        {
            PersistedGrants.AddOrUpdate(grant.Key, grant, (key, old) => grant);
        }
    }

    public class OAuth2Config
    {
        public string ApiName { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }

    public static class UnconventionalExtensions
    {
        // ReSharper disable once StyleCop.SA1305
        public static void AddUnconventionalAuth(this IServiceCollection services, string authorityUrl)
        {
            // ReSharper disable once StyleCop.SA1305
            var oAuth2Config = new OAuth2Config
            {
                ApiName = "TrackTv",
                ClientId = "TrackTv",
                ClientSecret = "a5e87597-0109-41bf-807a-15882fbd6261"
            };

            services.AddSingleton(oAuth2Config);

            // configure identity server with in-memory stores, keys, clients and scopes
            var client = new Client
            {
                ClientId = oAuth2Config.ClientId,
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AccessTokenLifetime = (int)TimeSpan.FromDays(30).TotalSeconds,

                ClientSecrets =
                {
                    new Secret(oAuth2Config.ClientSecret.Sha256())
                },
                AllowedScopes =
                {
                    oAuth2Config.ApiName
                }
            };

            var resource = new ApiResource(oAuth2Config.ApiName, oAuth2Config.ApiName);

            services.AddMvcCore().AddAuthorization().AddJsonFormatters();

            services.AddIdentityServer()
                    .AddInMemoryApiResources(new List<ApiResource>
                    {
                        resource
                    })
                    .AddInMemoryClients(new List<Client>
                    {
                        client
                    })
                    .AddInMemoryPersistedGrants()
                    .AddProfileService<IdentityServerProfileService>()
                    .AddSigningCredential(new X509Certificate2(Path.Combine(Global.ConfigDirectory, "certificate.pfx"), string.Empty));

            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = authorityUrl;
                        options.RequireHttpsMetadata = false;

                        options.ApiName = oAuth2Config.ApiName;
                    });

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, IdentityServerProfileService>();
            services.AddTransient<IPersistedGrantStore, PersistentGrantStore>();
        }

        public static void UseUnconventionalAuth(this IApplicationBuilder app)
        {
            app.UseIdentityServer();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            app.UseAuthentication();
        }
    }
}
namespace TrackTv.WebServices.Controllers.Public
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using IdentityModel.Client;

    using IdentityServer4.Models;

    using log4net;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using Newtonsoft.Json;

    using TrackTv.Services.Profile;
    using TrackTv.WebServices.Infrastructure;

    [Route("api/public/[controller]")]
    public class AuthController : Controller
    {
        public AuthController(
            IProfileService profilesService,
            ApplicationDbContext context,
            OAuth2Config auth2Config,
            IConfiguration configuration, ILog logger)
        {
            this.ProfilesService = profilesService;
            this.DbContext = context;
            this.Auth2Config = auth2Config;
            this.Configuration = configuration;
            this.Logger = logger;
        }

        private OAuth2Config Auth2Config { get; }

        private IConfiguration Configuration { get; }

        private ILog Logger { get; }

        private ApplicationDbContext DbContext { get; }

        private IProfileService ProfilesService { get; }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            string authority = this.Configuration["Server:Urls"].Split(",").First();

            this.Logger.Debug(authority);

            var discoveryResponse = await DiscoveryClient.GetAsync(authority);

            this.Logger.Debug(JsonConvert.SerializeObject(discoveryResponse.Json, Formatting.Indented));

            var tokenClient = new TokenClient(discoveryResponse.TokenEndpoint, this.Auth2Config.ClientId, this.Auth2Config.ClientSecret);

            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(model.Username, model.Password, this.Auth2Config.ApiName);

            if (tokenResponse.IsError)
            {
                return this.Failure(tokenResponse.Error, tokenResponse.ErrorDescription);
            }

            return this.Success(tokenResponse.Json);
        }

        [HttpPost("[action]")]
        [ServiceFilter(typeof(InTransactionFilter))]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Failure(this.ModelState);
            }
          
            if (this.DbContext.Users.Any(u => u.Username == model.Username))
            {
                return this.Failure($"A user with an username '{model.Username}' already exists.");
            }

            var user = new User
            {
                Username = model.Username,
                ProfileId = await this.ProfilesService.CreateProfileAsync(model.Username),
                Password = model.Password.Sha512(),
                IsAdmin = false
            };

            // Check the user for validity
            this.DbContext.Users.Add(user);

            await this.DbContext.SaveChangesAsync();

            return this.Success();
        }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
    }
}
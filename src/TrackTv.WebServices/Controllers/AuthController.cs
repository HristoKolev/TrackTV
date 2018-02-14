﻿namespace TrackTv.WebServices.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using IdentityModel.Client;

    using IdentityServer4.Models;

    using log4net;

    using LinqToDB;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using TrackTv.Data;
    using TrackTv.Services.Profile;
    using TrackTv.WebServices.Infrastructure;

    [Route("api/public/[controller]")]
    public class AuthController : Controller
    {
        public AuthController(
            ProfileService profilesService,
            OAuth2Config auth2Config,
            IConfiguration configuration,
            ILog logger,
            IDbService dbService)
        {
            this.ProfilesService = profilesService;
            this.Auth2Config = auth2Config;
            this.Configuration = configuration;
            this.DbService = dbService;
        }

        private OAuth2Config Auth2Config { get; }

        private IConfiguration Configuration { get; }

        private IDbService DbService { get; }

        private ProfileService ProfilesService { get; }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            string authority = this.Configuration["Server:Urls"].Split(",").First();

            var discoveryResponse = await DiscoveryClient.GetAsync(authority).ConfigureAwait(false);

            var tokenClient = new TokenClient(discoveryResponse.TokenEndpoint, this.Auth2Config.ClientId, this.Auth2Config.ClientSecret);

            var tokenResponse = await tokenClient
                                      .RequestResourceOwnerPasswordAsync(model.Username, model.Password, this.Auth2Config.ApiName)
                                      .ConfigureAwait(false);

            if (tokenResponse.IsError)
            {
                return this.Failure(tokenResponse.Error, tokenResponse.ErrorDescription);
            }

            return this.Success(tokenResponse.Json);
        }

        [HttpPost("[action]")]
        [ServiceFilter(typeof(InTransactionFilter))]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Failure(this.ModelState);
            }

            if (await this.DbService.Users.AnyAsync(u => u.Username == model.Username).ConfigureAwait(false))
            {
                return this.Failure($"A user with an username '{model.Username}' already exists.");
            }

            // Check the user for validity

            await this.DbService.Insert(new UserPoco
                      {
                          Username = model.Username,
                          ProfileId = await this.ProfilesService.CreateProfileAsync(model.Username).ConfigureAwait(false),
                          Password = model.Password.Sha512(),
                          IsAdmin = false
                      })
                      .ConfigureAwait(false);

            return this.Success();
        }
    }

    public class RegisterViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Username { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Username { get; set; }
    }
}
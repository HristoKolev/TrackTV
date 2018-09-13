namespace TrackTv.WebServices.Controllers
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using log4net;

    using LinqToDB;

    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Data;
    using TrackTv.Services;
    using TrackTv.WebServices.Infrastructure;

    [Route("api/public/[controller]")]
    public class AuthController : Controller
    {
        public AuthController(ProfileService profilesService, ILog logger, IDbService dbService, SessionService sessionService)
        {
            this.ProfilesService = profilesService;

            this.DbService = dbService;
            this.SessionService = sessionService;
        }

        private IDbService DbService { get; }

        private ProfileService ProfilesService { get; }

        private SessionService SessionService { get; }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.Failure(this.ModelState);
            }

            string hashedPassword = this.SessionService.HashPassword(model.Password);

            var user = await this.DbService.Users.Where(x => x.Username == model.Username && x.Password == hashedPassword)
                                 .FirstOrDefaultAsync()
                                 .ConfigureAwait(false);

            if (user == null)
            {
                return this.Failure("Wrong username and/or passowrd.");
            }

            var publicSession = new PublicSessionModel
            {
                ProfileID = user.ProfileID
            };

            string token = this.SessionService.SignSession(publicSession);

            return this.Success(new
            {
                token
            });
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

            string hashedPassword = this.SessionService.HashPassword(model.Password);

            // Check the user for validity

            await this.DbService.Insert(new UserPoco
                      {
                          Username = model.Username,
                          ProfileID = await this.ProfilesService.CreateProfileAsync(model.Username).ConfigureAwait(false),
                          Password = hashedPassword,
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
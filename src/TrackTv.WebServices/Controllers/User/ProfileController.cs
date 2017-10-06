namespace TrackTv.WebServices.Controllers.User
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Profile;
    using TrackTv.WebServices.Infrastructure;

    [Authorize]
    [Route("api/user/[controller]")]
    public class ProfileController : Controller
    {
        public ProfileController(IProfileService profileService)
        {
            this.ProfileService = profileService;
        }

        private IProfileService ProfileService { get; }

        public async Task<IActionResult> Get()
        {
            return this.Success(await this.ProfileService.GetProfileAsync(this.User.GetProfileId()).ConfigureAwait(false));
        }
    }
}
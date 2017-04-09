namespace TrackTv.WebServices.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using TrackTv.Services.Profile;
    using TrackTv.WebServices.Infrastructure;

    [Route("api/[controller]")]
    public class ProfileController : Controller
    {
        public ProfileController(IProfileService profileService)
        {
            this.ProfileService = profileService;
        }

        private IProfileService ProfileService { get; }

        public async Task<IActionResult> Get()
        {
            return this.Ok(await this.ProfileService.GetProfileAsync(this.User.GetProfileId()));
        }
    }
}
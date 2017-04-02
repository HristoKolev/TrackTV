namespace TrackTv.WebServices.Infrastructure
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    public class ApplicationUser : IdentityUser
    {
        public int ProfileId { get; set; }
    }
}
namespace TrackTv.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser
    {
        public Profile Profile { get; set; }

        [ForeignKey("Profile")]
        public int ProfileId { get; set; }
    }
}
namespace TrackTv.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Internal;

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser
    {
        public Profile Profile { get; set; }

        [ForeignKey("Profile")]
        public int ProfileId { get; set; }
    }
}
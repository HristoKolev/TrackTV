namespace TrackTV.Models
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using TrackTV.Data.Common.Models.Contracts;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        private ICollection<Show> shows;

        private ICollection<Episode> watchedEpisodes;

        public ApplicationUser()
        {
            this.watchedEpisodes = new HashSet<Episode>();
            this.shows = new HashSet<Show>();
            this.CreatedOn = DateTime.Now;
        }

        public int MinutesSpendWatching { get; set; }

        public virtual ICollection<Show> Shows
        {
            get
            {
                return this.shows;
            }

            set
            {
                this.shows = value;
            }
        }

        public virtual ICollection<Episode> WatchedEpisodes
        {
            get
            {
                return this.watchedEpisodes;
            }

            set
            {
                this.watchedEpisodes = value;
            }
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            ClaimsIdentity userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}
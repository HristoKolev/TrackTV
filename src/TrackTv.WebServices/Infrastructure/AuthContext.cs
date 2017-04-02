namespace TrackTv.WebServices.Infrastructure
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AuthContext : IdentityDbContext<ApplicationUser>
    {
        public AuthContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserRole<string>>().HasKey(role => new
            {
                role.UserId,
                role.RoleId
            });
            builder.Entity<IdentityUserToken<string>>().HasKey(token => new
            {
                token.LoginProvider,
                token.Name,
                token.UserId
            });
            builder.Entity<IdentityUserLogin<string>>().HasKey(login => new
            {
                login.LoginProvider,
                login.ProviderKey
            });

            base.OnModelCreating(builder);
        }
    }
}
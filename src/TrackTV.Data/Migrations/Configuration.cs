namespace TrackTV.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using TrackTV.Models;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        private const string Password = "123456";

        private const string RoleName = "Admin";

        private const string Username = "admin@example.com";

        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                CreateAdminRole(context);
                ApplicationUser user = CreateUser();
                AddUserToRole(user, RoleName, context);
            }
        }

        private static void AddUserToRole(IUser<string> user, string roleName, ApplicationDbContext context)
        {
            UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            {
                userManager.AddToRole(user.Id, roleName);
                context.SaveChanges();
            }
        }

        private static void CreateAdminRole(DbContext context)
        {
            RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(RoleName))
            {
                roleManager.Create(new IdentityRole(RoleName));
            }
        }

        private static ApplicationUser CreateUser()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                ApplicationUser newUser = new ApplicationUser
                {
                    UserName = Username,
                    Email = Username,
                    DeletedOn = DateTime.Now
                };

                UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                userManager.Create(newUser, Password);

                context.SaveChanges();

                return newUser;
            }
        }
    }
}
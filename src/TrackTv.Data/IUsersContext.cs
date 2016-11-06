namespace TrackTv.Data
{
    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;
    using TrackTv.Models.Joint;

    public interface IUsersContext
    {
        DbSet<ShowsUsers> ShowsUsers { get; set; }

        DbSet<User> Users { get; set; }
    }
}
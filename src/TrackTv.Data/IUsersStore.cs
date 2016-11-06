namespace TrackTv.Data
{
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;
    using TrackTv.Models.Joint;

    public interface IUsersStore
    {
        DbSet<ShowsUsers> ShowsUsers { get; }

        DbSet<User> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
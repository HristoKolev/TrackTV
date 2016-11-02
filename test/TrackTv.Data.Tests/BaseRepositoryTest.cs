namespace TrackTv.Data.Tests
{
    using Microsoft.EntityFrameworkCore;

    using TrackTV.Data;

    public class BaseRepositoryTest
    {
        protected static TrackTvDbContext CreateContext(string databaseName)
        {
            var builder = new DbContextOptionsBuilder<TrackTvDbContext>();
            builder.UseInMemoryDatabase(databaseName);

            var context = new TrackTvDbContext(builder.Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
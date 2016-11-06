namespace TrackTv.DataRetrieval.Data.Tests
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using TrackTv.Data;

    public class BaseRepositoryTest
    {
        protected static TrackTvDbContext CreateContext()
        {
            var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<TrackTvDbContext>();
            builder.UseInMemoryDatabase().UseInternalServiceProvider(serviceProvider);

            var context = new TrackTvDbContext(builder.Options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }
    }
}
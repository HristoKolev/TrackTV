namespace TrackTv
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        public static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            var builder = new DbContextOptionsBuilder();

            builder.UseSqlServer(@"Server=.;Database=TrackTvDb;Trusted_Connection=True;MultipleActiveResultSets=True;");

            using (var db = new TrackTvDbContext(builder.Options))
            {
                await db.Database.MigrateAsync();

                Console.WriteLine(await db.Shows.AnyAsync());
            }
        }
    }
}
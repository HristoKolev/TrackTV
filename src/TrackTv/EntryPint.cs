namespace TrackTv
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    public class EntryPint
    {
        public static void Main(string[] args) => new EntryPint().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            using (var db = new TrackTvDbContext())
            {
                Console.WriteLine(await db.Shows.AnyAsync());
            }
        }
    }
}
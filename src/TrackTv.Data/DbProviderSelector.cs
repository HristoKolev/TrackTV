namespace TrackTv.Data
{
    using Microsoft.EntityFrameworkCore;

    public static class DbProviderSelector
    {
        public static void SelectProvider(this DbContextOptionsBuilder builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }
    }
}
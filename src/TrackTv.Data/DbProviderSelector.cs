namespace TrackTv.Data
{
    using Microsoft.EntityFrameworkCore;

    public static class DbProviderSelector
    {
        public static void SelectProvider(this DbContextOptionsBuilder builder, string connectionString)
        {
            if (!builder.IsConfigured)
            {
                builder.UseMySql(connectionString, optionsBuilder => optionsBuilder.MaxBatchSize(100));
            }
        }
    }
}
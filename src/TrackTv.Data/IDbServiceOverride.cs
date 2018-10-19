namespace TrackTv.Data
{
    using Npgsql;

    public interface IDbService : IDbService<TrackTvPocos>
    {
    }

    public class DbService : DbService<TrackTvPocos>, IDbService
    {
        public DbService(NpgsqlConnection dbConnection)
            : base(dbConnection, new TrackTvMetadata())
        {
        }
    }
}
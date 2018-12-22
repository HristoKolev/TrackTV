namespace TrackTv.Data
{
    using Npgsql;
    
    using PgNet;

    public interface IDbService : IDbService<TrackTvPocos>
    {
    }

    public class DbService : DbService<TrackTvPocos>, IDbService
    {
        public DbService(NpgsqlConnection dbConnection)
            : base(dbConnection)
        {
        }
    }
}
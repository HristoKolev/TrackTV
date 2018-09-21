namespace TrackTv.Data
{
    using Npgsql;

    public interface IDbService : IDbService<DbPocos>
    {
    }

    public class DbService : DbService<DbPocos>, IDbService
    {
        public DbService(NpgsqlConnection dbConnection)
            : base(dbConnection)
        {
        }
    }
}
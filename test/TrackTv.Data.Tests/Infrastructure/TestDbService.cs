namespace TrackTv.Data.Tests.Infrastructure
{
    using Npgsql;

    public class TestDbService : DbService<TestDbPocos>
    {
        const string TestConnectionString = "Server=vm5.lan;Port=4202;Database=test;Uid=test;Pwd=test;";

        public TestDbService(NpgsqlConnection dbConnection)
            : base(dbConnection)
        {
            this.Connection = dbConnection;
        }

        public NpgsqlConnection Connection { get; }

        public static TestDbService Create()
        {
            var builder = new NpgsqlConnectionStringBuilder(TestConnectionString)
            {
                Enlist = false,
            };

            var connection = new NpgsqlConnection(builder.ToString());

            return new TestDbService(connection);
        }
    }
}
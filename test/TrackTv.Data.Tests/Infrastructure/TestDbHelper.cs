namespace TrackTv.Data.Tests.Infrastructure
{
    using System.IO;
    using System.Threading.Tasks;

    public class TestDbHelper
    {
        private TestDbService Db { get; }

        public TestDbHelper(TestDbService db)
        {
            this.Db = db;
        }

        public async Task ExecuteFile(string filePath)
        {
            string contents = await File.ReadAllTextAsync(filePath);

            await this.Db.ExecuteNonQuery(contents);
        }
    }
}
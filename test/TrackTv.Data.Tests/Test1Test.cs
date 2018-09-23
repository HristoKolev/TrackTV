namespace TrackTv.Data.Tests
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using TrackTv.Data.Tests.Infrastructure;

    using Xunit;

    public class Test1Test : DatabaseTest
    {
        [Theory]
        [ClassData(typeof(GeneratedData<Test1Poco>))]
        public async Task Crud(Test1Poco poco)
        {
            Assert.True(poco.TestID == default);

            int id = await this.Db.Save(poco);

            Assert.True(id > 0);

            await this.Db.UpdateChangesOnly(poco);

            int updatedId = await this.Db.Save(poco);

            Assert.Equal(id, updatedId);

            await this.Db.Delete(poco);
        }

        [Theory]
        [ClassData(typeof(GeneratedBulkData<Test1Poco>))]
        public async Task BulkInsert(List<Test1Poco> poco)
        {
            await this.Db.BulkInsert(poco);
        }
    }
}
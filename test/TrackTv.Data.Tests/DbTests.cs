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
            int id = await this.Db.Save(poco);

            var readFromDb = await this.Db.FindByID<Test1Poco>(id);

            foreach (var getter in TestDbPocos.Test1PocoMetadata.Getters.Values)
            {
                Assert.Equal(getter(poco), getter(readFromDb));
            }

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

		[Theory]
        [ClassData(typeof(GeneratedFilterData<Test1Poco, Test1FM>))]
        public async Task Filter(Test1FM filter)
        {
            await this.Db.FilterInternal<Test1Poco, Test1CM>(filter);
        }
    }

}
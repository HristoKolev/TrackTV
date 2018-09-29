namespace TrackTv.Data.Tests
{
	using System;
    using System.Collections.Generic;
	using System.Linq;
    using System.Threading.Tasks;	

    using LinqToDB;

    using TrackTv.Data.Tests.Infrastructure;

    using Xunit;

    public class Test1Test : DatabaseTest
    {
        [Theory]
        [ClassData(typeof(GeneratedData<Test1Poco>))]
        public async Task Crud(Test1Poco poco)
        {
            int id = await this.Db.Insert(poco);

            var readFromDb = await this.Db.FindByID<Test1Poco>(id);

            foreach (var getter in TestDbPocos.Test1PocoMetadata.Getters.Values)
            {
                Assert.Equal(getter(poco), getter(readFromDb));
            }

            int updatedId = await this.Db.Update(poco);

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
        public async Task FilterSql(Test1FM filter)
        {
            await this.Db.FilterInternal<Test1Poco, Test1CM>(filter);
        }

		[Theory]
        [ClassData(typeof(GeneratedFilterData<Test1Poco, Test1FM>))]
        public async Task FilterQueryable(Test1FM filter)
        {
            await this.Db.GetTable<Test1Poco>().Filter(filter).ToArrayAsync();
        }

		[Theory]
        [ClassData(typeof(GeneratedData<Test1Poco>))]
        public void Getters(Test1Poco poco)
        {
            var getters = TestDbPocos.Test1PocoMetadata.Getters;

			Assert.Equal(poco.TestID, getters["test_id"](poco));
			Assert.Equal(poco.TestName1, getters["test_name1"](poco));
			Assert.Equal(poco.TestName2, getters["test_name2"](poco));
			Assert.Equal(poco.TestDate1, getters["test_date1"](poco));
			Assert.Equal(poco.TestDate2, getters["test_date2"](poco));
			Assert.Equal(poco.TestTimestamp1, getters["test_timestamp1"](poco));
			Assert.Equal(poco.TestTimestamp2, getters["test_timestamp2"](poco));
			Assert.Equal(poco.TestBoolean1, getters["test_boolean1"](poco));
			Assert.Equal(poco.TestBoolean2, getters["test_boolean2"](poco));
			Assert.Equal(poco.TestInteger1, getters["test_integer1"](poco));
			Assert.Equal(poco.TestInteger2, getters["test_integer2"](poco));
			Assert.Equal(poco.TestBigint1, getters["test_bigint1"](poco));
			Assert.Equal(poco.TestBigint2, getters["test_bigint2"](poco));
			Assert.Equal(poco.TestText1, getters["test_text1"](poco));
			Assert.Equal(poco.TestText2, getters["test_text2"](poco));
			Assert.Equal(poco.TestReal1, getters["test_real1"](poco));
			Assert.Equal(poco.TestReal2, getters["test_real2"](poco));
			Assert.Equal(poco.TestDouble1, getters["test_double1"](poco));
			Assert.Equal(poco.TestDouble2, getters["test_double2"](poco));
			Assert.Equal(poco.TestChar1, getters["test_char1"](poco));
			Assert.Equal(poco.TestChar2, getters["test_char2"](poco));
        }

		[Theory]
        [ClassData(typeof(GeneratedData<Test1Poco>))]
        public void Clone(Test1Poco poco)
        {
            var clone = TestDbPocos.Test1PocoMetadata.Clone;

			var newObj = clone(poco);

			Assert.NotEqual(poco, newObj);

			Assert.Equal(poco.TestID, newObj.TestID);
			Assert.Equal(poco.TestName1, newObj.TestName1);
			Assert.Equal(poco.TestName2, newObj.TestName2);
			Assert.Equal(poco.TestDate1, newObj.TestDate1);
			Assert.Equal(poco.TestDate2, newObj.TestDate2);
			Assert.Equal(poco.TestTimestamp1, newObj.TestTimestamp1);
			Assert.Equal(poco.TestTimestamp2, newObj.TestTimestamp2);
			Assert.Equal(poco.TestBoolean1, newObj.TestBoolean1);
			Assert.Equal(poco.TestBoolean2, newObj.TestBoolean2);
			Assert.Equal(poco.TestInteger1, newObj.TestInteger1);
			Assert.Equal(poco.TestInteger2, newObj.TestInteger2);
			Assert.Equal(poco.TestBigint1, newObj.TestBigint1);
			Assert.Equal(poco.TestBigint2, newObj.TestBigint2);
			Assert.Equal(poco.TestText1, newObj.TestText1);
			Assert.Equal(poco.TestText2, newObj.TestText2);
			Assert.Equal(poco.TestReal1, newObj.TestReal1);
			Assert.Equal(poco.TestReal2, newObj.TestReal2);
			Assert.Equal(poco.TestDouble1, newObj.TestDouble1);
			Assert.Equal(poco.TestDouble2, newObj.TestDouble2);
			Assert.Equal(poco.TestChar1, newObj.TestChar1);
			Assert.Equal(poco.TestChar2, newObj.TestChar2);
        }

		[Theory]
        [ClassData(typeof(GeneratedData<Test1Poco>))]
		// ReSharper disable once CyclomaticComplexity
        public void GenerateParameters(Test1Poco poco)
        {
            var getParameters = TestDbPocos.Test1PocoMetadata.GenerateParameters;

			var parameters = getParameters(poco);

            var columns = TestDbPocos.Test1PocoMetadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();
            var getters = TestDbPocos.Test1PocoMetadata.Getters;

            for (int i = 0; i < columns.Length; i++)
            {
                var column = columns[i];
                var getter = getters[column.ColumnName];

                var parameter = parameters[i];

                Assert.Equal(getter(poco), parameter.Value == DBNull.Value ? null : parameter.Value);
            }
        }		
    }

}
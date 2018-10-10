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
        public void Setters(Test1Poco poco)
        {
            var setters = TestDbPocos.Test1PocoMetadata.Setters;

			var newObj = new Test1Poco();

			setters["test_id"](newObj, poco.TestID);
			Assert.Equal(poco.TestID, newObj.TestID);

			setters["test_name1"](newObj, poco.TestName1);
			Assert.Equal(poco.TestName1, newObj.TestName1);

			setters["test_name2"](newObj, poco.TestName2);
			Assert.Equal(poco.TestName2, newObj.TestName2);

			setters["test_date1"](newObj, poco.TestDate1);
			Assert.Equal(poco.TestDate1, newObj.TestDate1);

			setters["test_date2"](newObj, poco.TestDate2);
			Assert.Equal(poco.TestDate2, newObj.TestDate2);

			setters["test_timestamp1"](newObj, poco.TestTimestamp1);
			Assert.Equal(poco.TestTimestamp1, newObj.TestTimestamp1);

			setters["test_timestamp2"](newObj, poco.TestTimestamp2);
			Assert.Equal(poco.TestTimestamp2, newObj.TestTimestamp2);

			setters["test_boolean1"](newObj, poco.TestBoolean1);
			Assert.Equal(poco.TestBoolean1, newObj.TestBoolean1);

			setters["test_boolean2"](newObj, poco.TestBoolean2);
			Assert.Equal(poco.TestBoolean2, newObj.TestBoolean2);

			setters["test_integer1"](newObj, poco.TestInteger1);
			Assert.Equal(poco.TestInteger1, newObj.TestInteger1);

			setters["test_integer2"](newObj, poco.TestInteger2);
			Assert.Equal(poco.TestInteger2, newObj.TestInteger2);

			setters["test_bigint1"](newObj, poco.TestBigint1);
			Assert.Equal(poco.TestBigint1, newObj.TestBigint1);

			setters["test_bigint2"](newObj, poco.TestBigint2);
			Assert.Equal(poco.TestBigint2, newObj.TestBigint2);

			setters["test_text1"](newObj, poco.TestText1);
			Assert.Equal(poco.TestText1, newObj.TestText1);

			setters["test_text2"](newObj, poco.TestText2);
			Assert.Equal(poco.TestText2, newObj.TestText2);

			setters["test_real1"](newObj, poco.TestReal1);
			Assert.Equal(poco.TestReal1, newObj.TestReal1);

			setters["test_real2"](newObj, poco.TestReal2);
			Assert.Equal(poco.TestReal2, newObj.TestReal2);

			setters["test_double1"](newObj, poco.TestDouble1);
			Assert.Equal(poco.TestDouble1, newObj.TestDouble1);

			setters["test_double2"](newObj, poco.TestDouble2);
			Assert.Equal(poco.TestDouble2, newObj.TestDouble2);

			setters["test_char1"](newObj, poco.TestChar1);
			Assert.Equal(poco.TestChar1, newObj.TestChar1);

			setters["test_char2"](newObj, poco.TestChar2);
			Assert.Equal(poco.TestChar2, newObj.TestChar2);

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

				Assert.Equal(getter(poco) ?? DBNull.Value, parameter.Value);
            }
        }

		[Theory]
        [ClassData(typeof(GeneratedData<Test1Poco>))]
        // ReSharper disable once CyclomaticComplexity
        public void GetAllColumns(Test1Poco poco)
        {
            var getAllColumns = TestDbPocos.Test1PocoMetadata.GetAllColumns;

            var (columnNames, parameters) = getAllColumns(poco);

            var columns = TestDbPocos.Test1PocoMetadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();
            var getters = TestDbPocos.Test1PocoMetadata.Getters;

            for (int i = 0; i < columns.Length; i++)
            {
                var column = columns[i];
                var getter = getters[column.ColumnName];
                var parameter = parameters[i];

                Assert.Equal(columnNames[i], column.ColumnName);

                Assert.Equal(getter(poco) ?? DBNull.Value, parameter.Value);
            }
        }

		[Theory]
        [ClassData(typeof(GeneratedBulkData<Test1Poco>))]
        public void GetColumnChanges(List<Test1Poco> pocos)
        {
            var getColumnChanges = TestDbPocos.Test1PocoMetadata.GetColumnChanges;

            var columns = TestDbPocos.Test1PocoMetadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();
            var getters = TestDbPocos.Test1PocoMetadata.Getters;

            var allColumnNames = new HashSet<string>(columns.Select(x => x.ColumnName));

            foreach (var (instance1, instance2) in pocos.Zip(Enumerable.Reverse(pocos), (x, y) => (x, y)))
            {
                var (columnNames, parameters) = getColumnChanges(instance1, instance2);

                Assert.Equal(parameters.Count, columnNames.Count);
                Assert.True(columnNames.Count <= columns.Length);

                foreach (string columnName in columnNames)
                {
                    Assert.Contains(columnName, allColumnNames);
                }

                foreach (var column in columns)
                {
                    var getter = getters[column.ColumnName];

                    var value1 = getter(instance1);
                    var value2 = getter(instance2);

                    if (this.StupidEquals(value1, value2))
                    {
                        Assert.DoesNotContain(column.ColumnName, columnNames);
                    }
                    else
                    {
                        Assert.Contains(column.ColumnName, columnNames);
                        int index = columnNames.IndexOf(column.ColumnName);
                        var parameter = parameters[index];

                        Assert.Equal(column.NpgsDataType, parameter.NpgsqlDbType);

                        Assert.Equal(value2 ?? DBNull.Value, parameter.Value);
                    }
                }
            }
        }
    }

    public class Test2Test : DatabaseTest
    {
        [Theory]
        [ClassData(typeof(GeneratedData<Test2Poco>))]
        public async Task Crud(Test2Poco poco)
        {
            int id = await this.Db.Insert(poco);

            var readFromDb = await this.Db.FindByID<Test2Poco>(id);

            foreach (var getter in TestDbPocos.Test2PocoMetadata.Getters.Values)
            {
                Assert.Equal(getter(poco), getter(readFromDb));
            }

            int updatedId = await this.Db.Update(poco);

            Assert.Equal(id, updatedId);

            await this.Db.Delete(poco);
        }

        [Theory]
        [ClassData(typeof(GeneratedBulkData<Test2Poco>))]
        public async Task BulkInsert(List<Test2Poco> poco)
        {
            await this.Db.BulkInsert(poco);
        }

		[Theory]
        [ClassData(typeof(GeneratedFilterData<Test2Poco, Test2FM>))]
        public async Task FilterSql(Test2FM filter)
        {
            await this.Db.FilterInternal<Test2Poco, Test2CM>(filter);
        }

		[Theory]
        [ClassData(typeof(GeneratedFilterData<Test2Poco, Test2FM>))]
        public async Task FilterQueryable(Test2FM filter)
        {
            await this.Db.GetTable<Test2Poco>().Filter(filter).ToArrayAsync();
        }

		[Theory]
        [ClassData(typeof(GeneratedData<Test2Poco>))]
        public void Getters(Test2Poco poco)
        {
            var getters = TestDbPocos.Test2PocoMetadata.Getters;

			Assert.Equal(poco.TestID, getters["test_id"](poco));
			Assert.Equal(poco.TestName, getters["test_name"](poco));
			Assert.Equal(poco.TestDate, getters["test_date"](poco));
        }

		[Theory]
        [ClassData(typeof(GeneratedData<Test2Poco>))]
        public void Setters(Test2Poco poco)
        {
            var setters = TestDbPocos.Test2PocoMetadata.Setters;

			var newObj = new Test2Poco();

			setters["test_id"](newObj, poco.TestID);
			Assert.Equal(poco.TestID, newObj.TestID);

			setters["test_name"](newObj, poco.TestName);
			Assert.Equal(poco.TestName, newObj.TestName);

			setters["test_date"](newObj, poco.TestDate);
			Assert.Equal(poco.TestDate, newObj.TestDate);

        }

		[Theory]
        [ClassData(typeof(GeneratedData<Test2Poco>))]
        public void Clone(Test2Poco poco)
        {
            var clone = TestDbPocos.Test2PocoMetadata.Clone;

			var newObj = clone(poco);

			Assert.NotEqual(poco, newObj);

			Assert.Equal(poco.TestID, newObj.TestID);
			Assert.Equal(poco.TestName, newObj.TestName);
			Assert.Equal(poco.TestDate, newObj.TestDate);
        }

		[Theory]
        [ClassData(typeof(GeneratedData<Test2Poco>))]
		// ReSharper disable once CyclomaticComplexity
        public void GenerateParameters(Test2Poco poco)
        {
            var getParameters = TestDbPocos.Test2PocoMetadata.GenerateParameters;

			var parameters = getParameters(poco);

            var columns = TestDbPocos.Test2PocoMetadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();
            var getters = TestDbPocos.Test2PocoMetadata.Getters;

            for (int i = 0; i < columns.Length; i++)
            {
                var column = columns[i];
                var getter = getters[column.ColumnName];

                var parameter = parameters[i];

				Assert.Equal(getter(poco) ?? DBNull.Value, parameter.Value);
            }
        }

		[Theory]
        [ClassData(typeof(GeneratedData<Test2Poco>))]
        // ReSharper disable once CyclomaticComplexity
        public void GetAllColumns(Test2Poco poco)
        {
            var getAllColumns = TestDbPocos.Test2PocoMetadata.GetAllColumns;

            var (columnNames, parameters) = getAllColumns(poco);

            var columns = TestDbPocos.Test2PocoMetadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();
            var getters = TestDbPocos.Test2PocoMetadata.Getters;

            for (int i = 0; i < columns.Length; i++)
            {
                var column = columns[i];
                var getter = getters[column.ColumnName];
                var parameter = parameters[i];

                Assert.Equal(columnNames[i], column.ColumnName);

                Assert.Equal(getter(poco) ?? DBNull.Value, parameter.Value);
            }
        }

		[Theory]
        [ClassData(typeof(GeneratedBulkData<Test2Poco>))]
        public void GetColumnChanges(List<Test2Poco> pocos)
        {
            var getColumnChanges = TestDbPocos.Test2PocoMetadata.GetColumnChanges;

            var columns = TestDbPocos.Test2PocoMetadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();
            var getters = TestDbPocos.Test2PocoMetadata.Getters;

            var allColumnNames = new HashSet<string>(columns.Select(x => x.ColumnName));

            foreach (var (instance1, instance2) in pocos.Zip(Enumerable.Reverse(pocos), (x, y) => (x, y)))
            {
                var (columnNames, parameters) = getColumnChanges(instance1, instance2);

                Assert.Equal(parameters.Count, columnNames.Count);
                Assert.True(columnNames.Count <= columns.Length);

                foreach (string columnName in columnNames)
                {
                    Assert.Contains(columnName, allColumnNames);
                }

                foreach (var column in columns)
                {
                    var getter = getters[column.ColumnName];

                    var value1 = getter(instance1);
                    var value2 = getter(instance2);

                    if (this.StupidEquals(value1, value2))
                    {
                        Assert.DoesNotContain(column.ColumnName, columnNames);
                    }
                    else
                    {
                        Assert.Contains(column.ColumnName, columnNames);
                        int index = columnNames.IndexOf(column.ColumnName);
                        var parameter = parameters[index];

                        Assert.Equal(column.NpgsDataType, parameter.NpgsqlDbType);

                        Assert.Equal(value2 ?? DBNull.Value, parameter.Value);
                    }
                }
            }
        }
    }

}
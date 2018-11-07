namespace TrackTv.Data.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Threading.Tasks;	

	using LinqToDB;

	using Npgsql;

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

			Assert.Equal(poco.TestID, readFromDb.TestID);
			Assert.Equal(poco.TestName1, readFromDb.TestName1);
			Assert.Equal(poco.TestName2, readFromDb.TestName2);
			Assert.Equal(poco.TestDate1, readFromDb.TestDate1);
			Assert.Equal(poco.TestDate2, readFromDb.TestDate2);
			Assert.Equal(poco.TestTimestamp1, readFromDb.TestTimestamp1);
			Assert.Equal(poco.TestTimestamp2, readFromDb.TestTimestamp2);
			Assert.Equal(poco.TestBoolean1, readFromDb.TestBoolean1);
			Assert.Equal(poco.TestBoolean2, readFromDb.TestBoolean2);
			Assert.Equal(poco.TestInteger1, readFromDb.TestInteger1);
			Assert.Equal(poco.TestInteger2, readFromDb.TestInteger2);
			Assert.Equal(poco.TestBigint1, readFromDb.TestBigint1);
			Assert.Equal(poco.TestBigint2, readFromDb.TestBigint2);
			Assert.Equal(poco.TestText1, readFromDb.TestText1);
			Assert.Equal(poco.TestText2, readFromDb.TestText2);
			Assert.Equal(poco.TestReal1, readFromDb.TestReal1);
			Assert.Equal(poco.TestReal2, readFromDb.TestReal2);
			Assert.Equal(poco.TestDouble1, readFromDb.TestDouble1);
			Assert.Equal(poco.TestDouble2, readFromDb.TestDouble2);
			Assert.Equal(poco.TestChar1, readFromDb.TestChar1);
			Assert.Equal(poco.TestChar2, readFromDb.TestChar2);

			int updatedId = await this.Db.Update(poco);

			Assert.Equal(id, updatedId);

			await this.Db.Delete(poco);
		}

		[Theory]
		[ClassData(typeof(GeneratedData<Test1Poco>))]
		public async Task CrudNonPocoRead(Test1Poco poco)
		{
			int id = await this.Db.Insert(poco);

			var readFromDb = await this.Db.QueryOne<Test1BM>("select * from test1 where test_id = @pk;", new NpgsqlParameter("pk", id));

			Assert.Equal(poco.TestID, readFromDb.TestID);
			Assert.Equal(poco.TestName1, readFromDb.TestName1);
			Assert.Equal(poco.TestName2, readFromDb.TestName2);
			Assert.Equal(poco.TestDate1, readFromDb.TestDate1);
			Assert.Equal(poco.TestDate2, readFromDb.TestDate2);
			Assert.Equal(poco.TestTimestamp1, readFromDb.TestTimestamp1);
			Assert.Equal(poco.TestTimestamp2, readFromDb.TestTimestamp2);
			Assert.Equal(poco.TestBoolean1, readFromDb.TestBoolean1);
			Assert.Equal(poco.TestBoolean2, readFromDb.TestBoolean2);
			Assert.Equal(poco.TestInteger1, readFromDb.TestInteger1);
			Assert.Equal(poco.TestInteger2, readFromDb.TestInteger2);
			Assert.Equal(poco.TestBigint1, readFromDb.TestBigint1);
			Assert.Equal(poco.TestBigint2, readFromDb.TestBigint2);
			Assert.Equal(poco.TestText1, readFromDb.TestText1);
			Assert.Equal(poco.TestText2, readFromDb.TestText2);
			Assert.Equal(poco.TestReal1, readFromDb.TestReal1);
			Assert.Equal(poco.TestReal2, readFromDb.TestReal2);
			Assert.Equal(poco.TestDouble1, readFromDb.TestDouble1);
			Assert.Equal(poco.TestDouble2, readFromDb.TestDouble2);
			Assert.Equal(poco.TestChar1, readFromDb.TestChar1);
			Assert.Equal(poco.TestChar2, readFromDb.TestChar2);

			int updatedId = await this.Db.Update(poco);

			Assert.Equal(id, updatedId);

			await this.Db.Delete(poco);
		}

		[Theory]
		[ClassData(typeof(GeneratedData<Test1Poco>))]
		public async Task SelectCm(Test1Poco poco)
		{
			await this.Db.Insert(poco);

			var pocoFromDb = await this.Db.Poco.Test1.FirstAsync();
			var cmFromDb = await this.Db.Poco.Test1.SelectCm<Test1Poco, Test1CM>().FirstAsync();

			Assert.Equal(pocoFromDb.TestID, cmFromDb.TestID);
			Assert.Equal(pocoFromDb.TestName1, cmFromDb.TestName1);
			Assert.Equal(pocoFromDb.TestName2, cmFromDb.TestName2);
			Assert.Equal(pocoFromDb.TestDate1, cmFromDb.TestDate1);
			Assert.Equal(pocoFromDb.TestDate2, cmFromDb.TestDate2);
			Assert.Equal(pocoFromDb.TestTimestamp1, cmFromDb.TestTimestamp1);
			Assert.Equal(pocoFromDb.TestTimestamp2, cmFromDb.TestTimestamp2);
			Assert.Equal(pocoFromDb.TestBoolean1, cmFromDb.TestBoolean1);
			Assert.Equal(pocoFromDb.TestBoolean2, cmFromDb.TestBoolean2);
			Assert.Equal(pocoFromDb.TestInteger1, cmFromDb.TestInteger1);
			Assert.Equal(pocoFromDb.TestInteger2, cmFromDb.TestInteger2);
			Assert.Equal(pocoFromDb.TestBigint1, cmFromDb.TestBigint1);
			Assert.Equal(pocoFromDb.TestBigint2, cmFromDb.TestBigint2);
			Assert.Equal(pocoFromDb.TestText1, cmFromDb.TestText1);
			Assert.Equal(pocoFromDb.TestText2, cmFromDb.TestText2);
			Assert.Equal(pocoFromDb.TestReal1, cmFromDb.TestReal1);
			Assert.Equal(pocoFromDb.TestReal2, cmFromDb.TestReal2);
			Assert.Equal(pocoFromDb.TestDouble1, cmFromDb.TestDouble1);
			Assert.Equal(pocoFromDb.TestDouble2, cmFromDb.TestDouble2);
			Assert.Equal(pocoFromDb.TestChar1, cmFromDb.TestChar1);
			Assert.Equal(pocoFromDb.TestChar2, cmFromDb.TestChar2);
		}

		[Theory]
		[ClassData(typeof(GeneratedBulkData<Test1Poco>))]
		public async Task BulkInsert(List<Test1Poco> poco)
		{
			await this.Db.BulkInsert(poco);
		}

		[Fact]
		public async Task OrderByPrimaryKey()
		{
			await this.Db.GetTable<Test1Poco>().OrderByPrimaryKey().ToArrayAsync();
		}

		[Fact]
		public async Task OrderByPrimaryKeyDescending()
		{
			await this.Db.GetTable<Test1Poco>().OrderByPrimaryKeyDescending().ToArrayAsync();
		}

		[Theory]
		[ClassData(typeof(GeneratedBulkData<Test1Poco>))]
		public void GetColumnChanges(List<Test1Poco> pocos)
		{
			var getColumnChanges = TestDbMetadata.Test1PocoMetadata.GetColumnChanges;

			var columns = TestDbMetadata.Test1PocoMetadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();
			var getters = DbCodeGenerator.GenerateGetters<Test1Poco>();

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

		[Theory]
		[ClassData(typeof(GeneratedData<Test1Poco>))]
		// ReSharper disable once CyclomaticComplexity
		public void GenerateParameters(Test1Poco poco)
		{
			var getParameters = TestDbMetadata.Test1PocoMetadata.GenerateParameters;

			var parameters = getParameters(poco);

			var columns = TestDbMetadata.Test1PocoMetadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();
			var getters = DbCodeGenerator.GenerateGetters<Test1Poco>();

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
			var getAllColumns = TestDbMetadata.Test1PocoMetadata.GetAllColumns;

			var (columnNames, parameters) = getAllColumns(poco);

			var columns = TestDbMetadata.Test1PocoMetadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();
			var getters = DbCodeGenerator.GenerateGetters<Test1Poco>();

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
		[ClassData(typeof(GeneratedFilterData<Test1Poco, Test1FM>))]
		public async Task FilterSql(Test1FM filter)
		{
			await this.Db.FilterInternal<Test1Poco, Test1CM>(filter);
		}

		[Fact]
		public async Task SelectCmDry()
		{
			await this.Db.Poco.Test1.ToArrayAsync();
			await this.Db.Poco.Test1.SelectCm<Test1Poco, Test1CM>().ToArrayAsync();
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
			var getters = DbCodeGenerator.GenerateGetters<Test1Poco>();

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
			var setters = DbCodeGenerator.GenerateSetters<Test1Poco>();

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
			var clone = TestDbMetadata.Test1PocoMetadata.Clone;

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
		[ClassData(typeof(GeneratedFilterData<Test1Poco, Test1FM>))]
		public void ParseFm(Test1FM filter)
		{
			var properties = typeof(Test1FM).GetProperties().Where(x => x.GetValue(filter) != null);
			var attributes = properties.Select(x => x.GetCustomAttribute<FilterOperatorAttribute>()).ToList();

			var expectedNames = attributes.Select(x => x.ColumnName).ToList();
			var expectedOperators = attributes.Select(x => x.QueryOperatorType).ToList();
			var expectedValues = properties.Select(x =>
				{
					var attr = x.GetCustomAttribute<FilterOperatorAttribute>();

					if (attr.QueryOperatorType    == QueryOperatorType.IsNull
						|| attr.QueryOperatorType == QueryOperatorType.IsNotNull)
					{
						return null;
					}

					return x.GetValue(filter);
				}).ToList();

			var (columnNames, columnParameters, operators) = TestDbMetadata.Test1PocoMetadata.ParseFm(filter);

			Assert.Equal(expectedNames, columnNames);
			Assert.Equal(expectedOperators, operators);
			Assert.Equal(expectedValues, columnParameters.Select(x => x?.Value).ToList());
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

			Assert.Equal(poco.TestID, readFromDb.TestID);
			Assert.Equal(poco.TestName, readFromDb.TestName);
			Assert.Equal(poco.TestDate, readFromDb.TestDate);

			int updatedId = await this.Db.Update(poco);

			Assert.Equal(id, updatedId);

			await this.Db.Delete(poco);
		}

		[Theory]
		[ClassData(typeof(GeneratedData<Test2Poco>))]
		public async Task CrudNonPocoRead(Test2Poco poco)
		{
			int id = await this.Db.Insert(poco);

			var readFromDb = await this.Db.QueryOne<Test2BM>("select * from test2 where test_id = @pk;", new NpgsqlParameter("pk", id));

			Assert.Equal(poco.TestID, readFromDb.TestID);
			Assert.Equal(poco.TestName, readFromDb.TestName);
			Assert.Equal(poco.TestDate, readFromDb.TestDate);

			int updatedId = await this.Db.Update(poco);

			Assert.Equal(id, updatedId);

			await this.Db.Delete(poco);
		}

		[Theory]
		[ClassData(typeof(GeneratedData<Test2Poco>))]
		public async Task SelectCm(Test2Poco poco)
		{
			await this.Db.Insert(poco);

			var pocoFromDb = await this.Db.Poco.Test2.FirstAsync();
			var cmFromDb = await this.Db.Poco.Test2.SelectCm<Test2Poco, Test2CM>().FirstAsync();

			Assert.Equal(pocoFromDb.TestID, cmFromDb.TestID);
			Assert.Equal(pocoFromDb.TestName, cmFromDb.TestName);
			Assert.Equal(pocoFromDb.TestDate, cmFromDb.TestDate);
		}

		[Theory]
		[ClassData(typeof(GeneratedBulkData<Test2Poco>))]
		public async Task BulkInsert(List<Test2Poco> poco)
		{
			await this.Db.BulkInsert(poco);
		}

		[Fact]
		public async Task OrderByPrimaryKey()
		{
			await this.Db.GetTable<Test2Poco>().OrderByPrimaryKey().ToArrayAsync();
		}

		[Fact]
		public async Task OrderByPrimaryKeyDescending()
		{
			await this.Db.GetTable<Test2Poco>().OrderByPrimaryKeyDescending().ToArrayAsync();
		}

		[Theory]
		[ClassData(typeof(GeneratedBulkData<Test2Poco>))]
		public void GetColumnChanges(List<Test2Poco> pocos)
		{
			var getColumnChanges = TestDbMetadata.Test2PocoMetadata.GetColumnChanges;

			var columns = TestDbMetadata.Test2PocoMetadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();
			var getters = DbCodeGenerator.GenerateGetters<Test2Poco>();

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

		[Theory]
		[ClassData(typeof(GeneratedData<Test2Poco>))]
		// ReSharper disable once CyclomaticComplexity
		public void GenerateParameters(Test2Poco poco)
		{
			var getParameters = TestDbMetadata.Test2PocoMetadata.GenerateParameters;

			var parameters = getParameters(poco);

			var columns = TestDbMetadata.Test2PocoMetadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();
			var getters = DbCodeGenerator.GenerateGetters<Test2Poco>();

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
			var getAllColumns = TestDbMetadata.Test2PocoMetadata.GetAllColumns;

			var (columnNames, parameters) = getAllColumns(poco);

			var columns = TestDbMetadata.Test2PocoMetadata.Columns.Where(x => !x.IsPrimaryKey).ToArray();
			var getters = DbCodeGenerator.GenerateGetters<Test2Poco>();

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
		[ClassData(typeof(GeneratedFilterData<Test2Poco, Test2FM>))]
		public async Task FilterSql(Test2FM filter)
		{
			await this.Db.FilterInternal<Test2Poco, Test2CM>(filter);
		}

		[Fact]
		public async Task SelectCmDry()
		{
			await this.Db.Poco.Test2.ToArrayAsync();
			await this.Db.Poco.Test2.SelectCm<Test2Poco, Test2CM>().ToArrayAsync();
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
			var getters = DbCodeGenerator.GenerateGetters<Test2Poco>();

			Assert.Equal(poco.TestID, getters["test_id"](poco));
			Assert.Equal(poco.TestName, getters["test_name"](poco));
			Assert.Equal(poco.TestDate, getters["test_date"](poco));
		}

		[Theory]
		[ClassData(typeof(GeneratedData<Test2Poco>))]
		public void Setters(Test2Poco poco)
		{
			var setters = DbCodeGenerator.GenerateSetters<Test2Poco>();

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
			var clone = TestDbMetadata.Test2PocoMetadata.Clone;

			var newObj = clone(poco);

			Assert.NotEqual(poco, newObj);

			Assert.Equal(poco.TestID, newObj.TestID);
			Assert.Equal(poco.TestName, newObj.TestName);
			Assert.Equal(poco.TestDate, newObj.TestDate);
		}

		[Theory]
		[ClassData(typeof(GeneratedFilterData<Test2Poco, Test2FM>))]
		public void ParseFm(Test2FM filter)
		{
			var properties = typeof(Test2FM).GetProperties().Where(x => x.GetValue(filter) != null);
			var attributes = properties.Select(x => x.GetCustomAttribute<FilterOperatorAttribute>()).ToList();

			var expectedNames = attributes.Select(x => x.ColumnName).ToList();
			var expectedOperators = attributes.Select(x => x.QueryOperatorType).ToList();
			var expectedValues = properties.Select(x =>
				{
					var attr = x.GetCustomAttribute<FilterOperatorAttribute>();

					if (attr.QueryOperatorType    == QueryOperatorType.IsNull
						|| attr.QueryOperatorType == QueryOperatorType.IsNotNull)
					{
						return null;
					}

					return x.GetValue(filter);
				}).ToList();

			var (columnNames, columnParameters, operators) = TestDbMetadata.Test2PocoMetadata.ParseFm(filter);

			Assert.Equal(expectedNames, columnNames);
			Assert.Equal(expectedOperators, operators);
			Assert.Equal(expectedValues, columnParameters.Select(x => x?.Value).ToList());
		}
	}

	public class View1Test : DatabaseTest
	{
		[Theory]
		[ClassData(typeof(GeneratedFilterData<View1Poco, View1FM>))]
		public async Task FilterSql(View1FM filter)
		{
			await this.Db.FilterInternal<View1Poco, View1CM>(filter);
		}

		[Fact]
		public async Task SelectCmDry()
		{
			await this.Db.Poco.View1.ToArrayAsync();
			await this.Db.Poco.View1.SelectCm<View1Poco, View1CM>().ToArrayAsync();
		}

		[Theory]
		[ClassData(typeof(GeneratedFilterData<View1Poco, View1FM>))]
		public async Task FilterQueryable(View1FM filter)
		{
			await this.Db.GetTable<View1Poco>().Filter(filter).ToArrayAsync();
		}

		[Theory]
		[ClassData(typeof(GeneratedData<View1Poco>))]
		public void Getters(View1Poco poco)
		{
			var getters = DbCodeGenerator.GenerateGetters<View1Poco>();

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
			Assert.Equal(poco.TestName, getters["test_name"](poco));
			Assert.Equal(poco.TestDate, getters["test_date"](poco));
		}

		[Theory]
		[ClassData(typeof(GeneratedData<View1Poco>))]
		public void Setters(View1Poco poco)
		{
			var setters = DbCodeGenerator.GenerateSetters<View1Poco>();

			var newObj = new View1Poco();

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

			setters["test_name"](newObj, poco.TestName);
			Assert.Equal(poco.TestName, newObj.TestName);

			setters["test_date"](newObj, poco.TestDate);
			Assert.Equal(poco.TestDate, newObj.TestDate);

		}

		[Theory]
		[ClassData(typeof(GeneratedData<View1Poco>))]
		public void Clone(View1Poco poco)
		{
			var clone = TestDbMetadata.View1PocoMetadata.Clone;

			var newObj = clone(poco);

			Assert.NotEqual(poco, newObj);

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
			Assert.Equal(poco.TestName, newObj.TestName);
			Assert.Equal(poco.TestDate, newObj.TestDate);
		}

		[Theory]
		[ClassData(typeof(GeneratedFilterData<View1Poco, View1FM>))]
		public void ParseFm(View1FM filter)
		{
			var properties = typeof(View1FM).GetProperties().Where(x => x.GetValue(filter) != null);
			var attributes = properties.Select(x => x.GetCustomAttribute<FilterOperatorAttribute>()).ToList();

			var expectedNames = attributes.Select(x => x.ColumnName).ToList();
			var expectedOperators = attributes.Select(x => x.QueryOperatorType).ToList();
			var expectedValues = properties.Select(x =>
				{
					var attr = x.GetCustomAttribute<FilterOperatorAttribute>();

					if (attr.QueryOperatorType    == QueryOperatorType.IsNull
						|| attr.QueryOperatorType == QueryOperatorType.IsNotNull)
					{
						return null;
					}

					return x.GetValue(filter);
				}).ToList();

			var (columnNames, columnParameters, operators) = TestDbMetadata.View1PocoMetadata.ParseFm(filter);

			Assert.Equal(expectedNames, columnNames);
			Assert.Equal(expectedOperators, operators);
			Assert.Equal(expectedValues, columnParameters.Select(x => x?.Value).ToList());
		}
	}

}
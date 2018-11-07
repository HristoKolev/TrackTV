namespace TrackTv.Data.Tests
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Linq;
	using LinqToDB;
	using LinqToDB.Mapping;

	using NpgsqlTypes;
	
	/// <summary>
	/// <para>Table name: 'test1'.</para>
	/// <para>Table schema: 'public'.</para>
	/// </summary>
	[Table(Schema="public", Name = "test1")]
	public class Test1Poco : IPoco<Test1Poco>
	{
		/// <summary>
		/// <para>Column name: 'test_id'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>Primary key of table: 'test1'.</para>
		/// <para>Primary key constraint name: 'test1_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
		/// </summary>
		[PrimaryKey, Identity]
		[Column(Name = "test_id", DataType = DataType.Int32)]
		public int TestID { get; set; }

		/// <summary>
		/// <para>Column name: 'test_name1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
		/// </summary>
		[NotNull]
		[Column(Name = "test_name1", DataType = DataType.NVarChar)]
		public string TestName1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_name2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_name2", DataType = DataType.NVarChar)]
		public string TestName2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_date1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'date'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Date'.</para>
		/// <para>CLR type: 'DateTime'.</para>
		/// <para>linq2db data type: 'DataType.Date'.</para>
		/// </summary>
		[NotNull]
		[Column(Name = "test_date1", DataType = DataType.Date)]
		public DateTime TestDate1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_date2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'date'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Date'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.Date'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_date2", DataType = DataType.Date)]
		public DateTime? TestDate2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_timestamp1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
		/// </summary>
		[NotNull]
		[Column(Name = "test_timestamp1", DataType = DataType.DateTime2)]
		public DateTime TestTimestamp1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_timestamp2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_timestamp2", DataType = DataType.DateTime2)]
		public DateTime? TestTimestamp2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_boolean1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'boolean'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Boolean'.</para>
		/// <para>CLR type: 'bool'.</para>
		/// <para>linq2db data type: 'DataType.Boolean'.</para>
		/// </summary>
		[NotNull]
		[Column(Name = "test_boolean1", DataType = DataType.Boolean)]
		public bool TestBoolean1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_boolean2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'boolean'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Boolean'.</para>
		/// <para>CLR type: 'bool?'.</para>
		/// <para>linq2db data type: 'DataType.Boolean'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_boolean2", DataType = DataType.Boolean)]
		public bool? TestBoolean2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_integer1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int?'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_integer1", DataType = DataType.Int32)]
		public int? TestInteger1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_integer2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
		/// </summary>
		[NotNull]
		[Column(Name = "test_integer2", DataType = DataType.Int32)]
		public int TestInteger2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_bigint1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'bigint'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Bigint'.</para>
		/// <para>CLR type: 'long?'.</para>
		/// <para>linq2db data type: 'DataType.Int64'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_bigint1", DataType = DataType.Int64)]
		public long? TestBigint1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_bigint2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'bigint'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Bigint'.</para>
		/// <para>CLR type: 'long'.</para>
		/// <para>linq2db data type: 'DataType.Int64'.</para>
		/// </summary>
		[NotNull]
		[Column(Name = "test_bigint2", DataType = DataType.Int64)]
		public long TestBigint2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_text1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_text1", DataType = DataType.Text)]
		public string TestText1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_text2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
		/// </summary>
		[NotNull]
		[Column(Name = "test_text2", DataType = DataType.Text)]
		public string TestText2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_real1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'real'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Real'.</para>
		/// <para>CLR type: 'float?'.</para>
		/// <para>linq2db data type: 'DataType.Single'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_real1", DataType = DataType.Single)]
		public float? TestReal1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_real2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'real'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Real'.</para>
		/// <para>CLR type: 'float'.</para>
		/// <para>linq2db data type: 'DataType.Single'.</para>
		/// </summary>
		[NotNull]
		[Column(Name = "test_real2", DataType = DataType.Single)]
		public float TestReal2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_double1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'double precision'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Double'.</para>
		/// <para>CLR type: 'double?'.</para>
		/// <para>linq2db data type: 'DataType.Double'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_double1", DataType = DataType.Double)]
		public double? TestDouble1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_double2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'double precision'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Double'.</para>
		/// <para>CLR type: 'double'.</para>
		/// <para>linq2db data type: 'DataType.Double'.</para>
		/// </summary>
		[NotNull]
		[Column(Name = "test_double2", DataType = DataType.Double)]
		public double TestDouble2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_char1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Char'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NChar'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_char1", DataType = DataType.NChar)]
		public string TestChar1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_char2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Char'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NChar'.</para>
		/// </summary>
		[NotNull]
		[Column(Name = "test_char2", DataType = DataType.NChar)]
		public string TestChar2 { get; set; }

		public static TableMetadataModel<Test1Poco> Metadata => TestDbMetadata.Test1PocoMetadata;

		public Test1BM ToBm()
		{
			return new Test1BM
			{
				TestID = this.TestID,
				TestName1 = this.TestName1,
				TestName2 = this.TestName2,
				TestDate1 = this.TestDate1,
				TestDate2 = this.TestDate2,
				TestTimestamp1 = this.TestTimestamp1,
				TestTimestamp2 = this.TestTimestamp2,
				TestBoolean1 = this.TestBoolean1,
				TestBoolean2 = this.TestBoolean2,
				TestInteger1 = this.TestInteger1,
				TestInteger2 = this.TestInteger2,
				TestBigint1 = this.TestBigint1,
				TestBigint2 = this.TestBigint2,
				TestText1 = this.TestText1,
				TestText2 = this.TestText2,
				TestReal1 = this.TestReal1,
				TestReal2 = this.TestReal2,
				TestDouble1 = this.TestDouble1,
				TestDouble2 = this.TestDouble2,
				TestChar1 = this.TestChar1,
				TestChar2 = this.TestChar2,
			};
		}
	}

	/// <summary>
	/// <para>Table name: 'test2'.</para>
	/// <para>Table schema: 'public'.</para>
	/// </summary>
	[Table(Schema="public", Name = "test2")]
	public class Test2Poco : IPoco<Test2Poco>
	{
		/// <summary>
		/// <para>Column name: 'test_id'.</para>
		/// <para>Table name: 'test2'.</para>
		/// <para>Primary key of table: 'test2'.</para>
		/// <para>Primary key constraint name: 'test2_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
		/// </summary>
		[PrimaryKey, Identity]
		[Column(Name = "test_id", DataType = DataType.Int32)]
		public int TestID { get; set; }

		/// <summary>
		/// <para>Column name: 'test_name'.</para>
		/// <para>Table name: 'test2'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
		/// </summary>
		[NotNull]
		[Column(Name = "test_name", DataType = DataType.Text)]
		public string TestName { get; set; }

		/// <summary>
		/// <para>Column name: 'test_date'.</para>
		/// <para>Table name: 'test2'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
		/// </summary>
		[NotNull]
		[Column(Name = "test_date", DataType = DataType.DateTime2)]
		public DateTime TestDate { get; set; }

		public static TableMetadataModel<Test2Poco> Metadata => TestDbMetadata.Test2PocoMetadata;

		public Test2BM ToBm()
		{
			return new Test2BM
			{
				TestID = this.TestID,
				TestName = this.TestName,
				TestDate = this.TestDate,
			};
		}
	}

	/// <summary>
	/// <para>Table name: 'view1'.</para>
	/// <para>Table schema: 'public'.</para>
	/// </summary>
	[Table(Schema="public", Name = "view1")]
	public class View1Poco : IReadOnlyPoco<View1Poco>
	{
		/// <summary>
		/// <para>Column name: 'test_name1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_name1", DataType = DataType.NVarChar)]
		public string TestName1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_name2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_name2", DataType = DataType.NVarChar)]
		public string TestName2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_date1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'date'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Date'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.Date'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_date1", DataType = DataType.Date)]
		public DateTime? TestDate1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_date2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'date'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Date'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.Date'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_date2", DataType = DataType.Date)]
		public DateTime? TestDate2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_timestamp1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_timestamp1", DataType = DataType.DateTime2)]
		public DateTime? TestTimestamp1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_timestamp2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_timestamp2", DataType = DataType.DateTime2)]
		public DateTime? TestTimestamp2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_boolean1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'boolean'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Boolean'.</para>
		/// <para>CLR type: 'bool?'.</para>
		/// <para>linq2db data type: 'DataType.Boolean'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_boolean1", DataType = DataType.Boolean)]
		public bool? TestBoolean1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_boolean2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'boolean'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Boolean'.</para>
		/// <para>CLR type: 'bool?'.</para>
		/// <para>linq2db data type: 'DataType.Boolean'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_boolean2", DataType = DataType.Boolean)]
		public bool? TestBoolean2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_integer1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int?'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_integer1", DataType = DataType.Int32)]
		public int? TestInteger1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_integer2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int?'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_integer2", DataType = DataType.Int32)]
		public int? TestInteger2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_bigint1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'bigint'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Bigint'.</para>
		/// <para>CLR type: 'long?'.</para>
		/// <para>linq2db data type: 'DataType.Int64'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_bigint1", DataType = DataType.Int64)]
		public long? TestBigint1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_bigint2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'bigint'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Bigint'.</para>
		/// <para>CLR type: 'long?'.</para>
		/// <para>linq2db data type: 'DataType.Int64'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_bigint2", DataType = DataType.Int64)]
		public long? TestBigint2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_text1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_text1", DataType = DataType.Text)]
		public string TestText1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_text2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_text2", DataType = DataType.Text)]
		public string TestText2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_real1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'real'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Real'.</para>
		/// <para>CLR type: 'float?'.</para>
		/// <para>linq2db data type: 'DataType.Single'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_real1", DataType = DataType.Single)]
		public float? TestReal1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_real2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'real'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Real'.</para>
		/// <para>CLR type: 'float?'.</para>
		/// <para>linq2db data type: 'DataType.Single'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_real2", DataType = DataType.Single)]
		public float? TestReal2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_double1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'double precision'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Double'.</para>
		/// <para>CLR type: 'double?'.</para>
		/// <para>linq2db data type: 'DataType.Double'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_double1", DataType = DataType.Double)]
		public double? TestDouble1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_double2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'double precision'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Double'.</para>
		/// <para>CLR type: 'double?'.</para>
		/// <para>linq2db data type: 'DataType.Double'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_double2", DataType = DataType.Double)]
		public double? TestDouble2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_char1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Char'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NChar'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_char1", DataType = DataType.NChar)]
		public string TestChar1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_char2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Char'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NChar'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_char2", DataType = DataType.NChar)]
		public string TestChar2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_name'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_name", DataType = DataType.Text)]
		public string TestName { get; set; }

		/// <summary>
		/// <para>Column name: 'test_date'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
		/// </summary>
		[Nullable]
		[Column(Name = "test_date", DataType = DataType.DateTime2)]
		public DateTime? TestDate { get; set; }

		public static TableMetadataModel<View1Poco> Metadata => TestDbMetadata.View1PocoMetadata;

	}


	/// <summary>
	/// <para>Table name: 'test1'.</para>
	/// <para>Table schema: 'public'.</para>
	/// </summary>
	public class Test1CM : ICatalogModel<Test1Poco>
	{
		/// <summary>
		/// <para>Column name: 'test_id'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>Primary key of table: 'test1'.</para>
		/// <para>Primary key constraint name: 'test1_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
		/// </summary>
		public int TestID { get; set; }

		/// <summary>
		/// <para>Column name: 'test_name1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
		/// </summary>
		public string TestName1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_name2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
		/// </summary>
		public string TestName2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_date1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'date'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Date'.</para>
		/// <para>CLR type: 'DateTime'.</para>
		/// <para>linq2db data type: 'DataType.Date'.</para>
		/// </summary>
		public DateTime TestDate1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_date2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'date'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Date'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.Date'.</para>
		/// </summary>
		public DateTime? TestDate2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_timestamp1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
		/// </summary>
		public DateTime TestTimestamp1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_timestamp2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
		/// </summary>
		public DateTime? TestTimestamp2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_boolean1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'boolean'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Boolean'.</para>
		/// <para>CLR type: 'bool'.</para>
		/// <para>linq2db data type: 'DataType.Boolean'.</para>
		/// </summary>
		public bool TestBoolean1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_boolean2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'boolean'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Boolean'.</para>
		/// <para>CLR type: 'bool?'.</para>
		/// <para>linq2db data type: 'DataType.Boolean'.</para>
		/// </summary>
		public bool? TestBoolean2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_integer1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int?'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
		/// </summary>
		public int? TestInteger1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_integer2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
		/// </summary>
		public int TestInteger2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_bigint1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'bigint'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Bigint'.</para>
		/// <para>CLR type: 'long?'.</para>
		/// <para>linq2db data type: 'DataType.Int64'.</para>
		/// </summary>
		public long? TestBigint1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_bigint2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'bigint'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Bigint'.</para>
		/// <para>CLR type: 'long'.</para>
		/// <para>linq2db data type: 'DataType.Int64'.</para>
		/// </summary>
		public long TestBigint2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_text1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
		/// </summary>
		public string TestText1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_text2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
		/// </summary>
		public string TestText2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_real1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'real'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Real'.</para>
		/// <para>CLR type: 'float?'.</para>
		/// <para>linq2db data type: 'DataType.Single'.</para>
		/// </summary>
		public float? TestReal1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_real2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'real'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Real'.</para>
		/// <para>CLR type: 'float'.</para>
		/// <para>linq2db data type: 'DataType.Single'.</para>
		/// </summary>
		public float TestReal2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_double1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'double precision'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Double'.</para>
		/// <para>CLR type: 'double?'.</para>
		/// <para>linq2db data type: 'DataType.Double'.</para>
		/// </summary>
		public double? TestDouble1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_double2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'double precision'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Double'.</para>
		/// <para>CLR type: 'double'.</para>
		/// <para>linq2db data type: 'DataType.Double'.</para>
		/// </summary>
		public double TestDouble2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_char1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Char'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NChar'.</para>
		/// </summary>
		public string TestChar1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_char2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Char'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NChar'.</para>
		/// </summary>
		public string TestChar2 { get; set; }

	}

	/// <summary>
	/// <para>Table name: 'test2'.</para>
	/// <para>Table schema: 'public'.</para>
	/// </summary>
	public class Test2CM : ICatalogModel<Test2Poco>
	{
		/// <summary>
		/// <para>Column name: 'test_id'.</para>
		/// <para>Table name: 'test2'.</para>
		/// <para>Primary key of table: 'test2'.</para>
		/// <para>Primary key constraint name: 'test2_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
		/// </summary>
		public int TestID { get; set; }

		/// <summary>
		/// <para>Column name: 'test_name'.</para>
		/// <para>Table name: 'test2'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
		/// </summary>
		public string TestName { get; set; }

		/// <summary>
		/// <para>Column name: 'test_date'.</para>
		/// <para>Table name: 'test2'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
		/// </summary>
		public DateTime TestDate { get; set; }

	}

	/// <summary>
	/// <para>Table name: 'view1'.</para>
	/// <para>Table schema: 'public'.</para>
	/// </summary>
	public class View1CM : ICatalogModel<View1Poco>
	{
		/// <summary>
		/// <para>Column name: 'test_name1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
		/// </summary>
		public string TestName1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_name2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
		/// </summary>
		public string TestName2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_date1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'date'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Date'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.Date'.</para>
		/// </summary>
		public DateTime? TestDate1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_date2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'date'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Date'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.Date'.</para>
		/// </summary>
		public DateTime? TestDate2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_timestamp1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
		/// </summary>
		public DateTime? TestTimestamp1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_timestamp2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
		/// </summary>
		public DateTime? TestTimestamp2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_boolean1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'boolean'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Boolean'.</para>
		/// <para>CLR type: 'bool?'.</para>
		/// <para>linq2db data type: 'DataType.Boolean'.</para>
		/// </summary>
		public bool? TestBoolean1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_boolean2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'boolean'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Boolean'.</para>
		/// <para>CLR type: 'bool?'.</para>
		/// <para>linq2db data type: 'DataType.Boolean'.</para>
		/// </summary>
		public bool? TestBoolean2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_integer1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int?'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
		/// </summary>
		public int? TestInteger1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_integer2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int?'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
		/// </summary>
		public int? TestInteger2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_bigint1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'bigint'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Bigint'.</para>
		/// <para>CLR type: 'long?'.</para>
		/// <para>linq2db data type: 'DataType.Int64'.</para>
		/// </summary>
		public long? TestBigint1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_bigint2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'bigint'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Bigint'.</para>
		/// <para>CLR type: 'long?'.</para>
		/// <para>linq2db data type: 'DataType.Int64'.</para>
		/// </summary>
		public long? TestBigint2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_text1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
		/// </summary>
		public string TestText1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_text2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
		/// </summary>
		public string TestText2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_real1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'real'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Real'.</para>
		/// <para>CLR type: 'float?'.</para>
		/// <para>linq2db data type: 'DataType.Single'.</para>
		/// </summary>
		public float? TestReal1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_real2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'real'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Real'.</para>
		/// <para>CLR type: 'float?'.</para>
		/// <para>linq2db data type: 'DataType.Single'.</para>
		/// </summary>
		public float? TestReal2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_double1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'double precision'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Double'.</para>
		/// <para>CLR type: 'double?'.</para>
		/// <para>linq2db data type: 'DataType.Double'.</para>
		/// </summary>
		public double? TestDouble1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_double2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'double precision'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Double'.</para>
		/// <para>CLR type: 'double?'.</para>
		/// <para>linq2db data type: 'DataType.Double'.</para>
		/// </summary>
		public double? TestDouble2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_char1'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Char'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NChar'.</para>
		/// </summary>
		public string TestChar1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_char2'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Char'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NChar'.</para>
		/// </summary>
		public string TestChar2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_name'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
		/// </summary>
		public string TestName { get; set; }

		/// <summary>
		/// <para>Column name: 'test_date'.</para>
		/// <para>Table name: 'view1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
		/// </summary>
		public DateTime? TestDate { get; set; }

	}


	/// <summary>
	/// <para>Table name: 'test1'.</para>
	/// <para>Table schema: 'public'.</para>
	/// </summary>
	public class Test1FM : IFilterModel<Test1Poco>
	{
		[FilterOperator(QueryOperatorType.Equal, "TestID", NpgsqlDbType.Integer, "test_id")]
		public int? TestID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestID", NpgsqlDbType.Integer, "test_id")]
		public int? TestID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan, "TestID", NpgsqlDbType.Integer, "test_id")]
		public int? TestID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual, "TestID", NpgsqlDbType.Integer, "test_id")]
		public int? TestID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan, "TestID", NpgsqlDbType.Integer, "test_id")]
		public int? TestID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual, "TestID", NpgsqlDbType.Integer, "test_id")]
		public int? TestID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestID", NpgsqlDbType.Integer, "test_id")]
		public int[] TestID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestID", NpgsqlDbType.Integer, "test_id")]
		public int[] TestID_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string TestName1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string TestName1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string TestName1_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string TestName1_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string TestName1_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string TestName1_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string TestName1_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string TestName1_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string[] TestName1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string[] TestName1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string TestName2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string TestName2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string TestName2_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string TestName2_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string TestName2_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string TestName2_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string TestName2_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string TestName2_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public bool? TestName2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public bool? TestName2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string[] TestName2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string[] TestName2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestDate1", NpgsqlDbType.Date, "test_date1")]
		public DateTime? TestDate1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestDate1", NpgsqlDbType.Date, "test_date1")]
		public DateTime? TestDate1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan, "TestDate1", NpgsqlDbType.Date, "test_date1")]
		public DateTime? TestDate1_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual, "TestDate1", NpgsqlDbType.Date, "test_date1")]
		public DateTime? TestDate1_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan, "TestDate1", NpgsqlDbType.Date, "test_date1")]
		public DateTime? TestDate1_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual, "TestDate1", NpgsqlDbType.Date, "test_date1")]
		public DateTime? TestDate1_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestDate1", NpgsqlDbType.Date, "test_date1")]
		public DateTime[] TestDate1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestDate1", NpgsqlDbType.Date, "test_date1")]
		public DateTime[] TestDate1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestDate2", NpgsqlDbType.Date, "test_date2")]
		public DateTime? TestDate2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestDate2", NpgsqlDbType.Date, "test_date2")]
		public DateTime? TestDate2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestDate2", NpgsqlDbType.Date, "test_date2")]
		public bool? TestDate2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestDate2", NpgsqlDbType.Date, "test_date2")]
		public bool? TestDate2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestDate2", NpgsqlDbType.Date, "test_date2")]
		public DateTime[] TestDate2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestDate2", NpgsqlDbType.Date, "test_date2")]
		public DateTime[] TestDate2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestTimestamp1", NpgsqlDbType.Timestamp, "test_timestamp1")]
		public DateTime? TestTimestamp1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestTimestamp1", NpgsqlDbType.Timestamp, "test_timestamp1")]
		public DateTime? TestTimestamp1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan, "TestTimestamp1", NpgsqlDbType.Timestamp, "test_timestamp1")]
		public DateTime? TestTimestamp1_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual, "TestTimestamp1", NpgsqlDbType.Timestamp, "test_timestamp1")]
		public DateTime? TestTimestamp1_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan, "TestTimestamp1", NpgsqlDbType.Timestamp, "test_timestamp1")]
		public DateTime? TestTimestamp1_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual, "TestTimestamp1", NpgsqlDbType.Timestamp, "test_timestamp1")]
		public DateTime? TestTimestamp1_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestTimestamp1", NpgsqlDbType.Timestamp, "test_timestamp1")]
		public DateTime[] TestTimestamp1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestTimestamp1", NpgsqlDbType.Timestamp, "test_timestamp1")]
		public DateTime[] TestTimestamp1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestTimestamp2", NpgsqlDbType.Timestamp, "test_timestamp2")]
		public DateTime? TestTimestamp2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestTimestamp2", NpgsqlDbType.Timestamp, "test_timestamp2")]
		public DateTime? TestTimestamp2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestTimestamp2", NpgsqlDbType.Timestamp, "test_timestamp2")]
		public bool? TestTimestamp2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestTimestamp2", NpgsqlDbType.Timestamp, "test_timestamp2")]
		public bool? TestTimestamp2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestTimestamp2", NpgsqlDbType.Timestamp, "test_timestamp2")]
		public DateTime[] TestTimestamp2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestTimestamp2", NpgsqlDbType.Timestamp, "test_timestamp2")]
		public DateTime[] TestTimestamp2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestBoolean1", NpgsqlDbType.Boolean, "test_boolean1")]
		public bool? TestBoolean1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestBoolean1", NpgsqlDbType.Boolean, "test_boolean1")]
		public bool? TestBoolean1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestBoolean1", NpgsqlDbType.Boolean, "test_boolean1")]
		public bool[] TestBoolean1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestBoolean1", NpgsqlDbType.Boolean, "test_boolean1")]
		public bool[] TestBoolean1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestBoolean2", NpgsqlDbType.Boolean, "test_boolean2")]
		public bool? TestBoolean2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestBoolean2", NpgsqlDbType.Boolean, "test_boolean2")]
		public bool? TestBoolean2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestBoolean2", NpgsqlDbType.Boolean, "test_boolean2")]
		public bool? TestBoolean2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestBoolean2", NpgsqlDbType.Boolean, "test_boolean2")]
		public bool? TestBoolean2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestBoolean2", NpgsqlDbType.Boolean, "test_boolean2")]
		public bool[] TestBoolean2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestBoolean2", NpgsqlDbType.Boolean, "test_boolean2")]
		public bool[] TestBoolean2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestInteger1", NpgsqlDbType.Integer, "test_integer1")]
		public int? TestInteger1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestInteger1", NpgsqlDbType.Integer, "test_integer1")]
		public int? TestInteger1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestInteger1", NpgsqlDbType.Integer, "test_integer1")]
		public bool? TestInteger1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestInteger1", NpgsqlDbType.Integer, "test_integer1")]
		public bool? TestInteger1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestInteger1", NpgsqlDbType.Integer, "test_integer1")]
		public int[] TestInteger1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestInteger1", NpgsqlDbType.Integer, "test_integer1")]
		public int[] TestInteger1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestInteger2", NpgsqlDbType.Integer, "test_integer2")]
		public int? TestInteger2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestInteger2", NpgsqlDbType.Integer, "test_integer2")]
		public int? TestInteger2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan, "TestInteger2", NpgsqlDbType.Integer, "test_integer2")]
		public int? TestInteger2_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual, "TestInteger2", NpgsqlDbType.Integer, "test_integer2")]
		public int? TestInteger2_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan, "TestInteger2", NpgsqlDbType.Integer, "test_integer2")]
		public int? TestInteger2_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual, "TestInteger2", NpgsqlDbType.Integer, "test_integer2")]
		public int? TestInteger2_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestInteger2", NpgsqlDbType.Integer, "test_integer2")]
		public int[] TestInteger2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestInteger2", NpgsqlDbType.Integer, "test_integer2")]
		public int[] TestInteger2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestBigint1", NpgsqlDbType.Bigint, "test_bigint1")]
		public long? TestBigint1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestBigint1", NpgsqlDbType.Bigint, "test_bigint1")]
		public long? TestBigint1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestBigint1", NpgsqlDbType.Bigint, "test_bigint1")]
		public bool? TestBigint1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestBigint1", NpgsqlDbType.Bigint, "test_bigint1")]
		public bool? TestBigint1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestBigint1", NpgsqlDbType.Bigint, "test_bigint1")]
		public long[] TestBigint1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestBigint1", NpgsqlDbType.Bigint, "test_bigint1")]
		public long[] TestBigint1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestBigint2", NpgsqlDbType.Bigint, "test_bigint2")]
		public long? TestBigint2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestBigint2", NpgsqlDbType.Bigint, "test_bigint2")]
		public long? TestBigint2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan, "TestBigint2", NpgsqlDbType.Bigint, "test_bigint2")]
		public long? TestBigint2_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual, "TestBigint2", NpgsqlDbType.Bigint, "test_bigint2")]
		public long? TestBigint2_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan, "TestBigint2", NpgsqlDbType.Bigint, "test_bigint2")]
		public long? TestBigint2_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual, "TestBigint2", NpgsqlDbType.Bigint, "test_bigint2")]
		public long? TestBigint2_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestBigint2", NpgsqlDbType.Bigint, "test_bigint2")]
		public long[] TestBigint2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestBigint2", NpgsqlDbType.Bigint, "test_bigint2")]
		public long[] TestBigint2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string TestText1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string TestText1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string TestText1_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string TestText1_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string TestText1_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string TestText1_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string TestText1_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string TestText1_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public bool? TestText1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public bool? TestText1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string[] TestText1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string[] TestText1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string TestText2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string TestText2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string TestText2_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string TestText2_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string TestText2_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string TestText2_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string TestText2_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string TestText2_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string[] TestText2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string[] TestText2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestReal1", NpgsqlDbType.Real, "test_real1")]
		public float? TestReal1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestReal1", NpgsqlDbType.Real, "test_real1")]
		public float? TestReal1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestReal1", NpgsqlDbType.Real, "test_real1")]
		public bool? TestReal1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestReal1", NpgsqlDbType.Real, "test_real1")]
		public bool? TestReal1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestReal1", NpgsqlDbType.Real, "test_real1")]
		public float[] TestReal1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestReal1", NpgsqlDbType.Real, "test_real1")]
		public float[] TestReal1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestReal2", NpgsqlDbType.Real, "test_real2")]
		public float? TestReal2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestReal2", NpgsqlDbType.Real, "test_real2")]
		public float? TestReal2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan, "TestReal2", NpgsqlDbType.Real, "test_real2")]
		public float? TestReal2_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual, "TestReal2", NpgsqlDbType.Real, "test_real2")]
		public float? TestReal2_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan, "TestReal2", NpgsqlDbType.Real, "test_real2")]
		public float? TestReal2_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual, "TestReal2", NpgsqlDbType.Real, "test_real2")]
		public float? TestReal2_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestReal2", NpgsqlDbType.Real, "test_real2")]
		public float[] TestReal2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestReal2", NpgsqlDbType.Real, "test_real2")]
		public float[] TestReal2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestDouble1", NpgsqlDbType.Double, "test_double1")]
		public double? TestDouble1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestDouble1", NpgsqlDbType.Double, "test_double1")]
		public double? TestDouble1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestDouble1", NpgsqlDbType.Double, "test_double1")]
		public bool? TestDouble1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestDouble1", NpgsqlDbType.Double, "test_double1")]
		public bool? TestDouble1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestDouble1", NpgsqlDbType.Double, "test_double1")]
		public double[] TestDouble1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestDouble1", NpgsqlDbType.Double, "test_double1")]
		public double[] TestDouble1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestDouble2", NpgsqlDbType.Double, "test_double2")]
		public double? TestDouble2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestDouble2", NpgsqlDbType.Double, "test_double2")]
		public double? TestDouble2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan, "TestDouble2", NpgsqlDbType.Double, "test_double2")]
		public double? TestDouble2_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual, "TestDouble2", NpgsqlDbType.Double, "test_double2")]
		public double? TestDouble2_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan, "TestDouble2", NpgsqlDbType.Double, "test_double2")]
		public double? TestDouble2_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual, "TestDouble2", NpgsqlDbType.Double, "test_double2")]
		public double? TestDouble2_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestDouble2", NpgsqlDbType.Double, "test_double2")]
		public double[] TestDouble2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestDouble2", NpgsqlDbType.Double, "test_double2")]
		public double[] TestDouble2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string TestChar1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string TestChar1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string TestChar1_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string TestChar1_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string TestChar1_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string TestChar1_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string TestChar1_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string TestChar1_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public bool? TestChar1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public bool? TestChar1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string[] TestChar1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string[] TestChar1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string TestChar2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string TestChar2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string TestChar2_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string TestChar2_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string TestChar2_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string TestChar2_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string TestChar2_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string TestChar2_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string[] TestChar2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string[] TestChar2_IsNotIn { get; set; }

	}

	/// <summary>
	/// <para>Table name: 'test2'.</para>
	/// <para>Table schema: 'public'.</para>
	/// </summary>
	public class Test2FM : IFilterModel<Test2Poco>
	{
		[FilterOperator(QueryOperatorType.Equal, "TestID", NpgsqlDbType.Integer, "test_id")]
		public int? TestID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestID", NpgsqlDbType.Integer, "test_id")]
		public int? TestID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan, "TestID", NpgsqlDbType.Integer, "test_id")]
		public int? TestID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual, "TestID", NpgsqlDbType.Integer, "test_id")]
		public int? TestID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan, "TestID", NpgsqlDbType.Integer, "test_id")]
		public int? TestID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual, "TestID", NpgsqlDbType.Integer, "test_id")]
		public int? TestID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestID", NpgsqlDbType.Integer, "test_id")]
		public int[] TestID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestID", NpgsqlDbType.Integer, "test_id")]
		public int[] TestID_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestName", NpgsqlDbType.Text, "test_name")]
		public string TestName { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestName", NpgsqlDbType.Text, "test_name")]
		public string TestName_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestName", NpgsqlDbType.Text, "test_name")]
		public string TestName_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestName", NpgsqlDbType.Text, "test_name")]
		public string TestName_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestName", NpgsqlDbType.Text, "test_name")]
		public string TestName_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestName", NpgsqlDbType.Text, "test_name")]
		public string TestName_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestName", NpgsqlDbType.Text, "test_name")]
		public string TestName_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestName", NpgsqlDbType.Text, "test_name")]
		public string TestName_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestName", NpgsqlDbType.Text, "test_name")]
		public string[] TestName_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestName", NpgsqlDbType.Text, "test_name")]
		public string[] TestName_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestDate", NpgsqlDbType.Timestamp, "test_date")]
		public DateTime? TestDate { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestDate", NpgsqlDbType.Timestamp, "test_date")]
		public DateTime? TestDate_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan, "TestDate", NpgsqlDbType.Timestamp, "test_date")]
		public DateTime? TestDate_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual, "TestDate", NpgsqlDbType.Timestamp, "test_date")]
		public DateTime? TestDate_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan, "TestDate", NpgsqlDbType.Timestamp, "test_date")]
		public DateTime? TestDate_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual, "TestDate", NpgsqlDbType.Timestamp, "test_date")]
		public DateTime? TestDate_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestDate", NpgsqlDbType.Timestamp, "test_date")]
		public DateTime[] TestDate_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestDate", NpgsqlDbType.Timestamp, "test_date")]
		public DateTime[] TestDate_IsNotIn { get; set; }

	}

	/// <summary>
	/// <para>Table name: 'view1'.</para>
	/// <para>Table schema: 'public'.</para>
	/// </summary>
	public class View1FM : IFilterModel<View1Poco>
	{
		[FilterOperator(QueryOperatorType.Equal, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string TestName1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string TestName1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string TestName1_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string TestName1_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string TestName1_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string TestName1_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string TestName1_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string TestName1_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public bool? TestName1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public bool? TestName1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string[] TestName1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestName1", NpgsqlDbType.Varchar, "test_name1")]
		public string[] TestName1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string TestName2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string TestName2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string TestName2_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string TestName2_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string TestName2_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string TestName2_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string TestName2_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string TestName2_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public bool? TestName2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public bool? TestName2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string[] TestName2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestName2", NpgsqlDbType.Varchar, "test_name2")]
		public string[] TestName2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestDate1", NpgsqlDbType.Date, "test_date1")]
		public DateTime? TestDate1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestDate1", NpgsqlDbType.Date, "test_date1")]
		public DateTime? TestDate1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestDate1", NpgsqlDbType.Date, "test_date1")]
		public bool? TestDate1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestDate1", NpgsqlDbType.Date, "test_date1")]
		public bool? TestDate1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestDate1", NpgsqlDbType.Date, "test_date1")]
		public DateTime[] TestDate1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestDate1", NpgsqlDbType.Date, "test_date1")]
		public DateTime[] TestDate1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestDate2", NpgsqlDbType.Date, "test_date2")]
		public DateTime? TestDate2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestDate2", NpgsqlDbType.Date, "test_date2")]
		public DateTime? TestDate2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestDate2", NpgsqlDbType.Date, "test_date2")]
		public bool? TestDate2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestDate2", NpgsqlDbType.Date, "test_date2")]
		public bool? TestDate2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestDate2", NpgsqlDbType.Date, "test_date2")]
		public DateTime[] TestDate2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestDate2", NpgsqlDbType.Date, "test_date2")]
		public DateTime[] TestDate2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestTimestamp1", NpgsqlDbType.Timestamp, "test_timestamp1")]
		public DateTime? TestTimestamp1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestTimestamp1", NpgsqlDbType.Timestamp, "test_timestamp1")]
		public DateTime? TestTimestamp1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestTimestamp1", NpgsqlDbType.Timestamp, "test_timestamp1")]
		public bool? TestTimestamp1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestTimestamp1", NpgsqlDbType.Timestamp, "test_timestamp1")]
		public bool? TestTimestamp1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestTimestamp1", NpgsqlDbType.Timestamp, "test_timestamp1")]
		public DateTime[] TestTimestamp1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestTimestamp1", NpgsqlDbType.Timestamp, "test_timestamp1")]
		public DateTime[] TestTimestamp1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestTimestamp2", NpgsqlDbType.Timestamp, "test_timestamp2")]
		public DateTime? TestTimestamp2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestTimestamp2", NpgsqlDbType.Timestamp, "test_timestamp2")]
		public DateTime? TestTimestamp2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestTimestamp2", NpgsqlDbType.Timestamp, "test_timestamp2")]
		public bool? TestTimestamp2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestTimestamp2", NpgsqlDbType.Timestamp, "test_timestamp2")]
		public bool? TestTimestamp2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestTimestamp2", NpgsqlDbType.Timestamp, "test_timestamp2")]
		public DateTime[] TestTimestamp2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestTimestamp2", NpgsqlDbType.Timestamp, "test_timestamp2")]
		public DateTime[] TestTimestamp2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestBoolean1", NpgsqlDbType.Boolean, "test_boolean1")]
		public bool? TestBoolean1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestBoolean1", NpgsqlDbType.Boolean, "test_boolean1")]
		public bool? TestBoolean1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestBoolean1", NpgsqlDbType.Boolean, "test_boolean1")]
		public bool? TestBoolean1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestBoolean1", NpgsqlDbType.Boolean, "test_boolean1")]
		public bool? TestBoolean1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestBoolean1", NpgsqlDbType.Boolean, "test_boolean1")]
		public bool[] TestBoolean1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestBoolean1", NpgsqlDbType.Boolean, "test_boolean1")]
		public bool[] TestBoolean1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestBoolean2", NpgsqlDbType.Boolean, "test_boolean2")]
		public bool? TestBoolean2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestBoolean2", NpgsqlDbType.Boolean, "test_boolean2")]
		public bool? TestBoolean2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestBoolean2", NpgsqlDbType.Boolean, "test_boolean2")]
		public bool? TestBoolean2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestBoolean2", NpgsqlDbType.Boolean, "test_boolean2")]
		public bool? TestBoolean2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestBoolean2", NpgsqlDbType.Boolean, "test_boolean2")]
		public bool[] TestBoolean2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestBoolean2", NpgsqlDbType.Boolean, "test_boolean2")]
		public bool[] TestBoolean2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestInteger1", NpgsqlDbType.Integer, "test_integer1")]
		public int? TestInteger1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestInteger1", NpgsqlDbType.Integer, "test_integer1")]
		public int? TestInteger1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestInteger1", NpgsqlDbType.Integer, "test_integer1")]
		public bool? TestInteger1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestInteger1", NpgsqlDbType.Integer, "test_integer1")]
		public bool? TestInteger1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestInteger1", NpgsqlDbType.Integer, "test_integer1")]
		public int[] TestInteger1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestInteger1", NpgsqlDbType.Integer, "test_integer1")]
		public int[] TestInteger1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestInteger2", NpgsqlDbType.Integer, "test_integer2")]
		public int? TestInteger2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestInteger2", NpgsqlDbType.Integer, "test_integer2")]
		public int? TestInteger2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestInteger2", NpgsqlDbType.Integer, "test_integer2")]
		public bool? TestInteger2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestInteger2", NpgsqlDbType.Integer, "test_integer2")]
		public bool? TestInteger2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestInteger2", NpgsqlDbType.Integer, "test_integer2")]
		public int[] TestInteger2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestInteger2", NpgsqlDbType.Integer, "test_integer2")]
		public int[] TestInteger2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestBigint1", NpgsqlDbType.Bigint, "test_bigint1")]
		public long? TestBigint1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestBigint1", NpgsqlDbType.Bigint, "test_bigint1")]
		public long? TestBigint1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestBigint1", NpgsqlDbType.Bigint, "test_bigint1")]
		public bool? TestBigint1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestBigint1", NpgsqlDbType.Bigint, "test_bigint1")]
		public bool? TestBigint1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestBigint1", NpgsqlDbType.Bigint, "test_bigint1")]
		public long[] TestBigint1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestBigint1", NpgsqlDbType.Bigint, "test_bigint1")]
		public long[] TestBigint1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestBigint2", NpgsqlDbType.Bigint, "test_bigint2")]
		public long? TestBigint2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestBigint2", NpgsqlDbType.Bigint, "test_bigint2")]
		public long? TestBigint2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestBigint2", NpgsqlDbType.Bigint, "test_bigint2")]
		public bool? TestBigint2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestBigint2", NpgsqlDbType.Bigint, "test_bigint2")]
		public bool? TestBigint2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestBigint2", NpgsqlDbType.Bigint, "test_bigint2")]
		public long[] TestBigint2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestBigint2", NpgsqlDbType.Bigint, "test_bigint2")]
		public long[] TestBigint2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string TestText1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string TestText1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string TestText1_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string TestText1_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string TestText1_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string TestText1_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string TestText1_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string TestText1_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public bool? TestText1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public bool? TestText1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string[] TestText1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestText1", NpgsqlDbType.Text, "test_text1")]
		public string[] TestText1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string TestText2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string TestText2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string TestText2_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string TestText2_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string TestText2_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string TestText2_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string TestText2_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string TestText2_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public bool? TestText2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public bool? TestText2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string[] TestText2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestText2", NpgsqlDbType.Text, "test_text2")]
		public string[] TestText2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestReal1", NpgsqlDbType.Real, "test_real1")]
		public float? TestReal1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestReal1", NpgsqlDbType.Real, "test_real1")]
		public float? TestReal1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestReal1", NpgsqlDbType.Real, "test_real1")]
		public bool? TestReal1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestReal1", NpgsqlDbType.Real, "test_real1")]
		public bool? TestReal1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestReal1", NpgsqlDbType.Real, "test_real1")]
		public float[] TestReal1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestReal1", NpgsqlDbType.Real, "test_real1")]
		public float[] TestReal1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestReal2", NpgsqlDbType.Real, "test_real2")]
		public float? TestReal2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestReal2", NpgsqlDbType.Real, "test_real2")]
		public float? TestReal2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestReal2", NpgsqlDbType.Real, "test_real2")]
		public bool? TestReal2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestReal2", NpgsqlDbType.Real, "test_real2")]
		public bool? TestReal2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestReal2", NpgsqlDbType.Real, "test_real2")]
		public float[] TestReal2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestReal2", NpgsqlDbType.Real, "test_real2")]
		public float[] TestReal2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestDouble1", NpgsqlDbType.Double, "test_double1")]
		public double? TestDouble1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestDouble1", NpgsqlDbType.Double, "test_double1")]
		public double? TestDouble1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestDouble1", NpgsqlDbType.Double, "test_double1")]
		public bool? TestDouble1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestDouble1", NpgsqlDbType.Double, "test_double1")]
		public bool? TestDouble1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestDouble1", NpgsqlDbType.Double, "test_double1")]
		public double[] TestDouble1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestDouble1", NpgsqlDbType.Double, "test_double1")]
		public double[] TestDouble1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestDouble2", NpgsqlDbType.Double, "test_double2")]
		public double? TestDouble2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestDouble2", NpgsqlDbType.Double, "test_double2")]
		public double? TestDouble2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestDouble2", NpgsqlDbType.Double, "test_double2")]
		public bool? TestDouble2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestDouble2", NpgsqlDbType.Double, "test_double2")]
		public bool? TestDouble2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestDouble2", NpgsqlDbType.Double, "test_double2")]
		public double[] TestDouble2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestDouble2", NpgsqlDbType.Double, "test_double2")]
		public double[] TestDouble2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string TestChar1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string TestChar1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string TestChar1_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string TestChar1_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string TestChar1_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string TestChar1_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string TestChar1_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string TestChar1_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public bool? TestChar1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public bool? TestChar1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string[] TestChar1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestChar1", NpgsqlDbType.Char, "test_char1")]
		public string[] TestChar1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string TestChar2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string TestChar2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string TestChar2_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string TestChar2_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string TestChar2_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string TestChar2_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string TestChar2_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string TestChar2_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public bool? TestChar2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public bool? TestChar2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string[] TestChar2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestChar2", NpgsqlDbType.Char, "test_char2")]
		public string[] TestChar2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestName", NpgsqlDbType.Text, "test_name")]
		public string TestName { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestName", NpgsqlDbType.Text, "test_name")]
		public string TestName_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestName", NpgsqlDbType.Text, "test_name")]
		public string TestName_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestName", NpgsqlDbType.Text, "test_name")]
		public string TestName_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestName", NpgsqlDbType.Text, "test_name")]
		public string TestName_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestName", NpgsqlDbType.Text, "test_name")]
		public string TestName_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestName", NpgsqlDbType.Text, "test_name")]
		public string TestName_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestName", NpgsqlDbType.Text, "test_name")]
		public string TestName_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestName", NpgsqlDbType.Text, "test_name")]
		public bool? TestName_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestName", NpgsqlDbType.Text, "test_name")]
		public bool? TestName_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestName", NpgsqlDbType.Text, "test_name")]
		public string[] TestName_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestName", NpgsqlDbType.Text, "test_name")]
		public string[] TestName_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestDate", NpgsqlDbType.Timestamp, "test_date")]
		public DateTime? TestDate { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestDate", NpgsqlDbType.Timestamp, "test_date")]
		public DateTime? TestDate_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestDate", NpgsqlDbType.Timestamp, "test_date")]
		public bool? TestDate_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestDate", NpgsqlDbType.Timestamp, "test_date")]
		public bool? TestDate_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestDate", NpgsqlDbType.Timestamp, "test_date")]
		public DateTime[] TestDate_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestDate", NpgsqlDbType.Timestamp, "test_date")]
		public DateTime[] TestDate_IsNotIn { get; set; }

	}


	/// <summary>
	/// <para>Table name: 'test1'.</para>
	/// <para>Table schema: 'public'.</para>
	/// </summary>
	public partial class Test1BM : IBusinessModel<Test1Poco>
	{
		/// <summary>
		/// <para>Column name: 'test_id'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>Primary key of table: 'test1'.</para>
		/// <para>Primary key constraint name: 'test1_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
		/// </summary>
		public int TestID { get; set; }

		/// <summary>
		/// <para>Column name: 'test_name1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
		/// </summary>
		public string TestName1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_name2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character varying'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Varchar'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NVarChar'.</para>
		/// </summary>
		public string TestName2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_date1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'date'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Date'.</para>
		/// <para>CLR type: 'DateTime'.</para>
		/// <para>linq2db data type: 'DataType.Date'.</para>
		/// </summary>
		public DateTime TestDate1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_date2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'date'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Date'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.Date'.</para>
		/// </summary>
		public DateTime? TestDate2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_timestamp1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
		/// </summary>
		public DateTime TestTimestamp1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_timestamp2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime?'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
		/// </summary>
		public DateTime? TestTimestamp2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_boolean1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'boolean'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Boolean'.</para>
		/// <para>CLR type: 'bool'.</para>
		/// <para>linq2db data type: 'DataType.Boolean'.</para>
		/// </summary>
		public bool TestBoolean1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_boolean2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'boolean'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Boolean'.</para>
		/// <para>CLR type: 'bool?'.</para>
		/// <para>linq2db data type: 'DataType.Boolean'.</para>
		/// </summary>
		public bool? TestBoolean2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_integer1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int?'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
		/// </summary>
		public int? TestInteger1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_integer2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
		/// </summary>
		public int TestInteger2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_bigint1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'bigint'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Bigint'.</para>
		/// <para>CLR type: 'long?'.</para>
		/// <para>linq2db data type: 'DataType.Int64'.</para>
		/// </summary>
		public long? TestBigint1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_bigint2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'bigint'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Bigint'.</para>
		/// <para>CLR type: 'long'.</para>
		/// <para>linq2db data type: 'DataType.Int64'.</para>
		/// </summary>
		public long TestBigint2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_text1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
		/// </summary>
		public string TestText1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_text2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
		/// </summary>
		public string TestText2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_real1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'real'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Real'.</para>
		/// <para>CLR type: 'float?'.</para>
		/// <para>linq2db data type: 'DataType.Single'.</para>
		/// </summary>
		public float? TestReal1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_real2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'real'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Real'.</para>
		/// <para>CLR type: 'float'.</para>
		/// <para>linq2db data type: 'DataType.Single'.</para>
		/// </summary>
		public float TestReal2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_double1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'double precision'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Double'.</para>
		/// <para>CLR type: 'double?'.</para>
		/// <para>linq2db data type: 'DataType.Double'.</para>
		/// </summary>
		public double? TestDouble1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_double2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'double precision'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Double'.</para>
		/// <para>CLR type: 'double'.</para>
		/// <para>linq2db data type: 'DataType.Double'.</para>
		/// </summary>
		public double TestDouble2 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_char1'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is nullable.</para>
		/// <para>PostgreSQL data type: 'character'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Char'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NChar'.</para>
		/// </summary>
		public string TestChar1 { get; set; }

		/// <summary>
		/// <para>Column name: 'test_char2'.</para>
		/// <para>Table name: 'test1'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'character'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Char'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.NChar'.</para>
		/// </summary>
		public string TestChar2 { get; set; }

		public Test1Poco ToPoco()
		{
			return new Test1Poco
			{
				TestID = this.TestID,
				TestName1 = this.TestName1,
				TestName2 = this.TestName2,
				TestDate1 = this.TestDate1,
				TestDate2 = this.TestDate2,
				TestTimestamp1 = this.TestTimestamp1,
				TestTimestamp2 = this.TestTimestamp2,
				TestBoolean1 = this.TestBoolean1,
				TestBoolean2 = this.TestBoolean2,
				TestInteger1 = this.TestInteger1,
				TestInteger2 = this.TestInteger2,
				TestBigint1 = this.TestBigint1,
				TestBigint2 = this.TestBigint2,
				TestText1 = this.TestText1,
				TestText2 = this.TestText2,
				TestReal1 = this.TestReal1,
				TestReal2 = this.TestReal2,
				TestDouble1 = this.TestDouble1,
				TestDouble2 = this.TestDouble2,
				TestChar1 = this.TestChar1,
				TestChar2 = this.TestChar2,
			};
		}
	}

	/// <summary>
	/// <para>Table name: 'test2'.</para>
	/// <para>Table schema: 'public'.</para>
	/// </summary>
	public partial class Test2BM : IBusinessModel<Test2Poco>
	{
		/// <summary>
		/// <para>Column name: 'test_id'.</para>
		/// <para>Table name: 'test2'.</para>
		/// <para>Primary key of table: 'test2'.</para>
		/// <para>Primary key constraint name: 'test2_pkey'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'integer'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Integer'.</para>
		/// <para>CLR type: 'int'.</para>
		/// <para>linq2db data type: 'DataType.Int32'.</para>
		/// </summary>
		public int TestID { get; set; }

		/// <summary>
		/// <para>Column name: 'test_name'.</para>
		/// <para>Table name: 'test2'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'text'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Text'.</para>
		/// <para>CLR type: 'string'.</para>
		/// <para>linq2db data type: 'DataType.Text'.</para>
		/// </summary>
		public string TestName { get; set; }

		/// <summary>
		/// <para>Column name: 'test_date'.</para>
		/// <para>Table name: 'test2'.</para>
		/// <para>This column is not nullable.</para>
		/// <para>PostgreSQL data type: 'timestamp without time zone'.</para>
		/// <para>NpgsqlDbType: 'NpgsqlDbType.Timestamp'.</para>
		/// <para>CLR type: 'DateTime'.</para>
		/// <para>linq2db data type: 'DataType.DateTime2'.</para>
		/// </summary>
		public DateTime TestDate { get; set; }

		public Test2Poco ToPoco()
		{
			return new Test2Poco
			{
				TestID = this.TestID,
				TestName = this.TestName,
				TestDate = this.TestDate,
			};
		}
	}

	public class TestDbPocos : IDbPocos<TestDbPocos>
	{
		/// <summary>
		/// <para>Database table 'test1'.</para>
		/// </summary>
		public IQueryable<Test1Poco> Test1 => this.DbService.GetTable<Test1Poco>();

		/// <summary>
		/// <para>Database table 'test1'.</para>
		/// <para>Filter model 'Test1FM'.</para>
		/// <para>Catalog model 'Test1CM'.</para>
		/// </summary>
		public Task<List<Test1CM>> Filter(Test1FM filter) => this.DbService.FilterInternal<Test1Poco, Test1CM>(filter);

		/// <summary>
		/// <para>Database table 'test2'.</para>
		/// </summary>
		public IQueryable<Test2Poco> Test2 => this.DbService.GetTable<Test2Poco>();

		/// <summary>
		/// <para>Database table 'test2'.</para>
		/// <para>Filter model 'Test2FM'.</para>
		/// <para>Catalog model 'Test2CM'.</para>
		/// </summary>
		public Task<List<Test2CM>> Filter(Test2FM filter) => this.DbService.FilterInternal<Test2Poco, Test2CM>(filter);

		/// <summary>
		/// <para>Database table 'view1'.</para>
		/// </summary>
		public IQueryable<View1Poco> View1 => this.DbService.GetTable<View1Poco>();

		/// <summary>
		/// <para>Database table 'view1'.</para>
		/// <para>Filter model 'View1FM'.</para>
		/// <para>Catalog model 'View1CM'.</para>
		/// </summary>
		public Task<List<View1CM>> Filter(View1FM filter) => this.DbService.FilterInternal<View1Poco, View1CM>(filter);


		public IDbService<TestDbPocos> DbService { private get; set; }
	}

	public static class TestDbPocosExtensions
	{
		/// <summary>
		/// <para>Database table 'test1'.</para>
		/// </summary>
		public static IQueryable<Test1CM> SelectCm(this IQueryable<Test1Poco> collection) => collection.SelectCm<Test1Poco, Test1CM>();

		/// <summary>
		/// <para>Database table 'test2'.</para>
		/// </summary>
		public static IQueryable<Test2CM> SelectCm(this IQueryable<Test2Poco> collection) => collection.SelectCm<Test2Poco, Test2CM>();

		/// <summary>
		/// <para>Database table 'view1'.</para>
		/// </summary>
		public static IQueryable<View1CM> SelectCm(this IQueryable<View1Poco> collection) => collection.SelectCm<View1Poco, View1CM>();

	}

	public class TestDbMetadata : IDbMetadata
	{
		internal static TableMetadataModel<Test1Poco> Test1PocoMetadata;

		internal static TableMetadataModel<Test2Poco> Test2PocoMetadata;

		internal static TableMetadataModel<View1Poco> View1PocoMetadata;

		private static readonly object InitLock = new object();

		private static bool Initialized;

		// ReSharper disable once FunctionComplexityOverflow
		// ReSharper disable once CyclomaticComplexity
		private static void InitializeInternal()
		{
			Test1PocoMetadata = new TableMetadataModel<Test1Poco>
			{
				ClassName = "Test1",
				PluralClassName = "Test1",
				TableName = "test1",
				TableSchema = "public",
				PrimaryKeyColumnName = "test_id",
				PrimaryKeyPropertyName = "TestID",
				GetPrimaryKey = (instance) => instance.TestID,
				SetPrimaryKey = (instance, val) => instance.TestID = val,
				IsNew = (instance) => instance.TestID == default,
				Columns = new List<ColumnMetadataModel>
				{
					new ColumnMetadataModel
					{
						ClrTypeName = "int",
						ClrType = typeof(int),
						ClrNonNullableTypeName = "int",
						ClrNonNullableType = typeof(int),
						ClrNullableTypeName = "int?",
						ClrNullableType = typeof(int?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_id",
						DbDataType = "integer",
						IsPrimaryKey = bool.Parse("True"),
						PrimaryKeyConstraintName = "test1_pkey" == string.Empty ? null : "test1_pkey",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("False"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("False"),
						Linq2dbDataTypeName = "DataType.Int32",
						Linq2dbDataType = DataType.Int32,
						NpgsDataTypeName = "NpgsqlDbType.Integer",
						NpgsDataType = NpgsqlDbType.Integer,
						PropertyName = "TestID",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "string",
						ClrType = typeof(string),
						ClrNonNullableTypeName = "string",
						ClrNonNullableType = typeof(string),
						ClrNullableTypeName = "string",
						ClrNullableType = typeof(string),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_name1",
						DbDataType = "character varying",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("False"),
						IsClrValueType = bool.Parse("False"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.NVarChar",
						Linq2dbDataType = DataType.NVarChar,
						NpgsDataTypeName = "NpgsqlDbType.Varchar",
						NpgsDataType = NpgsqlDbType.Varchar,
						PropertyName = "TestName1",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "string",
						ClrType = typeof(string),
						ClrNonNullableTypeName = "string",
						ClrNonNullableType = typeof(string),
						ClrNullableTypeName = "string",
						ClrNullableType = typeof(string),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_name2",
						DbDataType = "character varying",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("False"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.NVarChar",
						Linq2dbDataType = DataType.NVarChar,
						NpgsDataTypeName = "NpgsqlDbType.Varchar",
						NpgsDataType = NpgsqlDbType.Varchar,
						PropertyName = "TestName2",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "DateTime",
						ClrType = typeof(DateTime),
						ClrNonNullableTypeName = "DateTime",
						ClrNonNullableType = typeof(DateTime),
						ClrNullableTypeName = "DateTime?",
						ClrNullableType = typeof(DateTime?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_date1",
						DbDataType = "date",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("False"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("False"),
						Linq2dbDataTypeName = "DataType.Date",
						Linq2dbDataType = DataType.Date,
						NpgsDataTypeName = "NpgsqlDbType.Date",
						NpgsDataType = NpgsqlDbType.Date,
						PropertyName = "TestDate1",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "DateTime?",
						ClrType = typeof(DateTime?),
						ClrNonNullableTypeName = "DateTime",
						ClrNonNullableType = typeof(DateTime),
						ClrNullableTypeName = "DateTime?",
						ClrNullableType = typeof(DateTime?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_date2",
						DbDataType = "date",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Date",
						Linq2dbDataType = DataType.Date,
						NpgsDataTypeName = "NpgsqlDbType.Date",
						NpgsDataType = NpgsqlDbType.Date,
						PropertyName = "TestDate2",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "DateTime",
						ClrType = typeof(DateTime),
						ClrNonNullableTypeName = "DateTime",
						ClrNonNullableType = typeof(DateTime),
						ClrNullableTypeName = "DateTime?",
						ClrNullableType = typeof(DateTime?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_timestamp1",
						DbDataType = "timestamp without time zone",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("False"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("False"),
						Linq2dbDataTypeName = "DataType.DateTime2",
						Linq2dbDataType = DataType.DateTime2,
						NpgsDataTypeName = "NpgsqlDbType.Timestamp",
						NpgsDataType = NpgsqlDbType.Timestamp,
						PropertyName = "TestTimestamp1",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "DateTime?",
						ClrType = typeof(DateTime?),
						ClrNonNullableTypeName = "DateTime",
						ClrNonNullableType = typeof(DateTime),
						ClrNullableTypeName = "DateTime?",
						ClrNullableType = typeof(DateTime?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_timestamp2",
						DbDataType = "timestamp without time zone",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.DateTime2",
						Linq2dbDataType = DataType.DateTime2,
						NpgsDataTypeName = "NpgsqlDbType.Timestamp",
						NpgsDataType = NpgsqlDbType.Timestamp,
						PropertyName = "TestTimestamp2",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "bool",
						ClrType = typeof(bool),
						ClrNonNullableTypeName = "bool",
						ClrNonNullableType = typeof(bool),
						ClrNullableTypeName = "bool?",
						ClrNullableType = typeof(bool?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_boolean1",
						DbDataType = "boolean",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("False"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("False"),
						Linq2dbDataTypeName = "DataType.Boolean",
						Linq2dbDataType = DataType.Boolean,
						NpgsDataTypeName = "NpgsqlDbType.Boolean",
						NpgsDataType = NpgsqlDbType.Boolean,
						PropertyName = "TestBoolean1",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "bool?",
						ClrType = typeof(bool?),
						ClrNonNullableTypeName = "bool",
						ClrNonNullableType = typeof(bool),
						ClrNullableTypeName = "bool?",
						ClrNullableType = typeof(bool?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_boolean2",
						DbDataType = "boolean",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Boolean",
						Linq2dbDataType = DataType.Boolean,
						NpgsDataTypeName = "NpgsqlDbType.Boolean",
						NpgsDataType = NpgsqlDbType.Boolean,
						PropertyName = "TestBoolean2",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "int?",
						ClrType = typeof(int?),
						ClrNonNullableTypeName = "int",
						ClrNonNullableType = typeof(int),
						ClrNullableTypeName = "int?",
						ClrNullableType = typeof(int?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_integer1",
						DbDataType = "integer",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Int32",
						Linq2dbDataType = DataType.Int32,
						NpgsDataTypeName = "NpgsqlDbType.Integer",
						NpgsDataType = NpgsqlDbType.Integer,
						PropertyName = "TestInteger1",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "int",
						ClrType = typeof(int),
						ClrNonNullableTypeName = "int",
						ClrNonNullableType = typeof(int),
						ClrNullableTypeName = "int?",
						ClrNullableType = typeof(int?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_integer2",
						DbDataType = "integer",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("False"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("False"),
						Linq2dbDataTypeName = "DataType.Int32",
						Linq2dbDataType = DataType.Int32,
						NpgsDataTypeName = "NpgsqlDbType.Integer",
						NpgsDataType = NpgsqlDbType.Integer,
						PropertyName = "TestInteger2",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "long?",
						ClrType = typeof(long?),
						ClrNonNullableTypeName = "long",
						ClrNonNullableType = typeof(long),
						ClrNullableTypeName = "long?",
						ClrNullableType = typeof(long?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_bigint1",
						DbDataType = "bigint",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Int64",
						Linq2dbDataType = DataType.Int64,
						NpgsDataTypeName = "NpgsqlDbType.Bigint",
						NpgsDataType = NpgsqlDbType.Bigint,
						PropertyName = "TestBigint1",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "long",
						ClrType = typeof(long),
						ClrNonNullableTypeName = "long",
						ClrNonNullableType = typeof(long),
						ClrNullableTypeName = "long?",
						ClrNullableType = typeof(long?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_bigint2",
						DbDataType = "bigint",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("False"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("False"),
						Linq2dbDataTypeName = "DataType.Int64",
						Linq2dbDataType = DataType.Int64,
						NpgsDataTypeName = "NpgsqlDbType.Bigint",
						NpgsDataType = NpgsqlDbType.Bigint,
						PropertyName = "TestBigint2",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "string",
						ClrType = typeof(string),
						ClrNonNullableTypeName = "string",
						ClrNonNullableType = typeof(string),
						ClrNullableTypeName = "string",
						ClrNullableType = typeof(string),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_text1",
						DbDataType = "text",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("False"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Text",
						Linq2dbDataType = DataType.Text,
						NpgsDataTypeName = "NpgsqlDbType.Text",
						NpgsDataType = NpgsqlDbType.Text,
						PropertyName = "TestText1",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "string",
						ClrType = typeof(string),
						ClrNonNullableTypeName = "string",
						ClrNonNullableType = typeof(string),
						ClrNullableTypeName = "string",
						ClrNullableType = typeof(string),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_text2",
						DbDataType = "text",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("False"),
						IsClrValueType = bool.Parse("False"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Text",
						Linq2dbDataType = DataType.Text,
						NpgsDataTypeName = "NpgsqlDbType.Text",
						NpgsDataType = NpgsqlDbType.Text,
						PropertyName = "TestText2",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "float?",
						ClrType = typeof(float?),
						ClrNonNullableTypeName = "float",
						ClrNonNullableType = typeof(float),
						ClrNullableTypeName = "float?",
						ClrNullableType = typeof(float?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_real1",
						DbDataType = "real",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Single",
						Linq2dbDataType = DataType.Single,
						NpgsDataTypeName = "NpgsqlDbType.Real",
						NpgsDataType = NpgsqlDbType.Real,
						PropertyName = "TestReal1",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "float",
						ClrType = typeof(float),
						ClrNonNullableTypeName = "float",
						ClrNonNullableType = typeof(float),
						ClrNullableTypeName = "float?",
						ClrNullableType = typeof(float?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_real2",
						DbDataType = "real",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("False"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("False"),
						Linq2dbDataTypeName = "DataType.Single",
						Linq2dbDataType = DataType.Single,
						NpgsDataTypeName = "NpgsqlDbType.Real",
						NpgsDataType = NpgsqlDbType.Real,
						PropertyName = "TestReal2",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "double?",
						ClrType = typeof(double?),
						ClrNonNullableTypeName = "double",
						ClrNonNullableType = typeof(double),
						ClrNullableTypeName = "double?",
						ClrNullableType = typeof(double?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_double1",
						DbDataType = "double precision",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Double",
						Linq2dbDataType = DataType.Double,
						NpgsDataTypeName = "NpgsqlDbType.Double",
						NpgsDataType = NpgsqlDbType.Double,
						PropertyName = "TestDouble1",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "double",
						ClrType = typeof(double),
						ClrNonNullableTypeName = "double",
						ClrNonNullableType = typeof(double),
						ClrNullableTypeName = "double?",
						ClrNullableType = typeof(double?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_double2",
						DbDataType = "double precision",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("False"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("False"),
						Linq2dbDataTypeName = "DataType.Double",
						Linq2dbDataType = DataType.Double,
						NpgsDataTypeName = "NpgsqlDbType.Double",
						NpgsDataType = NpgsqlDbType.Double,
						PropertyName = "TestDouble2",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "string",
						ClrType = typeof(string),
						ClrNonNullableTypeName = "string",
						ClrNonNullableType = typeof(string),
						ClrNullableTypeName = "string",
						ClrNullableType = typeof(string),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_char1",
						DbDataType = "character",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("False"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.NChar",
						Linq2dbDataType = DataType.NChar,
						NpgsDataTypeName = "NpgsqlDbType.Char",
						NpgsDataType = NpgsqlDbType.Char,
						PropertyName = "TestChar1",
						TableName = "test1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "string",
						ClrType = typeof(string),
						ClrNonNullableTypeName = "string",
						ClrNonNullableType = typeof(string),
						ClrNullableTypeName = "string",
						ClrNullableType = typeof(string),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_char2",
						DbDataType = "character",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("False"),
						IsClrValueType = bool.Parse("False"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.NChar",
						Linq2dbDataType = DataType.NChar,
						NpgsDataTypeName = "NpgsqlDbType.Char",
						NpgsDataType = NpgsqlDbType.Char,
						PropertyName = "TestChar2",
						TableName = "test1",
						TableSchema = "public",
					},
				}
			};

			Test1PocoMetadata.Clone = DbCodeGenerator.GetClone<Test1Poco>();
			Test1PocoMetadata.GenerateParameters = DbCodeGenerator.GetGenerateParameters(Test1PocoMetadata);
			Test1PocoMetadata.GetColumnChanges = DbCodeGenerator.GetGetColumnChanges(Test1PocoMetadata);
			Test1PocoMetadata.GetAllColumns = DbCodeGenerator.GetGetAllColumns(Test1PocoMetadata);
			Test1PocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(Test1PocoMetadata, typeof(Test1FM));

			Test2PocoMetadata = new TableMetadataModel<Test2Poco>
			{
				ClassName = "Test2",
				PluralClassName = "Test2",
				TableName = "test2",
				TableSchema = "public",
				PrimaryKeyColumnName = "test_id",
				PrimaryKeyPropertyName = "TestID",
				GetPrimaryKey = (instance) => instance.TestID,
				SetPrimaryKey = (instance, val) => instance.TestID = val,
				IsNew = (instance) => instance.TestID == default,
				Columns = new List<ColumnMetadataModel>
				{
					new ColumnMetadataModel
					{
						ClrTypeName = "int",
						ClrType = typeof(int),
						ClrNonNullableTypeName = "int",
						ClrNonNullableType = typeof(int),
						ClrNullableTypeName = "int?",
						ClrNullableType = typeof(int?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_id",
						DbDataType = "integer",
						IsPrimaryKey = bool.Parse("True"),
						PrimaryKeyConstraintName = "test2_pkey" == string.Empty ? null : "test2_pkey",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("False"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("False"),
						Linq2dbDataTypeName = "DataType.Int32",
						Linq2dbDataType = DataType.Int32,
						NpgsDataTypeName = "NpgsqlDbType.Integer",
						NpgsDataType = NpgsqlDbType.Integer,
						PropertyName = "TestID",
						TableName = "test2",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "string",
						ClrType = typeof(string),
						ClrNonNullableTypeName = "string",
						ClrNonNullableType = typeof(string),
						ClrNullableTypeName = "string",
						ClrNullableType = typeof(string),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_name",
						DbDataType = "text",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("False"),
						IsClrValueType = bool.Parse("False"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Text",
						Linq2dbDataType = DataType.Text,
						NpgsDataTypeName = "NpgsqlDbType.Text",
						NpgsDataType = NpgsqlDbType.Text,
						PropertyName = "TestName",
						TableName = "test2",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "DateTime",
						ClrType = typeof(DateTime),
						ClrNonNullableTypeName = "DateTime",
						ClrNonNullableType = typeof(DateTime),
						ClrNullableTypeName = "DateTime?",
						ClrNullableType = typeof(DateTime?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_date",
						DbDataType = "timestamp without time zone",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("False"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("False"),
						Linq2dbDataTypeName = "DataType.DateTime2",
						Linq2dbDataType = DataType.DateTime2,
						NpgsDataTypeName = "NpgsqlDbType.Timestamp",
						NpgsDataType = NpgsqlDbType.Timestamp,
						PropertyName = "TestDate",
						TableName = "test2",
						TableSchema = "public",
					},
				}
			};

			Test2PocoMetadata.Clone = DbCodeGenerator.GetClone<Test2Poco>();
			Test2PocoMetadata.GenerateParameters = DbCodeGenerator.GetGenerateParameters(Test2PocoMetadata);
			Test2PocoMetadata.GetColumnChanges = DbCodeGenerator.GetGetColumnChanges(Test2PocoMetadata);
			Test2PocoMetadata.GetAllColumns = DbCodeGenerator.GetGetAllColumns(Test2PocoMetadata);
			Test2PocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(Test2PocoMetadata, typeof(Test2FM));

			View1PocoMetadata = new TableMetadataModel<View1Poco>
			{
				ClassName = "View1",
				PluralClassName = "View1",
				TableName = "view1",
				TableSchema = "public",
				Columns = new List<ColumnMetadataModel>
				{
					new ColumnMetadataModel
					{
						ClrTypeName = "string",
						ClrType = typeof(string),
						ClrNonNullableTypeName = "string",
						ClrNonNullableType = typeof(string),
						ClrNullableTypeName = "string",
						ClrNullableType = typeof(string),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_name1",
						DbDataType = "character varying",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("False"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.NVarChar",
						Linq2dbDataType = DataType.NVarChar,
						NpgsDataTypeName = "NpgsqlDbType.Varchar",
						NpgsDataType = NpgsqlDbType.Varchar,
						PropertyName = "TestName1",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "string",
						ClrType = typeof(string),
						ClrNonNullableTypeName = "string",
						ClrNonNullableType = typeof(string),
						ClrNullableTypeName = "string",
						ClrNullableType = typeof(string),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_name2",
						DbDataType = "character varying",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("False"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.NVarChar",
						Linq2dbDataType = DataType.NVarChar,
						NpgsDataTypeName = "NpgsqlDbType.Varchar",
						NpgsDataType = NpgsqlDbType.Varchar,
						PropertyName = "TestName2",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "DateTime?",
						ClrType = typeof(DateTime?),
						ClrNonNullableTypeName = "DateTime",
						ClrNonNullableType = typeof(DateTime),
						ClrNullableTypeName = "DateTime?",
						ClrNullableType = typeof(DateTime?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_date1",
						DbDataType = "date",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Date",
						Linq2dbDataType = DataType.Date,
						NpgsDataTypeName = "NpgsqlDbType.Date",
						NpgsDataType = NpgsqlDbType.Date,
						PropertyName = "TestDate1",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "DateTime?",
						ClrType = typeof(DateTime?),
						ClrNonNullableTypeName = "DateTime",
						ClrNonNullableType = typeof(DateTime),
						ClrNullableTypeName = "DateTime?",
						ClrNullableType = typeof(DateTime?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_date2",
						DbDataType = "date",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Date",
						Linq2dbDataType = DataType.Date,
						NpgsDataTypeName = "NpgsqlDbType.Date",
						NpgsDataType = NpgsqlDbType.Date,
						PropertyName = "TestDate2",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "DateTime?",
						ClrType = typeof(DateTime?),
						ClrNonNullableTypeName = "DateTime",
						ClrNonNullableType = typeof(DateTime),
						ClrNullableTypeName = "DateTime?",
						ClrNullableType = typeof(DateTime?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_timestamp1",
						DbDataType = "timestamp without time zone",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.DateTime2",
						Linq2dbDataType = DataType.DateTime2,
						NpgsDataTypeName = "NpgsqlDbType.Timestamp",
						NpgsDataType = NpgsqlDbType.Timestamp,
						PropertyName = "TestTimestamp1",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "DateTime?",
						ClrType = typeof(DateTime?),
						ClrNonNullableTypeName = "DateTime",
						ClrNonNullableType = typeof(DateTime),
						ClrNullableTypeName = "DateTime?",
						ClrNullableType = typeof(DateTime?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_timestamp2",
						DbDataType = "timestamp without time zone",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.DateTime2",
						Linq2dbDataType = DataType.DateTime2,
						NpgsDataTypeName = "NpgsqlDbType.Timestamp",
						NpgsDataType = NpgsqlDbType.Timestamp,
						PropertyName = "TestTimestamp2",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "bool?",
						ClrType = typeof(bool?),
						ClrNonNullableTypeName = "bool",
						ClrNonNullableType = typeof(bool),
						ClrNullableTypeName = "bool?",
						ClrNullableType = typeof(bool?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_boolean1",
						DbDataType = "boolean",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Boolean",
						Linq2dbDataType = DataType.Boolean,
						NpgsDataTypeName = "NpgsqlDbType.Boolean",
						NpgsDataType = NpgsqlDbType.Boolean,
						PropertyName = "TestBoolean1",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "bool?",
						ClrType = typeof(bool?),
						ClrNonNullableTypeName = "bool",
						ClrNonNullableType = typeof(bool),
						ClrNullableTypeName = "bool?",
						ClrNullableType = typeof(bool?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_boolean2",
						DbDataType = "boolean",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Boolean",
						Linq2dbDataType = DataType.Boolean,
						NpgsDataTypeName = "NpgsqlDbType.Boolean",
						NpgsDataType = NpgsqlDbType.Boolean,
						PropertyName = "TestBoolean2",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "int?",
						ClrType = typeof(int?),
						ClrNonNullableTypeName = "int",
						ClrNonNullableType = typeof(int),
						ClrNullableTypeName = "int?",
						ClrNullableType = typeof(int?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_integer1",
						DbDataType = "integer",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Int32",
						Linq2dbDataType = DataType.Int32,
						NpgsDataTypeName = "NpgsqlDbType.Integer",
						NpgsDataType = NpgsqlDbType.Integer,
						PropertyName = "TestInteger1",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "int?",
						ClrType = typeof(int?),
						ClrNonNullableTypeName = "int",
						ClrNonNullableType = typeof(int),
						ClrNullableTypeName = "int?",
						ClrNullableType = typeof(int?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_integer2",
						DbDataType = "integer",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Int32",
						Linq2dbDataType = DataType.Int32,
						NpgsDataTypeName = "NpgsqlDbType.Integer",
						NpgsDataType = NpgsqlDbType.Integer,
						PropertyName = "TestInteger2",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "long?",
						ClrType = typeof(long?),
						ClrNonNullableTypeName = "long",
						ClrNonNullableType = typeof(long),
						ClrNullableTypeName = "long?",
						ClrNullableType = typeof(long?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_bigint1",
						DbDataType = "bigint",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Int64",
						Linq2dbDataType = DataType.Int64,
						NpgsDataTypeName = "NpgsqlDbType.Bigint",
						NpgsDataType = NpgsqlDbType.Bigint,
						PropertyName = "TestBigint1",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "long?",
						ClrType = typeof(long?),
						ClrNonNullableTypeName = "long",
						ClrNonNullableType = typeof(long),
						ClrNullableTypeName = "long?",
						ClrNullableType = typeof(long?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_bigint2",
						DbDataType = "bigint",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Int64",
						Linq2dbDataType = DataType.Int64,
						NpgsDataTypeName = "NpgsqlDbType.Bigint",
						NpgsDataType = NpgsqlDbType.Bigint,
						PropertyName = "TestBigint2",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "string",
						ClrType = typeof(string),
						ClrNonNullableTypeName = "string",
						ClrNonNullableType = typeof(string),
						ClrNullableTypeName = "string",
						ClrNullableType = typeof(string),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_text1",
						DbDataType = "text",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("False"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Text",
						Linq2dbDataType = DataType.Text,
						NpgsDataTypeName = "NpgsqlDbType.Text",
						NpgsDataType = NpgsqlDbType.Text,
						PropertyName = "TestText1",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "string",
						ClrType = typeof(string),
						ClrNonNullableTypeName = "string",
						ClrNonNullableType = typeof(string),
						ClrNullableTypeName = "string",
						ClrNullableType = typeof(string),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_text2",
						DbDataType = "text",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("False"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Text",
						Linq2dbDataType = DataType.Text,
						NpgsDataTypeName = "NpgsqlDbType.Text",
						NpgsDataType = NpgsqlDbType.Text,
						PropertyName = "TestText2",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "float?",
						ClrType = typeof(float?),
						ClrNonNullableTypeName = "float",
						ClrNonNullableType = typeof(float),
						ClrNullableTypeName = "float?",
						ClrNullableType = typeof(float?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_real1",
						DbDataType = "real",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Single",
						Linq2dbDataType = DataType.Single,
						NpgsDataTypeName = "NpgsqlDbType.Real",
						NpgsDataType = NpgsqlDbType.Real,
						PropertyName = "TestReal1",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "float?",
						ClrType = typeof(float?),
						ClrNonNullableTypeName = "float",
						ClrNonNullableType = typeof(float),
						ClrNullableTypeName = "float?",
						ClrNullableType = typeof(float?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_real2",
						DbDataType = "real",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Single",
						Linq2dbDataType = DataType.Single,
						NpgsDataTypeName = "NpgsqlDbType.Real",
						NpgsDataType = NpgsqlDbType.Real,
						PropertyName = "TestReal2",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "double?",
						ClrType = typeof(double?),
						ClrNonNullableTypeName = "double",
						ClrNonNullableType = typeof(double),
						ClrNullableTypeName = "double?",
						ClrNullableType = typeof(double?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_double1",
						DbDataType = "double precision",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Double",
						Linq2dbDataType = DataType.Double,
						NpgsDataTypeName = "NpgsqlDbType.Double",
						NpgsDataType = NpgsqlDbType.Double,
						PropertyName = "TestDouble1",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "double?",
						ClrType = typeof(double?),
						ClrNonNullableTypeName = "double",
						ClrNonNullableType = typeof(double),
						ClrNullableTypeName = "double?",
						ClrNullableType = typeof(double?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_double2",
						DbDataType = "double precision",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Double",
						Linq2dbDataType = DataType.Double,
						NpgsDataTypeName = "NpgsqlDbType.Double",
						NpgsDataType = NpgsqlDbType.Double,
						PropertyName = "TestDouble2",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "string",
						ClrType = typeof(string),
						ClrNonNullableTypeName = "string",
						ClrNonNullableType = typeof(string),
						ClrNullableTypeName = "string",
						ClrNullableType = typeof(string),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_char1",
						DbDataType = "character",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("False"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.NChar",
						Linq2dbDataType = DataType.NChar,
						NpgsDataTypeName = "NpgsqlDbType.Char",
						NpgsDataType = NpgsqlDbType.Char,
						PropertyName = "TestChar1",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "string",
						ClrType = typeof(string),
						ClrNonNullableTypeName = "string",
						ClrNonNullableType = typeof(string),
						ClrNullableTypeName = "string",
						ClrNullableType = typeof(string),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_char2",
						DbDataType = "character",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("False"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.NChar",
						Linq2dbDataType = DataType.NChar,
						NpgsDataTypeName = "NpgsqlDbType.Char",
						NpgsDataType = NpgsqlDbType.Char,
						PropertyName = "TestChar2",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "string",
						ClrType = typeof(string),
						ClrNonNullableTypeName = "string",
						ClrNonNullableType = typeof(string),
						ClrNullableTypeName = "string",
						ClrNullableType = typeof(string),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_name",
						DbDataType = "text",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("False"),
						IsClrNullableType = bool.Parse("False"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.Text",
						Linq2dbDataType = DataType.Text,
						NpgsDataTypeName = "NpgsqlDbType.Text",
						NpgsDataType = NpgsqlDbType.Text,
						PropertyName = "TestName",
						TableName = "view1",
						TableSchema = "public",
					},
					new ColumnMetadataModel
					{
						ClrTypeName = "DateTime?",
						ClrType = typeof(DateTime?),
						ClrNonNullableTypeName = "DateTime",
						ClrNonNullableType = typeof(DateTime),
						ClrNullableTypeName = "DateTime?",
						ClrNullableType = typeof(DateTime?),
						ColumnComment = "" == string.Empty ? null : "",
						Comments = "".Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
						ColumnName = "test_date",
						DbDataType = "timestamp without time zone",
						IsPrimaryKey = bool.Parse("False"),
						PrimaryKeyConstraintName = "" == string.Empty ? null : "",
						IsForeignKey = bool.Parse("False"),
						ForeignKeyConstraintName = "" == string.Empty ? null : "",
						ForeignKeyReferenceColumnName = "" == string.Empty ? null : "",
						ForeignKeyReferenceSchemaName = "" == string.Empty ? null : "",
						ForeignKeyReferenceTableName = "" == string.Empty ? null : "",
						IsNullable = bool.Parse("True"),
						IsClrValueType = bool.Parse("True"),
						IsClrNullableType = bool.Parse("True"),
						IsClrReferenceType = bool.Parse("True"),
						Linq2dbDataTypeName = "DataType.DateTime2",
						Linq2dbDataType = DataType.DateTime2,
						NpgsDataTypeName = "NpgsqlDbType.Timestamp",
						NpgsDataType = NpgsqlDbType.Timestamp,
						PropertyName = "TestDate",
						TableName = "view1",
						TableSchema = "public",
					},
				}
			};

			View1PocoMetadata.Clone = DbCodeGenerator.GetClone<View1Poco>();
			View1PocoMetadata.ParseFm = DbCodeGenerator.GetParseFm(View1PocoMetadata, typeof(View1FM));

		}

		public static void Initialize()
		{
			if(Initialized)
			{
				return;
			}

			lock(InitLock)
			{
				if(Initialized)
				{
					return;
				}

				InitializeInternal();

				Initialized = true;
			}
		}

		static TestDbMetadata()
		{
			Initialize();
		}
	}
}

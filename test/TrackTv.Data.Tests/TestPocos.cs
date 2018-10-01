namespace TrackTv.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Linq;
	using LinqToDB;
    using LinqToDB.Mapping;

	using NpgsqlTypes;
	using Npgsql;

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

		TableMetadataModel<Test1Poco> IPoco<Test1Poco>.Metadata => TestDbPocos.Test1PocoMetadata;

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
    /// <para>Table name: 'test1'.</para>
	/// <para>Table schema: 'public'.</para>
    /// </summary>
    public class Test1FM : IFilterModel<Test1Poco>
    {
		[FilterOperator(QueryOperatorType.Equal, "TestID", NpgsqlDbType.Integer)]
        public int? TestID { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestID", NpgsqlDbType.Integer)]
        public int? TestID_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan, "TestID", NpgsqlDbType.Integer)]
        public int? TestID_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual, "TestID", NpgsqlDbType.Integer)]
        public int? TestID_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan, "TestID", NpgsqlDbType.Integer)]
        public int? TestID_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual, "TestID", NpgsqlDbType.Integer)]
        public int? TestID_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestID", NpgsqlDbType.Integer)]
		public int[] TestID_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestID", NpgsqlDbType.Integer)]
		public int[] TestID_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestName1", NpgsqlDbType.Varchar)]
        public string TestName1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestName1", NpgsqlDbType.Varchar)]
        public string TestName1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestName1", NpgsqlDbType.Varchar)]
        public string TestName1_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestName1", NpgsqlDbType.Varchar)]
        public string TestName1_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestName1", NpgsqlDbType.Varchar)]
        public string TestName1_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestName1", NpgsqlDbType.Varchar)]
        public string TestName1_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestName1", NpgsqlDbType.Varchar)]
        public string TestName1_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestName1", NpgsqlDbType.Varchar)]
        public string TestName1_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestName1", NpgsqlDbType.Varchar)]
		public string[] TestName1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestName1", NpgsqlDbType.Varchar)]
		public string[] TestName1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestName2", NpgsqlDbType.Varchar)]
        public string TestName2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestName2", NpgsqlDbType.Varchar)]
        public string TestName2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestName2", NpgsqlDbType.Varchar)]
        public string TestName2_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestName2", NpgsqlDbType.Varchar)]
        public string TestName2_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestName2", NpgsqlDbType.Varchar)]
        public string TestName2_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestName2", NpgsqlDbType.Varchar)]
        public string TestName2_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestName2", NpgsqlDbType.Varchar)]
        public string TestName2_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestName2", NpgsqlDbType.Varchar)]
        public string TestName2_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestName2", NpgsqlDbType.Varchar)]
		public bool? TestName2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestName2", NpgsqlDbType.Varchar)]
		public bool? TestName2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestName2", NpgsqlDbType.Varchar)]
		public string[] TestName2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestName2", NpgsqlDbType.Varchar)]
		public string[] TestName2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestDate1", NpgsqlDbType.Date)]
        public DateTime? TestDate1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestDate1", NpgsqlDbType.Date)]
        public DateTime? TestDate1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan, "TestDate1", NpgsqlDbType.Date)]
        public DateTime? TestDate1_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual, "TestDate1", NpgsqlDbType.Date)]
        public DateTime? TestDate1_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan, "TestDate1", NpgsqlDbType.Date)]
        public DateTime? TestDate1_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual, "TestDate1", NpgsqlDbType.Date)]
        public DateTime? TestDate1_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestDate1", NpgsqlDbType.Date)]
		public DateTime[] TestDate1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestDate1", NpgsqlDbType.Date)]
		public DateTime[] TestDate1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestDate2", NpgsqlDbType.Date)]
        public DateTime? TestDate2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestDate2", NpgsqlDbType.Date)]
        public DateTime? TestDate2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestDate2", NpgsqlDbType.Date)]
		public bool? TestDate2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestDate2", NpgsqlDbType.Date)]
		public bool? TestDate2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestDate2", NpgsqlDbType.Date)]
		public DateTime[] TestDate2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestDate2", NpgsqlDbType.Date)]
		public DateTime[] TestDate2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestTimestamp1", NpgsqlDbType.Timestamp)]
        public DateTime? TestTimestamp1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestTimestamp1", NpgsqlDbType.Timestamp)]
        public DateTime? TestTimestamp1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan, "TestTimestamp1", NpgsqlDbType.Timestamp)]
        public DateTime? TestTimestamp1_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual, "TestTimestamp1", NpgsqlDbType.Timestamp)]
        public DateTime? TestTimestamp1_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan, "TestTimestamp1", NpgsqlDbType.Timestamp)]
        public DateTime? TestTimestamp1_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual, "TestTimestamp1", NpgsqlDbType.Timestamp)]
        public DateTime? TestTimestamp1_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestTimestamp1", NpgsqlDbType.Timestamp)]
		public DateTime[] TestTimestamp1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestTimestamp1", NpgsqlDbType.Timestamp)]
		public DateTime[] TestTimestamp1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestTimestamp2", NpgsqlDbType.Timestamp)]
        public DateTime? TestTimestamp2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestTimestamp2", NpgsqlDbType.Timestamp)]
        public DateTime? TestTimestamp2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestTimestamp2", NpgsqlDbType.Timestamp)]
		public bool? TestTimestamp2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestTimestamp2", NpgsqlDbType.Timestamp)]
		public bool? TestTimestamp2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestTimestamp2", NpgsqlDbType.Timestamp)]
		public DateTime[] TestTimestamp2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestTimestamp2", NpgsqlDbType.Timestamp)]
		public DateTime[] TestTimestamp2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestBoolean1", NpgsqlDbType.Boolean)]
        public bool? TestBoolean1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestBoolean1", NpgsqlDbType.Boolean)]
        public bool? TestBoolean1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestBoolean1", NpgsqlDbType.Boolean)]
		public bool[] TestBoolean1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestBoolean1", NpgsqlDbType.Boolean)]
		public bool[] TestBoolean1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestBoolean2", NpgsqlDbType.Boolean)]
        public bool? TestBoolean2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestBoolean2", NpgsqlDbType.Boolean)]
        public bool? TestBoolean2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestBoolean2", NpgsqlDbType.Boolean)]
		public bool? TestBoolean2_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestBoolean2", NpgsqlDbType.Boolean)]
		public bool? TestBoolean2_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestBoolean2", NpgsqlDbType.Boolean)]
		public bool[] TestBoolean2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestBoolean2", NpgsqlDbType.Boolean)]
		public bool[] TestBoolean2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestInteger1", NpgsqlDbType.Integer)]
        public int? TestInteger1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestInteger1", NpgsqlDbType.Integer)]
        public int? TestInteger1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestInteger1", NpgsqlDbType.Integer)]
		public bool? TestInteger1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestInteger1", NpgsqlDbType.Integer)]
		public bool? TestInteger1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestInteger1", NpgsqlDbType.Integer)]
		public int[] TestInteger1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestInteger1", NpgsqlDbType.Integer)]
		public int[] TestInteger1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestInteger2", NpgsqlDbType.Integer)]
        public int? TestInteger2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestInteger2", NpgsqlDbType.Integer)]
        public int? TestInteger2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan, "TestInteger2", NpgsqlDbType.Integer)]
        public int? TestInteger2_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual, "TestInteger2", NpgsqlDbType.Integer)]
        public int? TestInteger2_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan, "TestInteger2", NpgsqlDbType.Integer)]
        public int? TestInteger2_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual, "TestInteger2", NpgsqlDbType.Integer)]
        public int? TestInteger2_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestInteger2", NpgsqlDbType.Integer)]
		public int[] TestInteger2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestInteger2", NpgsqlDbType.Integer)]
		public int[] TestInteger2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestBigint1", NpgsqlDbType.Bigint)]
        public long? TestBigint1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestBigint1", NpgsqlDbType.Bigint)]
        public long? TestBigint1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestBigint1", NpgsqlDbType.Bigint)]
		public bool? TestBigint1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestBigint1", NpgsqlDbType.Bigint)]
		public bool? TestBigint1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestBigint1", NpgsqlDbType.Bigint)]
		public long[] TestBigint1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestBigint1", NpgsqlDbType.Bigint)]
		public long[] TestBigint1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestBigint2", NpgsqlDbType.Bigint)]
        public long? TestBigint2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestBigint2", NpgsqlDbType.Bigint)]
        public long? TestBigint2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan, "TestBigint2", NpgsqlDbType.Bigint)]
        public long? TestBigint2_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual, "TestBigint2", NpgsqlDbType.Bigint)]
        public long? TestBigint2_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan, "TestBigint2", NpgsqlDbType.Bigint)]
        public long? TestBigint2_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual, "TestBigint2", NpgsqlDbType.Bigint)]
        public long? TestBigint2_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestBigint2", NpgsqlDbType.Bigint)]
		public long[] TestBigint2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestBigint2", NpgsqlDbType.Bigint)]
		public long[] TestBigint2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestText1", NpgsqlDbType.Text)]
        public string TestText1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestText1", NpgsqlDbType.Text)]
        public string TestText1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestText1", NpgsqlDbType.Text)]
        public string TestText1_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestText1", NpgsqlDbType.Text)]
        public string TestText1_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestText1", NpgsqlDbType.Text)]
        public string TestText1_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestText1", NpgsqlDbType.Text)]
        public string TestText1_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestText1", NpgsqlDbType.Text)]
        public string TestText1_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestText1", NpgsqlDbType.Text)]
        public string TestText1_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestText1", NpgsqlDbType.Text)]
		public bool? TestText1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestText1", NpgsqlDbType.Text)]
		public bool? TestText1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestText1", NpgsqlDbType.Text)]
		public string[] TestText1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestText1", NpgsqlDbType.Text)]
		public string[] TestText1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestText2", NpgsqlDbType.Text)]
        public string TestText2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestText2", NpgsqlDbType.Text)]
        public string TestText2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestText2", NpgsqlDbType.Text)]
        public string TestText2_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestText2", NpgsqlDbType.Text)]
        public string TestText2_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestText2", NpgsqlDbType.Text)]
        public string TestText2_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestText2", NpgsqlDbType.Text)]
        public string TestText2_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestText2", NpgsqlDbType.Text)]
        public string TestText2_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestText2", NpgsqlDbType.Text)]
        public string TestText2_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestText2", NpgsqlDbType.Text)]
		public string[] TestText2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestText2", NpgsqlDbType.Text)]
		public string[] TestText2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestReal1", NpgsqlDbType.Real)]
        public float? TestReal1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestReal1", NpgsqlDbType.Real)]
        public float? TestReal1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestReal1", NpgsqlDbType.Real)]
		public bool? TestReal1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestReal1", NpgsqlDbType.Real)]
		public bool? TestReal1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestReal1", NpgsqlDbType.Real)]
		public float[] TestReal1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestReal1", NpgsqlDbType.Real)]
		public float[] TestReal1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestReal2", NpgsqlDbType.Real)]
        public float? TestReal2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestReal2", NpgsqlDbType.Real)]
        public float? TestReal2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan, "TestReal2", NpgsqlDbType.Real)]
        public float? TestReal2_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual, "TestReal2", NpgsqlDbType.Real)]
        public float? TestReal2_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan, "TestReal2", NpgsqlDbType.Real)]
        public float? TestReal2_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual, "TestReal2", NpgsqlDbType.Real)]
        public float? TestReal2_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestReal2", NpgsqlDbType.Real)]
		public float[] TestReal2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestReal2", NpgsqlDbType.Real)]
		public float[] TestReal2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestDouble1", NpgsqlDbType.Double)]
        public double? TestDouble1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestDouble1", NpgsqlDbType.Double)]
        public double? TestDouble1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestDouble1", NpgsqlDbType.Double)]
		public bool? TestDouble1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestDouble1", NpgsqlDbType.Double)]
		public bool? TestDouble1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestDouble1", NpgsqlDbType.Double)]
		public double[] TestDouble1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestDouble1", NpgsqlDbType.Double)]
		public double[] TestDouble1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestDouble2", NpgsqlDbType.Double)]
        public double? TestDouble2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestDouble2", NpgsqlDbType.Double)]
        public double? TestDouble2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.LessThan, "TestDouble2", NpgsqlDbType.Double)]
        public double? TestDouble2_LessThan { get; set; }

		[FilterOperator(QueryOperatorType.LessThanOrEqual, "TestDouble2", NpgsqlDbType.Double)]
        public double? TestDouble2_LessThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThan, "TestDouble2", NpgsqlDbType.Double)]
        public double? TestDouble2_GreaterThan { get; set; }

		[FilterOperator(QueryOperatorType.GreaterThanOrEqual, "TestDouble2", NpgsqlDbType.Double)]
        public double? TestDouble2_GreaterThanOrEqual { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestDouble2", NpgsqlDbType.Double)]
		public double[] TestDouble2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestDouble2", NpgsqlDbType.Double)]
		public double[] TestDouble2_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestChar1", NpgsqlDbType.Char)]
        public string TestChar1 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestChar1", NpgsqlDbType.Char)]
        public string TestChar1_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestChar1", NpgsqlDbType.Char)]
        public string TestChar1_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestChar1", NpgsqlDbType.Char)]
        public string TestChar1_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestChar1", NpgsqlDbType.Char)]
        public string TestChar1_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestChar1", NpgsqlDbType.Char)]
        public string TestChar1_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestChar1", NpgsqlDbType.Char)]
        public string TestChar1_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestChar1", NpgsqlDbType.Char)]
        public string TestChar1_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsNull, "TestChar1", NpgsqlDbType.Char)]
		public bool? TestChar1_IsNull { get; set; }

		[FilterOperator(QueryOperatorType.IsNotNull, "TestChar1", NpgsqlDbType.Char)]
		public bool? TestChar1_IsNotNull { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestChar1", NpgsqlDbType.Char)]
		public string[] TestChar1_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestChar1", NpgsqlDbType.Char)]
		public string[] TestChar1_IsNotIn { get; set; }

		[FilterOperator(QueryOperatorType.Equal, "TestChar2", NpgsqlDbType.Char)]
        public string TestChar2 { get; set; }

		[FilterOperator(QueryOperatorType.NotEqual, "TestChar2", NpgsqlDbType.Char)]
        public string TestChar2_NotEqual { get; set; }

		[FilterOperator(QueryOperatorType.StartsWith, "TestChar2", NpgsqlDbType.Char)]
        public string TestChar2_StartsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotStartWith, "TestChar2", NpgsqlDbType.Char)]
        public string TestChar2_DoesNotStartWith { get; set; }

		[FilterOperator(QueryOperatorType.EndsWith, "TestChar2", NpgsqlDbType.Char)]
        public string TestChar2_EndsWith { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotEndWith, "TestChar2", NpgsqlDbType.Char)]
        public string TestChar2_DoesNotEndWith { get; set; }

		[FilterOperator(QueryOperatorType.Contains, "TestChar2", NpgsqlDbType.Char)]
        public string TestChar2_Contains { get; set; }

		[FilterOperator(QueryOperatorType.DoesNotContain, "TestChar2", NpgsqlDbType.Char)]
        public string TestChar2_DoesNotContain { get; set; }

		[FilterOperator(QueryOperatorType.IsIn, "TestChar2", NpgsqlDbType.Char)]
		public string[] TestChar2_IsIn { get; set; }

		[FilterOperator(QueryOperatorType.IsNotIn, "TestChar2", NpgsqlDbType.Char)]
		public string[] TestChar2_IsNotIn { get; set; }

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
    
    public class TestDbPocos : IDbPocos<TestDbPocos>
    {
		private static IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> TableToPropertyMap;

        internal static TableMetadataModel<Test1Poco> Test1PocoMetadata;
		
		private static IReadOnlyDictionary<Type, object> StaticMetadataByPocoType;

		private static volatile object InitLock = new object();

		private static bool Initialized;

        // ReSharper disable once FunctionComplexityOverflow
        // ReSharper disable once CyclomaticComplexity
		private static void InitializeInternal()
		{
			TableToPropertyMap = new Dictionary<string, IReadOnlyDictionary<string, string>>
			{
				{"test1", new Dictionary<string, string>
				{
					{"test_id", "TestID"},
					{"test_name1", "TestName1"},
					{"test_name2", "TestName2"},
					{"test_date1", "TestDate1"},
					{"test_date2", "TestDate2"},
					{"test_timestamp1", "TestTimestamp1"},
					{"test_timestamp2", "TestTimestamp2"},
					{"test_boolean1", "TestBoolean1"},
					{"test_boolean2", "TestBoolean2"},
					{"test_integer1", "TestInteger1"},
					{"test_integer2", "TestInteger2"},
					{"test_bigint1", "TestBigint1"},
					{"test_bigint2", "TestBigint2"},
					{"test_text1", "TestText1"},
					{"test_text2", "TestText2"},
					{"test_real1", "TestReal1"},
					{"test_real2", "TestReal2"},
					{"test_double1", "TestDouble1"},
					{"test_double2", "TestDouble2"},
					{"test_char1", "TestChar1"},
					{"test_char2", "TestChar2"},
				}},
			};

			Test1PocoMetadata = new TableMetadataModel<Test1Poco>
			{
				ClassName = "Test1",
				PluralClassName = "Test1",
				PrimaryKeyColumnName = "test_id",
				PrimaryKeyPropertyName = "TestID",
				TableName = "test1",
				TableSchema = "public",
				GetPrimaryKey = (instance) => instance.TestID,
				SetPrimaryKey = (instance, val) => instance.TestID = val,
				IsNew = (instance) => instance.TestID == default,
				Clone = DbServiceHelpers.GetClone<Test1Poco>(),
				MapToCM = DbServiceHelpers.GetMapToCM<Test1Poco, Test1CM>(),
				Setters = DbServiceHelpers.GetSetters<Test1Poco>(TableToPropertyMap["test1"]),
				Getters = DbServiceHelpers.GetGetters<Test1Poco>(TableToPropertyMap["test1"]),
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
			
			Test1PocoMetadata.GenerateParameters = DbServiceHelpers.GetGenerateParameters(Test1PocoMetadata);			

			Test1PocoMetadata.GetColumnChanges = (dbInstance, myInstance) =>
			{
				var changedColumnNames = new List<string>();
				var changedColumnParameters = new List<NpgsqlParameter>();

				if(dbInstance.TestName1 != myInstance.TestName1)
				{
					changedColumnNames.Add("test_name1");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.TestName1 ?? (object)DBNull.Value });
				}

				if(dbInstance.TestName2 != myInstance.TestName2)
				{
					changedColumnNames.Add("test_name2");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = myInstance.TestName2 ?? (object)DBNull.Value });
				}

				if(dbInstance.TestDate1 != myInstance.TestDate1)
				{
					changedColumnNames.Add("test_date1");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Date) { Value = myInstance.TestDate1 });
				}

				if(dbInstance.TestDate2 != myInstance.TestDate2)
				{
					changedColumnNames.Add("test_date2");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Date) { Value = myInstance.TestDate2 ?? (object)DBNull.Value });
				}

				if(dbInstance.TestTimestamp1 != myInstance.TestTimestamp1)
				{
					changedColumnNames.Add("test_timestamp1");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = myInstance.TestTimestamp1 });
				}

				if(dbInstance.TestTimestamp2 != myInstance.TestTimestamp2)
				{
					changedColumnNames.Add("test_timestamp2");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = myInstance.TestTimestamp2 ?? (object)DBNull.Value });
				}

				if(dbInstance.TestBoolean1 != myInstance.TestBoolean1)
				{
					changedColumnNames.Add("test_boolean1");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Boolean) { Value = myInstance.TestBoolean1 });
				}

				if(dbInstance.TestBoolean2 != myInstance.TestBoolean2)
				{
					changedColumnNames.Add("test_boolean2");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Boolean) { Value = myInstance.TestBoolean2 ?? (object)DBNull.Value });
				}

				if(dbInstance.TestInteger1 != myInstance.TestInteger1)
				{
					changedColumnNames.Add("test_integer1");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.TestInteger1 ?? (object)DBNull.Value });
				}

				if(dbInstance.TestInteger2 != myInstance.TestInteger2)
				{
					changedColumnNames.Add("test_integer2");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = myInstance.TestInteger2 });
				}

				if(dbInstance.TestBigint1 != myInstance.TestBigint1)
				{
					changedColumnNames.Add("test_bigint1");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Bigint) { Value = myInstance.TestBigint1 ?? (object)DBNull.Value });
				}

				if(dbInstance.TestBigint2 != myInstance.TestBigint2)
				{
					changedColumnNames.Add("test_bigint2");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Bigint) { Value = myInstance.TestBigint2 });
				}

				if(dbInstance.TestText1 != myInstance.TestText1)
				{
					changedColumnNames.Add("test_text1");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = myInstance.TestText1 ?? (object)DBNull.Value });
				}

				if(dbInstance.TestText2 != myInstance.TestText2)
				{
					changedColumnNames.Add("test_text2");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = myInstance.TestText2 ?? (object)DBNull.Value });
				}

				if(dbInstance.TestReal1 != myInstance.TestReal1)
				{
					changedColumnNames.Add("test_real1");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Real) { Value = myInstance.TestReal1 ?? (object)DBNull.Value });
				}

				if(dbInstance.TestReal2 != myInstance.TestReal2)
				{
					changedColumnNames.Add("test_real2");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Real) { Value = myInstance.TestReal2 });
				}

				if(dbInstance.TestDouble1 != myInstance.TestDouble1)
				{
					changedColumnNames.Add("test_double1");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Double) { Value = myInstance.TestDouble1 ?? (object)DBNull.Value });
				}

				if(dbInstance.TestDouble2 != myInstance.TestDouble2)
				{
					changedColumnNames.Add("test_double2");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Double) { Value = myInstance.TestDouble2 });
				}

				if(dbInstance.TestChar1 != myInstance.TestChar1)
				{
					changedColumnNames.Add("test_char1");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = myInstance.TestChar1 ?? (object)DBNull.Value });
				}

				if(dbInstance.TestChar2 != myInstance.TestChar2)
				{
					changedColumnNames.Add("test_char2");
					changedColumnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = myInstance.TestChar2 ?? (object)DBNull.Value });
				}

				return (changedColumnNames, changedColumnParameters);
			};

			Test1PocoMetadata.GetAllColumns = (instance) =>
			{
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();

				columnNames.Add("test_name1");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.TestName1 ?? (object)DBNull.Value });
				columnNames.Add("test_name2");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = instance.TestName2 ?? (object)DBNull.Value });
				columnNames.Add("test_date1");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Date) { Value = instance.TestDate1 });
				columnNames.Add("test_date2");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Date) { Value = instance.TestDate2 ?? (object)DBNull.Value });
				columnNames.Add("test_timestamp1");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.TestTimestamp1 });
				columnNames.Add("test_timestamp2");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = instance.TestTimestamp2 ?? (object)DBNull.Value });
				columnNames.Add("test_boolean1");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Boolean) { Value = instance.TestBoolean1 });
				columnNames.Add("test_boolean2");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Boolean) { Value = instance.TestBoolean2 ?? (object)DBNull.Value });
				columnNames.Add("test_integer1");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.TestInteger1 ?? (object)DBNull.Value });
				columnNames.Add("test_integer2");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = instance.TestInteger2 });
				columnNames.Add("test_bigint1");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Bigint) { Value = instance.TestBigint1 ?? (object)DBNull.Value });
				columnNames.Add("test_bigint2");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Bigint) { Value = instance.TestBigint2 });
				columnNames.Add("test_text1");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = instance.TestText1 ?? (object)DBNull.Value });
				columnNames.Add("test_text2");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = instance.TestText2 ?? (object)DBNull.Value });
				columnNames.Add("test_real1");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Real) { Value = instance.TestReal1 ?? (object)DBNull.Value });
				columnNames.Add("test_real2");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Real) { Value = instance.TestReal2 });
				columnNames.Add("test_double1");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Double) { Value = instance.TestDouble1 ?? (object)DBNull.Value });
				columnNames.Add("test_double2");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Double) { Value = instance.TestDouble2 });
				columnNames.Add("test_char1");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = instance.TestChar1 ?? (object)DBNull.Value });
				columnNames.Add("test_char2");
				columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = instance.TestChar2 ?? (object)DBNull.Value });
				return (columnNames, columnParameters);
			};

			Test1PocoMetadata.ParseFM = (instance) => {
				var columnNames = new List<string>();
				var columnParameters = new List<NpgsqlParameter>();
				var operators = new List<QueryOperatorType>();

				var fm = instance as Test1FM;

				if(fm.TestID != null)
				{
					columnNames.Add("test_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.TestID });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestID_NotEqual != null)
				{
					columnNames.Add("test_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.TestID_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestID_LessThan != null)
				{
					columnNames.Add("test_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.TestID_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.TestID_LessThanOrEqual != null)
				{
					columnNames.Add("test_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.TestID_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.TestID_GreaterThan != null)
				{
					columnNames.Add("test_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.TestID_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.TestID_GreaterThanOrEqual != null)
				{
					columnNames.Add("test_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.TestID_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.TestID_IsIn != null)
				{
					columnNames.Add("test_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.TestID_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestID_IsNotIn != null)
				{
					columnNames.Add("test_id");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.TestID_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestName1 != null)
				{
					columnNames.Add("test_name1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.TestName1 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestName1_NotEqual != null)
				{
					columnNames.Add("test_name1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.TestName1_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestName1_StartsWith != null)
				{
					columnNames.Add("test_name1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.TestName1_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.TestName1_DoesNotStartWith != null)
				{
					columnNames.Add("test_name1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.TestName1_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.TestName1_EndsWith != null)
				{
					columnNames.Add("test_name1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.TestName1_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.TestName1_DoesNotEndWith != null)
				{
					columnNames.Add("test_name1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.TestName1_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.TestName1_Contains != null)
				{
					columnNames.Add("test_name1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.TestName1_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.TestName1_DoesNotContain != null)
				{
					columnNames.Add("test_name1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.TestName1_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.TestName1_IsIn != null)
				{
					columnNames.Add("test_name1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.TestName1_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestName1_IsNotIn != null)
				{
					columnNames.Add("test_name1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.TestName1_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestName2 != null)
				{
					columnNames.Add("test_name2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.TestName2 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestName2_NotEqual != null)
				{
					columnNames.Add("test_name2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.TestName2_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestName2_StartsWith != null)
				{
					columnNames.Add("test_name2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.TestName2_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.TestName2_DoesNotStartWith != null)
				{
					columnNames.Add("test_name2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.TestName2_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.TestName2_EndsWith != null)
				{
					columnNames.Add("test_name2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.TestName2_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.TestName2_DoesNotEndWith != null)
				{
					columnNames.Add("test_name2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.TestName2_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.TestName2_Contains != null)
				{
					columnNames.Add("test_name2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.TestName2_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.TestName2_DoesNotContain != null)
				{
					columnNames.Add("test_name2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Varchar) { Value = fm.TestName2_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.TestName2_IsNull != null)
				{
					columnNames.Add("test_name2");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.TestName2_IsNotNull != null)
				{
					columnNames.Add("test_name2");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}

				if(fm.TestName2_IsIn != null)
				{
					columnNames.Add("test_name2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.TestName2_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestName2_IsNotIn != null)
				{
					columnNames.Add("test_name2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Varchar) { Value = fm.TestName2_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestDate1 != null)
				{
					columnNames.Add("test_date1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Date) { Value = fm.TestDate1 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestDate1_NotEqual != null)
				{
					columnNames.Add("test_date1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Date) { Value = fm.TestDate1_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestDate1_LessThan != null)
				{
					columnNames.Add("test_date1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Date) { Value = fm.TestDate1_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.TestDate1_LessThanOrEqual != null)
				{
					columnNames.Add("test_date1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Date) { Value = fm.TestDate1_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.TestDate1_GreaterThan != null)
				{
					columnNames.Add("test_date1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Date) { Value = fm.TestDate1_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.TestDate1_GreaterThanOrEqual != null)
				{
					columnNames.Add("test_date1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Date) { Value = fm.TestDate1_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.TestDate1_IsIn != null)
				{
					columnNames.Add("test_date1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Date) { Value = fm.TestDate1_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestDate1_IsNotIn != null)
				{
					columnNames.Add("test_date1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Date) { Value = fm.TestDate1_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestDate2 != null)
				{
					columnNames.Add("test_date2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Date) { Value = fm.TestDate2 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestDate2_NotEqual != null)
				{
					columnNames.Add("test_date2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Date) { Value = fm.TestDate2_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestDate2_IsNull != null)
				{
					columnNames.Add("test_date2");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.TestDate2_IsNotNull != null)
				{
					columnNames.Add("test_date2");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}

				if(fm.TestDate2_IsIn != null)
				{
					columnNames.Add("test_date2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Date) { Value = fm.TestDate2_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestDate2_IsNotIn != null)
				{
					columnNames.Add("test_date2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Date) { Value = fm.TestDate2_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestTimestamp1 != null)
				{
					columnNames.Add("test_timestamp1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.TestTimestamp1 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestTimestamp1_NotEqual != null)
				{
					columnNames.Add("test_timestamp1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.TestTimestamp1_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestTimestamp1_LessThan != null)
				{
					columnNames.Add("test_timestamp1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.TestTimestamp1_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.TestTimestamp1_LessThanOrEqual != null)
				{
					columnNames.Add("test_timestamp1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.TestTimestamp1_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.TestTimestamp1_GreaterThan != null)
				{
					columnNames.Add("test_timestamp1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.TestTimestamp1_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.TestTimestamp1_GreaterThanOrEqual != null)
				{
					columnNames.Add("test_timestamp1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.TestTimestamp1_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.TestTimestamp1_IsIn != null)
				{
					columnNames.Add("test_timestamp1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.TestTimestamp1_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestTimestamp1_IsNotIn != null)
				{
					columnNames.Add("test_timestamp1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.TestTimestamp1_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestTimestamp2 != null)
				{
					columnNames.Add("test_timestamp2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.TestTimestamp2 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestTimestamp2_NotEqual != null)
				{
					columnNames.Add("test_timestamp2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Timestamp) { Value = fm.TestTimestamp2_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestTimestamp2_IsNull != null)
				{
					columnNames.Add("test_timestamp2");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.TestTimestamp2_IsNotNull != null)
				{
					columnNames.Add("test_timestamp2");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}

				if(fm.TestTimestamp2_IsIn != null)
				{
					columnNames.Add("test_timestamp2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.TestTimestamp2_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestTimestamp2_IsNotIn != null)
				{
					columnNames.Add("test_timestamp2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Timestamp) { Value = fm.TestTimestamp2_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestBoolean1 != null)
				{
					columnNames.Add("test_boolean1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Boolean) { Value = fm.TestBoolean1 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestBoolean1_NotEqual != null)
				{
					columnNames.Add("test_boolean1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Boolean) { Value = fm.TestBoolean1_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestBoolean1_IsIn != null)
				{
					columnNames.Add("test_boolean1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Boolean) { Value = fm.TestBoolean1_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestBoolean1_IsNotIn != null)
				{
					columnNames.Add("test_boolean1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Boolean) { Value = fm.TestBoolean1_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestBoolean2 != null)
				{
					columnNames.Add("test_boolean2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Boolean) { Value = fm.TestBoolean2 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestBoolean2_NotEqual != null)
				{
					columnNames.Add("test_boolean2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Boolean) { Value = fm.TestBoolean2_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestBoolean2_IsNull != null)
				{
					columnNames.Add("test_boolean2");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.TestBoolean2_IsNotNull != null)
				{
					columnNames.Add("test_boolean2");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}

				if(fm.TestBoolean2_IsIn != null)
				{
					columnNames.Add("test_boolean2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Boolean) { Value = fm.TestBoolean2_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestBoolean2_IsNotIn != null)
				{
					columnNames.Add("test_boolean2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Boolean) { Value = fm.TestBoolean2_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestInteger1 != null)
				{
					columnNames.Add("test_integer1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.TestInteger1 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestInteger1_NotEqual != null)
				{
					columnNames.Add("test_integer1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.TestInteger1_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestInteger1_IsNull != null)
				{
					columnNames.Add("test_integer1");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.TestInteger1_IsNotNull != null)
				{
					columnNames.Add("test_integer1");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}

				if(fm.TestInteger1_IsIn != null)
				{
					columnNames.Add("test_integer1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.TestInteger1_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestInteger1_IsNotIn != null)
				{
					columnNames.Add("test_integer1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.TestInteger1_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestInteger2 != null)
				{
					columnNames.Add("test_integer2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.TestInteger2 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestInteger2_NotEqual != null)
				{
					columnNames.Add("test_integer2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.TestInteger2_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestInteger2_LessThan != null)
				{
					columnNames.Add("test_integer2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.TestInteger2_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.TestInteger2_LessThanOrEqual != null)
				{
					columnNames.Add("test_integer2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.TestInteger2_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.TestInteger2_GreaterThan != null)
				{
					columnNames.Add("test_integer2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.TestInteger2_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.TestInteger2_GreaterThanOrEqual != null)
				{
					columnNames.Add("test_integer2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Integer) { Value = fm.TestInteger2_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.TestInteger2_IsIn != null)
				{
					columnNames.Add("test_integer2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.TestInteger2_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestInteger2_IsNotIn != null)
				{
					columnNames.Add("test_integer2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Integer) { Value = fm.TestInteger2_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestBigint1 != null)
				{
					columnNames.Add("test_bigint1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Bigint) { Value = fm.TestBigint1 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestBigint1_NotEqual != null)
				{
					columnNames.Add("test_bigint1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Bigint) { Value = fm.TestBigint1_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestBigint1_IsNull != null)
				{
					columnNames.Add("test_bigint1");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.TestBigint1_IsNotNull != null)
				{
					columnNames.Add("test_bigint1");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}

				if(fm.TestBigint1_IsIn != null)
				{
					columnNames.Add("test_bigint1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Bigint) { Value = fm.TestBigint1_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestBigint1_IsNotIn != null)
				{
					columnNames.Add("test_bigint1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Bigint) { Value = fm.TestBigint1_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestBigint2 != null)
				{
					columnNames.Add("test_bigint2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Bigint) { Value = fm.TestBigint2 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestBigint2_NotEqual != null)
				{
					columnNames.Add("test_bigint2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Bigint) { Value = fm.TestBigint2_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestBigint2_LessThan != null)
				{
					columnNames.Add("test_bigint2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Bigint) { Value = fm.TestBigint2_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.TestBigint2_LessThanOrEqual != null)
				{
					columnNames.Add("test_bigint2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Bigint) { Value = fm.TestBigint2_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.TestBigint2_GreaterThan != null)
				{
					columnNames.Add("test_bigint2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Bigint) { Value = fm.TestBigint2_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.TestBigint2_GreaterThanOrEqual != null)
				{
					columnNames.Add("test_bigint2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Bigint) { Value = fm.TestBigint2_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.TestBigint2_IsIn != null)
				{
					columnNames.Add("test_bigint2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Bigint) { Value = fm.TestBigint2_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestBigint2_IsNotIn != null)
				{
					columnNames.Add("test_bigint2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Bigint) { Value = fm.TestBigint2_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestText1 != null)
				{
					columnNames.Add("test_text1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.TestText1 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestText1_NotEqual != null)
				{
					columnNames.Add("test_text1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.TestText1_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestText1_StartsWith != null)
				{
					columnNames.Add("test_text1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.TestText1_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.TestText1_DoesNotStartWith != null)
				{
					columnNames.Add("test_text1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.TestText1_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.TestText1_EndsWith != null)
				{
					columnNames.Add("test_text1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.TestText1_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.TestText1_DoesNotEndWith != null)
				{
					columnNames.Add("test_text1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.TestText1_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.TestText1_Contains != null)
				{
					columnNames.Add("test_text1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.TestText1_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.TestText1_DoesNotContain != null)
				{
					columnNames.Add("test_text1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.TestText1_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.TestText1_IsNull != null)
				{
					columnNames.Add("test_text1");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.TestText1_IsNotNull != null)
				{
					columnNames.Add("test_text1");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}

				if(fm.TestText1_IsIn != null)
				{
					columnNames.Add("test_text1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = fm.TestText1_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestText1_IsNotIn != null)
				{
					columnNames.Add("test_text1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = fm.TestText1_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestText2 != null)
				{
					columnNames.Add("test_text2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.TestText2 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestText2_NotEqual != null)
				{
					columnNames.Add("test_text2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.TestText2_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestText2_StartsWith != null)
				{
					columnNames.Add("test_text2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.TestText2_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.TestText2_DoesNotStartWith != null)
				{
					columnNames.Add("test_text2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.TestText2_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.TestText2_EndsWith != null)
				{
					columnNames.Add("test_text2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.TestText2_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.TestText2_DoesNotEndWith != null)
				{
					columnNames.Add("test_text2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.TestText2_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.TestText2_Contains != null)
				{
					columnNames.Add("test_text2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.TestText2_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.TestText2_DoesNotContain != null)
				{
					columnNames.Add("test_text2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Text) { Value = fm.TestText2_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.TestText2_IsIn != null)
				{
					columnNames.Add("test_text2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = fm.TestText2_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestText2_IsNotIn != null)
				{
					columnNames.Add("test_text2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Text) { Value = fm.TestText2_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestReal1 != null)
				{
					columnNames.Add("test_real1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Real) { Value = fm.TestReal1 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestReal1_NotEqual != null)
				{
					columnNames.Add("test_real1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Real) { Value = fm.TestReal1_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestReal1_IsNull != null)
				{
					columnNames.Add("test_real1");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.TestReal1_IsNotNull != null)
				{
					columnNames.Add("test_real1");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}

				if(fm.TestReal1_IsIn != null)
				{
					columnNames.Add("test_real1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Real) { Value = fm.TestReal1_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestReal1_IsNotIn != null)
				{
					columnNames.Add("test_real1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Real) { Value = fm.TestReal1_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestReal2 != null)
				{
					columnNames.Add("test_real2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Real) { Value = fm.TestReal2 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestReal2_NotEqual != null)
				{
					columnNames.Add("test_real2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Real) { Value = fm.TestReal2_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestReal2_LessThan != null)
				{
					columnNames.Add("test_real2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Real) { Value = fm.TestReal2_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.TestReal2_LessThanOrEqual != null)
				{
					columnNames.Add("test_real2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Real) { Value = fm.TestReal2_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.TestReal2_GreaterThan != null)
				{
					columnNames.Add("test_real2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Real) { Value = fm.TestReal2_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.TestReal2_GreaterThanOrEqual != null)
				{
					columnNames.Add("test_real2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Real) { Value = fm.TestReal2_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.TestReal2_IsIn != null)
				{
					columnNames.Add("test_real2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Real) { Value = fm.TestReal2_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestReal2_IsNotIn != null)
				{
					columnNames.Add("test_real2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Real) { Value = fm.TestReal2_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestDouble1 != null)
				{
					columnNames.Add("test_double1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Double) { Value = fm.TestDouble1 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestDouble1_NotEqual != null)
				{
					columnNames.Add("test_double1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Double) { Value = fm.TestDouble1_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestDouble1_IsNull != null)
				{
					columnNames.Add("test_double1");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.TestDouble1_IsNotNull != null)
				{
					columnNames.Add("test_double1");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}

				if(fm.TestDouble1_IsIn != null)
				{
					columnNames.Add("test_double1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Double) { Value = fm.TestDouble1_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestDouble1_IsNotIn != null)
				{
					columnNames.Add("test_double1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Double) { Value = fm.TestDouble1_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestDouble2 != null)
				{
					columnNames.Add("test_double2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Double) { Value = fm.TestDouble2 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestDouble2_NotEqual != null)
				{
					columnNames.Add("test_double2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Double) { Value = fm.TestDouble2_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestDouble2_LessThan != null)
				{
					columnNames.Add("test_double2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Double) { Value = fm.TestDouble2_LessThan });
					operators.Add(QueryOperatorType.LessThan);
				}		 

				if(fm.TestDouble2_LessThanOrEqual != null)
				{
					columnNames.Add("test_double2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Double) { Value = fm.TestDouble2_LessThanOrEqual });
					operators.Add(QueryOperatorType.LessThanOrEqual);
				}		 

				if(fm.TestDouble2_GreaterThan != null)
				{
					columnNames.Add("test_double2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Double) { Value = fm.TestDouble2_GreaterThan });
					operators.Add(QueryOperatorType.GreaterThan);
				}		 

				if(fm.TestDouble2_GreaterThanOrEqual != null)
				{
					columnNames.Add("test_double2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Double) { Value = fm.TestDouble2_GreaterThanOrEqual });
					operators.Add(QueryOperatorType.GreaterThanOrEqual);
				}		 

				if(fm.TestDouble2_IsIn != null)
				{
					columnNames.Add("test_double2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Double) { Value = fm.TestDouble2_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestDouble2_IsNotIn != null)
				{
					columnNames.Add("test_double2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Double) { Value = fm.TestDouble2_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestChar1 != null)
				{
					columnNames.Add("test_char1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = fm.TestChar1 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestChar1_NotEqual != null)
				{
					columnNames.Add("test_char1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = fm.TestChar1_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestChar1_StartsWith != null)
				{
					columnNames.Add("test_char1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = fm.TestChar1_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.TestChar1_DoesNotStartWith != null)
				{
					columnNames.Add("test_char1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = fm.TestChar1_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.TestChar1_EndsWith != null)
				{
					columnNames.Add("test_char1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = fm.TestChar1_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.TestChar1_DoesNotEndWith != null)
				{
					columnNames.Add("test_char1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = fm.TestChar1_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.TestChar1_Contains != null)
				{
					columnNames.Add("test_char1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = fm.TestChar1_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.TestChar1_DoesNotContain != null)
				{
					columnNames.Add("test_char1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = fm.TestChar1_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.TestChar1_IsNull != null)
				{
					columnNames.Add("test_char1");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNull);
				}

				if(fm.TestChar1_IsNotNull != null)
				{
					columnNames.Add("test_char1");
					columnParameters.Add(null);
					operators.Add(QueryOperatorType.IsNotNull);
				}

				if(fm.TestChar1_IsIn != null)
				{
					columnNames.Add("test_char1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Char) { Value = fm.TestChar1_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestChar1_IsNotIn != null)
				{
					columnNames.Add("test_char1");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Char) { Value = fm.TestChar1_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}

				if(fm.TestChar2 != null)
				{
					columnNames.Add("test_char2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = fm.TestChar2 });
					operators.Add(QueryOperatorType.Equal);
				}		 

				if(fm.TestChar2_NotEqual != null)
				{
					columnNames.Add("test_char2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = fm.TestChar2_NotEqual });
					operators.Add(QueryOperatorType.NotEqual);
				}		 

				if(fm.TestChar2_StartsWith != null)
				{
					columnNames.Add("test_char2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = fm.TestChar2_StartsWith });
					operators.Add(QueryOperatorType.StartsWith);
				}		 

				if(fm.TestChar2_DoesNotStartWith != null)
				{
					columnNames.Add("test_char2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = fm.TestChar2_DoesNotStartWith });
					operators.Add(QueryOperatorType.DoesNotStartWith);
				}		 

				if(fm.TestChar2_EndsWith != null)
				{
					columnNames.Add("test_char2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = fm.TestChar2_EndsWith });
					operators.Add(QueryOperatorType.EndsWith);
				}		 

				if(fm.TestChar2_DoesNotEndWith != null)
				{
					columnNames.Add("test_char2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = fm.TestChar2_DoesNotEndWith });
					operators.Add(QueryOperatorType.DoesNotEndWith);
				}		 

				if(fm.TestChar2_Contains != null)
				{
					columnNames.Add("test_char2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = fm.TestChar2_Contains });
					operators.Add(QueryOperatorType.Contains);
				}		 

				if(fm.TestChar2_DoesNotContain != null)
				{
					columnNames.Add("test_char2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Char) { Value = fm.TestChar2_DoesNotContain });
					operators.Add(QueryOperatorType.DoesNotContain);
				}		 

				if(fm.TestChar2_IsIn != null)
				{
					columnNames.Add("test_char2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Char) { Value = fm.TestChar2_IsIn });
					operators.Add(QueryOperatorType.IsIn);
				}

				if(fm.TestChar2_IsNotIn != null)
				{
					columnNames.Add("test_char2");
					columnParameters.Add(new NpgsqlParameter(null, NpgsqlDbType.Array | NpgsqlDbType.Char) { Value = fm.TestChar2_IsNotIn });
					operators.Add(QueryOperatorType.IsNotIn);
				}


				return (columnNames, columnParameters, operators);
			};			
			
			StaticMetadataByPocoType = new Dictionary<Type, object>
			{
				{typeof(Test1Poco), Test1PocoMetadata},
			};		
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

		static TestDbPocos()
		{
			Initialize();
		}

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
		
		public IReadOnlyDictionary<Type, object> MetadataByPocoType => StaticMetadataByPocoType;

		public IDbService<TestDbPocos> DbService { private get; set; }
    }
}

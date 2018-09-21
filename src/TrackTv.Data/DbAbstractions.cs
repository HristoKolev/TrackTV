namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    using LinqToDB;

    using Npgsql;

    using NpgsqlTypes;

    /// <summary>
    /// Interface for all Poco classes.
    /// </summary>
    public interface IPoco<T>
        where T : IPoco<T>
    {
        TableMetadataModel<T> Metadata { get; }
    }

    public interface IDbService<TPocos> : IDisposable where TPocos : IDbPocos<TPocos>, new()
    {
        /// <summary>
        /// Calls `BeginTransaction` on the connection and returns the result.
        /// </summary>
        Task<NpgsqlTransaction> BeginTransaction();

        /// <summary>
        /// Inserts several records in single query.
        /// </summary>
        Task<int> BulkInsert<T>(IEnumerable<T> pocos, CancellationToken cancellationToken = default)
            where T : IPoco<T>;

        /// <summary>
        /// Deletes a record by its PrimaryKey.
        /// </summary>
        Task<int> Delete<T>(T poco, CancellationToken cancellationToken = default)
            where T : IPoco<T>;

        /// <summary>
        /// <para>Deletes records from a table by their IDs.</para>
        /// </summary>
        Task<int> Delete<T>(int[] ids, CancellationToken cancellationToken = default)
            where T : IPoco<T>;

        /// <summary>
        /// <para>Deletes a record by ID.</para>
        /// </summary>
        Task<int> Delete<T>(int id, CancellationToken cancellationToken = default)
            where T : IPoco<T>;

        /// <summary>
        /// Starts a transaction and runs the `body` function
        /// </summary>
        Task ExecuteInTransaction(Func<Task> body, TimeSpan? timeout = null);

        /// <summary>
        /// Starts a transaction and runs the `body` function
        /// </summary>
        Task ExecuteInTransaction(Func<NpgsqlTransaction, Task> body, TimeSpan? timeout = null);

        /// <summary>
        /// Starts a transaction, runs the `body` function
        /// and if it does not throw - commits the transaction.
        /// </summary>
        Task ExecuteInTransactionAndCommit(Func<Task> body, TimeSpan? timeout = null);

        /// <summary>
        /// Starts a transaction, runs the `body` function
        /// and if it does not throw and the transaction is not completed - commits the transaction.
        /// </summary>
        Task ExecuteInTransactionAndCommit(Func<NpgsqlTransaction, Task> body, TimeSpan? timeout = null);

        /// <summary>
        /// Executes a query and returns the rows affected.
        /// </summary>
        Task<int> ExecuteNonQuery(string sql, params NpgsqlParameter[] parameters);

        /// <summary>
        /// Executes a query and returns a scalar value of type T.
        /// It throws if the result set does not have exactly one column and one row.
        /// It throws if the return value is 'null' and the type T is a value type.
        /// </summary>
        Task<T> ExecuteScalar<T>(string sql, params NpgsqlParameter[] parameters);

        /// <summary>
        /// Inserts a record and attaches it's ID to the poco object. 
        /// </summary>
        Task<int> Insert<T>(T poco, CancellationToken cancellationToken = default)
            where T : IPoco<T>;

        /// <summary>
        /// Inserts a record and returns its ID.
        /// </summary>
        Task<int> InsertWithoutMutating<T>(T poco, CancellationToken cancellationToken = default)
            where T : IPoco<T>;

        Task<List<TCatalogModel>> FilterInternal<TPoco, TCatalogModel>(IFilterModel<TPoco> filter, CancellationToken cancellationToken = default)
            where TPoco : IPoco<TPoco>, new() where TCatalogModel : ICatalogModel<TPoco>;

        /// <summary>
        /// Creates a parameter of type T with NpgsqlDbType from the default type map 'defaultNpgsqlDbTypeMap'.
        /// </summary>
        NpgsqlParameter Parameter<T>(string parameterName, T value);

        /// <summary>
        /// Creates a parameter of type T by explicitly specifying NpgsqlDbType.
        /// </summary>
        NpgsqlParameter Parameter<T>(string parameterName, T value, NpgsqlDbType dbType);

        /// <summary>
        /// Executes a query and returns objects 
        /// </summary>
        Task<List<T>> Query<T>(string sql, params NpgsqlParameter[] parameters)
            where T : IPoco<T>, new();

        /// <summary>
        /// Returns one object of type T.
        /// If there are no rows then returns 'null';
        /// If there is more that one row then throws.
        /// </summary>
        Task<T> QueryOne<T>(string sql, params NpgsqlParameter[] parameters)
            where T : class, IPoco<T>, new();

        /// <summary>
        /// Saves a record to the database.
        /// If the poco object has a positive primary key it updates it.
        /// If the primary key value is 0 it inserts the record.
        /// Returns the record's primary key value.
        /// </summary>
        Task<int> Save<T>(T poco, CancellationToken cancellationToken = default)
            where T : class, IPoco<T>, new();

        /// <summary>
        /// Updates a record by its ID.
        /// Only updates the changed rows. 
        /// </summary>
        Task<int> Update<T>(T poco, CancellationToken cancellationToken = default)
            where T : class, IPoco<T>, new();

        /// <summary>
        /// Updates a record by its ID.
        /// Only updates the changed rows. 
        /// </summary>
        Task<int> UpdateChangesOnly<T>(T poco, CancellationToken cancellationToken = default)
            where T : class, IPoco<T>, new();

        IQueryable<T> GetTable<T>()
            where T : class, IPoco<T>;

        TPocos Poco { get; }
    }
    
    public class TableMetadataModel<T> : TableMetadataModel
        where T : IPoco<T>
    {
        /// <summary>
        /// <para>Clones the current object and returns the clone.</para>
        /// </summary>
        // ReSharper disable once NotAccessedField.Global
        public Func<T, T> Clone;

        /// <summary>		
        /// <para>Returns the primary key for the table.</para>
        /// </summary>   
        public Func<T, int> GetPrimaryKey;

        /// <summary>		
        /// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
        public Func<T, bool> IsNew;

        /// <summary>		
        /// <para>Sets the primary key for the table.</para>
        /// </summary> 
        public Action<T, int> SetPrimaryKey;

        public IReadOnlyDictionary<string, Action<T, object>> Setters;

        /// <summary>
        /// Generates a parameter for every non Primary Key column in the table.
        /// </summary>
        public Func<T, NpgsqlParameter[]> GenerateParameters { get; set; }

        public Func<T, ValueTuple<List<string>, List<NpgsqlParameter>>> GetAllColumns { get; set; }

        /// <summary>
        /// Generates the changes between 2 instances of the poco class.
        /// Returns the names of the changed columns and parameters for every column value.
        /// </summary>
        public Func<T, T, ValueTuple<List<string>, List<NpgsqlParameter>>> GetColumnChanges { get; set; }

        // ReSharper disable once InconsistentNaming
        public Func<T, ICatalogModel<T>> MapToCM { get; set; }

        public Func<IFilterModel<T>, ValueTuple<List<string>, List<NpgsqlParameter>, List<QueryOperatorType>>> ParseFM { get; set; }
    }

    /// <summary>
    /// Represemts a table in PostgreSQL
    /// </summary>
    public class TableMetadataModel
    {
        public string ClassName { get; set; }

        public List<ColumnMetadataModel> Columns { get; set; }

        public string PluralClassName { get; set; }

        public string PrimaryKeyColumnName { get; set; }

        public string PrimaryKeyPropertyName { get; set; }

        public string TableName { get; set; }

        public string TableSchema { get; set; }
    }

    /// <summary>
    /// Represents a column in PostgreSQL
    /// </summary>
    public class ColumnMetadataModel
    {
        public Type ClrNonNullableType { get; set; }

        public string ClrNonNullableTypeName { get; set; }

        public Type ClrNullableType { get; set; }

        public string ClrNullableTypeName { get; set; }

        public Type ClrType { get; set; }

        public string ClrTypeName { get; set; }

        public string ColumnComment { get; set; }

        public string ColumnName { get; set; }

        public string[] Comments { get; set; }

        public string DbDataType { get; set; }

        public string ForeignKeyConstraintName { get; set; }

        public string ForeignKeyReferenceColumnName { get; set; }

        public string ForeignKeyReferenceSchemaName { get; set; }

        public string ForeignKeyReferenceTableName { get; set; }

        public bool IsClrValueType { get; set; }

        public bool IsClrNullableType { get; set; }

        public bool IsClrReferenceType { get; set; }

        public bool IsForeignKey { get; set; }

        public bool IsNullable { get; set; }

        public bool IsPrimaryKey { get; set; }

        // ReSharper disable once InconsistentNaming
        public DataType Linq2dbDataType { get; set; }

        // ReSharper disable once InconsistentNaming
        public string Linq2dbDataTypeName { get; set; }

        public NpgsqlDbType NpgsDataType { get; set; }

        public string NpgsDataTypeName { get; set; }

        public string PrimaryKeyConstraintName { get; set; }

        public string PropertyName { get; set; }

        public string TableName { get; set; }

        public string TableSchema { get; set; }

        public string[] ValidOperators { get; set; }
    }

    /// <summary>
    /// Interface for all Catalog models
    /// </summary>
    public interface ICatalogModel<TPoco>
        where TPoco : IPoco<TPoco>
    {
    }

    /// <summary>
    /// Interface for all Filter models
    /// </summary>
    public interface IFilterModel<TPoco>
        where TPoco : IPoco<TPoco>
    {
    }

    /// <summary>
    /// Interface for all Business models
    /// </summary>
    public interface IBusinessModel<TPoco>
        where TPoco : IPoco<TPoco>
    {
        TPoco ToPoco();
    }

    public enum QueryOperatorType
    {
        Equal,

        NotEqual,

        LessThan,

        LessThanOrEqual,

        GreaterThan,

        GreaterThanOrEqual,

        StartsWith,

        DoesNotStartWith,

        EndsWith,

        DoesNotEndWith,

        Contains,

        DoesNotContain,

        IsNull,

        IsNotNull,

        IsIn,

        IsNotIn
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class FilterOperatorAttribute : Attribute
    {
        public FilterOperatorAttribute(QueryOperatorType queryOperatorType)
        {
            this.QueryOperatorType = queryOperatorType;
        }

        public FilterOperatorAttribute(QueryOperatorType queryOperatorType, string propertyName)
        {
            this.QueryOperatorType = queryOperatorType;
            this.PropertyName = propertyName;
        }

        public string PropertyName { get; }

        public QueryOperatorType QueryOperatorType { get; }
    }

    public interface IDbPocos<TDbPocos>
        where TDbPocos : IDbPocos<TDbPocos>, new()
    {
        IReadOnlyDictionary<Type, object> MetadataByPocoType { get; }

        IDbService<TDbPocos> DbService { set; }
    }
}
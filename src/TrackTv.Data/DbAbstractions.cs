namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
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

    public partial interface IDbService : IDisposable
    {
        /// <summary>
        /// Inserts several records in single query.
        /// </summary>
        Task<int> BulkInsert<T>(IEnumerable<T> pocos)
            where T : IPoco<T>;

        /// <summary>
        /// Deletes a record by its PrimaryKey.
        /// </summary>
        Task<int> Delete<T>(T poco)
            where T : IPoco<T>;

        /// <summary>
        /// <para>Deletes records from a table by their IDs.</para>
        /// </summary>
        Task<int> Delete<T>(int[] ids)
            where T : IPoco<T>;

        /// <summary>
        /// <para>Deletes a record by ID.</para>
        /// </summary>
        Task<int> Delete<T>(int id)
            where T : IPoco<T>;

        Task ExecuteInTransaction(Func<Task> body, TimeSpan? timeout = null);

        Task ExecuteInTransaction(Func<ITransactionHandle, Task> body, TimeSpan? timeout = null);

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
        Task<int> Insert<T>(T poco)
            where T : IPoco<T>;

        /// <summary>
        /// Inserts a record and returns its ID.
        /// </summary>
        Task<int> InsertWithoutMutating<T>(T poco)
            where T : IPoco<T>;

        /// <summary>
        /// Creates a parameter of type T with NpgsqlDbType from the default type map 'defaultNpgsqlDbTypeMap'.
        /// </summary>
        NpgsqlParameter<T> Parameter<T>(string parameterName, T value);

        /// <summary>
        /// Creates a parameter of type T by explicitly specifying NpgsqlDbType.
        /// </summary>
        NpgsqlParameter<T> Parameter<T>(string parameterName, T value, NpgsqlDbType dbType);

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
        Task<int> Save<T>(T poco)
            where T : class, IPoco<T>, new();

        /// <summary>
        /// Updates a record by its ID.
        /// Only updates the changed rows. 
        /// </summary>
        Task<int> Update<T>(T poco)
            where T : class, IPoco<T>, new();
    }

    /// <summary>
    /// An object that gives the user the ability to rollback a transaction inside a transaction-wrapped function.
    /// If `Rollback` is not called and the function body does not throw an exception,
    /// the transaction will automatically be committed after the function returns.
    /// </summary>
    public interface ITransactionHandle
    {
        /// <summary>
        /// Rollbacks the transaction.
        /// </summary>
        Task Rollback();
    }

    /// <summary>
    /// Represemts a table in PostgreSQL
    /// </summary>
    public class TableMetadataModel<T>
        where T : IPoco<T>
    {
        /// <summary>
        /// <para>Clones the current object and returns the clone.</para>
        /// </summary>
        public Func<T, T> Clone;

        /// <summary>		
        /// <para>Returns the primary key for the table.</para>
        /// </summary>   
        public Func<T, int> GetPrimaryKey;

        public IReadOnlyDictionary<string, Func<T, object>> Getters;

        /// <summary>		
        /// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
        public Func<T, bool> IsNew;

        /// <summary>		
        /// <para>Sets the primary key for the table.</para>
        /// </summary> 
        public Action<T, int> SetPrimaryKey;

        public IReadOnlyDictionary<string, Action<T, object>> Setters;

        public string ClassName { get; set; }

        public List<ColumnMetadataModel<T>> Columns { get; set; }

        public Dictionary<string, ColumnMetadataModel<T>> ColumnsByName { get; set; }

        public Func<T, int, NpgsqlParameter[]> GenerateParameters { get; set; }

        public Func<T, T, ValueTuple<List<string>, List<NpgsqlParameter>>> GetColumnChanges { get; set; }

        public string PluralClassName { get; set; }

        public string PrimaryKeyColumnName { get; set; }

        public string PrimaryKeyPropertyName { get; set; }

        public string TableName { get; set; }

        public string TableSchema { get; set; }
    }

    /// <summary>
    /// Represents a column in PostgreSQL
    /// </summary>
    public class ColumnMetadataModel<T>
    {
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

        public Func<T, object> GetValue { get; set; }

        public bool IsForeignKey { get; set; }

        public bool IsNullable { get; set; }

        public bool IsPrimaryKey { get; set; }

        public DataType Linq2dbDataType { get; set; }

        public string Linq2dbDataTypeName { get; set; }

        public NpgsqlDbType NpgsDataType { get; set; }

        public string NpgsDataTypeName { get; set; }

        public string PrimaryKeyConstraintName { get; set; }

        public string PropertyName { get; set; }

        public Action<T, object> SetValue { get; set; }

        public string TableName { get; set; }

        public string TableSchema { get; set; }
    }
}
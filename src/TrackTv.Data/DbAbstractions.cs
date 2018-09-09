namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    using LinqToDB;

    using Npgsql;

    using NpgsqlTypes;

    /// <summary>
    /// Interface for all Poco classes.
    /// </summary>
    public interface IPoco<T> where T : IPoco<T>
    {
        TableMetadataModel<T> Metadata { get; }
    }

    public partial interface IDbService : IDisposable
    {
        void BulkInsertSync<TPoco>(IEnumerable<TPoco> list)
            where TPoco : IPoco<TPoco>;

        Task Delete<TPoco>(TPoco poco)
            where TPoco : IPoco<TPoco>;

        Task Delete<TPoco>(int[] ids)
            where TPoco : IPoco<TPoco>;

        Task Delete<TPoco>(int id)
            where TPoco : IPoco<TPoco>;

        Task ExecuteInTransaction(Func<Task> body);

        Task ExecuteInTransaction(Func<IDbTransaction, Task> body, TimeSpan? timeout = null);

        Task<int> Insert<TPoco>(TPoco poco)
            where TPoco : IPoco<TPoco>;

        Task<int> Save<TPoco>(TPoco poco)
            where TPoco : IPoco<TPoco>;

        Task Update<TPoco>(TPoco poco)
            where TPoco : IPoco<TPoco>;

        Task<List<T>> Query<T>(string sql, params NpgsqlParameter[] parameters)
            where T : IPoco<T>, new();

        Task<T> QueryOne<T>(string sql, params NpgsqlParameter[] parameters)
            where T : class, IPoco<T>, new();

        Task<T> ExecuteScalar<T>(string sql, params NpgsqlParameter[] parameters);

        Task<int> ExecuteNonQuery(string sql, params NpgsqlParameter[] parameters);

        NpgsqlParameter<T> Parameter<T>(string parameterName, T value);

        NpgsqlParameter<T> Parameter<T>(string parameterName, T value, NpgsqlDbType dbType);
    }

    /// <summary>
    /// Represemts a table in PostgreSQL
    /// </summary>
    public class TableMetadataModel<T>
        where T : IPoco<T>
    {
        public string ClassName { get; set; }

        public List<ColumnMetadataModel<T>> Columns { get; set; }

        public string PluralClassName { get; set; }

        public string PrimaryKeyColumnName { get; set; }

        public string PrimaryKeyPropertyName { get; set; }

        public string TableName { get; set; }

        public string TableSchema { get; set; }

        public Dictionary<string, ColumnMetadataModel<T>> ColumnsByName { get; set; }

        /// <summary>		
        /// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
        public Func<T, bool> IsNew;

        /// <summary>		
        /// <para>Returns the primary key for the table.</para>
        /// </summary>   
        public Func<T, int> GetPrimaryKey;

        /// <summary>		
        /// <para>Sets the primary key for the table.</para>
        /// </summary> 
        public Action<T, int> SetPrimaryKey;

        /// <summary>
        /// <para>Clones the current object and returns the clone.</para>
        /// </summary>
        public Func<T, T> Clone;

        public IReadOnlyDictionary<string, Func<T, object>> Getters;
        
        public IReadOnlyDictionary<string, Action<T, object>> Setters;
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

        public bool IsForeignKey { get; set; }

        public bool IsNullable { get; set; }

        public bool IsPrimaryKey { get; set; }

        public DataType Linq2dbDataType { get; set; }

        public string Linq2dbDataTypeName { get; set; }

        public string PrimaryKeyConstraintName { get; set; }

        public string PropertyName { get; set; }

        public string TableName { get; set; }

        public string TableSchema { get; set; }

        public NpgsqlDbType NpgsDataType { get; set; }

        public string NpgsDataTypeName { get; set; }

        public Action<T, object> SetValue { get; set; }

        public Func<T, object> GetValue { get; set; }
    }
}
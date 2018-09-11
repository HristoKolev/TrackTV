namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    using LinqToDB.Data;
    using LinqToDB.DataProvider.PostgreSQL;

    using Npgsql;

    using NpgsqlTypes;

    public partial class DbService : IDbService
    {
        /// <summary>
        /// The default parameter type map that is used when creating parameters without specifying the NpgsqlDbType explicitly.
        /// </summary>
        private static readonly Dictionary<Type, NpgsqlDbType> DefaultNpgsqlDbTypeMap = new Dictionary<Type, NpgsqlDbType>
        {
            { typeof(int), NpgsqlDbType.Integer },
            { typeof(long), NpgsqlDbType.Bigint },
            { typeof(bool), NpgsqlDbType.Boolean },
            { typeof(float), NpgsqlDbType.Real },
            { typeof(double), NpgsqlDbType.Double },
            { typeof(short), NpgsqlDbType.Smallint },
            { typeof(decimal), NpgsqlDbType.Numeric },
            { typeof(string), NpgsqlDbType.Text },
            { typeof(DateTime), NpgsqlDbType.Timestamp },
            { typeof(byte[]), NpgsqlDbType.Bytea },
            { typeof(int?), NpgsqlDbType.Integer },
            { typeof(long?), NpgsqlDbType.Bigint },
            { typeof(bool?), NpgsqlDbType.Boolean },
            { typeof(float?), NpgsqlDbType.Real },
            { typeof(double?), NpgsqlDbType.Double },
            { typeof(short?), NpgsqlDbType.Smallint },
            { typeof(decimal?), NpgsqlDbType.Numeric },
            { typeof(DateTime?), NpgsqlDbType.Timestamp },
            // ReSharper disable BitwiseOperatorOnEnumWithoutFlags
            { typeof(string[]), NpgsqlDbType.Array   | NpgsqlDbType.Text },
            { typeof(int[]), NpgsqlDbType.Array      | NpgsqlDbType.Integer },
            { typeof(DateTime[]), NpgsqlDbType.Array | NpgsqlDbType.Timestamp },
            // ReSharper restore BitwiseOperatorOnEnumWithoutFlags
        };

        private readonly NpgsqlConnection dbConnection;

        private DataConnection linqToDbConnection;

        public DbService(NpgsqlConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        private DataConnection LinqToDbConnection
        {
            get
            {
                // ReSharper disable once ConvertIfStatementToNullCoalescingExpression
                if (this.linqToDbConnection == null)
                {
                    this.linqToDbConnection = new DataConnection(new PostgreSQLDataProvider(), this.dbConnection, false);
                }

                return this.linqToDbConnection;
            }
        }

        public void Dispose()
        {
            this.LinqToDbConnection?.Dispose();
        }

        /// <summary>
        /// Executes a query and returns the rows affected.
        /// </summary>
        public async Task<int> ExecuteNonQuery(string sql, params NpgsqlParameter[] parameters)
        {
            if (sql == null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            await this.VerifyConnectionState().ConfigureAwait(false);

            using (var command = this.dbConnection.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.AddRange(parameters);

                await command.PrepareAsync().ConfigureAwait(false);

                return await command.ExecuteNonQueryAsync().ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Executes a query and returns a scalar value of type T.
        /// It throws if the result set does not have exactly one column and one row.
        /// It throws if the return value is 'null' and the type T is a value type.
        /// </summary>
        public async Task<T> ExecuteScalar<T>(string sql, params NpgsqlParameter[] parameters)
        {
            if (sql == null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            await this.VerifyConnectionState().ConfigureAwait(false);

            using (var command = this.dbConnection.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.AddRange(parameters);

                await command.PrepareAsync().ConfigureAwait(false);

                using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    if (reader.FieldCount == 0)
                    {
                        throw new ApplicationException("No columns returned for query that expected exactly one column.");
                    }

                    if (reader.FieldCount > 1)
                    {
                        throw new ApplicationException("More than one column returned for query that expected exactly one column.");
                    }

                    bool hasRow = await reader.ReadAsync().ConfigureAwait(false);

                    if (!hasRow)
                    {
                        throw new ApplicationException("No rows returned for query that expected exactly one row.");
                    }

                    var value = reader.GetValue(0);

                    bool hasMoreRows = await reader.ReadAsync().ConfigureAwait(false);

                    if (hasMoreRows)
                    {
                        throw new ApplicationException("More than one row returned for query that expected exactly one row.");
                    }

                    if (value is DBNull)
                    {
                        if (default(T) == null)
                        {
                            value = null;
                        }
                        else
                        {
                            throw new ApplicationException($"Cannot cast DBNull value to a value type parameter `{typeof(T).Name}`.");
                        }
                    }

                    return (T)value;
                }
            }
        }

        /// <summary>
        /// Creates a parameter of type T with NpgsqlDbType from the default type map 'defaultNpgsqlDbTypeMap'.
        /// </summary>
        public NpgsqlParameter<T> Parameter<T>(string parameterName, T value)
        {
            NpgsqlDbType dbType;

            var type = typeof(T);

            if (DefaultNpgsqlDbTypeMap.ContainsKey(type))
            {
                dbType = DefaultNpgsqlDbTypeMap[type];
            }
            else
            {
                throw new ApplicationException(
                    "Parameter type is not mapped to any \'NpgsqlDbType\'. Please specify a \'NpgsqlDbType\' explicitly.");
            }

            return this.Parameter(parameterName, value, dbType);
        }

        /// <summary>
        /// Creates a parameter of type T by explicitly specifying NpgsqlDbType.
        /// </summary>
        public NpgsqlParameter<T> Parameter<T>(string parameterName, T value, NpgsqlDbType dbType)
        {
            return new NpgsqlParameter<T>(parameterName, dbType)
            {
                TypedValue = value
            };
        }

        /// <summary>
        /// Executes a query and returns objects 
        /// </summary>
        public async Task<List<T>> Query<T>(string sql, params NpgsqlParameter[] parameters)
            where T : IPoco<T>, new()
        {
            if (sql == null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            await this.VerifyConnectionState().ConfigureAwait(false);

            var result = new List<T>();

            using (var command = this.dbConnection.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.AddRange(parameters);

                await command.PrepareAsync().ConfigureAwait(false);

                using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    // cached field count - I know it pointless, but I feel better by having it cached here.
                    int fieldCount = reader.FieldCount;

                    // cached setters for the result type
                    var setters = new Action<T, object>[fieldCount];

                    var metadata = GetMetadata<T>();

                    for (int i = 0; i < fieldCount; i++)
                    {
                        setters[i] = metadata.Setters[reader.GetName(i)];
                    }

                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        var instance = new T();

                        for (int i = 0; i < fieldCount; i++)
                        {
                            // ReSharper disable once AsyncConverter.CanBeUseAsyncMethodHighlighting
                            if (reader.IsDBNull(i))
                            {
                                setters[i](instance, null);
                            }
                            else
                            {
                                setters[i](instance, reader.GetValue(i));
                            }
                        }

                        result.Add(instance);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Returns one object of type T.
        /// If there are no rows then returns 'null';
        /// If there is more that one row then throws.
        /// </summary>
        public async Task<T> QueryOne<T>(string sql, params NpgsqlParameter[] parameters)
            where T : class, IPoco<T>, new()
        {
            if (sql == null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            await this.VerifyConnectionState().ConfigureAwait(false);

            using (var command = this.dbConnection.CreateCommand())
            {
                command.CommandText = sql;
                command.Parameters.AddRange(parameters);

                await command.PrepareAsync().ConfigureAwait(false);

                using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    bool hasRow = await reader.ReadAsync().ConfigureAwait(false);

                    if (!hasRow)
                    {
                        return null;
                    }

                    var instance = new T();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var setter = instance.Metadata.Setters[reader.GetName(i)];

                        // ReSharper disable once AsyncConverter.CanBeUseAsyncMethodHighlighting
                        if (reader.IsDBNull(i))
                        {
                            setter(instance, null);
                        }
                        else
                        {
                            setter(instance, reader.GetValue(i));
                        }
                    }

                    bool hasMoreRows = await reader.ReadAsync().ConfigureAwait(false);

                    if (hasMoreRows)
                    {
                        throw new ApplicationException("More than one row returned for query that expected only one row.");
                    }

                    return instance;
                }
            }
        }

        /// <summary>
        /// Opens the connection if it's closed.
        /// </summary>
        private Task VerifyConnectionState()
        {
            if (this.dbConnection.State == ConnectionState.Closed)
            {
                return this.dbConnection.OpenAsync();
            }

            return Task.CompletedTask;
        }
    }
}
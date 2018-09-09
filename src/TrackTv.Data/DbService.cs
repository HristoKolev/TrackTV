namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    using LinqToDB.Data;
    using LinqToDB.DataProvider;

    using Npgsql;

    using NpgsqlTypes;

    public partial class DbService : IDbService
    {
        private readonly Dictionary<Type, NpgsqlDbType> defaultNpgsqlDbTypeMap = new Dictionary<Type, NpgsqlDbType>
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
        };

        public DbService(NpgsqlConnection dbConnection, IDataProvider dataProvider)
        {
            this.DbConnection = dbConnection;
            this.DataConnection = new DataConnection(dataProvider, dbConnection, true);
        }

        private DataConnection DataConnection { get; }

        private NpgsqlConnection DbConnection { get; set; }

        public void Dispose()
        {
            this.DbConnection = null;
            this.DataConnection?.Dispose();
        }

        /// <summary>
        /// Executes a query and returns the rows affected.
        /// </summary>
        public async Task<int> ExecuteNonQuery(string sql, params NpgsqlParameter[] parameters)
        {
            await this.VerifyConnectionState();

            ValidateParameters(parameters);

            using (var command = this.DbConnection.CreateCommand())
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
            await this.VerifyConnectionState();

            ValidateParameters(parameters);

            using (var command = this.DbConnection.CreateCommand())
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

            if (this.defaultNpgsqlDbTypeMap.ContainsKey(type))
            {
                dbType = this.defaultNpgsqlDbTypeMap[type];
            }
            else
            {
                throw new ApplicationException($"Parameter type '{type.Name}' is not mapped to any 'NpgsqlDbType'. "
                                               + $"Please specify a 'NpgsqlDbType' explicitly. ParameterName: {parameterName}");
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
            await this.VerifyConnectionState();

            ValidateParameters(parameters);

            var result = new List<T>();

            using (var command = this.DbConnection.CreateCommand())
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
            await this.VerifyConnectionState();

            ValidateParameters(parameters);

            using (var command = this.DbConnection.CreateCommand())
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

        private static void ValidateParameters(NpgsqlParameter[] parameters)
        {
            //TODO: validate sql/parameter integrity
        }

        private async Task VerifyConnectionState()
        {
            //TODO: propper edge case handling on connection opening.

            if (this.DbConnection.State == ConnectionState.Closed)
            {
                await this.DbConnection.OpenAsync().ConfigureAwait(false);
            }
        }
    }
}
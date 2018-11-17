﻿namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading;
    using System.Threading.Tasks;

    using LinqToDB.Data;
    using LinqToDB.DataProvider.PostgreSQL;

    using Npgsql;

    using NpgsqlTypes;

    public partial class DbService<TPocos> : IDbService<TPocos> where TPocos : IDbPocos<TPocos>, new()
    {
        /// <summary>
        /// The default parameter type map that is used when creating parameters without specifying the NpgsqlDbType explicitly.
        /// </summary>
        // ReSharper disable once StaticMemberInGenericType
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

        private TPocos poco;

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

        public TPocos Poco
        {
            get
            {
                if (this.poco == null)
                {
                    this.poco = new TPocos
                    {
                        DbService = this
                    };
                }

                return this.poco;
            }
        }

        public void Dispose()
        {
            this.LinqToDbConnection?.Dispose();
        }

        public Task<int> ExecuteNonQuery(string sql, params NpgsqlParameter[] parameters)
        {
            if (sql == null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return this.ExecuteNonQueryInternal(sql, parameters);
        }

        public Task<T> ExecuteScalar<T>(string sql, params NpgsqlParameter[] parameters)
        {
            if (sql == null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return this.ExecuteScalarInternal<T>(sql, parameters);
        }

        public NpgsqlParameter Parameter<T>(string parameterName, T value)
        {
            if (value == null)
            {
                return new NpgsqlParameter(parameterName, DBNull.Value);
            }

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

        public NpgsqlParameter Parameter<T>(string parameterName, T value, NpgsqlDbType dbType)
        {
            if (value == null)
            {
                return new NpgsqlParameter(parameterName, DBNull.Value);
            }

            return new NpgsqlParameter(parameterName, dbType)
            {
                Value = value
            };
        }

        public Task<List<T>> Query<T>(string sql, params NpgsqlParameter[] parameters)
            where T : new()
        {
            if (sql == null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return this.QueryInternal<T>(sql, parameters);
        }

        public Task<T> QueryOne<T>(string sql, params NpgsqlParameter[] parameters)
            where T : class, new()
        {
            if (sql == null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            return this.QueryOneInternal<T>(sql, parameters);
        }

        private async Task<int> ExecuteNonQueryInternal(
            string sql,
            IEnumerable<NpgsqlParameter> parameters,
            CancellationToken cancellationToken = default)
        {
            await this.VerifyConnectionState(cancellationToken);

            using (var command = this.dbConnection.CreateCommand())
            {
                command.CommandText = sql;

                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                await command.PrepareAsync(cancellationToken);

                return await command.ExecuteNonQueryAsync(cancellationToken);
            }
        }

        private async Task<T> ExecuteScalarInternal<T>(
            string sql,
            IEnumerable<NpgsqlParameter> parameters,
            CancellationToken cancellationToken = default)
        {
            await this.VerifyConnectionState(cancellationToken);

            using (var command = this.dbConnection.CreateCommand())
            {
                command.CommandText = sql;

                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                await command.PrepareAsync(cancellationToken);

                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    if (reader.FieldCount == 0)
                    {
                        throw new ApplicationException("No columns returned for query that expected exactly one column.");
                    }

                    if (reader.FieldCount > 1)
                    {
                        throw new ApplicationException("More than one column returned for query that expected exactly one column.");
                    }

                    bool hasRow = await reader.ReadAsync(cancellationToken);

                    if (!hasRow)
                    {
                        throw new ApplicationException("No rows returned for query that expected exactly one row.");
                    }

                    var value = reader.GetValue(0);

                    bool hasMoreRows = await reader.ReadAsync(cancellationToken);

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
                            throw new ApplicationException("Cannot cast DBNull value to a value type parameter.");
                        }
                    }

                    return (T)value;
                }
            }
        }

        private async Task<List<T>> QueryInternal<T>(
            string sql,
            IEnumerable<NpgsqlParameter> parameters,
            CancellationToken cancellationToken = default)
            where T : new()
        {
            await this.VerifyConnectionState(cancellationToken);

            var result = new List<T>();

            using (var command = this.dbConnection.CreateCommand())
            {
                command.CommandText = sql;

                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                await command.PrepareAsync(cancellationToken);

                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    var setters = DbCodeGenerator.GenerateSetters<T>();

                    // cached field count - I know it pointless, but I feel better by having it cached here.
                    int fieldCount = reader.FieldCount;

                    // cached setters for the result type
                    var settersByColumnOrder = new Action<T, object>[fieldCount];

                    for (int i = 0; i < fieldCount; i++)
                    {
                        settersByColumnOrder[i] = setters[reader.GetName(i)];
                    }

                    while (await reader.ReadAsync(cancellationToken))
                    {
                        var instance = new T();

                        for (int i = 0; i < fieldCount; i++)
                        {
                            // ReSharper disable once AsyncConverter.CanBeUseAsyncMethodHighlighting
                            if (reader.IsDBNull(i))
                            {
                                settersByColumnOrder[i](instance, null);
                            }
                            else
                            {
                                settersByColumnOrder[i](instance, reader.GetValue(i));
                            }
                        }

                        result.Add(instance);
                    }
                }
            }

            return result;
        }

        private async Task<T> QueryOneInternal<T>(
            string sql,
            IEnumerable<NpgsqlParameter> parameters,
            CancellationToken cancellationToken = default)
            where T : class, new()
        {
            await this.VerifyConnectionState(cancellationToken);

            using (var command = this.dbConnection.CreateCommand())
            {
                command.CommandText = sql;

                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                await command.PrepareAsync(cancellationToken);

                using (var reader = await command.ExecuteReaderAsync(cancellationToken))
                {
                    var instance = new T();

                    var setters = DbCodeGenerator.GenerateSetters<T>();

                    bool hasRow = await reader.ReadAsync(cancellationToken);

                    if (!hasRow)
                    {
                        return null;
                    }

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var setter = setters[reader.GetName(i)];

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

                    bool hasMoreRows = await reader.ReadAsync(cancellationToken);

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
        private Task VerifyConnectionState(CancellationToken cancellationToken = default)
        {
            if (this.dbConnection.State == ConnectionState.Closed)
            {
                return this.dbConnection.OpenAsync(cancellationToken);
            }

            return Task.CompletedTask;
        }
    }
}
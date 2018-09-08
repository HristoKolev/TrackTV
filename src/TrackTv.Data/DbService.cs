namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    using LinqToDB;
    using LinqToDB.Data;
    using LinqToDB.DataProvider;

    using Npgsql;

    using NpgsqlTypes;

    public partial class DbService : IDbService
    {
        public DbService(NpgsqlConnection dbConnection, IDataProvider dataProvider)
        {
            this.DbConnection = dbConnection;
            this.DataConnection = new DataConnection(dataProvider, dbConnection, true);
        }

        private DataConnection DataConnection { get; }

        private NpgsqlConnection DbConnection { get; set; }

        /// <summary>
        /// This is sync. I don't like it.
        /// </summary>
        public void BulkInsertSync<TPoco>(IEnumerable<TPoco> list)
            where TPoco : IPoco<TPoco>
        {
            this.DataConnection.BulkCopy(list);
        }

        public Task Delete<TPoco>(TPoco poco) where TPoco : IPoco<TPoco>
        {
            var metadata = poco.Metadata;

            int pk = metadata.GetPrimaryKey(poco);

            return this.Delete<TPoco>(pk);
        }

        /// <summary>
        /// <para>Deletes a number of records from a table mapped to <see cref="TPoco"/> by ID.</para>
        /// </summary>
        public Task Delete<TPoco>(int[] ids)
            where TPoco : IPoco<TPoco>
        {
            if (ids.Length == 0)
            {
                return Task.CompletedTask;
            }

            var metadata = GetMetadata<TPoco>();

            string tableSchema = metadata.TableSchema;
            string tableName = metadata.TableName;
            string primaryKeyName = metadata.PrimaryKeyColumnName;

            string sql = $"DELETE FROM {tableSchema}.{tableName} WHERE {primaryKeyName} IN ({string.Join(", ", ids)});";

            throw new NotImplementedException();
        }

        /// <summary>
        /// <para>Deletes a record from a table mapped to <see cref="TPoco"/> by ID.</para>
        /// </summary>
        public Task Delete<TPoco>(int id)
            where TPoco : IPoco<TPoco>
        {
            var metadata = GetMetadata<TPoco>();

            string tableSchema = metadata.TableSchema;
            string tableName = metadata.TableName;
            string primaryKeyName = metadata.PrimaryKeyColumnName;

            string sql = $"DELETE FROM {tableSchema}.{tableName} WHERE {primaryKeyName} = {id};";

            return this.DataConnection.ExecuteAsync(sql);
        }

        public void Dispose()
        {
            this.DbConnection = null;
            this.DataConnection?.Dispose();
        }

        public Task ExecuteInTransaction(Func<Task> body) => this.ExecuteInTransaction(tr => body());

        public async Task ExecuteInTransaction(Func<IDbTransaction, Task> body, TimeSpan? timeout = null)
        {
            if (this.DbConnection.State != ConnectionState.Open)
            {
                await this.DbConnection.OpenAsync().ConfigureAwait(false);
            }

            using (var transaction = new DbTransactionWrapper(this.DbConnection.BeginTransaction()))
            {
                if (timeout == null)
                {
                    await body(transaction).ConfigureAwait(false);
                }
                else
                {
                    var timeoutTask = Task.Delay(timeout.Value);
                    var transactionTask = body(transaction);

                    var completedTask = await Task.WhenAny(transactionTask, timeoutTask).ConfigureAwait(false);

                    if (completedTask == timeoutTask)
                    {
                        throw new TimeoutException($"The db transaction timed out (timeout: {timeout})");
                    }

                    await transactionTask.ConfigureAwait(false);
                }

                if (!transaction.IsRolledBack)
                {
                    transaction.ActualCommit();
                }
            }
        }

        public Task<int> Insert<TPoco>(TPoco poco)
            where TPoco : IPoco<TPoco>
        {
            return this.DataConnection.InsertWithInt32IdentityAsync(poco);
        }

        public async Task<int> Save<TPoco>(TPoco poco)
            where TPoco : IPoco<TPoco>
        {
            var metadata = poco.Metadata;

            if (metadata.IsNew(poco))
            {
                return await this.Insert(poco).ConfigureAwait(false);
            }

            await this.Update(poco).ConfigureAwait(false);

            return metadata.GetPrimaryKey(poco);
        }

        public Task Update<TPoco>(TPoco poco)
            where TPoco : IPoco<TPoco>
        {
            return this.DataConnection.UpdateAsync(poco);
        }

        public async Task<List<T>> Query<T>(string sql, params NpgsqlParameter[] parameters) 
            where T: IPoco<T>, new()
        {
            if (this.DbConnection.State == ConnectionState.Closed)
            {
                await this.DbConnection.OpenAsync().ConfigureAwait(false);
            }
            
            //TODO: validate sql/parameter integrity

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

        private readonly Dictionary<Type, NpgsqlDbType> npgsqlDbTypeMap = new Dictionary<Type, NpgsqlDbType>
        {
            {typeof(int), NpgsqlDbType.Integer },
            {typeof(long), NpgsqlDbType.Bigint },
            {typeof(bool), NpgsqlDbType.Boolean },
            {typeof(float), NpgsqlDbType.Real },
            {typeof(double), NpgsqlDbType.Double },
            {typeof(short), NpgsqlDbType.Smallint },
            {typeof(decimal), NpgsqlDbType.Numeric },
            {typeof(string), NpgsqlDbType.Text },
            {typeof(DateTime), NpgsqlDbType.Timestamp },
            {typeof(byte[]), NpgsqlDbType.Bytea },
        };

        public NpgsqlParameter<T> Parameter<T>(string parameterName, T value)
        {
            NpgsqlDbType dbType;

            var type = typeof(T);

            if (this.npgsqlDbTypeMap.ContainsKey(type))
            {
                dbType = this.npgsqlDbTypeMap[type];
            }
            else
            {
                throw new ApplicationException($"Parameter type '{type.Name}' is not mapped to any 'NpgsqlDbType'. "
                                               + $"Please specify a 'NpgsqlDbType' explicitly. ParameterName: {parameterName}");
            }
            
            return new NpgsqlParameter<T>(parameterName, dbType)
            {
                TypedValue = value
            };
        }

        public NpgsqlParameter<T> Parameter<T>(string parameterName, T value, NpgsqlDbType dbType)
        {
            return new NpgsqlParameter<T>(parameterName, dbType)
            {
                TypedValue = value
            };
        }
    }
}
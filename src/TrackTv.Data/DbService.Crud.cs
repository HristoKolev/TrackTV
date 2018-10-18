namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    using Npgsql;

    public partial class DbService<TPocos>
    {
        /// <summary>
        /// Inserts several records in single query.
        /// </summary>
        public Task<int> BulkInsert<T>(IEnumerable<T> pocos, CancellationToken cancellationToken = default)
            where T : IPoco<T>
        {
            var metadata = this.GetMetadata<T>();
            var columns = metadata.Columns;

            var sqlBuilder = new StringBuilder(128);

            // STATEMENT HEADER
            sqlBuilder.Append("INSERT INTO \"");
            sqlBuilder.Append(metadata.TableSchema);
            sqlBuilder.Append("\".\"");
            sqlBuilder.Append(metadata.TableName);
            sqlBuilder.Append("\" (");

            bool headerFirstRun = true;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];

                if (!column.IsPrimaryKey)
                {
                    if (headerFirstRun)
                    {
                        sqlBuilder.Append("\"");
                        headerFirstRun = false;
                    }
                    else
                    {
                        sqlBuilder.Append(", \"");
                    }

                    sqlBuilder.Append(column.ColumnName);
                    sqlBuilder.Append('"');
                }
            }

            sqlBuilder.Append(") VALUES ");

            var allParameters = new List<NpgsqlParameter>();

            // PARAMETERS
            int paramIndex = 0;

            bool recordsFirstRun = true;

            foreach (var record in pocos)
            {
                if (!recordsFirstRun)
                {
                    sqlBuilder.Append(", ");
                }

                sqlBuilder.Append("\n(");
                recordsFirstRun = false;

                var parameters = metadata.GenerateParameters(record);

                allParameters.AddRange(parameters);
                
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (i != 0)
                    {
                        sqlBuilder.Append(", ");
                    }
                    
                    int currentIndex = paramIndex++;
                    var parameter = parameters[i];
                    parameter.ParameterName = "p" + currentIndex;

                    sqlBuilder.Append("@p");
                    sqlBuilder.Append(currentIndex);
                }

                sqlBuilder.Append(")");
            }

            sqlBuilder.Append(";");

            string sql = sqlBuilder.ToString();

            return this.ExecuteNonQueryInternal(sql, allParameters, cancellationToken);
        }

        /// <summary>
        /// Deletes a record by its PrimaryKey.
        /// </summary>
        public Task<int> Delete<T>(T poco, CancellationToken cancellationToken = default)
            where T : IPoco<T>
        {
            var metadata = poco.Metadata;

            int pk = metadata.GetPrimaryKey(poco);

            return this.Delete<T>(pk, cancellationToken);
        }

        /// <summary>
        /// <para>Deletes records from a table by their IDs.</para>
        /// </summary>
        public Task<int> Delete<T>(int[] ids, CancellationToken cancellationToken = default)
            where T : IPoco<T>
        {
            if (ids.Length == 0)
            {
                return Task.FromResult(0);
            }

            var metadata = this.GetMetadata<T>();

            string tableSchema = metadata.TableSchema;
            string tableName = metadata.TableName;
            string primaryKeyName = metadata.PrimaryKeyColumnName;

            var parameters = new[]
            {
                this.Parameter("pk", ids)
            };

            string sql = $"DELETE FROM \"{tableSchema}\".\"{tableName}\" WHERE \"{primaryKeyName}\" = any(@pk);";

            return this.ExecuteNonQueryInternal(sql, parameters, cancellationToken);
        }

        /// <summary>
        /// <para>Deletes a record by ID.</para>
        /// </summary>
        public Task<int> Delete<T>(int id, CancellationToken cancellationToken = default)
            where T : IPoco<T>
        {
            var metadata = this.GetMetadata<T>();

            string tableSchema = metadata.TableSchema;
            string tableName = metadata.TableName;
            string primaryKeyName = metadata.PrimaryKeyColumnName;

            var parameters = new[]
            {
                this.Parameter("pk", id)
            };

            string sql = $"DELETE FROM \"{tableSchema}\".\"{tableName}\" WHERE \"{primaryKeyName}\" = @pk;";

            return this.ExecuteNonQueryInternal(sql, parameters, cancellationToken);
        }

        /// <summary>
        /// Inserts a record and attaches it's ID to the poco object. 
        /// </summary>
        public async Task<int> Insert<T>(T poco, CancellationToken cancellationToken = default)
            where T : IPoco<T>
        {
            int pk = await this.InsertWithoutMutating(poco, cancellationToken);

            poco.Metadata.SetPrimaryKey(poco, pk);

            return pk;
        }

        /// <summary>
        /// Inserts a record and returns its ID.
        /// </summary>
        public Task<int> InsertWithoutMutating<T>(T poco, CancellationToken cancellationToken = default)
            where T : IPoco<T>
        {
            var metadata = poco.Metadata;

            var (columnNames, parameters) = metadata.GetAllColumns(poco);

            var sqlBuilder = new StringBuilder(128);

            // STATEMENT HEADER
            sqlBuilder.Append("INSERT INTO \"");
            sqlBuilder.Append(metadata.TableSchema);
            sqlBuilder.Append("\".\"");
            sqlBuilder.Append(metadata.TableName);
            sqlBuilder.Append("\" (");

            for (int i = 0; i < columnNames.Length; i++)
            {
                if (i != 0)
                {
                    sqlBuilder.Append(", ");
                }

                sqlBuilder.Append('"');
                sqlBuilder.Append(columnNames[i]);
                sqlBuilder.Append('"');
            }

            sqlBuilder.Append(")\n VALUES (");

            for (int i = 0; i < parameters.Length; i++)
            {
                if (i != 0)
                {
                    sqlBuilder.Append(", ");
                }

                sqlBuilder.Append("@p");
                sqlBuilder.Append(i);

                parameters[i].ParameterName = "p" + i;
            }

            // STATEMENT FOOTER
            sqlBuilder.Append(") RETURNING \"");
            sqlBuilder.Append(metadata.PrimaryKeyColumnName);
            sqlBuilder.Append("\";");

            string sql = sqlBuilder.ToString();

            return this.ExecuteScalarInternal<int>(sql, parameters, cancellationToken);
        }

        /// <summary>
        /// Saves a record to the database.
        /// If the poco object has a positive primary key it updates it.
        /// If the primary key value is 0 it inserts the record.
        /// Returns the record's primary key value.
        /// </summary>
        public async Task<int> Save<T>(T poco, CancellationToken cancellationToken = default)
            where T : class, IPoco<T>, new()
        {
            var metadata = poco.Metadata;

            if (metadata.IsNew(poco))
            {
                return await this.Insert(poco, cancellationToken);
            }

            await this.Update(poco, cancellationToken);

            return metadata.GetPrimaryKey(poco);
        }

        /// <summary>
        /// Updates a record by its ID.
        /// </summary>
        public Task<int> Update<T>(T poco, CancellationToken cancellationToken = default)
            where T : class, IPoco<T>, new()
        {
            var metadata = poco.Metadata;

            int pk = metadata.GetPrimaryKey(poco);

            if (pk == default)
            {
                throw new ApplicationException("Cannot update a model with primary key of 0.");
            }

            var (columnNames, parameters) = metadata.GetAllColumns(poco);

            if (columnNames.Length == 0)
            {
                return Task.FromResult(0); // nothing to update?
            }

            var sqlBuilder = new StringBuilder();

            sqlBuilder.Append("UPDATE \"");
            sqlBuilder.Append(metadata.TableSchema);
            sqlBuilder.Append("\".\"");
            sqlBuilder.Append(metadata.TableName);
            sqlBuilder.Append("\" SET ");

            for (int i = 0; i < columnNames.Length; i++)
            {
                string columnName = columnNames[i];
                var parameter = parameters[i];

                string paramName = "@p" + i;

                sqlBuilder.Append("\n\"");
                sqlBuilder.Append(columnName);
                sqlBuilder.Append("\" = ");
                sqlBuilder.Append(paramName);

                if (i != columnNames.Length - 1)
                {
                    sqlBuilder.Append(',');
                }

                parameter.ParameterName = paramName;
            }

            sqlBuilder.Append("\nWHERE \"");
            sqlBuilder.Append(metadata.PrimaryKeyColumnName);
            sqlBuilder.Append("\" = @pk;");

            string sql = sqlBuilder.ToString();

            var allParameters = parameters.Concat(new[]
            {
                this.Parameter("pk", pk)
            });

            return this.ExecuteNonQueryInternal(sql, allParameters, cancellationToken);
        }

        /// <summary>
        /// Updates a record by its ID.
        /// Only updates the changed rows. 
        /// </summary>
        public async Task<int> UpdateChangesOnly<T>(T poco, CancellationToken cancellationToken = default)
            where T : class, IPoco<T>, new()
        {
            var metadata = poco.Metadata;

            int pk = metadata.GetPrimaryKey(poco);

            if (pk == default)
            {
                throw new ApplicationException("Cannot update a model with primary key of 0.");
            }

            string selectSql =
                $"select * from \"{metadata.TableSchema}\".\"{metadata.TableName}\" where \"{metadata.PrimaryKeyColumnName}\" = @pk FOR UPDATE;";

            var selectParameters = new[]
            {
                this.Parameter("pk", pk)
            };

            var currentInstance = await this.QueryOnePocoInternal<T>(selectSql, selectParameters, cancellationToken);

            if (currentInstance == null)
            {
                throw new ApplicationException("Cannot update record: record does not exists."); // no record to update?
            }

            var (names, parameters) = metadata.GetColumnChanges(currentInstance, poco);

            if (names.Count == 0)
            {
                return 0; // nothing to update?
            }

            var sqlBuilder = new StringBuilder();

            sqlBuilder.Append("UPDATE \"");
            sqlBuilder.Append(metadata.TableSchema);
            sqlBuilder.Append("\".\"");
            sqlBuilder.Append(metadata.TableName);
            sqlBuilder.Append("\" SET ");

            for (int i = 0; i < names.Count; i++)
            {
                string name = names[i];
                var parameter = parameters[i];

                string paramName = "@p" + i;

                sqlBuilder.Append('\n');
                sqlBuilder.Append(name);
                sqlBuilder.Append(" = ");
                sqlBuilder.Append(paramName);

                if (i != names.Count - 1)
                {
                    sqlBuilder.Append(',');
                }

                parameter.ParameterName = paramName;
            }

            sqlBuilder.Append("\nWHERE ");
            sqlBuilder.Append(metadata.PrimaryKeyColumnName);
            sqlBuilder.Append(" = @pk;");

            parameters.Add(this.Parameter("pk", pk));

            string sql = sqlBuilder.ToString();

            return await this.ExecuteNonQueryInternal(sql, parameters, cancellationToken);
        }

        public Task<T> FindByID<T>(int id, CancellationToken cancellationToken = default)
            where T : class, IPoco<T>, new()
        {
            var metadata = this.GetMetadata<T>();

            string tableSchema = metadata.TableSchema;
            string tableName = metadata.TableName;
            string primaryKeyName = metadata.PrimaryKeyColumnName;

            var parameters = new[]
            {
                this.Parameter("pk", id)
            };

            string sql = $"SELECT * FROM \"{tableSchema}\".\"{tableName}\" WHERE \"{primaryKeyName}\" = @pk;";

            return this.QueryOnePocoInternal<T>(sql, parameters, cancellationToken);
        }
    }
}
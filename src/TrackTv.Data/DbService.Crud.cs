namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Npgsql;

    public partial class DbService
    {
        public Task<int> BulkInsert<T>(IEnumerable<T> pocos)
            where T : IPoco<T>
        {
            var metadata = GetMetadata<T>();
            var columns = metadata.Columns;

            var sqlBuilder = new StringBuilder(128);

            var parameters = new List<NpgsqlParameter>();

            // STATEMENT HEADER
            sqlBuilder.Append("INSERT INTO \"").Append(metadata.TableSchema).Append("\".\"").Append(metadata.TableName).Append("\" (");

            bool headerFirstRun = true;

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

                    sqlBuilder.Append(column.ColumnName).Append('"');
                }
            }

            sqlBuilder.Append(") VALUES ");

            // PARAMETERS

            int paramIndex = 0;

            bool recordsFirstRun = true;

            foreach (var record in pocos)
            {
                parameters.AddRange(metadata.GenerateParameters(record, paramIndex));

                if (recordsFirstRun)
                {
                    sqlBuilder.Append("\n(");
                    recordsFirstRun = false;
                }
                else
                {
                    sqlBuilder.Append(", \r(");
                }

                bool parametersFirstRun = true;

                for (int i = 0; i < columns.Count; i++)
                {
                    var column = columns[i];

                    if (!column.IsPrimaryKey)
                    {
                        if (parametersFirstRun)
                        {
                            sqlBuilder.Append("@p");
                            parametersFirstRun = false;
                        }
                        else
                        {
                            sqlBuilder.Append(", @p");
                        }

                        sqlBuilder.Append(paramIndex++);
                    }
                }

                sqlBuilder.Append(")");
            }

            sqlBuilder.Append(";");

            string sql = sqlBuilder.ToString();

            return this.ExecuteNonQuery(sql, parameters.ToArray());
        }

        public Task<int> Delete<T>(T poco)
            where T : IPoco<T>
        {
            var metadata = poco.Metadata;

            int pk = metadata.GetPrimaryKey(poco);

            return this.Delete<T>(pk);
        }

        /// <summary>
        /// <para>Deletes a number of records from a table mapped to <see cref="T"/> by ID.</para>
        /// </summary>
        public Task<int> Delete<T>(int[] ids)
            where T : IPoco<T>
        {
            if (ids.Length == 0)
            {
                return Task.FromResult(0);
            }

            var metadata = GetMetadata<T>();

            string tableSchema = metadata.TableSchema;
            string tableName = metadata.TableName;
            string primaryKeyName = metadata.PrimaryKeyColumnName;

            return this.ExecuteNonQuery($"DELETE FROM \"{tableSchema}\".\"{tableName}\" WHERE {primaryKeyName} = any(@p);",
                                        this.Parameter("p", ids));
        }

        /// <summary>
        /// <para>Deletes a record from a table mapped to <see cref="T"/> by ID.</para>
        /// </summary>
        public Task<int> Delete<T>(int id)
            where T : IPoco<T>
        {
            var metadata = GetMetadata<T>();

            string tableSchema = metadata.TableSchema;
            string tableName = metadata.TableName;
            string primaryKeyName = metadata.PrimaryKeyColumnName;

            return this.ExecuteNonQuery($"DELETE FROM \"{tableSchema}\".\"{tableName}\" WHERE {primaryKeyName} = @p;",
                                        this.Parameter("p", id));
        }

        public async Task<int> Insert<T>(T poco)
            where T : IPoco<T>
        {
            int pk = await this.InsertWithoutMutating(poco).ConfigureAwait(false);

            poco.Metadata.SetPrimaryKey(poco, pk);

            return pk;
        }

        public Task<int> InsertWithoutMutating<T>(T poco)
            where T : IPoco<T>
        {
            var metadata = poco.Metadata;
            var columns = metadata.Columns;

            var sqlBuilder = new StringBuilder(128);

            // STATEMENT HEADER
            sqlBuilder.Append("INSERT INTO \"");
            sqlBuilder.Append(metadata.TableSchema);
            sqlBuilder.Append("\".\"");
            sqlBuilder.Append(metadata.TableName);
            sqlBuilder.Append("\" (");

            bool headerFirstRun = true;

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

                    sqlBuilder.Append(column.ColumnName); sqlBuilder.Append('"');
                }
            }

            // STATEMENT VALUES 
            sqlBuilder.Append(") VALUES (");

            bool parametersFirstRun = true;

            int paramIndex = 0;

            for (int i = 0; i < columns.Count; i++)
            {
                var column = columns[i];

                if (!column.IsPrimaryKey)
                {
                    if (parametersFirstRun)
                    {
                        sqlBuilder.Append("@p");
                        parametersFirstRun = false;
                    }
                    else
                    {
                        sqlBuilder.Append(", @p");
                    }

                    sqlBuilder.Append(paramIndex++);
                }
            }

            // STATEMENT FOOTER
            sqlBuilder.Append(") RETURNING ");
            sqlBuilder.Append(metadata.PrimaryKeyColumnName);
            sqlBuilder.Append(";");

            string sql = sqlBuilder.ToString();

            var parameters = metadata.GenerateParameters(poco, 0);

            return this.ExecuteScalar<int>(sql, parameters);
        }

        public async Task<int> Save<T>(T poco)
            where T : class, IPoco<T>, new()
        {
            var metadata = poco.Metadata;

            if (metadata.IsNew(poco))
            {
                return await this.Insert(poco).ConfigureAwait(false);
            }

            await this.Update(poco).ConfigureAwait(false);

            return metadata.GetPrimaryKey(poco);
        }

        public async Task<int> Update<T>(T poco)
            where T : class, IPoco<T>, new()
        {
            var metadata = poco.Metadata;

            int pk = metadata.GetPrimaryKey(poco);

            if (pk == default)
            {
                throw new ApplicationException("Cannot update a model with primary key of 0.");
            }

            var currentInstance = await this.QueryOne<T>(
                $"select * from \"{metadata.TableSchema}\".\"{metadata.TableName}\" where \"{metadata.PrimaryKeyColumnName}\" = @p FOR UPDATE;",
                this.Parameter("p", pk));

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

            return await this.ExecuteNonQuery(sql, parameters.ToArray());
        }
    }
}
namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using LinqToDB;
    using LinqToDB.Data;

    public partial class DbService
    {
        /// <summary>
        /// This is sync. I don't like it.
        /// </summary>
        public void BulkInsertSync<TPoco>(IEnumerable<TPoco> list)
            where TPoco : IPoco<TPoco>
        {
            this.DataConnection.BulkCopy(list);
        }

        public Task Delete<TPoco>(TPoco poco)
            where TPoco : IPoco<TPoco>
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
    }
}
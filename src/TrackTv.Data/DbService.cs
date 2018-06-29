﻿namespace TrackTv.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    using LinqToDB;
    using LinqToDB.Data;
    using LinqToDB.DataProvider;

    using Npgsql;

    public partial class DbService : IDbService
    {
        private readonly ConcurrentStack<KeyValuePair<string, DataParameter[]>> sqlLog =
            new ConcurrentStack<KeyValuePair<string, DataParameter[]>>();

        public DbService(NpgsqlConnection dbConnection, IDataProvider dataProvider)
        {
            this.DbConnection = dbConnection;
            this.DataProvider = dataProvider as IProfiledDataProvider;

            this.DataConnection = new DataConnection(dataProvider, dbConnection, true);

            if (this.DataProvider != null)
            {
                this.DataProvider.OnInitCommand += this.OnInitCommand;
            }
        }

        private DataConnection DataConnection { get; }

        private IProfiledDataProvider DataProvider { get; }

        private NpgsqlConnection DbConnection { get; set; }

        public Task Delete<TPoco>(TPoco poco)
            where TPoco : IPoco
        {
            return this.Delete<TPoco>(poco.GetPrimaryKey());
        }

        /// <summary>
        /// <para>Deletes a number of records from a table mapped to <see cref="TPoco"/> by ID.</para>
        /// </summary>
        public Task Delete<TPoco>(int[] ids)
            where TPoco : IPoco
        {
            if (ids.Length == 0)
            {
                return Task.CompletedTask;
            }

            var type = typeof(TPoco);

            string tableSchema = TableSchemaMap[type];
            string tableName = TableNameMap[type];
            string primaryKeyName = PrimaryKeyMap[type];

            string sql = $"DELETE FROM {tableSchema}.{tableName} WHERE {primaryKeyName} IN ({string.Join(", ", ids)});";

            return this.DataConnection.ExecuteAsync(sql);
        }

        /// <summary>
        /// <para>Deletes a record from a table mapped to <see cref="TPoco"/> by ID.</para>
        /// </summary>
        public Task Delete<TPoco>(int id)
            where TPoco : IPoco
        {
            var type = typeof(TPoco);

            string tableSchema = TableSchemaMap[type];
            string tableName = TableNameMap[type];
            string primaryKeyName = PrimaryKeyMap[type];

            string sql = $"DELETE FROM {tableSchema}.{tableName} WHERE {primaryKeyName} = {id};";

            return this.DataConnection.ExecuteAsync(sql);
        }

        public void Dispose()
        {
            this.sqlLog.Clear();

            if (this.DataProvider != null)
            {
                this.DataProvider.OnInitCommand -= this.OnInitCommand;
            }

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

            this.sqlLog.Clear();

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
            where TPoco : IPoco
        {
            return this.DataConnection.InsertWithInt32IdentityAsync(poco);
        }

        public async Task<int> Save<TPoco>(TPoco poco)
            where TPoco : IPoco
        {
            if (poco.IsNew())
            {
                return await this.Insert(poco).ConfigureAwait(false);
            }

            await this.Update(poco).ConfigureAwait(false);

            return poco.GetPrimaryKey();
        }

        public Task Update<TPoco>(TPoco poco)
            where TPoco : IPoco
        {
            return this.DataConnection.UpdateAsync(poco);
        }

        /// <summary>
        /// This is sync. I don't like it.
        /// </summary>
        public void BulkInsertSync<TPoco>(IEnumerable<TPoco> list)
            where TPoco : IPoco
        {
            this.DataConnection.BulkCopy(list);
        }

        private void OnInitCommand(object sender, InitSqlCommandEventArgs args)
        {
            this.sqlLog.Push(new KeyValuePair<string, DataParameter[]>(args.CommandText, args.Parameters));
        }
    }

    public interface IPoco
    {
        /// <summary>		
        /// <para>Returns the primary key for the table.</para>
        /// </summary>   
        int GetPrimaryKey();

        /// <summary>		
        /// <para>Returns true if the record hasn't been inserted to the database yet.</para>
        /// </summary> 
        bool IsNew();
        
        /// <summary>		
        /// <para>Sets the primary key for the table.</para>
        /// </summary> 
        void SetPrimaryKey(int value);

        /// <summary>
        /// <para>Clones the current object and returns the clone.</para>
        /// </summary>
        IPoco Clone();
    }

    public partial interface IDbService : IDisposable
    {
        Task Delete<TPoco>(TPoco poco)
            where TPoco : IPoco;

        Task Delete<TPoco>(int[] ids)
            where TPoco : IPoco;

        Task Delete<TPoco>(int id)
            where TPoco : IPoco;

        Task ExecuteInTransaction(Func<Task> body);

        Task ExecuteInTransaction(Func<IDbTransaction, Task> body, TimeSpan? timeout = null);

        Task<int> Insert<TPoco>(TPoco poco)
            where TPoco : IPoco;

        Task<int> Save<TPoco>(TPoco poco)
            where TPoco : IPoco;

        Task Update<TPoco>(TPoco poco)
            where TPoco : IPoco;

        void BulkInsertSync<TPoco>(IEnumerable<TPoco> list)
            where TPoco : IPoco;
    }
}
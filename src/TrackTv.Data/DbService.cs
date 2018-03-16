namespace TrackTv.Data
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;

    using LinqToDB;
    using LinqToDB.Data;
    using LinqToDB.DataProvider;

    public partial class DbService : IDbService
    {
        private readonly ConcurrentStack<KeyValuePair<string, DataParameter[]>> sqlLog =
            new ConcurrentStack<KeyValuePair<string, DataParameter[]>>();

        public DbService(IDbConnection dbConnection, IDataProvider dataProvider)
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

        private IDbConnection DbConnection { get; set; }

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

            string tableName = this.tableNameMap[type];
            string primaryKeyName = this.primaryKeyMap[type];
            string tableSchema = this.tableSchemaMap[type];

            return this.DataConnection.ExecuteAsync($"DELETE FROM {tableSchema}.{tableName} WHERE {primaryKeyName} IN ({string.Join(", ", ids)});");
        }

        /// <summary>
        /// <para>Deletes a record from a table mapped to <see cref="TPoco"/> by ID.</para>
        /// </summary>
        public Task Delete<TPoco>(int id)
            where TPoco : IPoco
        {
            var type = typeof(TPoco);

            string tableName = this.tableNameMap[type];
            string primaryKeyName = this.primaryKeyMap[type];
            string tableSchema = this.tableSchemaMap[type];

            return this.DataConnection.ExecuteAsync($"DELETE FROM {tableSchema}.{tableName} WHERE {primaryKeyName} = {id};");
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

        public async Task ExecuteInTransaction(Func<IDbTransaction, Task> body)
        {
            if (this.DbConnection.State != ConnectionState.Open)
            {
                this.DbConnection.Open();
            }

            this.sqlLog.Clear();

            using (var transaction = new DbTransactionWrapper(this.DbConnection.BeginTransaction()))
            {
                await body(transaction).ConfigureAwait(false);

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

        public void BulkInsert<TPoco>(IEnumerable<TPoco> list)
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

        Task ExecuteInTransaction(Func<IDbTransaction, Task> body);

        Task<int> Insert<TPoco>(TPoco poco)
            where TPoco : IPoco;

        Task<int> Save<TPoco>(TPoco poco)
            where TPoco : IPoco;

        Task Update<TPoco>(TPoco poco)
            where TPoco : IPoco;

        void BulkInsert<TPoco>(IEnumerable<TPoco> list)
            where TPoco : IPoco;
    }
}
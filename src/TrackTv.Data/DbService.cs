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

        public Task DeleteAsync<TPoco>(TPoco poco)
            where TPoco : IPoco
        {
            return this.DataConnection.DeleteAsync(poco);
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

                if (!transaction.RolledBack)
                {
                    transaction.ActualCommit();
                }
            }
        }

        public Task<int> InsertAsync<TPoco>(TPoco poco)
            where TPoco : IPoco
        {
            return this.DataConnection.InsertWithInt32IdentityAsync(poco);
        }

        public async Task<int> SaveAsync<TPoco>(TPoco poco)
            where TPoco : IPoco
        {
            if (poco.IsNew())
            {
                return await this.InsertAsync(poco).ConfigureAwait(false);
            }

            await this.UpdateAsync(poco).ConfigureAwait(false);

            return poco.GetPrimaryKey();
        }

        public Task UpdateAsync<TPoco>(TPoco poco)
            where TPoco : IPoco
        {
            return this.DataConnection.UpdateAsync(poco);
        }

        private void OnInitCommand(object sender, InitSqlCommandEventArgs args)
        {
            this.sqlLog.Push(new KeyValuePair<string, DataParameter[]>(args.CommandText, args.Parameters));
        }
    }
}
namespace TrackTv.Data
{
    using System;
    using System.Threading.Tasks;

    using Npgsql;

    public partial class DbService
    {
        public Task ExecuteInTransaction(Func<Task> body, TimeSpan? timeout = null)
        {
            return this.ExecuteInTransaction(tr => body(), timeout);
        }

        public async Task ExecuteInTransaction(Func<ITransactionHandle, Task> body, TimeSpan? timeout = null)
        {
            await this.VerifyConnectionState().ConfigureAwait(false);

            using (var tx = this.dbConnection.BeginTransaction())
            {
                var handle = new TransactionHandle(tx);

                await RunBody(body, timeout, handle).ConfigureAwait(false);

                if (!handle.IsRolledBack)
                {
                    await tx.CommitAsync().ConfigureAwait(false);
                }
            }
        }

        private static async Task RunBody(Func<ITransactionHandle, Task> body, TimeSpan? timeout, ITransactionHandle transactionHandle)
        {
            if (timeout == null)
            {
                await body(transactionHandle).ConfigureAwait(false);
            }
            else
            {
                var timeoutTask = Task.Delay(timeout.Value);
                var transactionTask = body(transactionHandle);

                var completedTask = await Task.WhenAny(transactionTask, timeoutTask).ConfigureAwait(false);

                if (completedTask == timeoutTask)
                {
                    throw new TimeoutException("The db transaction timed out.");
                }

                await transactionTask.ConfigureAwait(false);
            }
        }
    }

    /// <summary>
    /// An object that gives the user the ability to rollback a transaction inside a transaction-wrapped function.
    /// If `Rollback` is not called and the function body does not throw an exception,
    /// the transaction will automatically be committed after the function returns.
    /// </summary>
    public class TransactionHandle : ITransactionHandle
    {
        private readonly NpgsqlTransaction transaction;

        public TransactionHandle(NpgsqlTransaction transaction)
        {
            this.transaction = transaction;
        }

        public bool IsRolledBack { get; private set; }

        /// <summary>
        /// Rollbacks the transaction.
        /// </summary>
        public async Task Rollback()
        {
            if (this.IsRolledBack)
            {
                return;
            }

            await this.transaction.RollbackAsync().ConfigureAwait(false);

            this.IsRolledBack = true;
        }
    }
}
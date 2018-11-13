namespace TrackTv.Data
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Npgsql;

    public partial class DbService<TPocos>
    {
        /// <summary>
        /// Calls `BeginTransaction` on the connection and returns the result.
        /// </summary>
        public async Task<NpgsqlTransaction> BeginTransaction()
        {
            await this.VerifyConnectionState();

            return this.dbConnection.BeginTransaction();
        }

        /// <summary>
        /// Starts a transaction and runs the `body` function.
        /// </summary>
        public async Task ExecuteInTransaction(Func<NpgsqlTransaction, Task> body, CancellationToken cancellationToken = default)
        {
            await this.VerifyConnectionState(cancellationToken);

            using (var transaction = await this.BeginTransaction())
            {
                if (cancellationToken == default)
                {
                    await body(transaction);
                }
                else
                {
                    var canceledTask = cancellationToken.AsTask();
                    var transactionTask = body(transaction);

                    var completedTask = await Task.WhenAny(transactionTask, canceledTask);

                    if (completedTask == canceledTask)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                    }

                    await transactionTask;
                }
            }
        }

        /// <summary>
        /// Starts a transaction, runs the `body` function
        /// and if it does not throw - commits the transaction.
        /// </summary>
        public Task ExecuteInTransaction(Func<Task> body, CancellationToken cancellationToken = default)
        {
            return this.ExecuteInTransaction(tr => body(), cancellationToken);
        }

        /// <summary>
        /// Starts a transaction, runs the `body` function
        /// and if it does not throw - commits the transaction.
        /// </summary>
        public Task ExecuteInTransactionAndCommit(Func<Task> body, CancellationToken cancellationToken = default)
        {
            return this.ExecuteInTransactionAndCommit(tr => body(), cancellationToken);
        }

        /// <summary>
        /// Starts a transaction, runs the `body` function
        /// and if it does not throw and the transaction is not completed - commits the transaction.
        /// </summary>
        public async Task ExecuteInTransactionAndCommit(Func<NpgsqlTransaction, Task> body, CancellationToken cancellationToken = default)
        {
            await this.VerifyConnectionState(cancellationToken);

            using (var transaction = await this.BeginTransaction())
            {
                if (cancellationToken == default)
                {
                    await body(transaction);
                }
                else
                {
                    var canceledTask = cancellationToken.AsTask();
                    var transactionTask = body(transaction);

                    var completedTask = await Task.WhenAny(transactionTask, canceledTask);

                    if (completedTask == canceledTask)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                    }

                    await transactionTask;
                }

                if (!transaction.IsCompleted)
                {
                    await transaction.CommitAsync(cancellationToken);
                }
            }
        }
    }
}
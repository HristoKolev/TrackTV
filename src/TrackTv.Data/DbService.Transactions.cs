﻿namespace TrackTv.Data
{
    using System;
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
        public async Task ExecuteInTransaction(Func<NpgsqlTransaction, Task> body, TimeSpan? timeout = null)
        {
            await this.VerifyConnectionState();

            using (var transaction = await this.BeginTransaction())
            {
                if (timeout == null)
                {
                    await body(transaction);
                }
                else
                {
                    var timeoutTask = Task.Delay(timeout.Value);
                    var transactionTask = body(transaction);

                    var completedTask = await Task.WhenAny(transactionTask, timeoutTask);

                    if (completedTask == timeoutTask)
                    {
                        throw new TimeoutException("The db transaction timed out.");
                    }

                    await transactionTask;
                }
            }
        }

        /// <summary>
        /// Starts a transaction, runs the `body` function
        /// and if it does not throw - commits the transaction.
        /// </summary>
        public Task ExecuteInTransaction(Func<Task> body, TimeSpan? timeout = null)
        {
            return this.ExecuteInTransaction(tr => body(), timeout);
        }

        /// <summary>
        /// Starts a transaction, runs the `body` function
        /// and if it does not throw - commits the transaction.
        /// </summary>
        public Task ExecuteInTransactionAndCommit(Func<Task> body, TimeSpan? timeout = null)
        {
            return this.ExecuteInTransactionAndCommit(tr => body(), timeout);
        }

        /// <summary>
        /// Starts a transaction, runs the `body` function
        /// and if it does not throw and the transaction is not completed - commits the transaction.
        /// </summary>
        public async Task ExecuteInTransactionAndCommit(Func<NpgsqlTransaction, Task> body, TimeSpan? timeout = null)
        {
            await this.VerifyConnectionState();

            using (var transaction = await this.BeginTransaction())
            {
                if (timeout == null)
                {
                    await body(transaction);
                }
                else
                {
                    var timeoutTask = Task.Delay(timeout.Value);
                    var transactionTask = body(transaction);

                    var completedTask = await Task.WhenAny(transactionTask, timeoutTask);

                    if (completedTask == timeoutTask)
                    {
                        throw new TimeoutException("The db transaction timed out.");
                    }

                    await transactionTask;
                }

                if (!transaction.IsCompleted)
                {
                    await transaction.CommitAsync();
                }
            }
        }
    }
}
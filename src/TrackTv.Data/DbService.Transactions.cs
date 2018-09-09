namespace TrackTv.Data
{
    using System;
    using System.Data;
    using System.Threading.Tasks;

    public partial class DbService
    {
        public Task ExecuteInTransaction(Func<Task> body) => this.ExecuteInTransaction(tr => body());

        public async Task ExecuteInTransaction(Func<IDbTransaction, Task> body, TimeSpan? timeout = null)
        {
            await this.VerifyConnectionState();

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
    }
}
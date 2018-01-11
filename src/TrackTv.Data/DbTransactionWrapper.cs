namespace TrackTv.Data
{
    using System;
    using System.Data;

    public class DbTransactionWrapper : IDbTransaction
    {
        public DbTransactionWrapper(IDbTransaction transaction)
        {
            this.Transaction = transaction;
        }

        public IDbConnection Connection => this.Transaction.Connection;

        public IsolationLevel IsolationLevel => this.Transaction.IsolationLevel;

        public bool RolledBack { get; set; }

        private IDbTransaction Transaction { get; }

        public void Commit()
        {
            throw new NotSupportedException(
                "The transaction cannot be commited from within the function. It will commit at the end if you dont explicitlly rollback or throw and exception.");
        }

        public void Dispose() => this.Transaction.Dispose();

        public void ActualCommit()
        {
            this.Transaction.Commit();
        }

        public void Rollback()
        {
            this.Transaction.Rollback();
            this.RolledBack = true;
        }
    }
}
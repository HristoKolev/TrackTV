namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;

    public interface ITransactionScope : IDisposable
    {
        void Complete();
    }

    public interface ITransactionScopeFactory : IDisposable
    {
        bool OpenTransaction { get; }

        ITransactionScope CreateScope();

        Task<ITransactionScope> CreateScopeAsync();
    }

    public class TransactionScope : ITransactionScope
    {
        public TransactionScope(IDbContextTransaction transaction)
        {
            this.Transaction = transaction;
        }

        public bool IsDisposed { get; private set; }

        private IDbContextTransaction Transaction { get; }

        public void Complete()
        {
            this.Transaction.Commit();
        }

        public void Dispose()
        {
            this.Transaction?.Dispose();
            this.IsDisposed = true;
        }
    }

    public class TransactionScopeFactory : ITransactionScopeFactory
    {
        public TransactionScopeFactory(DbContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public bool OpenTransaction => this.Scope != null && !this.Scope.IsDisposed;

        private DbContext DbContext { get; }

        private TransactionScope Scope { get; set; }

        public ITransactionScope CreateScope()
        {
            if (this.OpenTransaction)
            {
                throw new AlreadyInTransactionException("A transaction is already open.");
            }

            this.Scope = new TransactionScope(this.DbContext.Database.BeginTransaction());

            return this.Scope;
        }

        public async Task<ITransactionScope> CreateScopeAsync()
        {
            if (this.OpenTransaction)
            {
                throw new AlreadyInTransactionException("A transaction is already open.");
            }

            this.Scope = new TransactionScope(await this.DbContext.Database.BeginTransactionAsync());

            return this.Scope;
        }

        public void Dispose()
        {
            this.Scope?.Dispose();
        }
    }

    public class AlreadyInTransactionException : Exception
    {
        public AlreadyInTransactionException()
        {
        }

        public AlreadyInTransactionException(string message)
            : base(message)
        {
        }

        public AlreadyInTransactionException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
﻿namespace TrackTv.Data.Tests.Infrastructure
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;

    using Npgsql;

    using Xunit;

    public abstract class DatabaseTest : IAsyncLifetime
    {
        /// <summary>
        /// Called immediately after the class has been created, before it is used.
        /// </summary>
        [SuppressMessage("ReSharper", "AsyncConverter.AsyncAwaitMayBeElidedHighlighting")]
        public async Task InitializeAsync()
        {
            this.Db = TestDbService.Create();
            this.DbHelper = new TestDbHelper(this.Db);
            this.Tx = await this.Db.BeginTransaction();

            await this.DbHelper.ExecuteFile("./before-tests.sql");
        }

        private NpgsqlTransaction Tx { get; set; }

        private TestDbHelper DbHelper { get; set; }

        protected TestDbService Db { get; private set; }

        /// <summary>
        /// Called when an object is no longer needed. Called just before <see cref="M:System.IDisposable.Dispose" />
        /// if the class also implements that.
        /// </summary>
        [SuppressMessage("ReSharper", "AsyncConverter.AsyncAwaitMayBeElidedHighlighting")]
        public async Task DisposeAsync()
        {
            await this.Tx.RollbackAsync();
            
            this.Db.Connection.Dispose();
            this.Db.Dispose();
        }

        protected bool StupidEquals(object a, object b)
        {
            return a != null && (a.GetType().IsValueType || a is string) ? Equals(a, b) : ReferenceEquals(a, b);
        }
    }
}
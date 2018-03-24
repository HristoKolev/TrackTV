﻿namespace TrackTv.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    using LinqToDB;
    using LinqToDB.Data;
    using LinqToDB.DataProvider;
    using LinqToDB.Mapping;
    using LinqToDB.SqlProvider;

    public class LoggingDataProviderWrapper : IDataProvider, IProfiledDataProvider
    {
        public LoggingDataProviderWrapper(IDataProvider dataProvider)
        {
            this.DataProvider = dataProvider;
        }

        public event EventHandler<InitSqlCommandEventArgs> OnInitCommand;

        public string ConnectionNamespace => this.DataProvider.ConnectionNamespace;

        public Type DataReaderType => this.DataProvider.DataReaderType;

        public MappingSchema MappingSchema => this.DataProvider.MappingSchema;

        public string Name => this.DataProvider.Name;

        public SqlProviderFlags SqlProviderFlags => this.DataProvider.SqlProviderFlags;

        private IDataProvider DataProvider { get; }

        public BulkCopyRowsCopied BulkCopy<T>(DataConnection dataConnection, BulkCopyOptions options, IEnumerable<T> source)
        {
            return this.DataProvider.BulkCopy(dataConnection, options, source);
        }

        public Type ConvertParameterType(Type type, DataType dataType)
        {
            return this.DataProvider.ConvertParameterType(type, dataType);
        }

        public IDbConnection CreateConnection(string connectionString)
        {
            return this.DataProvider.CreateConnection(connectionString);
        }

        public ISqlBuilder CreateSqlBuilder()
        {
            return this.DataProvider.CreateSqlBuilder();
        }

        public void DisposeCommand(DataConnection dataConnection)
        {
            this.DataProvider.DisposeCommand(dataConnection);
        }

        public IDisposable ExecuteScope()
        {
            return this.DataProvider.ExecuteScope();
        }

        public CommandBehavior GetCommandBehavior(CommandBehavior commandBehavior)
        {
            return this.DataProvider.GetCommandBehavior(commandBehavior);
        }

        public object GetConnectionInfo(DataConnection dataConnection, string parameterName)
        {
            return this.DataProvider.GetConnectionInfo(dataConnection, parameterName);
        }

        public Expression GetReaderExpression(
            MappingSchema mappingSchema,
            IDataReader reader,
            int idx,
            Expression readerExpression,
            Type toType)
        {
            return this.DataProvider.GetReaderExpression(mappingSchema, reader, idx, readerExpression, toType);
        }

        public ISqlOptimizer GetSqlOptimizer()
        {
            return this.DataProvider.GetSqlOptimizer();
        }

        public void InitCommand(DataConnection dataConnection, CommandType commandType, string commandText, DataParameter[] parameters)
        {
            this.OnInitCommand?.Invoke(this, new InitSqlCommandEventArgs(commandText, parameters));

            this.DataProvider.InitCommand(dataConnection, commandType, commandText, parameters);
        }

        public bool IsCompatibleConnection(IDbConnection connection)
        {
            return this.DataProvider.IsCompatibleConnection(connection);
        }

        public bool? IsDBNullAllowed(IDataReader reader, int idx)
        {
            return this.DataProvider.IsDBNullAllowed(reader, idx);
        }

        public int Merge<T>(
            DataConnection dataConnection,
            Expression<Func<T, bool>> predicate,
            bool delete,
            IEnumerable<T> source,
            string tableName,
            string databaseName,
            string schemaName)
            where T : class
        {
            return this.DataProvider.Merge(dataConnection, predicate, delete, source, tableName, databaseName, schemaName);
        }

        public int Merge<TTarget, TSource>(DataConnection dataConnection, IMergeable<TTarget, TSource> merge)
            where TTarget : class where TSource : class
        {
            return this.DataProvider.Merge(dataConnection, merge);
        }

        public Task<int> MergeAsync<T>(
            DataConnection dataConnection,
            Expression<Func<T, bool>> predicate,
            bool delete,
            IEnumerable<T> source,
            string tableName,
            string databaseName,
            string schemaName,
            CancellationToken token)
            where T : class
        {
            return this.DataProvider.MergeAsync(dataConnection, predicate, delete, source, tableName, databaseName, schemaName, token);
        }

        public Task<int> MergeAsync<TTarget, TSource>(
            DataConnection dataConnection,
            IMergeable<TTarget, TSource> merge,
            CancellationToken token)
            where TTarget : class where TSource : class
        {
            return this.DataProvider.MergeAsync(dataConnection, merge, token);
        }

        public void SetParameter(IDbDataParameter parameter, string name, DataType dataType, object value)
        {
            this.DataProvider.SetParameter(parameter, name, dataType, value);
        }
    }

    public interface IProfiledDataProvider
    {
        event EventHandler<InitSqlCommandEventArgs> OnInitCommand;
    }

    public class InitSqlCommandEventArgs : EventArgs
    {
        public InitSqlCommandEventArgs(string commandText, DataParameter[] parameters)
        {
            this.CommandText = commandText;
            this.Parameters = parameters;
        }

        public string CommandText { get; }

        public DataParameter[] Parameters { get; }
    }
}
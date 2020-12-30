using System;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Bindings;

namespace DDDNETBB.Persistence.MongoDB.Tests.Stubs
{
    internal sealed class FakeMongoDbClientSession : IClientSessionHandle
    {
        public void Dispose()
        { }

        public void AbortTransaction(CancellationToken cancellationToken = new CancellationToken())
        { }

        public Task AbortTransactionAsync(CancellationToken cancellationToken = new CancellationToken())
            => Task.CompletedTask;

        public void AdvanceClusterTime(BsonDocument newClusterTime)
        { }

        public void AdvanceOperationTime(BsonTimestamp newOperationTime)
        { }

        public void CommitTransaction(CancellationToken cancellationToken = new CancellationToken())
        { }

        public Task CommitTransactionAsync(CancellationToken cancellationToken = new CancellationToken())
         => Task.CompletedTask;

        public void StartTransaction(TransactionOptions transactionOptions = null)
        { }

        public TResult WithTransaction<TResult>(Func<IClientSessionHandle, CancellationToken, TResult> callback,
            TransactionOptions transactionOptions = null,
            CancellationToken cancellationToken = new CancellationToken())
            => throw new NotImplementedException();

        public Task<TResult> WithTransactionAsync<TResult>(
            Func<IClientSessionHandle, CancellationToken, Task<TResult>> callbackAsync,
            TransactionOptions transactionOptions = null,
            CancellationToken cancellationToken = new CancellationToken())
            => throw new NotImplementedException();

        public IMongoClient Client { get; }
        public BsonDocument ClusterTime { get; }
        public bool IsImplicit { get; }
        public bool IsInTransaction { get; }
        public BsonTimestamp OperationTime { get; }
        public ClientSessionOptions Options { get; }
        public IServerSession ServerSession { get; }
        public ICoreSessionHandle WrappedCoreSession { get; }
        public IClientSessionHandle Fork() => new FakeMongoDbClientSession();
    }
}
using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using DDDNETBB.Core;
using DDDNETBB.Domain;
using DDDNETBB.Persistence.MongoDB.Exceptions;
using MongoDB.Driver;

[assembly: InternalsVisibleTo("ddd-net-bb.Persistence.MongoDB.Tests")]
namespace DDDNETBB.Persistence.MongoDB.Repositories
{
    internal sealed class MongoDbWriteRepository<TAggregateRoot, TKey> : IMongoDbWriteRepository<TAggregateRoot, TKey>
        where TAggregateRoot : EventedAggregateRoot<TKey>
        where TKey : Identity

    {
        private readonly IMongoCollection<TAggregateRoot> _collection;


        public MongoDbWriteRepository(IMongoDatabase database, string collectionName)
        {
            if (database is null)
                throw new PersistenceMongoDBConfigurationException($"Read repository cannot be created because of null {nameof(database)} arg");
            if (collectionName.IsNullEmptyOrWhitespace())
                throw new PersistenceMongoDBConfigurationException($"Read repository cannot be created for null or empty collection {nameof(collectionName)}");

            _collection = database.GetCollection<TAggregateRoot>(collectionName);
        }
        

        public Task AddAsync(TAggregateRoot aggregate, CancellationToken cancellationToken = default)
        {
            AssertAggregateRoot(aggregate);
            return _collection.InsertOneAsync(aggregate, cancellationToken: cancellationToken);
        }

        public Task UpdateAsync(TAggregateRoot aggregate, CancellationToken cancellationToken = default)
            => UpdateAsync(aggregate, a => a.Id.Equals(aggregate.Id), cancellationToken);

        public Task UpdateAsync(TAggregateRoot aggregate, Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken = default)
        {
            AssertAggregateRoot(aggregate);
            AssertPredicate(predicate);

            return _collection.ReplaceOneAsync(predicate, aggregate, cancellationToken: cancellationToken);
        }

        public Task DeleteAsync(TKey id, CancellationToken cancellationToken = default)
            => DeleteAsync(a => a.Id.Equals(id), cancellationToken);

        public Task DeleteAsync(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken = default)
        {
            AssertPredicate(predicate);
            return _collection.DeleteOneAsync(predicate, cancellationToken);
        }

        public Task<bool> ExistsAsync(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken = default)
        {
            AssertPredicate(predicate);
            return _collection.Find(predicate).AnyAsync(cancellationToken);
        }


        private void AssertAggregateRoot(TAggregateRoot aggregate)
        {
            if (aggregate is null)
                throw new PersistenceMongoDBConfigurationException($"The {nameof(aggregate)} cannot be null");
        }

        private static void AssertPredicate(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            if (predicate is null)
                throw new PersistenceMongoDBConfigurationException($"The {nameof(predicate)} cannot be null");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using DDDNETBB.Core;
using DDDNETBB.Core.Abstractions.Pagination;
using DDDNETBB.Core.Pagination;
using DDDNETBB.Domain;
using DDDNETBB.Persistence.MongoDB.Exceptions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

[assembly: InternalsVisibleTo("ddd-net-bb.Persistence.MongoDB.Tests")]
namespace DDDNETBB.Persistence.MongoDB.Repositories
{
    internal sealed class MongoDbQueryRepository<TAggregateRoot, TKey> : IMongoDbQueryRepository<TAggregateRoot, TKey>
        where TAggregateRoot : EventedAggregateRoot<TKey>
        where TKey : Identity
    {
        public IMongoCollection<TAggregateRoot> Collection { get; }


        public MongoDbQueryRepository(IMongoDatabase database, string collectionName)
        {
            if(database is null)
                throw new PersistenceMongoDBConfigurationException($"Read repository cannot be created because of null {nameof(database)} arg");
            if(collectionName.IsNullEmptyOrWhitespace())
                throw new PersistenceMongoDBConfigurationException($"Read repository cannot be created for null or empty collection {nameof(collectionName)}");

            Collection = database.GetCollection<TAggregateRoot>(collectionName);
        }


        public Task<TAggregateRoot> Get(TKey id, CancellationToken cancellationToken = default)
        {
            if (id is null)
                throw new PersistenceMongoDBConfigurationException($"The {nameof(id)} cannot be null");

            return Get(a => a.Id.Equals(id), cancellationToken);
        }

        public Task<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken = default)
        {
            AssertSearchPredicate(predicate);
            return Collection.Find(predicate).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<TAggregateRoot>> Find(Expression<Func<TAggregateRoot, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            AssertSearchPredicate(predicate);
            return await Collection.Find(predicate).ToListAsync(cancellationToken);
        }

        public Task<PagedResult<TAggregateRoot>> BrowseAsync<TQuery>(Expression<Func<TAggregateRoot, bool>> predicate, TQuery query, CancellationToken cancellationToken = default) where TQuery : IPagedQuery
        {
            AssertSearchPredicate(predicate);
            return Collection.AsQueryable().Where(predicate).Paginate(query, cancellationToken);
        }


        private static void AssertSearchPredicate(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            if (predicate is null)
                throw new PersistenceMongoDBConfigurationException($"The {nameof(predicate)} cannot be null");
        }
    }
}
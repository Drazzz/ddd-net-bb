using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DDDNETBB.Core.Abstractions.Pagination;
using DDDNETBB.Core.Pagination;
using DDDNETBB.Domain;
using MongoDB.Driver;

namespace DDDNETBB.Persistence.MongoDB
{
    public interface IMongoDbQueryRepository<TAggregateRoot, in TKey> : IEventedAggregateMongoDbReposiotry<TAggregateRoot, TKey>
        where TAggregateRoot : EventedAggregateRoot<TKey>
        where TKey : Identity
    {
        IMongoCollection<TAggregateRoot> Collection { get; }

        Task<TAggregateRoot> Get(TKey id, CancellationToken cancellationToken = default);
        Task<TAggregateRoot> Get(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken = default);
        Task<IReadOnlyCollection<TAggregateRoot>> Find(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken = default);
        Task<PagedResult<TAggregateRoot>> BrowseAsync<TQuery>(Expression<Func<TAggregateRoot, bool>> predicate, TQuery query,
            CancellationToken cancellationToken = default)
            where TQuery : IPagedQuery;
    }
}
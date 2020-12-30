using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DDDNETBB.Domain;

namespace DDDNETBB.Persistence.MongoDB
{
    public interface IMongoDbWriteRepository<TAggregateRoot, in TKey> : IEventedAggregateMongoDbReposiotry<TAggregateRoot, TKey>
        where TAggregateRoot : EventedAggregateRoot<TKey>
        where TKey : Identity
    {
        Task AddAsync(TAggregateRoot aggregate, CancellationToken cancellationToken = default);
        Task UpdateAsync(TAggregateRoot aggregate, CancellationToken cancellationToken = default);
        Task UpdateAsync(TAggregateRoot aggregate, Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken = default);
        Task DeleteAsync(TKey id, CancellationToken cancellationToken = default);
        Task DeleteAsync(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
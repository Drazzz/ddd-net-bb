using DDDNETBB.Domain;

namespace DDDNETBB.Persistence.MongoDB
{
    public interface IEventedAggregateMongoDbReposiotry<TAggregateRoot, in TKey>
        where TAggregateRoot : EventedAggregateRoot<TKey>
        where TKey : Identity
    { }
}
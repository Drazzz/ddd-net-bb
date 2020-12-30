using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using DDDNETBB.Persistence.MongoDB.Exceptions;
using MongoDB.Driver;

[assembly: InternalsVisibleTo("ddd-net-bb.Persistence.MongoDB.Tests")]
namespace DDDNETBB.Persistence.MongoDB.Factories
{
    internal sealed class MongoDbSessionFactory : IMongoDbSessionFactory
    {
        private readonly IMongoClient _mongoClient;


        public MongoDbSessionFactory(IMongoClient mongoClient)
            => _mongoClient = mongoClient ??
                             throw new PersistenceMongoDBConfigurationException($"Cannot init {nameof(MongoDbSessionFactory)} with null {nameof(mongoClient)}");


        public Task<IClientSessionHandle> Create() => Create(null);

        public Task<IClientSessionHandle> Create(ClientSessionOptions options) => _mongoClient.StartSessionAsync(options);
    }
}
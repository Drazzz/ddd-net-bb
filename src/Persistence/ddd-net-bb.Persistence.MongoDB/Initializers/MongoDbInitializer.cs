using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

[assembly: InternalsVisibleTo("ddd-net-bb.Persistence.MongoDB.Tests")]
namespace DDDNETBB.Persistence.MongoDB.Initializers
{
    internal sealed class MongoDbInitializer : IMongoDbInitializer
    {
        private static int _initialized;
        private readonly bool _requiredSeed;

        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoDbSeeder _seeder;


        public MongoDbInitializer(IMongoDatabase mongoDatabase, IMongoDbSeeder seeder, Options options)
        {
            _mongoDatabase = mongoDatabase ?? throw new ArgumentNullException(nameof(mongoDatabase));
            _seeder = seeder ?? throw new ArgumentNullException(nameof(seeder));
            _requiredSeed = options.Seed;
        }


        public Task Initialize()
        {
            if (Interlocked.Exchange(ref _initialized, 1) == 1)
                return Task.CompletedTask;

            return _requiredSeed ? _seeder.Seed(_mongoDatabase) : Task.CompletedTask;
        }
    }
}
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ddd-net-bb.Persistence.MongoDB.Tests")]
namespace DDDNETBB.Persistence.MongoDB.Builders
{
    internal sealed class MongoDbOptionsBuilder : IMongoDbOptionsBuilder
    {
        private readonly Options _options;


        private MongoDbOptionsBuilder(Options options) => _options = options ?? new Options();
        public static MongoDbOptionsBuilder From(Options options) => new(options);
        public static MongoDbOptionsBuilder New() => new(null);


        public IMongoDbOptionsBuilder WithConnectionString(string connectionString)
        {
            _options.ConnectionString = connectionString;
            return this;
        }

        public IMongoDbOptionsBuilder WithDatabaseName(string databaseName)
        {
            _options.DatabaseName = databaseName;
            return this;
        }

        public IMongoDbOptionsBuilder WithCollectionName(string collectionName)
        {
            _options.CollectionName = collectionName;
            return this;
        }

        public IMongoDbOptionsBuilder WithSeed(bool useSeed)
        {
            _options.Seed = useSeed;
            return this;
        }

        public IMongoDbOptionsBuilder WithRandomDatabaseSuffix(bool useRandomSuffix)
        {
            _options.SetRandomDatabaseSuffix = useRandomSuffix;
            return this;
        }

        public Options Build() => _options;
    }
}
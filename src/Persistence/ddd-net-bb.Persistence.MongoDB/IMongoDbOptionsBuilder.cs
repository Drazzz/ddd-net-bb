namespace DDDNETBB.Persistence.MongoDB
{
    public interface IMongoDbOptionsBuilder
    {
        IMongoDbOptionsBuilder WithConnectionString(string connectionString);
        IMongoDbOptionsBuilder WithDatabaseName(string databaseName);
        IMongoDbOptionsBuilder WithCollectionName(string collectionName);
        IMongoDbOptionsBuilder WithSeed(bool useSeed);
        IMongoDbOptionsBuilder WithRandomDatabaseSuffix(bool useRandomSuffix);

        Options Build();
    }
}
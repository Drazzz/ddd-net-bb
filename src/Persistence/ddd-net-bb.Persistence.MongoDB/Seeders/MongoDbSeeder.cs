using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DDDNETBB.Persistence.MongoDB.Seeders
{
    internal sealed class MongoDbSeeder : IMongoDbSeeder
    {
        public async Task Seed(IMongoDatabase database)
        {
            if(database is null)
                throw new ArgumentNullException(nameof(database));

            await CustomSeed(database);
        }

        private async Task CustomSeed(IMongoDatabase database)
        {
            var cursor = await database.ListCollectionsAsync();
            var collections = await cursor.ToListAsync();
            if (collections.Any())
                return;

            await Task.CompletedTask;
        }
    }
}
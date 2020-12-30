using System.Threading.Tasks;
using MongoDB.Driver;

namespace DDDNETBB.Persistence.MongoDB
{
    public interface IMongoDbSeeder
    {
        Task Seed(IMongoDatabase database);
    }
}
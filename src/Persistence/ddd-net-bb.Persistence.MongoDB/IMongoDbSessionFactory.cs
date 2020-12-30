using System.Threading.Tasks;
using MongoDB.Driver;

namespace DDDNETBB.Persistence.MongoDB
{
    public interface IMongoDbSessionFactory
    {
        Task<IClientSessionHandle> Create();
        Task<IClientSessionHandle> Create(ClientSessionOptions options);
    }
}
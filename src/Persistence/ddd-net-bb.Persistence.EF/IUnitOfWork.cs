using System.Threading;
using System.Threading.Tasks;

namespace DDDNETBB.Persistence.EF
{
    public interface IUnitOfWork
    {
        //
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);

        //for DBContext
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
    }
}
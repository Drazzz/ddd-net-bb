using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DDDNETBB.Persistence.EF
{
    public abstract class EntityFrameworkDbContext : DbContext, IUnitOfWork
    {
        protected EntityFrameworkDbContext(DbContextOptions options)
            : base(options) { }


        public abstract Task<bool> CommitAsync(CancellationToken cancellationToken = default);
    }
}
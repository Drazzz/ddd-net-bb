using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DDDNETBB.Persistence.EF.Tests.Stubs
{
    internal sealed class FakeDBContext : EntityFrameworkDbContext
    {
        private FakeDBContext(DbContextOptions options)
            : base(options) { }

        public static FakeDBContext Empty()
            => new FakeDBContext(new DbContextOptions<FakeDBContext>());


        public override Task<bool> CommitAsync(CancellationToken cancellationToken = default) => Task.FromResult(true);
    }
}
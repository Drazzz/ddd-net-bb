namespace DDDNETBB.Persistence.EF.Tests.Stubs
{
    internal sealed class FakeDBProvider : IDatabaseProvider
    {
        public EntityFrameworkDbContext CreateDBContext() => FakeDBContext.Empty();
    }
}
namespace DDDNETBB.Persistence.EF
{
    public interface IDatabaseProvider
    {
        EntityFrameworkDbContext CreateDBContext();
    }
}
using MyDoo.EFDal.DbContexts;

namespace MyDoo.EFDal;

public class BaseDAO : IAsyncDisposable
{
    protected readonly NpgsqlContext DbContext;

    protected BaseDAO(NpgsqlContext dbContext)
    {
        DbContext = dbContext;
    }
    
    public async ValueTask DisposeAsync()
    {
        await DbContext.DisposeAsync();
    }
}
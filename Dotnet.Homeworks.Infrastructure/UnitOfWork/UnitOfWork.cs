using Dotnet.Homeworks.Data.DatabaseContext;

namespace Dotnet.Homeworks.Infrastructure.UnitOfWork;

public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken token)
    {
        await appDbContext.SaveChangesAsync(token);
    }
}
using Dotnet.Homeworks.Data.DatabaseContext;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.Homeworks.DataAccess.Repositories;

public class ProductRepository(AppDbContext appDbContext) : IProductRepository
{
    public Task<IEnumerable<Product>> GetAllProductsAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(appDbContext.Products.AsEnumerable());
    }

    public async Task DeleteProductByGuidAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await appDbContext.Products
            .Where(x => x.Id.Equals(id))
            .SingleOrDefaultAsync(cancellationToken);

        if (product is not null)
        {
            appDbContext.Products.Remove(product);
        }
    }

    public Task UpdateProductAsync(Product product, CancellationToken cancellationToken)
    {
        return Task.FromResult(appDbContext.Products.Update(product));
    }

    public Task<Guid> InsertProductAsync(Product product, CancellationToken cancellationToken)
    {
        return Task.FromResult(appDbContext.Products.Add(product).Entity.Id);
    }
}
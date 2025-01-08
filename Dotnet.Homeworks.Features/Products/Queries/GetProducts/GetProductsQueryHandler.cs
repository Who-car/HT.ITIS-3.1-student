using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Infrastructure.Cqrs.Queries;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Features.Products.Queries.GetProducts;

internal sealed class GetProductsQueryHandler (
    IProductRepository productRepository,
    IUnitOfWork unitOfWork
) : IQueryHandler<GetProductsQuery, GetProductsDto>
{
    public async Task<Result<GetProductsDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllProductsAsync(cancellationToken);
        var productsDto = products.Select(x => new GetProductDto(x.Id, x.Name));
        
        return new Result<GetProductsDto>(new GetProductsDto(productsDto), true);
    }
}
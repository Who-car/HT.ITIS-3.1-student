using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Features.Products.Commands.InsertProduct;

internal sealed class InsertProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<InsertProductCommand, InsertProductDto>
{
    public async Task<Result<InsertProductDto>> Handle(InsertProductCommand request, CancellationToken cancellationToken)
    {
        var id = await productRepository.InsertProductAsync(new Product { Name = request.Name }, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new Result<InsertProductDto>(new InsertProductDto(id), true);
    }
}
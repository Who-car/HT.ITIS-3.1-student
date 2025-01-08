using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Features.Products.Commands.UpdateProduct;

internal sealed class UpdateProductCommandHandler (
    IProductRepository productRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<UpdateProductCommand>
{
    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        await productRepository.UpdateProductAsync(new Product { Id = request.Guid, Name = request.Name }, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new Result(true);
    }
}
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;
using Dotnet.Homeworks.Infrastructure.UnitOfWork;
using Dotnet.Homeworks.Shared.Dto;

namespace Dotnet.Homeworks.Features.Products.Commands.DeleteProduct;

internal sealed class DeleteProductByGuidCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<DeleteProductByGuidCommand>
{
    public async Task<Result> Handle(DeleteProductByGuidCommand request, CancellationToken cancellationToken = default)
    {
        await productRepository.DeleteProductByGuidAsync(request.Guid, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return new Result(true);
    }
}
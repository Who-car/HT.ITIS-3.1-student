using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;

namespace Dotnet.Homeworks.Features.Products.Commands.DeleteProduct;

public class DeleteProductByGuidCommand(Guid guid) : ICommand
{
    public Guid Guid { get; init; } = guid;
}
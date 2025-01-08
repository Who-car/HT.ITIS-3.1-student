using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;

namespace Dotnet.Homeworks.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommand(Guid guid, string name) : ICommand
{
    public Guid Guid { get; init; } = guid;
    public string Name { get; init; } = name;
}
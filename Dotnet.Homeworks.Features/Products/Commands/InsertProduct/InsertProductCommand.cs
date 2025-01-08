using Dotnet.Homeworks.Infrastructure.Cqrs.Commands;

namespace Dotnet.Homeworks.Features.Products.Commands.InsertProduct;

public class InsertProductCommand(string name) : ICommand<InsertProductDto>
{
    public string Name { get; init; } = name;
}

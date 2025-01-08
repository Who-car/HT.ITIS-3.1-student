using Dotnet.Homeworks.Features.Products.Commands.DeleteProduct;
using Dotnet.Homeworks.Features.Products.Commands.InsertProduct;
using Dotnet.Homeworks.Features.Products.Commands.UpdateProduct;
using Dotnet.Homeworks.Features.Products.Queries.GetProducts;
using Microsoft.AspNetCore.Mvc;
using IMediator = MediatR.IMediator;

namespace Dotnet.Homeworks.MainProject.Controllers;

[ApiController]
public class ProductManagementController(IMediator mediator) : ControllerBase
{
    [HttpGet("products")]
    public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new GetProductsQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPost("product")]
    public async Task<IActionResult> InsertProduct(string name, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new InsertProductCommand(name), cancellationToken);
        return Ok(result);
    }

    [HttpDelete("product")]
    public async Task<IActionResult> DeleteProduct(Guid guid, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteProductByGuidCommand(guid), cancellationToken);
        return Ok();
    }

    [HttpPut("product")]
    public async Task<IActionResult> UpdateProduct(Guid guid, string name, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateProductCommand(guid, name), cancellationToken);
        return Ok();
    }
}
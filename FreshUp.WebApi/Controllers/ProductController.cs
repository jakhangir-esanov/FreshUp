using Microsoft.AspNetCore.Mvc;

namespace FreshUp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator mediator;

    public ProductController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateProductCommand command)
        => Ok(await this.mediator.Send(new CreateProductCommand(command.Name, command.Price,
            command.Unit, command.Description, command.CategoryId)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateProductCommand command)
        => Ok(await this.mediator.Send(new UpdateProductCommand(command.Id, command.Name,
            command.Price, command.Unit, command.Description, command.CategoryId)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteProductCommand(id)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetProductQuery() { Id = id }));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await this.mediator.Send(new GetAllProductsQuery()));
}

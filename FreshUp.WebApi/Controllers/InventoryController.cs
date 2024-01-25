using Microsoft.AspNetCore.Mvc;

namespace FreshUp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
    private readonly IMediator mediator;

    public InventoryController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsyn(CreateInventoryCommand command)
        => Ok(await this.mediator.Send(new CreateInventoryCommand(command.ProductId, command.Quantity)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateInventoryCommand command)
        => Ok(await this.mediator.Send(new UpdateInventoryCommand(command.Id, command.ProductId, command.Quantity)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteInventoryCommand(id)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetInventoryQuery() { Id = id }));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await this.mediator.Send(new GetAllInventoriesQuery()));

}

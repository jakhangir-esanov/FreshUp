using Microsoft.AspNetCore.Mvc;

namespace FreshUp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator mediator;

    public OrderController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateOrderCommand command)
        => Ok(await this.mediator.Send(new CreateOrderCommand(command.OrderDate, command.TotalAmount, command.Status)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateOrderCommand command)
        => Ok(await this.mediator.Send(new UpdateOrderCommand(command.Id, command.OrderDate, command.TotalAmount, command.Status)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteOrderCommand(id)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetOrderQuery() { Id = id }));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await this.mediator.Send(new GetAllOrdersQuery()));
}

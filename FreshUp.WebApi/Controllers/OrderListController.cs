using Microsoft.AspNetCore.Mvc;

namespace FreshUp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderListController : ControllerBase
{
    private readonly IMediator mediator;

    public OrderListController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateOrderListCommand command)
        => Ok(await this.mediator.Send(new CreateOrderListCommand(command.Quantity,
           command.ProductId, command.OrderId)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateOrderListCommand command)
        => Ok(await this.mediator.Send(new UpdateOrderListCommand(command.Id,
            command.Quantity, command.ProductId, command.OrderId)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteOrderListCommand(id)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetOrderListQuery() { Id = id }));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await this.mediator.Send(new GetAllOrderListsQuery()));
}

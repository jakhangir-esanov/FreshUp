using FreshUp.Application.Queries.GetInventoryHistory;
using Microsoft.AspNetCore.Mvc;

namespace FreshUp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryHistoryController : ControllerBase
{
    private readonly IMediator mediator;

    public InventoryHistoryController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetInventoryHistoryQuery() { Id = id }));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await this.mediator.Send(new GetAllInventoryHistoriesQuery()));
}

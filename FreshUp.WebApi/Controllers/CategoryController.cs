using Microsoft.AspNetCore.Mvc;

namespace FreshUp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator mediator;

    public CategoryController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateCategoryCommand command)
        => Ok(await this.mediator.Send(new CreateCategoryCommand(command.Name, command.Description)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateCategoryCommand command)
        => Ok(await this.mediator.Send(new UpdateCategoryCommand(command.Id, command.Name, command.Description)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteCategoryCommand(id)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetCategoryQuery() { Id = id }));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await this.mediator.Send(new GetAllCategoriesQuery()));
}

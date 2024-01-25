using Microsoft.AspNetCore.Mvc;

namespace FreshUp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator mediator;

    public UserController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CreateUserCommand command)
        => Ok(await this.mediator.Send(new CreateUserCommand(command.UserName, command.Password,
            command.Email, command.PhoneNumber, command.Role)));

    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync(UpdateUserCommand command)
        => Ok(await this.mediator.Send(new UpdateUserCommand(command.Id, command.UserName,
            command.Password, command.Email, command.PhoneNumber, command.Role)));

    [HttpDelete("delete/{id:long}")]
    public async Task<IActionResult> DeleteAsync(long id)
        => Ok(await this.mediator.Send(new DeleteUserCommand(id)));

    [HttpGet("get-by-id/{id:long}")]
    public async Task<IActionResult> GetByIdAsync(long id)
        => Ok(await this.mediator.Send(new GetUserQuery() { Id = id }));

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await this.mediator.Send(new GetAllUsersQuery()));
}

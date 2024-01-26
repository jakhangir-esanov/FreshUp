using FreshUp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreshUp.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;

    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("user/login")]
    public async Task<IActionResult> GenerateTokenForUserAsync(string email, string password)
        => Ok(await this.authService.GenerateTokenAsync(email, password));
}

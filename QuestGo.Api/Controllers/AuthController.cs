using Microsoft.AspNetCore.Mvc;
using QuestGo.Api.Models;
using QuestGo.Services.Dtos;
using QuestGo.Services.Interfaces;

namespace QuestGo.Api.Controllers;

public class AuthController(IAuthService authService) : BaseController
{
    [HttpPost("login")]
    public async ValueTask<IActionResult> LoginAsync(LoginDto dto)
        => Ok(new Response
        {
            Code = 200,
            Message = "Login successful!",
            Data = await authService.AuthenticateAsync(dto)
        });

    [HttpGet("me")]
    public async ValueTask<IActionResult> GetCurrentUserAsync()
    {
        var user = await authService.GetCurrentUserAsync();
        return Ok(new Response
        {
            Code = 200,
            Message = "User retrieved successfully!",
            Data = user
        });
    }
}

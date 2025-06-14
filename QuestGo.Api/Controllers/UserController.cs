using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestGo.Api.Models;
using QuestGo.Domain.Configuratios;
using QuestGo.Services.Dtos;
using QuestGo.Services.Interfaces;

namespace QuestGo.Api.Controllers;

public class UserController(IUserService userService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await userService.GetAllAsync(@params),
        });

    [HttpPost]
    public async Task<IActionResult> CreateAsync(UserCreateDto dto)
        => Ok(new Response
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await userService.CreateAsync(dto),
        });

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetByIdAsync(long userId)
        => Ok(new Response
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await userService.GetByIdAsync(userId),
        });

    [HttpPut("{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync(long userId, [FromForm] UserUpdateDto dto)
        => Ok(new Response
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await userService.UpdateAsync(userId, dto),
        });

    [HttpDelete("{userId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(long userId)
        => Ok(new Response
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await userService.DeleteAsync(userId),
        });
}


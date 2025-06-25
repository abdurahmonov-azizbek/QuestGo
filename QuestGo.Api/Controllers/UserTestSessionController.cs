using Microsoft.AspNetCore.Mvc;
using QuestGo.Api.Models;
using QuestGo.Domain.Configuratios;
using QuestGo.Domain.Entities;
using QuestGo.Services.Dtos;
using QuestGo.Services.Interfaces;

namespace QuestGo.Api.Controllers;

public class UserTestSessionController(IUserTestSessionService userTestSessionService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] UserTestSessionCreateDto dto)
        => Ok(new Response()
        {
            Code = 200,
            Message = "OK",
            Data = await userTestSessionService.CreateAsync(dto)
        });

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response()
        {
            Code = 200,
            Message = "OK",
            Data = await userTestSessionService.GetAllAsync(@params)
        });

    [HttpGet("{userTestSessionId:long}")]
    public async Task<IActionResult> GetAsync([FromRoute] long userTestSessionId)
        => Ok(new Response()
        {
            Code = 200,
            Message = "OK",
            Data = await userTestSessionService.GetByIdAsync(userTestSessionId)
        });

    [HttpDelete("{userTestSessionId:long}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] long userTestSessionId)
        => Ok(new Response()
        {
            Code = 200,
            Message = "OK",
            Data = await userTestSessionService.DeleteAsync(userTestSessionId)
        });
}
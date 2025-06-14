using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestGo.Api.Models;
using QuestGo.Domain.Configuratios;
using QuestGo.Services.Dtos;
using QuestGo.Services.Interfaces;
using QuestGo.Services.Services;

namespace QuestGo.Api.Controllers;

public class TestController(ITestService testService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
      => Ok(new Response
      {
          Code = 200,
          Message = "Ok👍🏿",
          Data = await testService.GetAllAsync(@params),
      });

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAllByUserId(long userId, [FromQuery] PaginationParams @params)
        => Ok(new Response
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await testService.GetAllByUserIdAsync(userId, @params),
        });

    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> CreateAsync(TestCreateDto dto)
        => Ok(new Response
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await testService.CreateAsync(dto),
        });

    [HttpGet("{testId}")]
    public async Task<IActionResult> GetByIdAsync(long testId)
        => Ok(new Response
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await testService.GetByIdAsync(testId),
        });

    [HttpPut("{testId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync(long testId, [FromForm] TestUpdateDto dto)
        => Ok(new Response
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await testService.UpdateAsync(testId, dto),
        });

    [HttpDelete("{testId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(long testId)
        => Ok(new Response
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await testService.DeleteAsync(testId),
        });
}

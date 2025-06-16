using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestGo.Api.Models;
using QuestGo.Domain.Configuratios;
using QuestGo.Services.Dtos;
using QuestGo.Services.Interfaces;

namespace QuestGo.Api.Controllers;

public class QuestionController(IQuestionService questionService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response()
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await questionService.GetAllAsync(@params)
        });

    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> CreateAsync(QuestionCreateDto dto)
        => Ok(new Response()
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await questionService.CreateAsync(dto)
        });

    [HttpGet("{questionId:long}")]
    public async Task<IActionResult> GetByIdAsync(long questionId)
        => Ok(new Response()
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await questionService.GetByIdAsync(questionId)
        });

    [HttpPut("{questionId:long}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync(long questionId, [FromForm] QuestionUpdateDto dto)
        => Ok(new Response()
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await questionService.UpdateAsync(questionId, dto)
        });


    [HttpDelete("{questionId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(long questionId)
        => Ok(new Response
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await questionService.DeleteAsync(questionId),
        });
}
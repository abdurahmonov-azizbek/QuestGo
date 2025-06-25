using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestGo.Api.Models;
using QuestGo.Domain.Configuratios;
using QuestGo.Domain.Entities;
using QuestGo.Services.Dtos;
using QuestGo.Services.Interfaces;

namespace QuestGo.Api.Controllers;

public class QuestionOptionController(IQuestionOptionService questionOptionService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        => Ok(new Response()
        {
            Code = 200,
            Message = "OK",
            Data = await questionOptionService.GetAllAsync(@params)
        });

    [HttpGet("{questionOptionId:long}")]
    public async Task<IActionResult> GetAsync([FromRoute] long questionOptionId)
        => Ok(new Response()
        {
            Code = 200,
            Message = "OK",
            Data = await questionOptionService.GetByIdAsync(questionOptionId)
        });

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] QuestionOptionCreateDto dto)
        => Ok(new Response()
        {
            Code = 200,
            Message = "OK",
            Data = await questionOptionService.CreateAsync(dto)
        });

    [HttpPut("{questionOptionId:long}")]
    public async Task<IActionResult> UpdateAsync(long questionOptionId, [FromBody] QuestionOptionUpdateDto dto)
        => Ok(new Response()
        {
            Code = 200,
            Message = "OK",
            Data = await questionOptionService.UpdateAsync(questionOptionId, dto)
        });

    [HttpDelete("{questionOptionId:long}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(long questionOptionId)
        => Ok(new Response()
        {
            Code = 200,
            Message = "OK",
            Data = await questionOptionService.DeleteAsync(questionOptionId)
        });
}
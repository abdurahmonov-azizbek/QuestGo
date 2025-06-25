using AutoMapper.Configuration.Conventions;
using Microsoft.AspNetCore.Mvc;
using QuestGo.Api.Models;
using QuestGo.Domain.Configuratios;
using QuestGo.Domain.Entities;
using QuestGo.Services.Dtos;
using QuestGo.Services.Interfaces;

namespace QuestGo.Api.Controllers;

public class UserAnswerController(IUserAnswerService userAnswerService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams @params)
        => Ok(new Response()
        {
            Code = 200,
            Message = "Ok",
            Data = await userAnswerService.GetAllAsync(@params),
        });

    [HttpGet("{userAnswerId}")]
    public async Task<IActionResult> GetById([FromRoute] int userAnswerId)
        => Ok(new Response()
        {
            Code = 200,
            Message = "Ok",
            Data = await userAnswerService.GetByIdAsync(userAnswerId),
        });

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserAnswerCreateDto dto)
        => Ok(new Response()
        {
            Code = 200,
            Message = "Ok",
            Data = await userAnswerService.CreateAsync(dto),
        });

    [HttpPut("{userAnswerId}")]
    public async Task<IActionResult> Update(long userAnswerId, [FromBody] UserAnswerUpdateDto dto)
        => Ok(new Response()
        {
            Code = 200,
            Message = "Ok",
            Data = await userAnswerService.UpdateAsync(userAnswerId, dto),
        });

    [HttpDelete("{userAnswerId}")]
    public async Task<IActionResult> Delete(long userAnswerId)
        => Ok(new Response()
        {
            Code = 200,
            Message = "Ok",
            Data = await userAnswerService.DeleteAsync(userAnswerId),
        });
}
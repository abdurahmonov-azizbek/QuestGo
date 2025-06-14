using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestGo.Api.Models;
using QuestGo.Domain.Configuratios;
using QuestGo.Services.Dtos;
using QuestGo.Services.Interfaces;
using QuestGo.Services.Services;

namespace QuestGo.Api.Controllers;

public class CategoryController(ICategoryService categoryService) : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
       => Ok(new Response
       {
           Code = 200,
           Message = "Ok👍🏿",
           Data = await categoryService.GetAllAsync(@params),
       });

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateAsync(CategoryCreateDto dto)
        => Ok(new Response
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await categoryService.CreateAsync(dto),
        });

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetByIdAsync(long categoryId)
        => Ok(new Response
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await categoryService.GetByIdAsync(categoryId),
        });

    [HttpPut("{categoryId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync(long categoryId, [FromForm] CategoryUpdateDto dto)
        => Ok(new Response
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await categoryService.UpdateAsync(categoryId, dto),
        });

    [HttpDelete("{categoryId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync(long categoryId)
        => Ok(new Response
        {
            Code = 200,
            Message = "Ok👍🏿",
            Data = await categoryService.DeleteAsync(categoryId),
        });
}

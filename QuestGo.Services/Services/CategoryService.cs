using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuestGo.Data.Interfaces;
using QuestGo.Domain.Configuratios;
using QuestGo.Domain.Entities;
using QuestGo.Domain.Exceptions;
using QuestGo.Services.Dtos;
using QuestGo.Services.Dtos.Commons;
using QuestGo.Services.Extensions;
using QuestGo.Services.Interfaces;

namespace QuestGo.Services.Services;

public class CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
{
    public async ValueTask<CategoryResultDto> CreateAsync(CategoryCreateDto dto)
    {
        var existingCategory = await unitOfWork.Categories.SelectAsync(c => c.Name == dto.Name);
        if (existingCategory is not null && !existingCategory.IsDeleted)
            throw new QuestGoException(409, "Category already exists!");

        var mappedCategory = mapper.Map<Category>(dto);
        var category = await unitOfWork.Categories.InsertAsync(mappedCategory);
        await unitOfWork.SaveChangesAsync();

        return mapper.Map<CategoryResultDto>(category);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var category = await unitOfWork.Categories.SelectAsync(c => c.Id == id);

        if (category is null || category.IsDeleted)
            throw new QuestGoException(404, "Category not found!");

        category.IsDeleted = true;
        await unitOfWork.SaveChangesAsync();

        return true;
    }

    public async ValueTask<PagedResultDto<CategoryResultDto>> GetAllAsync(PaginationParams @params)
    {
        var categories = unitOfWork.Categories.SelectAll()
            .Where(c => !c.IsDeleted)
            .ToPagedList(@params);

        var count = await categories.CountAsync();
        var items = await categories.ToListAsync();
        var mappedItems = mapper.Map<List<CategoryResultDto>>(items);

        return new PagedResultDto<CategoryResultDto>()
        {
            TotalCount = count,
            Items = mappedItems,
        };
    }

    public async ValueTask<CategoryResultDto> GetByIdAsync(long id)
    {
        var category = await unitOfWork.Categories.SelectAsync(c => c.Id == id && !c.IsDeleted)
            ?? throw new QuestGoException(404, "Category not found!");

        return mapper.Map<CategoryResultDto>(category);
    }

    public async ValueTask<bool> UpdateAsync(long id, CategoryUpdateDto dto)
    {
        var category = await unitOfWork.Categories.SelectAsync(c => c.Id == id)
            ?? throw new QuestGoException(404, "Category not found!");

        mapper.Map(dto, category);
        category.UpdatedAt = DateTime.UtcNow.AddHours(5);

        return await unitOfWork.Categories.SaveChangesAsync();
    }
}
    
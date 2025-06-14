using QuestGo.Domain.Configuratios;
using QuestGo.Services.Dtos;
using QuestGo.Services.Dtos.Commons;

namespace QuestGo.Services.Interfaces;

public interface ICategoryService
{
    ValueTask<CategoryResultDto> CreateAsync(CategoryCreateDto dto);
    ValueTask<PagedResultDto<CategoryResultDto>> GetAllAsync(PaginationParams @params);
    ValueTask<CategoryResultDto> GetByIdAsync(long id);
    ValueTask<bool> UpdateAsync(long categoryId, CategoryUpdateDto dto);
    ValueTask<bool> DeleteAsync(long id);
}

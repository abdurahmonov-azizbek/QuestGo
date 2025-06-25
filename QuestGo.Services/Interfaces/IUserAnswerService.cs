using QuestGo.Domain.Configuratios;
using QuestGo.Services.Dtos;
using QuestGo.Services.Dtos.Commons;

namespace QuestGo.Services.Interfaces;

public interface IUserAnswerService
{
    ValueTask<UserAnswerResultDto> CreateAsync(UserAnswerCreateDto dto);
    ValueTask<PagedResultDto<UserAnswerResultDto>> GetAllAsync(PaginationParams @params);
    ValueTask<UserAnswerResultDto> GetByIdAsync(long id);
    ValueTask<bool> UpdateAsync(long id, UserAnswerUpdateDto dto);
    ValueTask<bool> DeleteAsync(long id);
}
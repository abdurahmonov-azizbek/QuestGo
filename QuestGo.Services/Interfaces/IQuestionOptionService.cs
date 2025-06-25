using QuestGo.Domain.Configuratios;
using QuestGo.Services.Dtos;
using QuestGo.Services.Dtos.Commons;

namespace QuestGo.Services.Interfaces;

public interface IQuestionOptionService
{
    ValueTask<QuestionOptionResultDto> CreateAsync(QuestionOptionCreateDto dto);
    ValueTask<PagedResultDto<QuestionOptionResultDto>> GetAllAsync(PaginationParams @params);
    ValueTask<QuestionOptionResultDto> GetByIdAsync(long id);
    ValueTask<bool> UpdateAsync(long id, QuestionOptionUpdateDto dto);
    ValueTask<bool> DeleteAsync(long id);
}
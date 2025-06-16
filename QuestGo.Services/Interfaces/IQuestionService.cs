using QuestGo.Domain.Configuratios;
using QuestGo.Domain.Entities;
using QuestGo.Services.Dtos;
using QuestGo.Services.Dtos.Commons;

namespace QuestGo.Services.Interfaces;

public interface IQuestionService
{
    ValueTask<QuestionResultDto> CreateAsync(QuestionCreateDto dto);
    ValueTask<PagedResultDto<QuestionResultDto>> GetAllAsync(PaginationParams @params);
    ValueTask<QuestionResultDto> GetByIdAsync(long questionId);
    ValueTask<IList<QuestionResultDto>> GetByTestId(long testId);
    ValueTask<bool> UpdateAsync(long questionId, QuestionUpdateDto dto);
    ValueTask<bool> DeleteAsync(long questionId);
}
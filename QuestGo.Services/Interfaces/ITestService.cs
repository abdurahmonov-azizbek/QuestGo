using QuestGo.Domain.Configuratios;
using QuestGo.Services.Dtos;
using QuestGo.Services.Dtos.Commons;

namespace QuestGo.Services.Interfaces;

public interface ITestService
{
    ValueTask<TestResultDto> CreateAsync(TestCreateDto dto);
    ValueTask<PagedResultDto<TestResultDto>> GetAllAsync(PaginationParams @params);
    ValueTask<PagedResultDto<TestResultDto>> GetAllByUserIdAsync(long userId, PaginationParams @params);
    ValueTask<TestResultDto> GetByIdAsync(long id);
    ValueTask<bool> UpdateAsync(long testId, TestUpdateDto dto);
    ValueTask<bool> DeleteAsync(long id);
}
    
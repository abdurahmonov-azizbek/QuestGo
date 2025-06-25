using QuestGo.Domain.Configuratios;
using QuestGo.Services.Dtos;
using QuestGo.Services.Dtos.Commons;

namespace QuestGo.Services.Interfaces;

public interface IUserTestSessionService
{
    ValueTask<UserTestSessionResultDto> CreateAsync(UserTestSessionCreateDto dto);
    ValueTask<PagedResultDto<UserTestSessionResultDto>> GetAllAsync(PaginationParams @params);
    ValueTask<UserTestSessionResultDto> GetByIdAsync(long id);
    ValueTask<bool> DeleteAsync(long id);
}
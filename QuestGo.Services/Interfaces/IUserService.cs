using QuestGo.Domain.Configuratios;
using QuestGo.Services.Dtos;
using QuestGo.Services.Dtos.Commons;

namespace QuestGo.Services.Interfaces;

public interface IUserService
{
    ValueTask<UserResultDto> CreateAsync(UserCreateDto dto);
    ValueTask<PagedResultDto<UserResultDto>> GetAllAsync(PaginationParams @params);
    ValueTask<UserResultDto> GetByEmailAsync(string email); 
    ValueTask<UserResultDto> GetByIdAsync(long id);
    ValueTask<bool> UpdateAsync(long id, UserUpdateDto dto);
    ValueTask<bool> DeleteAsync(long id);
}

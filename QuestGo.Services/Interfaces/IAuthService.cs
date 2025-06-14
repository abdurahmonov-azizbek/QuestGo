using QuestGo.Services.Dtos;

namespace QuestGo.Services.Interfaces;

public interface IAuthService
{
    ValueTask<LoginResultDto> AuthenticateAsync(LoginDto dto);
    ValueTask<UserResultDto> GetCurrentUserAsync();
}

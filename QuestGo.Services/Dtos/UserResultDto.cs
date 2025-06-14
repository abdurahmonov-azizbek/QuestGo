using QuestGo.Domain.Commons;
using QuestGo.Domain.Enums;

namespace QuestGo.Services.Dtos;

public class UserResultDto : Auditable
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Role Role { get; set; } = Role.User;
}

using QuestGo.Domain.Enums;

namespace QuestGo.Services.Dtos;

public class UserCreateDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Role Role { get; set; } = Role.User;
    public string Password { get; set; } = default!;
}

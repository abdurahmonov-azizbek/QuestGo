using QuestGo.Domain.Enums;

namespace QuestGo.Services.Dtos;

public class UserUpdateDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Role Role { get; set; } = Role.User;
    public string PasswordHash { get; set; } = default!;
}

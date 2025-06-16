using QuestGo.Domain.Commons;
using QuestGo.Domain.Enums;

namespace QuestGo.Domain.Entities;

public class User : Auditable
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Role Role { get; set; } = Role.User;
    public string PasswordHash { get; set; } = default!;
    public virtual List<Test> Tests { get; set; } = new();
    public virtual List<Question> Questions { get; set; } = new();
}
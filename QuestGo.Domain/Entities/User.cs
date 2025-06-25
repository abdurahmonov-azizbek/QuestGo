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
    public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
    public virtual ICollection<Question> Questions { get; set; } = new  List<Question>();
    public virtual ICollection<UserAnswer> UserAnswers { get; set; } = new  List<UserAnswer>();
    public virtual ICollection<UserTestSession> UserTestSessions { get; set; } = new  List<UserTestSession>();
}
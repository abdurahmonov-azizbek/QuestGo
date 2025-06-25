using QuestGo.Domain.Commons;

namespace QuestGo.Domain.Entities;

public class Test : Auditable
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public long CategoryId { get; set; }
    public long UserId { get; set; }
    public virtual Category Category { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();
    public virtual ICollection<UserTestSession> UserTestSessions { get; set; } = new List<UserTestSession>();
}

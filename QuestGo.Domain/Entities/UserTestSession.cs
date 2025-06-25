using QuestGo.Domain.Commons;

namespace QuestGo.Domain.Entities;

public class UserTestSession : Auditable
{
    public long UserId { get; set; }
    public long TestId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    
    public virtual User User { get; set; }
    public virtual Test Test { get; set; }
    public ICollection<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
}
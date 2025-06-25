using QuestGo.Domain.Commons;

namespace QuestGo.Domain.Entities;

public class UserAnswer : Auditable
{
    public long UserTestSessionId { get; set; }
    public long QuestionId { get; set; }
    public long QuestionOptionId { get; set; }
    public bool WasCorrect { get; set; }
    
    public virtual UserTestSession UserTestSession { get; set; }
    public virtual Question Question { get; set; }
    public virtual QuestionOption QuestionOption { get; set; }
}
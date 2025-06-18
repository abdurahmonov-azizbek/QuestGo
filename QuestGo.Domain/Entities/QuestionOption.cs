using QuestGo.Domain.Commons;

namespace QuestGo.Domain.Entities;

public class QuestionOption : Auditable
{
    public string Content { get; set; }
    public long QuestionId { get; set; }
    public bool IsCorrect { get; set; }
    public virtual Question Question { get; set; }
}
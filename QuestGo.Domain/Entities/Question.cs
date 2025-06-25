using QuestGo.Domain.Commons;

namespace QuestGo.Domain.Entities;

public class Question : Auditable
{
    public string Content { get; set; } = default!;
    public long TestId { get; set; }
    public virtual Test? Test { get; set; }
    public long UserId { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<QuestionOption> Options { get; set; } = new List<QuestionOption>();
    public virtual ICollection<UserAnswer>  UserAnswers { get; set; } = new List<UserAnswer>();
}
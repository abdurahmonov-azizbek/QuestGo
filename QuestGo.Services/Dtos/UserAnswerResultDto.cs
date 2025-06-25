using QuestGo.Domain.Commons;

namespace QuestGo.Services.Dtos;

public class UserAnswerResultDto : Auditable
{
    public long UserTestSessionId { get; set; }
    public long QuestionId { get; set; }
    public long QuestionOptionId { get; set; }
    public bool WasCorrect { get; set; }
}
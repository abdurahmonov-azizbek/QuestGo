using QuestGo.Domain.Commons;

namespace QuestGo.Services.Dtos;

public class QuestionOptionResultDto : Auditable
{
    public string Content { get; set; }
    public long QuestionId { get; set; }
    public bool IsCorrect { get; set; }
}
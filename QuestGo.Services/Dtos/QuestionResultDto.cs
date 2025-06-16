using QuestGo.Domain.Commons;

namespace QuestGo.Services.Dtos;

public class QuestionResultDto : Auditable
{
    public string Content { get; set; } = default!;
    public long TestId { get; set; }
    public long UserId { get; set; }
}
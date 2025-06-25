using QuestGo.Domain.Commons;

namespace QuestGo.Services.Dtos;

public class UserTestSessionResultDto : Auditable
{
    public long UserId { get; set; }
    public long TestId { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
}
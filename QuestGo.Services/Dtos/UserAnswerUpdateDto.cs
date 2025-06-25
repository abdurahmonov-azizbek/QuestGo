namespace QuestGo.Services.Dtos;

public class UserAnswerUpdateDto
{
    public long UserTestSessionId { get; set; }
    public long QuestionId { get; set; }
    public long QuestionOptionId { get; set; }
    public bool WasCorrect { get; set; }
}
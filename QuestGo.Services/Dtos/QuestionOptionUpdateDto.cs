namespace QuestGo.Services.Dtos;

public class QuestionOptionUpdateDto
{
    public string Content { get; set; }
    public long QuestionId { get; set; }
    public bool IsCorrect { get; set; }
}
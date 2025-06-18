namespace QuestGo.Services.Dtos;

public class QuestionOptionCreateDto
{
    public string Content { get; set; }
    public long QuestionId { get; set; }
    public bool IsCorrect { get; set; }
}
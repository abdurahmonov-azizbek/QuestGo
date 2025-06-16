namespace QuestGo.Services.Dtos;

public class QuestionCreateDto
{
    public string Content { get; set; } = default!;
    public long TestId { get; set; }
}
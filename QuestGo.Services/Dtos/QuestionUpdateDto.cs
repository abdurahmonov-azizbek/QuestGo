namespace QuestGo.Services.Dtos;

public class QuestionUpdateDto
{
    public string Content { get; set; } = default!;
    public long TestId { get; set; }
}
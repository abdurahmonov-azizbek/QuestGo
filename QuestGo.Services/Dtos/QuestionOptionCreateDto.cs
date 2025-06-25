namespace QuestGo.Services.Dtos;

public class QuestionOptionCreateDto
{
    public string Option { get; set; }
    public long QuestionId { get; set; }
    public bool IsCorrect { get; set; }
}
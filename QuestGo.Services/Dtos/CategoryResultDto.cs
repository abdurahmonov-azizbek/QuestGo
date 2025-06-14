using QuestGo.Domain.Commons;

namespace QuestGo.Services.Dtos;

public class CategoryResultDto : Auditable
{
    public string Name { get; set; } = default!;
}

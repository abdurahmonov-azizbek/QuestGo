using QuestGo.Domain.Commons;

namespace QuestGo.Domain.Entities;

public class Test : Auditable
{
    public string Name { get; set; } = default!;
    public long UserId { get; set; }
}

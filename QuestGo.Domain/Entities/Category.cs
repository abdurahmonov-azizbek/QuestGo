using QuestGo.Domain.Commons;

namespace QuestGo.Domain.Entities
{
    public class Category : Auditable
    {
        public string Name { get; set; } = default!;
    }
}

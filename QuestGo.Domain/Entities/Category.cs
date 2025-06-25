using QuestGo.Domain.Commons;

namespace QuestGo.Domain.Entities
{
    public class Category : Auditable
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public virtual ICollection<Test> Tests { get; set; } = new List<Test>();
    }
}

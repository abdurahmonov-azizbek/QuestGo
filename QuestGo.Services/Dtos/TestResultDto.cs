using QuestGo.Domain.Commons;

namespace QuestGo.Services.Dtos
{
    public class TestResultDto : Auditable
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public long UserId { get; set; }
        public long CategoryId { get; set; }
    }
}

using QuestGo.Domain.Commons;

namespace QuestGo.Services.Dtos
{
    public class TestResultDto : Auditable
    {
        public string Name { get; set; } = default!;
        public long UserId { get; set; }
    }
}

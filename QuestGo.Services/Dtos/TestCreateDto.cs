namespace QuestGo.Services.Dtos
{
    public class TestCreateDto
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public long CategoryId { get; set; }
    }
}

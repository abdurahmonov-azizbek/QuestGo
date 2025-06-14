namespace QuestGo.Services.Dtos.Commons;

public class PagedResultDto<T>
{
    public int TotalCount { get; set; }
    public List<T> Items { get; set; } = new List<T>();
}

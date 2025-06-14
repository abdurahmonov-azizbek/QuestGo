namespace QuestGo.Domain.Configuratios;

public class PaginationParams
{
    private const int MaxPageSize = 100;
    private int _pageSize;
    public int PageSize
    {
        get => _pageSize == 0 ? 10 : _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value < 1 ? 10 : value;
    }
    public int PageIndex { get; set; } = 1;
}

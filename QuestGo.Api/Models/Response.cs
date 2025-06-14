namespace QuestGo.Api.Models;

public class Response
{
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public object Data { get; set; } = string.Empty;
}

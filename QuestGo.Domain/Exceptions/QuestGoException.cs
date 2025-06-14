namespace QuestGo.Domain.Exceptions;

public class QuestGoException(
    int code = 500,
    string message = "Something went wrong") : Exception(message)
{
    public int Code { get; } = code;
}

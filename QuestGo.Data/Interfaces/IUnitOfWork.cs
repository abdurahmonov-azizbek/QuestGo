using QuestGo.Domain.Entities;

namespace QuestGo.Data.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> Users { get; }
    IRepository<Category> Categories { get; }
    IRepository<Test> Tests { get; }
    IRepository<Question> Questions { get; }
    IRepository<QuestionOption> QuestionOptions { get; }
    ValueTask<bool> SaveChangesAsync();
}

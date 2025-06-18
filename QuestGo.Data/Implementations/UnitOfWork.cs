using QuestGo.Data.Contexts;
using QuestGo.Data.Interfaces;
using QuestGo.Domain.Entities;

namespace QuestGo.Data.Implementations;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public IRepository<User> Users => new Repository<User>(context);
    public IRepository<Category> Categories => new Repository<Category>(context);
    public IRepository<Test> Tests => new Repository<Test>(context);
    public IRepository<Question> Questions => new Repository<Question>(context);
    public IRepository<QuestionOption> QuestionOptions => new Repository<QuestionOption>(context);


    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async ValueTask<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() >= 0;
    }
}
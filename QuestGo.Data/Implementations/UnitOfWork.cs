using QuestGo.Data.Contexts;
using QuestGo.Data.Interfaces;
using QuestGo.Domain.Entities;

namespace QuestGo.Data.Implementations;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public IRepository<User> Users { get; } = new Repository<User>(context);
    public IRepository<Category> Categories { get; } = new Repository<Category>(context);
    public IRepository<Test> Tests { get; } = new Repository<Test>(context);


    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public async ValueTask<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() >= 0;
    }
}

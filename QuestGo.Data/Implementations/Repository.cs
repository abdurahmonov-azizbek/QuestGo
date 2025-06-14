using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuestGo.Data.Contexts;
using QuestGo.Data.Interfaces;
using QuestGo.Domain.Commons;
using System.Linq.Expressions;

namespace QuestGo.Data.Implementations;

public class Repository<TEntity>(AppDbContext context) : IRepository<TEntity>
    where TEntity : Auditable
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async ValueTask<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await this.SelectAsync(expression);
        if (entity is not null)
        {
            entity.IsDeleted = true;
            return true;
        }

        return false;
    }

    public bool DeleteMany(Expression<Func<TEntity, bool>> expression)
    {
        var entities = _dbSet.Where(expression);

        if (entities.Any())
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
            }
            return true;
        }
        return false;
    }

    public async ValueTask<TEntity> InsertAsync(TEntity entity)
    {
        EntityEntry<TEntity> entry = await this._dbSet.AddAsync(entity);

        return entry.Entity;
    }

    public async ValueTask<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>>? expression = null, string[]? includes = null)
    {
        var query = expression is not null ? _dbSet.Where(expression) : _dbSet.AsQueryable();

        if (includes is not null)
        {
            foreach (var include in includes)
            {
                query.Include(include);
            }
        }

        return query;
    }

    public async ValueTask<TEntity?> SelectAsync(Expression<Func<TEntity, bool>> expression, string[]? includes = null)
    {
        return await this.SelectAll(expression, includes).FirstOrDefaultAsync(x => !x.IsDeleted);
    }

    public TEntity Update(TEntity entity)
    {
        var entry = _dbSet.Update(entity);

        return entry.Entity;
    }
}

using Microsoft.EntityFrameworkCore;
using QuestGo.Domain.Entities;

namespace QuestGo.Data.Contexts;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Test> Tests { get; set; }
}

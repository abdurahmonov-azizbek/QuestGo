using Microsoft.EntityFrameworkCore;
using QuestGo.Domain.Entities;

namespace QuestGo.Data.Contexts;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionOption> QuestionOptions { get; set; }
    public DbSet<UserAnswer>  UserAnswers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Test>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tests)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Question>()
            .HasOne(q => q.Test)
            .WithMany(t => t.Questions)
            .HasForeignKey(q => q.TestId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Question>()
            .HasOne(q => q.User)
            .WithMany(u => u.Questions)
            .HasForeignKey(q => q.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<QuestionOption>()
            .HasOne(o => o.Question)
            .WithMany(q => q.Options)
            .HasForeignKey(o => o.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Test>()
            .HasOne(t => t.Category)
            .WithMany(c => c.Tests)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserTestSession>()
            .HasOne(s => s.User)
            .WithMany(u => u.UserTestSessions)
            .HasForeignKey(s => s.UserId);

        modelBuilder.Entity<UserTestSession>()
            .HasOne(s => s.Test)
            .WithMany(t => t.UserTestSessions)
            .HasForeignKey(s => s.TestId);
        
        modelBuilder.Entity<UserAnswer>()
            .HasOne(u => u.UserTestSession)
            .WithMany(s => s.UserAnswers)
            .HasForeignKey(u => u.UserTestSessionId);
        
        modelBuilder.Entity<UserAnswer>()
            .HasOne(u => u.Question)
            .WithMany(q => q.UserAnswers)
            .HasForeignKey(u => u.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<UserAnswer>()
            .HasOne(u => u.QuestionOption)
            .WithMany(o => o.UserAnswers)
            .HasForeignKey(u => u.QuestionOptionId);
        
        base.OnModelCreating(modelBuilder);
    }
}

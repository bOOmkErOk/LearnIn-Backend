using LearnIn.Application.Interfaces.Persistence;
using LearnIn.Domain.Entities.Auth;
using LearnIn.Domain.Entities.Courses;
using LearnIn.Domain.Entities.Subscriptions;
using LearnIn.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace LearnIn.Infrastructure.Data;

public class DataContext : DbContext, IDataContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Course> Courses { get; set; }
    
    public DbSet<Subscription> Subscriptions { get; set; }
    
    public DbSet<Lesson> Lessons { get; set; }
    
    public DbSet<EmailVerification>  EmailVerifications { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}
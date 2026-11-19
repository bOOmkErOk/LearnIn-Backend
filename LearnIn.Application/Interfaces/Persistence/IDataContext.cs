using LearnIn.Domain.Entities.Auth;
using LearnIn.Domain.Entities.Courses;
using LearnIn.Domain.Entities.Subscriptions;
using LearnIn.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace LearnIn.Application.Interfaces.Persistence;

public interface IDataContext
{
    DbSet<User> Users { get; set; }
    
    DbSet<Course> Courses { get; set; }
    
    DbSet<Subscription> Subscriptions { get; set; }
    
    DbSet<Lesson> Lessons { get; set; }
    
    DbSet<EmailVerification>  EmailVerifications { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken ct);
}
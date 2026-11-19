using LearnIn.Domain.Entities.Auth;
using LearnIn.Domain.Entities.Courses;
using LearnIn.Domain.Entities.Subscriptions;
using LearnIn.Domain.Enums.Users;

namespace LearnIn.Domain.Entities.Users;

public class User : Entity<int>
{
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public string Email { get; set; } = string.Empty;
    
    public decimal Balance { get; set; }
    
    public string Password { get; set; } = string.Empty;
    
    public UserEnums Role { get; set; }
    
    public bool IsEmailVerified { get; set; } = false;
    
    public EmailVerification? EmailVerification { get; set; }

    public ICollection<Course> CreatedCourses { get; set; } = new List<Course>();
    
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}
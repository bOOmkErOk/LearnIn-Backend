using LearnIn.Domain.Entities.Users;

namespace LearnIn.Domain.Entities.Subscriptions;

public class Subscription : Entity<int>
{
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal PricePaid { get; set; }

    public int TeacherId { get; set; }
    
    public bool IsCurrentlyActive => DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;
}
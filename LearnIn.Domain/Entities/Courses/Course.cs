namespace LearnIn.Domain.Entities.Courses;

public class Course : Entity<int>
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    
    public int TeacherId { get; set; }
    
    public string PromoVideoUrl { get; set; } = string.Empty;
    
    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

}
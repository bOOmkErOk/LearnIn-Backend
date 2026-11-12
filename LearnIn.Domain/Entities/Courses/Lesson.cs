namespace LearnIn.Domain.Entities.Courses;

public class Lesson : Entity<int>
{
    public string Title { get; set; } = null!;
    
    public string Description { get; set; } = string.Empty;
    
    public string VideoUrl { get; set; } = null!;
    
    public int SequenceNumber { get; set; }
    
    public int DurationSeconds { get; set; }
    
    public int CourseId { get; set; }
    
    public Course Course { get; set; } = null!;
    
}
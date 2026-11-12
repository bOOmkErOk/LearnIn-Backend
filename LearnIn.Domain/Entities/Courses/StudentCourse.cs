using LearnIn.Domain.Entities.Users;

namespace LearnIn.Domain.Entities.Courses;

public class StudentCourse
{
    public int StudentId { get; set; }
    public User Student { get; set; } = null!;

    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;

    public decimal TeacherEarningAtActivation { get; set; }
}
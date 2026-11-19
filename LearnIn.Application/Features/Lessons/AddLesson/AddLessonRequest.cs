namespace LearnIn.Application.Features.Lessons.AddLesson;

public record AddLessonRequest(
    int CourseId, 
    string Title,
    string Description,
    string VideoUrl, 
    int DurationSeconds, 
    int SequenceNumber);
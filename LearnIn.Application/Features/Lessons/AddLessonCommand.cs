using MediatR;
using LearnIn.Domain.Results;

namespace LearnIn.Application.Features.Lessons;

public record AddLessonCommand(
    int CourseId, 
    string Title, 
    string Description,
    string VideoUrl, 
    int DurationSeconds, 
    int SequenceNumber,
    int TeacherId) : IRequest<Result<int>>;
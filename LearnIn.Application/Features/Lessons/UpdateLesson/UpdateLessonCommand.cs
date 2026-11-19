using LearnIn.Domain.Results;
using MediatR;

namespace LearnIn.Application.Features.Lessons.UpdateLesson;

public record UpdateLessonCommand(
    int LessonId,
    string Title,
    string Description,
    string VideoUrl,
    int TeacherId
    ) : IRequest<Result<int>>;